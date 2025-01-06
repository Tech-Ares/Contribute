using KUX.Models.ApiResultManage;
using KUX.Models.CzimAdmin.Request.ChatCustomer;
using KUX.Models.CzimSection;
using KUX.Models.Entities.Framework;
using KUX.Models.KuxAdmin.Request.ChatRoom;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.CzimSection;
using KUX.Services.Admin.Framework;
using KUX.Services.Admin.ServicesAdmin;
using KUX.Services.EaseImServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUX.Services.Admin.CzmiSection
{
    /// <summary>
    /// 客服管理
    /// </summary>
    public class CzimChatCustomerService : AdminBaseService<CzimChatCustomerRepository>
    {
        /// <summary>
        /// 环信聊天服务
        /// </summary>
        private readonly EaseImService _easeImService;

        private readonly CzimMemberService czimMemberService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="repository">默认仓储</param>
        /// <param name="easeImService">环信聊天服务</param>
        /// <param name="_czimMemberService">会员服务</param>
        /// <returns></returns>
        public CzimChatCustomerService(CzimChatCustomerRepository repository, EaseImService easeImService, CzimMemberService _czimMemberService) : base(repository)
        {
            this._easeImService = easeImService;
            czimMemberService = _czimMemberService;
        }
        //客服好友列表 
        /// <summary>
        /// 客服好友列表
        /// </summary>
        /// <param name="dto">查询条件</param>
        /// <returns></returns>
        public async Task<PageViewModel> GetChatCustomerOfFriendAsync(ChatCustomerOfFriendDto dto)
        {
            //根据聊天室客服id去查询当前客服的所有好友列表
            var result =(await Repository.Orm.Select<CzimChatFriendList,CzimMemberInfo>()
                                     .LeftJoin(s => s.t1.MemberId == s.t2.Id)

                                     .WhereIf(!string.IsNullOrWhiteSpace(dto.UserId),s=>s.t1.UserId==dto.UserId)
                                     .Page(dto.Page, dto.Size)
                                     .Count(out var total)
                                     .ToListAsync(s=>new
                                     {
                                         s.t2.NickName,
                                         s.t2.Avatar,
                                         s.t2.BsvNumber,
                                         s.t2.Birthday,
                                         s.t2.LoginName,
                                         s.t2.IsAgent,
                                         s.t2.Channel
                                     }));
            return await Repository.AsPageViewModelAsync(result,dto.Page,dto.Size,total);
        }


        /// <summary>
        /// 获取用户个人信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public async Task<object> FindUserAsync(string userId)
        {
            //先查当前用户是否是超管 
            var isAdmin = await Repository.Orm.Select<SysUser, CzimChatCustomer>()
                                        .LeftJoin(s => s.t1.Id == s.t2.UserId)
                                        .Where(s => s.t1.Id == userId && s.t1.IsActive && s.t2 != null)
                                        .FirstAsync();

            if (isAdmin != null)
            {
                //不是则用chatcustomer的数据
                var customerInfo = await Repository.Orm.Select<CzimChatCustomer>()
                                        .Where(s => s.UserId == userId)
                                        .FirstAsync();
                return new CzimChatCustomer()
                {
                    Avatar = customerInfo.Avatar,
                    NickName = customerInfo.NickName,
                    IsAgent = customerInfo.IsAgent,
                    UserId = userId
                };
            }
            else
            {
                var admin = await Repository.Orm.Select<SysUser>()
                                        .Where(s => s.Id == userId)
                                        .FirstAsync();
                //是超管就用sysuser的数据
                return new SysUser()
                {
                    Name = admin.Name,
                    LoginName = admin.LoginName,
                    Password = admin.Password
                };
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<(bool, string)> UpdateUserInfoAsync(string userId,UpdateUserInfoDto dto)
        {
            //先查当前用户是否是超管 
            var isAdmin = await Repository.Orm.Select<SysUser, CzimChatCustomer>()
                                        .LeftJoin(s => s.t1.Id == s.t2.UserId)
                                        .Where(s => s.t1.Id == userId && s.t1.IsActive && s.t2 != null)
                                        .FirstAsync();
            if (isAdmin!=null)
            {
                var isUpdate = await Repository.Orm.Update<CzimChatCustomer>()
                                          .Where(s => s.UserId ==userId)
                                          .Set(s => s.Avatar, dto.Avatar)
                                          .Set(s => s.NickName, dto.NickName)
                                          .ExecuteAffrowsAsync();
                if (isUpdate > 0)
                {
                    return (true, "修改成功！");
                }
                return (false, "修改失败！");
            }
            else
            {
                MessageBox.Show("超级管理员信息不允许修改！");
                return default;
            }
            
        }
        /// <summary>
        /// 修改账户密码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<(bool,string)> UpdatePasswordAsync(string userId,UpdatePasswordDto dto)
        {
            //判断是否是超管 如果是超管 则将密码重置为默认值,
            //不是超管，则可以修改密码
            var isAdmin = await Repository.Orm.Select<SysUser, CzimChatCustomer>()
                                        .LeftJoin(s => s.t1.Id == s.t2.UserId)
                                        .Where(s => s.t1.Id == userId && s.t1.IsActive && s.t2 != null)
                                        .FirstAsync();
            //不为空则为普通用户
            if (isAdmin!=null)
            {
                var isUpdate = await Repository.Orm.Update<SysUser>()
                                           .Set(s => s.Password,dto.Password)
                                           .Where(s=>s.Id==userId)
                                           .ExecuteAffrowsAsync();
                if (isUpdate>0)
                {
                    return (true,"修改成功！");
                }
                else
                {
                    return (false,"修改失败！");
                }
            }
            else
            {
                var isUpdate = await Repository.Orm.Update<SysUser>()
                                             .Set(s => s.Password, "123456")
                                             .Where(s => s.Id == userId)
                                             .ExecuteAffrowsAsync();
                if (isUpdate>0)
                {
                    return (true,"重置成功！");
                }
                return (false,"重置失败！");
            }
        }
        /// <summary>
        /// 新增推广专员接口
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<object> AddAgent(string userId, ChatCustomerSaveDto dto)
        {
            //推广专员是有两个身份，
            //一个是客服身份(是否是推广专员字段为true)---特殊的客服身份
            //一个是手机端app的用户身份，用于登录手机端app，推广用
            //新增客服信息
            var res = await this.Repository.Orm.Update<CzimChatCustomer>()
                .Set(s => s.IsAgent, dto.IsAgent)
                .Where(s => s.UserId == dto.UserId)
                .ExecuteAffrowsAsync();
            //新增用户信息
            var infoResult = await this.Repository.Orm.Update<CzimMemberInfo>()
                //用户表也有是否是推广员字段
                .Set(s => s.IsAgent, dto.IsAgent)
                .Where(s => s.Id == dto.UserId)
                .ExecuteAffrowsAsync();
            //两种身份信息都新增成功才是成功
            if (res > 0 && infoResult > 0)
                return (true, "提交成功！");
            //否则是失败
            //缺逻辑控制，判断当前哪条数据新增成功，就干掉哪条数据
            else
                return (false, "提交失败");
        }

        /// <summary>
        /// 获取所有管理员信息
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetAllCustomerAsync()
        {
            //查询当前状态有效的web聊天室客服信息
            var result = await this.Repository.Orm.Select<CzimChatCustomer>()
                                    .Where(w => w.IsActive)
                                    .ToListAsync(t => new
                                    {
                                        ManagerId = t.UserId,
                                        ManagerName = t.NickName
                                    });

            return result;
        }

        /// <summary>
        /// 获取客服相关所有群信息
        /// </summary>
        /// <param name="userId">管理员id</param>
        /// <returns></returns>
        public async Task<List<CzimChatroomInfo>> GetChatRoomInfoAsync(string userId)
        {
            //获取可用的聊天室信息
            var result = await this.Repository.Orm.Select<CzimChatroomInfo>()
                                    //根据web管理员的用户id去查询
                                    .Where(w => w.ManagerId == userId && w.IsActive)
                                    .ToListAsync();
            return result;
        }

        /// <summary>
        /// 获取所有客服信息
        /// </summary>
        /// <param name="dto">查询条件</param>
        /// <returns></returns>
        public async Task<PageViewModel> FindCustomerListAsync(ChatCustomerSearchDto dto)
        {
            //客服表与后台账号表关联
            //带出所有相关客服的信息
            var result = await this.Repository.Orm.Select<CzimChatCustomer, SysUser,CzimMemberInfo>()
                                    .LeftJoin(s=>s.t3.Id==s.t1.UserId)
                                    .LeftJoin(l => l.t1.UserId == l.t2.Id)
                                    .WhereIf(!string.IsNullOrWhiteSpace(dto?.NickName), w => w.t1.NickName.Contains(dto.NickName))
                                    .Where(s=>s.t2!=null)
                                    .OrderByDescending(o => o.t1.IsActive)
                                    .Count(out var total)
                                    .Page(dto.Page, dto.Size)
                                    .ToListAsync(t => new
                                    {
                                        t.t1.Id,
                                        t.t1.UserId,
                                        t.t1.Avatar,
                                        t.t1.NickName,
                                        t.t1.IsActive,
                                        CDate = t.t1.CDate == DateTime.MinValue ? "" : t.t1.CDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                        t.t1.CUser,
                                        t.t2.LoginName,
                                        t.t1.ManTime,
                                        t.t1.IsAgent,
                                        appLoginName= t.t3.LoginName ?? ""
                                    });

            return await this.Repository.AsPageViewModelAsync(result, dto.Page, dto.Size, total);
        }

        /// <summary>
        /// 获取所有可以成为客服的帐号
        /// </summary>
        /// <returns></returns>
        public async Task<object> FindCustomerUserAsync()
        {
            //查询当前所有客服信息(可用状态)
            var customers = await this.Repository.Orm.Select<CzimChatCustomer>()
                                    .Where(s => s.IsActive)
                                    .ToListAsync();
            //以用户表为主表，用户角色表，角色表关联
            //用户表
            var users = await this.Repository.Orm.Select<SysUser, SysUserRole, SysRole>()
                                    .LeftJoin(l => l.t1.Id == l.t2.UserId)
                                    .LeftJoin(l => l.t2.RoleId == l.t3.Id)
                                    //判断当前角色编号为3的才是客服角色
                                    .Where(w => w.t1.IsActive && w.t3.Number == 3)
                                    //查询最终返回web账户表
                                    .ToListAsync(t => t.t1);

            // 查找不在客服列表中的用户
            // web用户表(主键id是用户id)当前用户表的id不为客服的userid 查询出身份不是客服的web账号 筛选出不是客服的客服账号 有点绕，
            // 客服角色并不代表是客服，只有在客服表的客服才是真正客服，这条过滤是 筛选出 不在聊天客服表中的 客服角色 的web账号
            var result = users.Where(w => !customers.Select(s => s.UserId).Contains(w.Id)).Select(s => new { value = s.Id, label = s.Name });

            return result;
        }

        /// <summary>
        /// 新增保存客服信息
        /// </summary>
        /// <param name="userId">管理员id</param>
        /// <param name="ccsd">客服信息</param>
        /// <returns></returns>
        public async Task<(bool, string)> SaveCustomerAsync(string userId, ChatCustomerSaveDto ccsd)
        {
            var doDate = DateTime.Now;
            //查询客服 根据当前传入的userid 判断当前用户是不是客服
            var hasCustomer = await this.Repository.Orm.Select<CzimChatCustomer>()
                                        .Where(w => w.UserId == ccsd.UserId)
                                        .AnyAsync();
            //查询根据当前登录名称 查询到的app用户信息 存在即为true
            var hasMember = await this.Repository.Orm.Select<CzimMemberInfo>()
                                        .Where(w => w.LoginName == ccsd.LoginName)
                                        .AnyAsync();
            
            if (hasMember)
            {
                MessageBox.Show("用户登录名称重复，请修改用户登录名称！");
            }
            
            if (hasCustomer)
            {
                return (false, "当前账号已存在数据！请重新选择后台账户");
            }

            //聊天客服表
            CzimChatCustomer ccc = new CzimChatCustomer()
            {
                UserId = ccsd.UserId,
                Avatar = ccsd.Avatar,
                NickName = ccsd.NickName,
                IsAgent = ccsd.IsAgent,
                IsActive = true,
            };

            // 判断如果为推广员，需要注册app用户表
            if (ccc.IsAgent)
            {
                //非空校验
                if (string.IsNullOrEmpty(ccsd.LoginName))
                {
                    MessageBox.Show("账户名称必填！");
                }
                if (string.IsNullOrEmpty(ccsd.PassWord))
                {
                    MessageBox.Show("密码必填！");
                }
                //调用环信添加app账号新增接口，传入web账号id，及app登录账号，app密码，app账户昵称，是否是推广员
                var memberInfo = await czimMemberService.AddMemberAsync(ccc.UserId, ccc.UserId, 2, ccsd.LoginName, ccsd.PassWord, ccsd.NickName, ccc.IsAgent);
                //如果环信返回的是false则提示异常
                if (!memberInfo.Item1)
                {
                    MessageBox.Show("聊天账号注册异常，请联系管理员");
                    return default;
                }
                //将环信id存入客服表
                ccc.EaseChatCustomerId = memberInfo.Item2;
            }
            //不是推广员
            else
            {
                // 调用环信创建web用户方法 传入用户id，及用户昵称
                var dataIm = await _easeImService.CreateUsersAsync(ccsd.UserId, ccsd.NickName);
                //若是环信不为空 并且 返回的环信账户实体长度大于0 并且 账户实体的激活信息为true
                //
                if (dataIm != null && dataIm.entities?.Count > 0 && dataIm.entities[0].activated)
                {
                    // 可以执行
                }
                //环信账号注册失败则报错
                else
                {
                    MessageBox.Show("环信账号注册失败，可能重复注册！");
                    return default;
                }
                //将环信uuid存入客服的环信id中
                ccc.EaseChatCustomerId = dataIm.entities[0].uuid;
            }
            //两个环信id不一样是因为调用的环信接口的返回值不一样，其实存入的都是环信账户id


            // 保存客服信息
            var insCCC = await this.Repository.Orm.Insert(ccc).ExecuteAffrowsAsync();
            //若是保存客户信息不成功
            if (insCCC <= 0)
            {
                //调用环信删除数据的接口，传入我们的web账户的userid
                // 删除环信id
                await _easeImService.DeleteUsersAsync(ccc.UserId);
                MessageBox.Show("注册失败！");
            }
            return default;
        }

        /// <summary>
        /// 客服帐号激活/失效
        /// </summary>
        /// <param name="userId">管理员id</param>
        /// <param name="crad">聊天室信息</param>
        /// <returns></returns>
        public async Task<(bool, string)> CustomerActiveAsync(string userId, ChatRoomActiveDto crad)
        {
            //查询条件为web客服id去修改当前聊天客服的状态
            //改为可用/不可用
            var result = await this.Repository.Orm.Update<CzimChatCustomer>()
                                    .Set(s => s.IsActive, crad.IsActive)
                                    .Set(s => s.UUser, userId)
                                    .Set(s => s.UDate, DateTime.Now)
                                    .Where(w => w.Id == crad.Id)
                                    .ExecuteAffrowsAsync();

            if (result > 0)
            {
                return (true, crad.IsActive ? "账户激活成功" : "客服账户关闭成功");
            }

            return (false, "操作失败，请选择客服帐号重新处理");
        }
    }
}