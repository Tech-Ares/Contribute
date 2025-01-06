using KUX.Infrastructure;
using KUX.Infrastructure.Redis;
using KUX.Infrastructure.Token;
using KUX.Models.ApiResultManage;
using KUX.Models.CzimApp.AppBo;
using KUX.Models.CzimApp.Request.AppLogin;
using KUX.Models.CzimSection;
using KUX.Models.Enums;
using KUX.Repositories.CzimSection;
using KUX.Services.EaseImServices;
using Microsoft.AspNetCore.Http;

namespace KUX.Services.App.Accounts
{
    /// <summary>
    /// 用户账户（授权）服务
    /// </summary>
    public class AuthorMemberService : AppBaseService<CzimMemberRepository>
    {
        /// <summary>
        /// 授权会员信息
        /// </summary>
        private readonly MemberCacheBo _memberInfo;

        /// <summary>
        /// 配置文件
        /// </summary>
        private readonly AppConfiguration _appConfiguration;

        /// <summary>
        /// 请求上下文
        /// </summary>
        private readonly HttpContext _httpContext;

        /// <summary>
        /// 缓存服务
        /// </summary>
        private readonly IRedisService redisService;

        /// <summary>
        /// 缓存持续时间
        /// token有效期
        /// </summary>
        private readonly int AccountTime = 3;

        /// <summary>
        /// 环信服务
        /// </summary>
        private readonly EaseImService easeImService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="repository">会员仓储</param>
        /// <param name="_redisService">redis 缓存</param>
        /// <param name="appConfiguration">配置文件</param>
        /// <param name="httpContextAccessor">请求上下文</param>
        public AuthorMemberService(CzimMemberRepository repository,
            IRedisService _redisService,
            AppConfiguration appConfiguration,
            IHttpContextAccessor httpContextAccessor,
            EaseImService _easeImService) : base(repository)
        {
            redisService = _redisService;
            _appConfiguration = appConfiguration;
            _httpContext = httpContextAccessor.HttpContext;
            easeImService = _easeImService;
            this._memberInfo = this.FindMemberInfoByToken();
            // 这是redis默认链接库
            //redisService.SetDefaultDataBase(7);
        }

        /// <summary>
        /// 创建 token 根据账户 id
        /// </summary>
        /// <param name="id">crmid</param>
        /// <returns></returns>
        public string CreateToken(string id)
            => JwtTokenUtil.CreateToken(id, this._appConfiguration.JwtSecurityKey, this._appConfiguration.JwtKeyName, DateTime.Now.AddHours(24 * 3));

        /// <summary>
        /// 创建 token 根据账户 id
        /// </summary>
        /// <param name="id">crmid</param>
        /// <param name="expirs">过期时间</param>
        /// <returns></returns>
        public string CreateToken(string id, DateTime expirs)
            => JwtTokenUtil.CreateToken(id, this._appConfiguration.JwtSecurityKey, this._appConfiguration.JwtKeyName, expirs);

        /// <summary>
        /// 根据用户信息获取 Member 对象
        /// </summary>
        /// <param name="_token">用户token</param>
        /// <returns></returns>
        private MemberCacheBo FindMemberInfoByToken(string _token = "")
        {
            // 获取当前请求token
            var token = this._httpContext.Request.Headers[this._appConfiguration.AuthorizationKeyName].ToString();

            if (string.IsNullOrWhiteSpace(token))
            {
                return default;
            }

            if (this._httpContext.User.Identity == null && _token == "")
            {
                return default;
            }

            string id = "";
            if (_token != "")
            {
                id = JwtTokenUtil.ReadJwtToken(token);
            }
            else
            {
                id = this._httpContext.User.Identity.Name;
            }
            //var id = JwtTokenUtil.ReadJwtToken(token).ToGuid();
            //var id = this._httpContext.User.Identity.Name.ToGuid();

            //var id = this._httpContext.User.Identity.Name;

            if (string.IsNullOrWhiteSpace(id))
            {
                return default;
            }

            return GetMemberCache(id);
        }

        /// <summary>
        /// 获取当前登录账户信息
        /// </summary>
        /// <returns></returns>
        public MemberCacheBo GetMemberInfo() => this._memberInfo;

        /// <summary>
        /// 清理所有缓存信息
        /// </summary>
        public void FlushDatabase()
        {
            // 在job中使用，每天凌晨刷新用户数据
            // 然后清理所有缓存
            var result = redisService.FlushDatabase(1).Result;
        }

        /// <summary>
        /// 刷新指定会员缓存
        /// </summary>
        /// <param name="mid"></param>
        public bool UpdMemberCache(string mid)
        {
            // 可以直接清除redis的key信息
            return redisService.DeleteByKeyAsync(mid).Result;
        }

        /// <summary>
        /// 获取当前会员id
        /// </summary>
        public string Mid => this._memberInfo == null ? "" : this._memberInfo.Id;

        /// <summary>
        /// 检查账户 登录信息 并返回 token
        /// </summary>
        /// <param name="LoginName">帐号</param>
        /// <param name="code">密码</param>
        /// <param name="meid">（设备id）</param>
        /// <returns></returns>
        public async Task<string> CheckAccountAsync(string LoginName, string code, string meid)
        {
            var memberInfo = await this.Repository.Orm.Select<CzimMemberInfo>()
                                        .Where(w => w.LoginName == LoginName)
                                        .FirstAsync();
            if (memberInfo == null ||
                string.IsNullOrWhiteSpace(memberInfo.Id))
            {
                MessageBox.Show("请输入正确的账号密码！");
            }
            // 判断密码是否正确
            if (memberInfo?.PassWord != code.Md5Encrypt())
            {
                MessageBox.Show("请输入正确的账号密码！");
            }

            if (!memberInfo.IsActive)
            {
                MessageBox.Show("账号不可用！");
            }

            // 判断 账号密码的meid是否存在
            if (string.IsNullOrWhiteSpace(memberInfo?.Meid))
            {
                // 更新meid
                await this.Repository.Orm.Update<CzimMemberInfo>()
                                        .Set(s => s.Meid, meid)
                                        .Where(w => w.Id == memberInfo.Id)
                                        .ExecuteAffrowsAsync();
            }

            // 更新最后一次登录时间
            var uresult = await this.Repository.Orm.Update<CzimMemberCapital>()
                                            .Set(s => s.LastLoginDate, DateTime.Now)
                                            .Where(w => w.MId == memberInfo.Id)
                                            .ExecuteAffrowsAsync();

            this.UpdateMemberCache(memberInfo?.Id);

            return this.CreateToken(memberInfo?.Id);
        }

        /// <summary>
        /// 获取当前登录账户信息
        /// </summary>
        /// <returns></returns>
        public MemberCacheBo GetMemberInfo(string token)
        {
            return FindMemberInfoByToken(token);
        }

        /// <summary>
        /// 注册会员
        /// </summary>
        /// <param name="meid">设备id</param>
        /// <param name="loginName">登录账号</param>
        /// <param name="pwd">登录密码</param>
        /// <returns></returns>
        public async Task<string> RegisterMemberAsync(MemberRegisterDto rad)
        {
            // 判断meid是否被使用
            var hasUse = await this.Repository.Orm.Select<CzimMemberInfo>()
                                        .Where(w => w.Meid == rad.MeId)
                                        .AnyAsync();
            if (hasUse)
            {
                MessageBox.Show("当前设备已被使用，不可重复注册！");
            }

            var hasLoginName = await this.Repository.Orm.Select<CzimMemberInfo>()
                                            .Where(w => w.LoginName == rad.LoginName)
                                            .AnyAsync();

            if (hasLoginName)
            {
                MessageBox.Show("账号重复！");
            }

            // 验证推荐码是否正确
            var needRcode = await VerifiReferralCodeAsync(rad.ReferralCode);

            // 生成会员对象
            CzimMemberInfo fm = new CzimMemberInfo()
            {
                LoginName = rad.LoginName,
                NickName = rad.NickName,
                Meid = rad.MeId,
                PassWord = rad.PassWord.Md5Encrypt(),
                CDate = DateTime.Now,
                UDate = DateTime.Now,
                IsActive = true,
                RNumber = needRcode ? rad.ReferralCode : "",
                RegDate = DateTime.Now,
                MemberShip = 1,
            };

            // 生成会员编号，Number
            CzimZnumberSeed fas = new CzimZnumberSeed()
            {
                MId = fm.Id,
                IsActive = true,
                CDate = DateTime.Now,
                UDate = DateTime.Now,
                CUser = fm.Id,
                UUser = fm.Id
            };

            // 获取会员编号
            var number = await this.Repository.GetNumberSeedAsync(fas);

            if (number > 0)
            {
                fm.BsvNumber = number.ToString();//.PadLeft(6, '0');
            }

            if (string.IsNullOrWhiteSpace(fm.NickName))
            {
                fm.NickName = $"xchat_{fm.BsvNumber}";
            }

            // 注册环信账号
            var easeResult = await easeImService.CreateUsersAsync(fm.Id, fm.NickName);

            if (easeResult == null ||
                easeResult.entities?.Count <= 0 ||
                !easeResult.entities[0].activated)
            {
                // 删除操作
                MessageBox.Show("注册失败，无法注册聊天服务器！");
            }

            // 生成环信id
            fm.EaseId = easeResult?.entities[0].uuid;

            // 获取专属客服
            var customer = await this.Repository.Orm.Select<CzimChatCustomer>()
                                        .Where(w => w.IsActive && !w.IsAgent)
                                        .OrderBy(o => o.ManTime)
                                        .FirstAsync();
            if (customer != null &&
                !string.IsNullOrWhiteSpace(customer.UserId))
            {
                fm.ExclusiveUserId = customer.UserId;
            }
            else
            {
                // 固定客服id
                fm.ExclusiveUserId = "e62b08484a5f4e2194a7023a5504beb4";
            }

            // 新增会员信息
            var insInfo = await this.Repository.InsertAsync(fm);

            if (insInfo != null &&
                !string.IsNullOrWhiteSpace(insInfo.BsvNumber))
            {
                // 判断是否有好友关系，没有好友关系，则添加好友关系
                if (needRcode)
                {
                    var _relM = await this.Repository.Orm.Select<CzimMemberInfo>()
                                                    .Where(w => w.BsvNumber == rad.ReferralCode)
                                                    .FirstAsync();
                    if (_relM != null && !string.IsNullOrWhiteSpace(_relM.EaseId))
                    {
                        var hasFriend = await this.Repository.Orm.Select<CzimChatFriendList>()
                                                            .Where(w => w.MemberId == fm.Id && w.UserId == _relM.Id)
                                                            .AnyAsync();
                        if (!hasFriend)
                        {
                            CzimChatFriendList ccfl = new CzimChatFriendList()
                            {
                                MemberId = fm.Id,
                                CDate = DateTime.Now,
                                CrtDate = DateTime.Now,
                                UDate = DateTime.Now,
                                IsActive = true,
                                UserId = _relM.Id,
                            };

                            await this.Repository.Orm.Insert<CzimChatFriendList>(ccfl).ExecuteAffrowsAsync();
                        }
                    }
                }

                // 更新专属客服服务次数
                var updcustomer = await this.Repository.Orm.Update<CzimChatCustomer>()
                                            .Set(s => s.ManTime + 1)
                                            .Where(w => w.UserId == fm.ExclusiveUserId)
                                            .ExecuteAffrowsAsync();

                // 生成会员资金表
                CzimMemberCapital omc = new CzimMemberCapital()
                {
                    MId = insInfo.Id,
                    LastLoginDate = DateTime.Now,
                    Capital = 0.0M,
                    CapitalLocking = 0.0M,
                    FrozenTime = DateTime.Now.AddDays(-1),
                    IsActive = true,
                    // 免费次数
                    FreeTimesPerDay = 0,
                };

                // 保存会员扩展表
                var insCapital = await this.Repository.Orm
                                            .Insert<CzimMemberCapital>(omc)
                                            .ExecuteAffrowsAsync();

                if (insCapital > 0)
                {
                    // 调用专属客服发送默认服务
                    var _targets = new List<string>();
                    _targets.Add(fm.Id);

                    var _cusMsg = $@"欢迎使用x-chat,在这里您的沟通将毫无畏惧。
                                我是您的专属客服【{customer?.NickName}】,致电我，有更多惊喜！";
                    var customerMessage = await easeImService.SendMessageAsync("users", _targets, _cusMsg, "txt", fm.ExclusiveUserId);

                    // 获取聊天室
                    var defaultRM = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                                        .Where(w => w.IsActive && !string.IsNullOrWhiteSpace(w.EaseChatgroupId))
                                                        .OrderByDescending(o => o.IsDefault)
                                                        .FirstAsync();

                    if (defaultRM != null)
                    {
                        // 添加会员聊天室
                        CzimChatgroupMember ccm = new CzimChatgroupMember()
                        {
                            EaseChatgroupId=defaultRM.EaseChatgroupId,
                            ChatgroupId = defaultRM.Id,
                            MemberId = fm.Id,
                            JoinDate = DateTime.Now,
                            IsActive = true,
                            CDate = DateTime.Now,
                            UDate = DateTime.Now,
                        };
                        // 聊天室增加会员
                        await this.Repository.Orm.Insert<CzimChatgroupMember>(ccm).ExecuteAffrowsAsync();

                        // 环信聊天室增加会员
                        await this.easeImService.ChatRoomAddUser(fm.Id, defaultRM.EaseChatgroupId);
                    }

                    return this.CreateToken(fm.Id);
                }

                // 删除主表注册记录
                var delInfo = await this.Repository.DeleteAsync(insInfo.Id);
                // 删除环信id
                await easeImService.DeleteUsersAsync(fm.Id);

                MessageBox.Show("会员注册失败，请联系推广人员！ ");
            }

            return this.CreateToken(fm.Id);
        }

        /// <summary>
        /// 判断推荐码是否正确
        /// </summary>
        /// <param name="rcode">推荐码</param>
        /// <returns></returns>
        private async Task<bool> VerifiReferralCodeAsync(string rcode)
        {
            // 判断验证码是否必填
            var sysBase = await this.Repository.Orm.Select<CzimBaseSetting>()
                                            .Where(w => w.TypeCode.Contains("ReferralCode") && w.IsActive)
                                            .FirstAsync();

            if (sysBase == null || sysBase.TypeValue != "1")
            {
                return true;
            }

            if (sysBase.TypeValue == "1" && string.IsNullOrWhiteSpace(rcode))
            {
                MessageBox.Show("推荐码必填！");
                return false;
            }

            if (sysBase.TypeValue == "1")
            {
                // 判断推荐码是否有效
                var minfo = await this.Repository.Orm.Select<CzimMemberInfo>()
                                                .Where(w => w.BsvNumber == rcode && w.IsActive && w.IsAgent)
                                                .AnyAsync();

                if (!minfo)
                {
                    MessageBox.Show("推荐码不可用，请填写正确的推荐码！");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 通过会员id，更新缓存信息
        /// </summary>
        /// <param name="mid">会员id</param>
        /// <returns></returns>
        private MemberCacheBo UpdateMemberCache(string mid)
        {
            if (string.IsNullOrWhiteSpace(mid))
            {
                return default; ;
            }

            try
            {
                var minfo = this.Repository.GetMemberById(mid).Result;

                if (minfo.Item1 == null)
                {
                    return default; ;
                }
                MemberCacheBo mcb = new MemberCacheBo();

                mcb.Id = minfo.Item1.Id;
                mcb.LoginName = minfo.Item1.LoginName;
                mcb.BsvNumber = minfo.Item1.BsvNumber;
                mcb.NickName = string.IsNullOrWhiteSpace(minfo.Item1.NickName) ? $"游客帐号_{minfo.Item1.BsvNumber}" : minfo.Item1.NickName;
                mcb.RNumber = minfo.Item1.RNumber;
                mcb.Avatar = minfo.Item1.Avatar;
                mcb.Phone = minfo.Item1.Phone;
                mcb.Gender = (GenderEnum)minfo.Item1.Gender;
                mcb.Mailbox = minfo.Item1.Mailbox;
                mcb.Introduce = minfo.Item1.Introduce;
                mcb.RegDate = minfo.Item1.RegDate;
                mcb.Meid = minfo.Item1.Meid;
                mcb.IsMember = minfo.Item1.IsMember;
                // 专属客服id
                mcb.ExclusiveUserId = minfo.Item1.ExclusiveUserId;
                mcb.IsAgent = minfo.Item1.IsAgent;

                // 扩展信息
                mcb.LastLoginDate = minfo.Item2 == null ? DateTime.MinValue : minfo.Item2.LastLoginDate;
                mcb.Capital = minfo.Item2 == null ? 0.0M : minfo.Item2.Capital;
                mcb.CapitalLocking = minfo.Item2 == null ? 0.0M : minfo.Item2.CapitalLocking;
                mcb.FrozenTime = minfo.Item2.FrozenTime;
                mcb.LoginCity = minfo.Item2.LoginCity;
                mcb.LoginLat = minfo.Item2.LoginLat;
                mcb.LoginLong = minfo.Item2.LoginLong;

                // 聊天室信息
                if (minfo.Item3?.Count > 0)
                {
                    mcb.MemberChatRooms = minfo.Item3.MapToList<CzimChatroomMember, MemberChatRoomBo>();
                }

                //更新缓存
                redisService.AddOrUpdateByKeyAsync<MemberCacheBo>(mid, mcb).Wait();

                return mcb;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //return false;
                return default;
            }
        }

        /// <summary>
        /// 获取会员缓存
        /// </summary>
        /// <param name="mid">会员id</param>
        /// <returns>返回缓存的会员信息</returns>
        private MemberCacheBo GetMemberCache(string mid)
        {
            // 是否存在key
            var hasRedis = redisService.ExistsByKeyAsync(mid).Result;

            if (!hasRedis)
            {
                var mcb = UpdateMemberCache(mid);
                return mcb;
            }

            var mcache = redisService.FindByKeyAsync<MemberCacheBo>(mid).Result;
            return mcache;
        }
    }
}