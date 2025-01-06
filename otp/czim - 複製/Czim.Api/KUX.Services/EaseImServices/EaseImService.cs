using KUX.Infrastructure;
using KUX.Infrastructure.Http;
using KUX.Infrastructure.Redis;
using KUX.Infrastructure.ScanDIService.Interface;
using KUX.Models.ApiResultManage;
using KUX.Models.Consts;
using KUX.Models.CzimSection;
using KUX.Repositories.CzimSection;
using KUX.Services.EaseImServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KUX.Services.EaseImServices;

/// <summary>
/// 环信聊天服务
/// </summary>
public class EaseImService : IDITransientSelf
{
    /// <summary>
    /// http 接口工厂
    /// </summary>
    private readonly IHttpClientFactory httpClientFactory;

    private readonly string org_name = "1107220118095349";
    private readonly string app_name = "xchat";

    private string grant_type = "client_credentials";

    private readonly string client_id = "YXA6clpiHVluTLS5WcphPQGeaQ";
    private readonly string client_secret = "YXA6TUqaY1wr4ZJLghnYEJPXNAKkjDA";

    /// <summary>
    /// 请求地址
    /// </summary>
    private readonly string httpUrl = "http://a61.easemob.com";

    /// <summary>
    /// redis key
    /// </summary>
    public static string redisEaseTokenKey
    {
        get { return "YXA6NT0epYNzQMW3HdqoZ6OOgg_xchat"; }
    }

    /// <summary>
    /// 环信配置文件仓储
    /// </summary>
    private CzimEaseImSettingRepository repository;

    /// <summary>
    /// 缓存服务
    /// </summary>
    private readonly IRedisService redisService;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="_httpClientFactory">http 工厂</param>
    /// <param name="_repository">环信配置文件仓储</param>
    /// <param name="_redisService">缓存</param>
    public EaseImService(IHttpClientFactory _httpClientFactory, CzimEaseImSettingRepository _repository, IRedisService _redisService)
    {
        httpClientFactory = _httpClientFactory;
        redisService = _redisService;
        repository = _repository;
    }

    /// <summary>
    /// 获取当前环信token
    /// </summary>
    /// <returns></returns>
    private async Task<string> GetTokenAsync()
    {
        // 从redis中获取token信息
        var hasToken = await redisService.ExistsByKeyAsync(EaseImService.redisEaseTokenKey);

        if (hasToken)
        {
            var setting = await redisService.FindByKeyAsync<CzimEaseImSetting>(EaseImService.redisEaseTokenKey);

            if (!string.IsNullOrWhiteSpace(setting.AccessToken) && setting.ExpiresTime > DateTime.Now)
            {
                return setting.AccessToken;
            }
        }
        // 获取当前对象
        var result = await this.repository.Orm.Select<CzimEaseImSetting>().FirstAsync();

        // 生成环信账号
        if (result == null || string.IsNullOrWhiteSpace(result.Id))
        {
            return default;
        }

        if (string.IsNullOrWhiteSpace(result.AccessToken) || result.ExpiresTime <= DateTime.Now)
        {
            // 重新生成token
            var _easeImToken = await this.EaseGetTokenAsync();

            if (_easeImToken != null)
            {
                result.AccessToken = _easeImToken.access_token;
                result.ApplicationId = _easeImToken.application;
                result.ExpiresTime = DateTime.Now.AddSeconds(_easeImToken.expires_in);

                // 更新token
                await this.repository.Orm.Update<CzimEaseImSetting>()
                                        .SetSource(result)
                                        .Where(w => w.Id == result.Id)
                                        .ExecuteAffrowsAsync();
            }
        }

        if (!string.IsNullOrWhiteSpace(result.AccessToken))
        {
            // 缓存redis
            await redisService.AddOrUpdateByKeyAsync<CzimEaseImSetting>(redisEaseTokenKey, result, result.ExpiresTime);
        }

        return result.AccessToken;
    }

    /// <summary>
    /// 环信token
    /// </summary>
    public string EaseToken
    {
        get
        {
            return GetTokenAsync().Result;
        }
    }

    #region 用户相关接口(创建环信账号，创建用户，修改用户推送昵称，修改用户属性，用户禁用，立即下线，删除用户，设置用户全局禁言)

    // 创建环信账号
    /// <summary>
    /// 创建token
    /// </summary>
    /// <returns></returns>
    private async Task<EaseImToken> EaseGetTokenAsync()
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/token";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        //headers.Add("Content-Type", "application/json");

        var content = new
        {
            grant_type = grant_type,
            client_id = client_id,
            client_secret = client_secret,
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象

            var tokenInfo = result.Deserialize<EaseImToken>();
            return tokenInfo;
        }
        return default;
    }

    public async Task<EaseBaseModel<EaseUsers, string>> CreateUsersTestAsync(string username, string nickname, string pwd)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/users";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = new
        {
            username = username,
            password = pwd,
            nickname = nickname,
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var usersInfo = result.Deserialize<EaseBaseModel<EaseUsers, string>>();

            return usersInfo;
        }
        return default;
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="username">用户账户</param>
    /// <param name="nickname">用户昵称</param>
    /// <returns></returns>
    public async Task<EaseBaseModel<EaseUsers, string>> CreateUsersAsync(string username, string nickname)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/users";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = new
        {
            username = username,
            password = $"{username}{CzimConsts.EaseSalt}".Md5Encrypt(),
            nickname = nickname,
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var usersInfo = result.Deserialize<EaseBaseModel<EaseUsers, string>>();

            return usersInfo;
        }
        return default;
    }

    /// <summary>
    /// 修改用户推送昵称
    /// </summary>
    /// <param name="username">用户id</param>
    /// <param name="nickname"></param>
    /// <returns></returns>
    public async Task<EaseBaseModel<EaseUsers, string>> UpdUserNameAsync(string username, string nickname)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/users/{username}";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = new
        {
            nickname = nickname,
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Put, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var usersInfo = result.Deserialize<EaseBaseModel<EaseUsers, string>>();

            return usersInfo;
        }
        return default;
    }

    /// <summary>
    /// 修改用户属性
    /// </summary>
    /// <param name="username">用户id</param>
    /// <param name="nickname">用户昵称</param>
    /// <param name="avatar">用户头像</param>
    /// <returns></returns>
    public async Task<EaseBaseModel<string, EaseUserMeta>> UpdUserMetaAsync(string username, string nickname, string avatar)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/metadata/user/{username}";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = new
        {
            nickname = nickname,
            avatar = avatar,
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Put, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var usersInfo = result.Deserialize<EaseBaseModel<string, EaseUserMeta>>();

            return usersInfo;
        }
        return default;
    }

    /// <summary>
    /// 用户禁用，立即下线
    /// </summary>
    /// <param name="username">用户id</param>
    /// <returns></returns>
    public async Task<EaseBaseModel<EaseUsers, string>> DeactivateUserAsync(string username)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/users/{username}/deactivate";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = "";

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Put, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var usersInfo = result.Deserialize<EaseBaseModel<EaseUsers, string>>();

            return usersInfo;
        }
        return default;
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<EaseBaseModel<EaseUsers, string>> DeleteUsersAsync(string username)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/users/{username}";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = "";

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Delete, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var usersInfo = result.Deserialize<EaseBaseModel<EaseUsers, string>>();

            return usersInfo;
        }
        return default;
    }

    //聊天室设置用户全局禁言
    /// <summary>
    /// 聊天室设置用户全局禁言
    /// </summary>
    /// <param name="username">用户id(web端userid)</param>
    /// <param name="chatRoomMuteTime">聊天室消息禁言时间</param>
    /// <returns>true为成功，false为失败</returns>
    public async Task<bool> ChatCustomerAllBan(string username, long chatRoomMuteTime)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/mutes";
        //定义字典类型 存储请求头信息
        Dictionary<string, string> headers = new Dictionary<string, string>();
        //存入token
        headers.Add("Authorization", $"Bearer {EaseToken}");
        var content = new
        {
            username = username,
            groupchat = chatRoomMuteTime,
            chatroom = chatRoomMuteTime
        }.ToJson();

        var result = await httpClientFactory.HttpSendStatusAsync(HttpMethod.Post, _httpurl, content, headers);

        int _status = result.Item1;

        // if (_status == 200)
        // {
        //     if (!string.IsNullOrWhiteSpace(_jsonData))
        //     {
        //         // 序列化token对象
        //         var addInfo = _jsonData.Deserialize<EaseCreateChatRoomModel<string, ChatCustomer>>();

        //         if (addInfo?.data?.result == "ok")
        //         {
        //             return true;
        //         }
        //     }
        //     return true;
        // }

        switch (_status)
        {
            case 406:
                MessageBox.Show("xxxxxxxxxx");
                break;

            case 200:
                string _jsonData = result.Item2;
                if (!string.IsNullOrWhiteSpace(_jsonData))
                {
                    // 序列化token对象
                    var addInfo = _jsonData.Deserialize<EaseCreateChatRoomModel<string, ChatCustomer>>();

                    if (addInfo?.data?.result == "ok")
                    {
                        return true;
                    }
                    MessageBox.Show("禁言失败");
                }

                break;

            default:
                break;
        }
        return false;
    }

    //聊天室设置用户全局禁言
    /// <summary>
    /// 聊天室设置用户全局禁言
    /// </summary>
    /// <param name="username">用户id(web端userid)</param>
    /// <param name="chatRoomMuteTime">聊天室消息禁言时间</param>
    /// <returns>true为成功，false为失败</returns>
    public async Task<bool> ChatGroupAllBan(string username, long chatRoomMuteTime)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/mutes";
        //定义字典类型 存储请求头信息
        Dictionary<string, string> headers = new Dictionary<string, string>();
        //存入token
        headers.Add("Authorization", $"Bearer {EaseToken}");
        var content = new
        {
            username = username,
            chatroom = chatRoomMuteTime
        }.ToJson();

        var result = await httpClientFactory.HttpSendStatusAsync(HttpMethod.Post, _httpurl, content, headers);

        int _status = result.Item1;

        // if (_status == 200)
        // {
        //     if (!string.IsNullOrWhiteSpace(_jsonData))
        //     {
        //         // 序列化token对象
        //         var addInfo = _jsonData.Deserialize<EaseCreateChatRoomModel<string, ChatCustomer>>();

        //         if (addInfo?.data?.result == "ok")
        //         {
        //             return true;
        //         }
        //     }
        //     return true;
        // }

        switch (_status)
        {
            case 406:
                MessageBox.Show("xxxxxxxxxx");
                break;

            case 200:
                string _jsonData = result.Item2;
                if (!string.IsNullOrWhiteSpace(_jsonData))
                {
                    // 序列化token对象
                    var addInfo = _jsonData.Deserialize<EaseCreateChatRoomModel<string, ChatCustomer>>();

                    if (addInfo?.data?.result == "ok")
                    {
                        return true;
                    }
                    MessageBox.Show("禁言失败");
                }

                break;

            default:
                break;
        }
        return false;
    }

    #endregion 用户相关接口(创建环信账号，创建用户，修改用户推送昵称，修改用户属性，用户禁用，立即下线，删除用户，设置用户全局禁言)

    #region 聊天室接口

    /// <summary>
    /// 创建聊天室
    /// </summary>
    /// <param name="name">聊天室名称</param>
    /// <param name="description">聊天室描述</param>
    /// <param name="owner">所有者</param>
    /// <param name="maxusers">最大人数</param>
    /// <param name="members">聊天室人数</param>
    /// <returns>返回聊天室id</returns>
    public async Task<string> CreateChatRoomAsync(string name, string description, string owner, List<string> members, int maxusers = 100000)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatrooms";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        if (members?.Count <= 0)
        {
            members.Add(owner);
        }

        var content = new
        {
            name = name,
            description = description,
            owner = owner,
            maxusers = maxusers,
            members = members
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var chatRoomInfo = result.Deserialize<EaseCreateChatRoomModel<string, EaseDataModel>>();

            if (chatRoomInfo != null && chatRoomInfo.data != null)
            {
                return chatRoomInfo.data.id;
            }
            return "";
        }
        return "";
    }

    /// <summary>
    /// 添加聊天室成员
    /// </summary>
    /// <param name="username">用户id(本系统的用户id/memberid)</param>
    /// <param name="chatroomid">聊天室id 聊天室唯一标识符，由环信服务器生成</param>
    /// <returns></returns>
    public async Task<ChatRoomUser> ChatRoomAddUser(string username, string chatroomid)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatrooms/{chatroomid}/users/{username}";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = "";

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var addInfo = result.Deserialize<EaseCreateChatRoomModel<string, ChatRoomUser>>();

            if (addInfo != null && addInfo.data != null)
            {
                return addInfo.data;
            }
        }
        return default;
    }

    /// <summary>
    /// 聊天室批量添加成员
    /// </summary>
    /// <param name="usernames">用户id(本系统的用户id/memberid)</param>
    /// <param name="chatroomid">聊天室id 聊天室唯一标识符，由环信服务器生成</param>
    /// <returns></returns>
    public async Task<int> ChatRoomBatchAddUsersAsync(List<string> usernames, string chatroomid)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatrooms/{chatroomid}/users";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = new
        {
            usernames = usernames
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var addInfo = result.Deserialize<EaseCreateChatRoomModel<string, ChatRoomBatchAddUser>>();

            if (addInfo != null && addInfo.data?.newmembers?.Count > 0)
            {
                return addInfo.data.newmembers.Count;
            }
        }
        return 0;
    }

    // /
    /// <summary>
    /// 删除聊天室成员
    /// </summary>
    /// <param name="username">用户im账户</param>
    /// <param name="chatroomid">聊天室id 聊天室唯一标识符，由环信服务器生成</param>
    /// <returns></returns>
    public async Task<ChatRoomUser> ChatRoomDeleteUser(string username, string chatroomid)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatrooms/{chatroomid}/users/{username}";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = "";

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Delete, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var addInfo = result.Deserialize<EaseBaseModel<string, ChatRoomUser>>();

            if (addInfo != null && addInfo.data?.Count > 0)
            {
                return addInfo.data[0];
            }
        }
        return default;
    }

    /// <summary>
    /// 添加聊天室管理员
    /// </summary>
    /// <param name="username">用户im账户</param>
    /// <param name="chatroomid">聊天室id 聊天室唯一标识符，由环信服务器生成</param>
    /// <returns></returns>
    public async Task<ChatRoomAdmin> ChatRoomAddAdmin(string username, string chatroomid)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatrooms/{chatroomid}/admin";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = new
        {
            newadmin = username
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var addInfo = result.Deserialize<EaseCreateChatRoomModel<string, ChatRoomAdmin>>();

            if (addInfo != null && addInfo.data != null)
            {
                return addInfo.data;
            }
        }
        return default;
    }

    /// <summary>
    /// 移除聊天室管理员
    /// </summary>
    /// <param name="username">用户im账户</param>
    /// <param name="chatroomid">聊天室id 聊天室唯一标识符，由环信服务器生成</param>
    /// <returns></returns>
    public async Task<ChatRoomAdmin> ChatRoomDeleteAdmin(string username, string chatroomid)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatrooms/{chatroomid}/admin/{username}";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = new
        {
            newadmin = username
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Delete, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var addInfo = result.Deserialize<EaseBaseModel<string, ChatRoomAdmin>>();

            if (addInfo != null && addInfo.data?.Count > 0)
            {
                return addInfo.data[0];
            }
        }
        return default;
    }

    /// <summary>
    /// 聊天室禁言用户
    /// </summary>
    /// <param name="usernames">用户im账户列表 </param>
    /// <param name="chatroomid">聊天室id 聊天室唯一标识符，由环信服务器生成</param>
    /// <param name="duration">禁言时间（毫秒）</param>
    /// <returns></returns>
    public async Task<List<ChatRoomMute>> ChatRoomMuteUser(List<string> usernames, string chatroomid, long duration)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatrooms/{chatroomid}/mute";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = new
        {
            usernames = usernames,
            mute_duration = duration
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var addInfo = result.Deserialize<EaseBaseModel<string, ChatRoomMute>>();

            if (addInfo != null && addInfo.data?.Count > 0)
            {
                return addInfo.data;
            }
        }
        return default;
    }

    /// <summary>
    /// 聊天室解除禁言用户
    /// </summary>
    /// <param name="usernames">用户im账户列表</param>
    /// <param name="chatroomid">聊天室id 聊天室唯一标识符，由环信服务器生成</param>
    /// <returns></returns>
    public async Task<List<ChatRoomMute>> ChatRoomNoMuteUser(List<string> usernames, string chatroomid)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatrooms/{chatroomid}/mute/{usernames[0]}";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = new
        {
            //usernames = usernames
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Delete, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var addInfo = result.Deserialize<EaseBaseModel<string, ChatRoomMute>>();

            if (addInfo != null && addInfo.data?.Count > 0)
            {
                return addInfo.data;
            }
        }
        return default;
    }

    /// <summary>
    /// 聊天室全局禁言
    /// </summary>
    /// <param name="chatroomid">聊天室id</param>
    /// <returns></returns>
    public async Task<bool> ChatRoomBan(string chatroomid)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatrooms/{chatroomid}/ban";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = "";

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var addInfo = result.Deserialize<EaseCreateChatRoomModel<string, ChatRoomBan>>();

            if (addInfo?.data?.mute == true)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 聊天室全局解禁
    /// </summary>
    /// <param name="chatroomid">聊天室id</param>
    /// <returns></returns>
    public async Task<bool> ChatRoomRemoveBan(string chatroomid)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatrooms/{chatroomid}/ban";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = "";

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Delete, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var addInfo = result.Deserialize<EaseCreateChatRoomModel<string, ChatRoomBan>>();

            if (addInfo?.data?.mute == false)
            {
                return true;
            }
        }
        return false;
    }

    #endregion 聊天室接口

    #region 群组接口

    /// <summary>
    /// 群组全局禁言
    /// </summary>
    /// <param name="chatgroupid">群组id</param>
    /// <returns></returns>
    public async Task<bool> ChatGroupBan(string chatgroupid)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatgroups/{chatgroupid}/ban";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = "";

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var addInfo = result.Deserialize<EaseCreateChatRoomModel<string, ChatRoomBan>>();

            if (addInfo?.data?.mute == true)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 群组全局解禁
    /// </summary>
    /// <param name="chatgroupid">群组id</param>
    /// <returns></returns>
    public async Task<bool> ChatGroupRemoveBan(string chatgroupid)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatrooms/{chatgroupid}/ban";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = "";

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Delete, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var addInfo = result.Deserialize<EaseCreateChatRoomModel<string, ChatRoomBan>>();

            if (addInfo?.data?.mute == false)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 创建群组
    /// </summary>
    /// <param name="dto">创建群组的对象</param>
    /// <returns>返回环信群组id</returns>
    public async Task<string> CreateChatGroupAsync(ImGroupCreateDto dto)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatgroups";
        //传入的token
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        //要发送的请求的参数
        var content = new
        {
            groupname = dto.GroupName,
            desc = dto.desc,
            @public = dto.IsOpen,
            maxusers = dto.MaxUsers,
            owner = dto.Owner,
            members = dto.Members
        }.ToJson();
        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var chatRoomInfo = result.Deserialize<EaseCreateChatRoomModel<string, ImGroupCreateVo>>();

            if (chatRoomInfo != null && chatRoomInfo.data != null)
            {
                return chatRoomInfo.data.groupid;
            }

            return "";
        }
        return "";
    }

    /// <summary>
    /// 群组单个增加成员
    /// </summary>
    /// <param name="chatgroupid">群组id(环信)</param>
    /// <param name="username">用户id(自己的)</param>
    /// <returns>成功/失败</returns>
    public async Task<bool> ChatgroupAddUserAsync(string chatgroupid, string username)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatgroups/{chatgroupid}/users/{username}";
        //传入的token
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        //要发送的请求的参数
        var content = "";
        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var chatRoomInfo = result.Deserialize<EaseCreateChatRoomModel<string, GroupAddUserVo>>();

            if (chatRoomInfo != null && chatRoomInfo.data != null)
            {
                return chatRoomInfo.data.result;
            }
            return default;
        }
        return default;
    }

    /// <summary>
    /// 添加群管理员
    /// </summary>
    /// <param name="chatgroupid"></param>
    /// <param name="username"></param>
    /// <returns>返回群组成员id</returns>
    public async Task<bool> ChatgroupAddManagerAsync(string chatgroupid, string username)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatgroups/{chatgroupid}/admin";
        //传入的token
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        //要发送的请求的参数
        var content = new
        {
            newadmin = username
        }.ToJson();
        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var chatRoomInfo = result.Deserialize<EaseCreateChatRoomModel<string, ChatRoomAdmin>>();

            if (chatRoomInfo != null && chatRoomInfo.data != null)
            {
                return chatRoomInfo.data?.result == "success";
            }
            return false;
        }
        return false;
    }

    /// <summary>
    /// 移除群管理员
    /// </summary>
    /// <param name="chatgroupid"></param>
    /// <param name="username"></param>
    /// <returns>成功/失败</returns>
    public async Task<string> ChatgroupDeleteManagerAsync(string chatgroupid, string username)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatgroups/{chatgroupid}/admin/{username}";
        //传入的token
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        //要发送的请求的参数
        var content = "";
        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Delete, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var chatRoomInfo = result.Deserialize<EaseCreateChatRoomModel<string, ChatGroupDelManagerVo>>();

            if (chatRoomInfo != null && chatRoomInfo.data != null)
            {
                return chatRoomInfo.data.result;
            }
            return default;
        }
        return default;
    }

    /// <summary>
    /// 移除单个群组成员
    /// </summary>
    /// <param name="chatgroupid"></param>
    /// <param name="username"></param>
    /// <returns>成功/失败</returns>
    public async Task<bool> ChatgroupDeleteUserAsync(string chatgroupid, string username)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatgroups/{chatgroupid}/users/{username}";
        //传入的token
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        //要发送的请求的参数
        var content = "";
        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Delete, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var chatRoomInfo = result.Deserialize<EaseCreateChatRoomModel<string, GroupAddUserVo>>();

            if (chatRoomInfo != null && chatRoomInfo.data != null)
            {
                return chatRoomInfo.data.result;
            }
            return default;
        }
        return default;
    }

    //批量移除群组成员
    /// <summary>
    /// 批量移除群组成员
    /// </summary>
    /// <param name="chatgroupid"></param>
    /// <param name="usernames"></param>
    /// <returns>返回群组成员id</returns>
    public async Task<List<string>> ChatgroupDeleteUserListAsync(string chatgroupid, List<string> usernames)
    {
        var users = string.Join(",", usernames.ToArray());
        if (string.IsNullOrWhiteSpace(users))
        {
            MessageBox.Show("删除异常！");
            return default;
        }
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatgroups/{chatgroupid}/users/{users}";
        //传入的token
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        //要发送的请求的参数
        var content = "";
        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Delete, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            if (usernames.Count > 1)
            {
                // 使用数组去接
                // 序列化token对象
                var chatRoomInfo = result.Deserialize<EaseBaseModel<string, GroupDeleteUserListVo>>();

                if (chatRoomInfo != null && chatRoomInfo.data != null)
                {
                    var _delInfo = chatRoomInfo.data.Where(w => w.result).Select(s => s.user).ToList();

                    return _delInfo;
                }
            }
            else
            {
                // 使用对象去接返回值，傻缺环信，一点不带脑子的,一会儿返回数组，一会儿返回对象

                // 序列化token对象
                var _deluser = result.Deserialize<EaseCreateChatRoomModel<string, GroupDeleteUserListVo>>();

                if (_deluser != null && _deluser.data != null && _deluser.data.result)
                {
                    return new List<string>(){
                        _deluser.data.user
                        };
                    // var _delInfo = _deluser.data.Where(w => w.result).Select(s => s.user).ToList();

                    // return _delInfo;
                }
            }

            return default;
        }
        return default;
    }

    /// <summary>
    /// 群组批量增加成员
    /// </summary>
    /// <param name="chatgroupid">群组id（环信id）</param>
    /// <param name="users">用户id</param>
    /// <returns>禁言成功的会员id</returns>
    public async Task<List<string>> ChatgroupAddUserListAsync(string chatgroupid, List<string> users)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatgroups/{chatgroupid}/users";
        //传入的token
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        //要发送的请求的参数
        var content = new
        {
            usernames = users
        }.ToJson();
        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var chatRoomInfo = result.Deserialize<EaseCreateChatRoomModel<string, GroupAddUserList>>();

            if (chatRoomInfo != null && chatRoomInfo.data != null)
            {
                return chatRoomInfo.data.newmembers;
            }
            return default;
        }
        return default;
    }

    /// <summary>
    /// 移除禁言
    /// </summary>
    /// <param name="chatgroupid">群组id</param>
    /// <param name="user">用户id</param>
    /// <returns>移除禁言成功/失败</returns>
    public async Task<bool> ChatgroupDeleteUserMuteAsync(string chatgroupid, string user)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatgroups/{chatgroupid}/mute/{user}";
        //传入的token
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        //要发送的请求的参数
        var content = "";
        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Delete, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var chatRoomInfo = result.Deserialize<EaseCreateChatRoomModel<string, ChatRoomMute>>();

            if (chatRoomInfo != null && chatRoomInfo.data != null)
            {
                return chatRoomInfo.data.result;
            }
            return default;
        }
        return default;
    }

    /// <summary>
    /// 修改群公告
    /// </summary>
    /// <param name="chatgroupid">群组id</param>
    /// <param name="announcement">公告内容</param>
    /// <returns></returns>
    public async Task<bool> ChatgroupUpdAnnouncementAsync(string chatgroupid, string announcement)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatgroups/{chatgroupid}/announcement";
        //传入的token
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        //要发送的请求的参数
        var content = new
        {
            announcement = announcement,
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var chatRoomInfo = result.Deserialize<EaseCreateChatRoomModel<string, GroupAnnouncement>>();

            if (chatRoomInfo != null && chatRoomInfo.data != null)
            {
                return chatRoomInfo.data.result;
            }
            return false;
        }
        return false;
    }

    /// <summary>
    /// 群组禁言用户
    /// </summary>
    /// <param name="usernames">用户im账户列表 </param>
    /// <param name="chatgroupid">聊天室id 聊天室唯一标识符，由环信服务器生成</param>
    /// <param name="duration">禁言时间（毫秒）</param>
    /// <returns></returns>
    public async Task<List<ChatRoomMute>> ChatGroupMuteUser(List<string> usernames, string chatgroupid, long duration)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatgroups/{chatgroupid}/mute";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = new
        {
            usernames = usernames,
            mute_duration = duration
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var addInfo = result.Deserialize<EaseBaseModel<string, ChatRoomMute>>();

            if (addInfo != null && addInfo.data?.Count > 0)
            {
                return addInfo.data;
            }
        }
        return default;
    }

    /// <summary>
    /// 群组解除禁言用户
    /// </summary>
    /// <param name="usernames">用户im账户列表</param>
    /// <param name="chatgroupid">聊天室id 聊天室唯一标识符，由环信服务器生成</param>
    /// <returns></returns>
    public async Task<List<ChatRoomMute>> ChatGroupNoMuteUser(List<string> usernames, string chatgroupid)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/chatgroups/{chatgroupid}/mute/{usernames[0]}";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = "";

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Delete, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var addInfo = result.Deserialize<EaseBaseModel<string, ChatRoomMute>>();

            if (addInfo != null && addInfo.data?.Count > 0)
            {
                return addInfo.data;
            }
        }
        return default;
    }

    //群组全局禁言

    #endregion 群组接口

    // 发送消息
    /// <summary>
    /// 后台服务发送消息
    /// </summary>
    /// <param name = "targetType">目标类型</param>
    /// <param name="target">目标id（列表）</param>
    /// <param name="msg">消息内容</param>
    /// <param name="type">消息类型；txt:文本消息，img：图片消息，loc：位置消息，audio：语音消息，video：视频消息，file：文件消息</param>
    /// <param name="from">发送人，默认admin</param>
    /// <returns></returns>
    public async Task<bool> SendMessageAsync(string targetType, List<string> target, string msg, string type, string from = "admin")
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/messages";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = new
        {
            target_type = targetType,
            target = target,
            msg = new
            {
                type = type,
                msg = msg,
            },
            from = from
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var recallInfo = result.Deserialize<EaseCreateChatRoomModel<string, Dictionary<string, string>>>();

            if (recallInfo != null)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 消息回撤
    /// </summary>
    /// <param name="msg_id">消息id</param>
    /// <param name="to">撤回消息的接收方</param>
    /// <param name="chat_type">单聊为'chat',群组为'groupchat'，聊天室为 'chatroom'</param>
    /// <returns></returns>
    public async Task<bool> MessagesRecallAsync(string msg_id, string to, string chat_type)
    {
        var _httpurl = $"{httpUrl}/{org_name}/{app_name}/messages/recall";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {EaseToken}");

        var content = new
        {
            msgs = new
            {
                msg_id = msg_id,
                to = to,
                chat_type = chat_type
            }
        }.ToJson();

        var result = await httpClientFactory.HttpSendAsync(HttpMethod.Post, _httpurl, content, headers);

        if (!string.IsNullOrWhiteSpace(result))
        {
            // 序列化token对象
            var recallInfo = result.Deserialize<EaseCreateChatRoomModel<string, MessagesRecall>>();

            if (recallInfo != null &&
                recallInfo?.data?.msgs?.Count > 0)
            {
                return true;
            }
        }
        return false;
    }
}