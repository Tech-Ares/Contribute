using KUX.Models.ApiResultManage;
using KUX.Models.BO;
using KUX.Models.CzimAdmin.Reponse.Chat;
using KUX.Models.CzimAdmin.Reponse.ChatRoom;
using KUX.Models.CzimAdmin.Request.ChatInfo;
using KUX.Models.CzimAdmin.Request.ChatRoom;
using KUX.Models.CzimAdmin.Request.CzimChat;
using KUX.Models.CzimSection;
using KUX.Models.Enums;
using KUX.Models.KuxAdmin.Request.ChatRoom;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.CzimSection;
using KUX.Services.Admin.ServicesAdmin;
using KUX.Services.EaseImServices;
using KUX.Services.EaseImServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUX.Services.Admin.CzmiSection
{
    /// <summary>
    /// 聊天服务
    /// </summary>
    public class CzimChatService : AdminBaseService<CzimChatRepository>
    {
        /// <summary>
        /// 环信服务
        /// </summary>
        private readonly EaseImService easeImService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="repository">默认仓储</param>
        /// <param name="_easeImService">环信服务</param>
        public CzimChatService(CzimChatRepository repository, EaseImService _easeImService) : base(repository)
        {
            this.easeImService = _easeImService;
        }

        /// <summary>
        /// 聊天室开启/关闭群体禁言
        /// </summary>
        /// <param name="ainfo">管理员账户信息</param>
        /// <param name="crad">聊天室信息</param>
        /// <returns></returns>
        public async Task<(bool, string)> ChatRoomProhibitAsync(AccountInfo ainfo, ChatRoomProhibitDto crad)
        {
            if (string.IsNullOrWhiteSpace(crad.ChatRoomId))
            {
                MessageBox.Show("聊天室信息不能为空");
            }
            if (string.IsNullOrWhiteSpace(crad.IsProhibit.ToString()))
            {
                MessageBox.Show("状态码不能为空");
            }
            var canDo = false;
            var doDate = DateTime.Now;

            #region 判断是否是管理员 管理员才可以禁言

            if (ainfo.IsAdministrator)
            {
                canDo = true;
            }

            // 查询管理员信息
            var allManager = await this.Repository.Orm.Select<CzimChatroomManager>()
                                .Where(w => w.ChatRoomId == crad.ChatRoomId && w.IsActive)
                                .ToListAsync(t => t.ManagerId);

            if (allManager.Contains(ainfo.UserId))
            {
                canDo = true;
            }

            if (!canDo)
            {
                return (false, "非管理员不可以操作！");
            }

            #endregion 判断是否是管理员 管理员才可以禁言

            //查询当前聊天室信息
            var ismdata = await this.Repository.Orm.Select<CzimChatroomInfo>()
                                                    .Where(w => w.Id == crad.ChatRoomId && w.IsActive)
                                                    .FirstAsync();

            //获取当前聊天室的所有会员(排除管理员)
            var chatRoomMemberId = await this.Repository.Orm.Select<CzimChatroomMember>()
                                                            .Where(s => s.ChatRoomId == crad.ChatRoomId)
                                                            .Where(w => !allManager.Contains(w.MemberId))
                                                            .ToListAsync(s => s.MemberId);
            //var imInFo = new List<ChatRoomMute>();
            bool Ban = false;
            //开启禁言
            if (crad.IsProhibit)
            {
                //调用环信接口
                Ban = await easeImService.ChatRoomBan(ismdata.EaseChatRoomId);
            }
            //解除禁言
            else
            {
                Ban = await easeImService.ChatRoomRemoveBan(ismdata.EaseChatRoomId);
            }
            //判断环信禁言是否成功，不成功则报错
            if (!Ban)
            {
                MessageBox.Show("环信处理异常，请联系管理员！");
            }
            //判断环信返回结果是否正确
            if (Ban)
            {
                //正确则自己服务器上的数据进行修改，修改当前聊天室的禁言状态
                var result = await this.Repository.Orm.Update<CzimChatroomInfo>()
                                            .Set(s => s.IsProhibit, crad.IsProhibit)
                                            .Set(s => s.UUser, ainfo.UserId)
                                            .Set(s => s.UDate, doDate)
                                            .Where(w => w.Id == crad.ChatRoomId)
                                            .ExecuteAffrowsAsync();
                //如果返回结果正常
                if (result > 0)
                {
                    //则提示正确
                    return (true, crad.IsProhibit ? "禁言成功！" : "群体解禁！");
                }
            }
            //提示操作不当
            return (false, "操作失败，请选择聊天室重新处理");
        }

        //群组开启/关闭群体禁言
        /// <summary>
        /// 群组开启/关闭群体禁言
        /// </summary>
        /// <param name="ainfo"></param>
        /// <param name="crad"></param>
        /// <returns></returns>
        public async Task<(bool, string)> ChatGroupProhibitAsync(AccountInfo ainfo, ChatRoomProhibitDto crad)
        {
            if (string.IsNullOrWhiteSpace(crad.ChatGroupId))
            {
                MessageBox.Show("群组信息不能为空");
            }
            if (string.IsNullOrWhiteSpace(crad.IsProhibit.ToString()))
            {
                MessageBox.Show("状态码不能为空");
            }
            var canDo = false;
            var doDate = DateTime.Now;

            #region 判断是否是管理员 管理员才可以禁言

            if (ainfo.IsAdministrator)
            {
                canDo = true;
            }

            if (!canDo)
            {
                // 判断是否为群管理员
                canDo = await this.Repository.Orm.Select<CzimChatgroupMember>()
                                    .Where(w => w.MemberId == ainfo.UserId && w.ChatgroupId == crad.ChatGroupId)
                                    .AnyAsync();
            }
            if (!canDo)
            {
                canDo = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                         .Where(w => w.OwnId == ainfo.Id && w.Id == crad.ChatGroupId)
                         .AnyAsync();
            }

            if (!canDo)
            {
                return (false, "非管理员不可以操作！");
            }

            #endregion 判断是否是管理员 管理员才可以禁言

            //查询当前聊天室信息
            var ismdata = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                                    .Where(w => w.Id == crad.ChatGroupId && w.IsActive)
                                                    .FirstAsync();

            bool Ban = false;
            //开启禁言
            if (crad.IsProhibit)
            {
                //调用环信接口
                Ban = await easeImService.ChatRoomBan(ismdata.EaseChatgroupId);
            }
            //解除禁言
            else
            {
                Ban = await easeImService.ChatRoomRemoveBan(ismdata.EaseChatgroupId);
            }
            //判断环信禁言是否成功，不成功则报错
            if (!Ban)
            {
                MessageBox.Show("环信处理异常，请联系管理员！");
            }
            //判断环信返回结果是否正确
            if (Ban)
            {
                //正确则自己服务器上的数据进行修改，修改当前聊天室的禁言状态
                var result = await this.Repository.Orm.Update<CzimChatgroupInfo>()
                                            .Set(s => s.IsProhibit, crad.IsProhibit)
                                            .Set(s => s.UUser, ainfo.UserId)
                                            .Set(s => s.UDate, doDate)
                                            .Where(w => w.Id == crad.ChatGroupId)
                                            .ExecuteAffrowsAsync();
                //如果返回结果正常
                if (result > 0)
                {
                    //则提示正确
                    return (true, crad.IsProhibit ? "禁言成功！" : "群体解禁！");
                }
            }
            //提示操作不当
            return (false, "操作失败，请选择聊天室重新处理");
        }

        //群组单个会员禁言
        /// <summary>
        /// 群组单个会员禁言
        /// </summary>
        /// <param name="ainfo">管理员id</param>
        /// <param name="crad">禁言信息</param>
        /// <returns></returns>
        public async Task<(bool, string)> MemberForbiddenGroupAsync(AccountInfo ainfo, ChatRoomMemberForbiddenDto crad)
        {
            if (string.IsNullOrWhiteSpace(crad.GroupId))
            {
                MessageBox.Show("聊天室信息不能为空");
            }
            if (string.IsNullOrWhiteSpace(crad.MemberId))
            {
                MessageBox.Show("会员信息不能为空！");
            }
            if (crad.Day == -1)
            {
                MessageBox.Show("天数不能不填！");
            }
            //是否是群管理员或者超管
            var canDo = false;

            var doDate = DateTime.Now;
            //判断是否是群管理员或者超管
            if (ainfo.IsAdministrator)
            {
                canDo = true;
            }
            if (!canDo)
            {
                // 判断是否为群管理员
                canDo = await this.Repository.Orm.Select<CzimChatgroupMember>()
                                    .Where(w => w.MemberId == ainfo.UserId && w.ChatgroupId == crad.GroupId)
                                    .AnyAsync();
            }
            if (!canDo)
            {
                canDo = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                         .Where(w => w.OwnId == ainfo.Id && w.Id == crad.GroupId)
                         .AnyAsync();
            }

            if (!canDo)
            {
                MessageBox.Show("非管理员不可以操作");
                //return (false, "非管理员不可以操作！");
            }
            //查询当前聊天室信息
            var ismdata = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                               //根据当前聊天室id去查询 并且当前聊天室状态为可用
                               .Where(w => w.Id == crad.GroupId && w.IsActive).FirstAsync();

            //当前聊天室信息的环信id如果为空
            if (string.IsNullOrWhiteSpace(ismdata?.EaseChatgroupId))
            {
                MessageBox.Show("聊天室的环信id异常，请联系管理员！");
            }

            //获取当前聊天室会员信息
            var chatGroupMemberId = await this.Repository.Orm.Select<CzimChatgroupMember>()
                                    //根据聊天室id及app会员id进行查询
                                    .Where(s => s.ChatgroupId == crad.GroupId && s.MemberId == crad.MemberId)
                                    //最终返回一个会员集合
                                    .ToListAsync(s => s.MemberId);

            //app用户禁言时间
            var MemberForbiddenTime = await this.Repository.Orm.Select<CzimChatgroupMember>()
                                    //根据聊天室id及app会员id进行查询
                                    .Where(s => s.ChatgroupId == crad.GroupId && s.MemberId == crad.MemberId)
                                    //返回用户禁言时间
                                    .FirstAsync(s => s.ForbiddenTime);

            var forbiddenTime = DateTime.Now;
            //聊天室禁言
            var imInFo = new List<ChatRoomMute>();
            if (crad.Day > 0)
            {
                if (MemberForbiddenTime != DateTime.MinValue && MemberForbiddenTime <= DateTime.Now)
                {
                    MessageBox.Show("禁言时间异常请重新输入");
                }
                else if (MemberForbiddenTime != DateTime.MinValue)
                {
                    MessageBox.Show("该用户已经被禁言");
                }
                // 开始禁言
                forbiddenTime = DateTime.Now.AddDays(crad.Day);
                //天数转换为秒
                var millisecond = crad.Day * 24 * 60 * 60 * 1000;
                //当前时间减去1970的毫秒数
                var timp = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                var sum = millisecond + timp;

                //环信禁言需要的时间是毫秒数
                imInFo = await easeImService.ChatGroupMuteUser(chatGroupMemberId, ismdata.EaseChatgroupId, sum);
                if (imInFo[0]?.result == false)
                {
                    MessageBox.Show("环信处理异常，请联系管理员！");
                }
                //聊天室成员信息
                var result = await this.Repository.Orm.Update<CzimChatgroupMember>()
                                        .Set(s => s.ForbiddenTime, forbiddenTime)
                                        .Set(s => s.UUser, ainfo.UserId)
                                        .Set(s => s.UDate, doDate)
                                        //根据当前聊天室id去查询
                                        .Where(w => w.ChatgroupId == crad.GroupId)
                                        .Where(s => s.MemberId == crad.MemberId)
                                        .ExecuteAffrowsAsync();

                if (result > 0)
                {
                    return (true, "禁言成功！");
                }
                //失败了则调用解除禁言接口
                await easeImService.ChatGroupNoMuteUser(chatGroupMemberId, ismdata.EaseChatgroupId);
            }
            else if (crad.Day <= 0)
            {
                // 解除禁言  调用环信解除禁言的接口
                imInFo = await easeImService.ChatGroupNoMuteUser(chatGroupMemberId, ismdata.EaseChatgroupId);
                if (imInFo[0]?.result == false)
                {
                    MessageBox.Show("环信处理异常，请联系管理员！");
                }
                if (imInFo[0]?.result == true)
                {
                    var result = await this.Repository.Orm.Update<CzimChatgroupMember>()
                                        .Set(s => s.ForbiddenTime, DateTime.MinValue)
                                        .Set(s => s.UUser, ainfo.UserId)
                                        .Set(s => s.UDate, doDate)
                                        //根据当前聊天室id去查询
                                        .Where(w => w.ChatgroupId == crad.GroupId)
                                        //根据当前app用户id去查询
                                        .Where(s => s.MemberId == crad.MemberId)
                                        .ExecuteAffrowsAsync();

                    if (result > 0)
                    {
                        return (true, "解除禁言成功！");
                    }
                }
                else
                {
                    MessageBox.Show("环信解除禁言异常，请联系管理员！");
                }
            }

            return (false, "操作失败，请选择会员重新处理");
        }

        /// <summary>
        /// 聊天室单个会员禁言
        /// </summary>
        /// <param name="ainfo">管理员id</param>
        /// <param name="crad">禁言信息</param>
        /// <returns></returns>
        public async Task<(bool, string)> MemberForbiddenAsync(AccountInfo ainfo, ChatRoomMemberForbiddenDto crad)
        {
            if (string.IsNullOrWhiteSpace(crad.ChatRoomId))
            {
                MessageBox.Show("聊天室信息不能为空");
            }
            if (string.IsNullOrWhiteSpace(crad.MemberId))
            {
                MessageBox.Show("会员信息不能为空！");
            }
            //是否是群管理员或者超管
            var canDo = false;

            var doDate = DateTime.Now;
            //判断是否是群管理员或者超管
            if (ainfo.IsAdministrator)
            {
                canDo = true;
            }
            else
            {
                // 判断是否为群管理员
                var ism = await this.Repository.Orm.Select<CzimChatroomManager>()
                                    .Where(w => w.ManagerId == ainfo.UserId && w.ChatRoomId == crad.ChatRoomId && w.IsActive)
                                    .AnyAsync();

                if (ism)
                {
                    canDo = true;
                }
            }

            if (!canDo)
            {
                return (false, "非管理员不可以操作！");
            }
            //查询当前聊天室信息
            var ismdata = await this.Repository.Orm.Select<CzimChatroomInfo>()
                               //根据当前聊天室id去查询 并且当前聊天室状态为可用
                               .Where(w => w.Id == crad.ChatRoomId && w.IsActive).FirstAsync();

            //当前聊天室信息的环信id如果为空
            if (string.IsNullOrWhiteSpace(ismdata.EaseChatRoomId))
            {
                MessageBox.Show("聊天室的环信id异常，请联系管理员！");
            }

            //获取当前聊天室会员信息
            var chatRoomMemberId = await this.Repository.Orm.Select<CzimChatroomMember>()
                                    //根据聊天室id及app会员id进行查询
                                    .Where(s => s.ChatRoomId == crad.ChatRoomId && s.MemberId == crad.MemberId)
                                    //最终返回一个会员集合
                                    .ToListAsync(s => s.MemberId);

            //app用户禁言时间
            var MemberForbiddenTime = await this.Repository.Orm.Select<CzimChatroomMember>()
                                    //根据聊天室id及app会员id进行查询
                                    .Where(s => s.ChatRoomId == crad.ChatRoomId && s.MemberId == crad.MemberId)
                                    //返回用户禁言时间
                                    .FirstAsync(s => s.ForbiddenTime);

            var forbiddenTime = DateTime.Now;
            //聊天室禁言
            var imInFo = new List<ChatRoomMute>();
            if (crad.Day > 0)
            {
                if (MemberForbiddenTime != DateTime.MinValue && MemberForbiddenTime <= DateTime.Now)
                {
                    MessageBox.Show("禁言时间异常请重新输入");
                }
                else if (MemberForbiddenTime != DateTime.MinValue)
                {
                    MessageBox.Show("该用户已经被禁言");
                }
                // 开始禁言
                forbiddenTime = DateTime.Now.AddDays(crad.Day);
                //天数转换为秒
                var millisecond = crad.Day * 24 * 60 * 60 * 1000;
                //当前时间减去1970的毫秒数
                var timp = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                var sum = millisecond + timp;

                //环信禁言需要的时间是毫秒数
                imInFo = await easeImService.ChatRoomMuteUser(chatRoomMemberId, ismdata.EaseChatRoomId, sum);
                if (imInFo[0]?.result == false)
                {
                    MessageBox.Show("环信处理异常，请联系管理员！");
                }
                //聊天室成员信息
                var result = await this.Repository.Orm.Update<CzimChatroomMember>()
                                        .Set(s => s.ForbiddenTime, forbiddenTime)
                                        .Set(s => s.UUser, ainfo.UserId)
                                        .Set(s => s.UDate, doDate)
                                        //根据当前聊天室id去查询
                                        .Where(w => w.ChatRoomId == crad.ChatRoomId)
                                        .Where(s => s.MemberId == crad.MemberId)
                                        .ExecuteAffrowsAsync();

                if (result > 0)
                {
                    return (true, "禁言成功！");
                }
                //失败了则调用解除禁言接口
                await easeImService.ChatRoomNoMuteUser(chatRoomMemberId, ismdata.EaseChatRoomId);
            }
            else if (crad.Day <= 0)
            {
                // 解除禁言  调用环信解除禁言的接口
                imInFo = await easeImService.ChatRoomNoMuteUser(chatRoomMemberId, ismdata.EaseChatRoomId);
                if (imInFo[0]?.result == false)
                {
                    MessageBox.Show("环信处理异常，请联系管理员！");
                }
                if (imInFo[0]?.result == true)
                {
                    var result = await this.Repository.Orm.Update<CzimChatroomMember>()
                                        .Set(s => s.ForbiddenTime, DateTime.MinValue)
                                        .Set(s => s.UUser, ainfo.UserId)
                                        .Set(s => s.UDate, doDate)
                                        //根据当前聊天室id去查询
                                        .Where(w => w.ChatRoomId == crad.ChatRoomId)
                                        //根据当前app用户id去查询
                                        .Where(s => s.MemberId == crad.MemberId)
                                        .ExecuteAffrowsAsync();

                    if (result > 0)
                    {
                        return (true, "解除禁言成功！");
                    }
                }
                else
                {
                    MessageBox.Show("环信解除禁言异常，请联系管理员！");
                }
            }

            return (false, "操作失败，请选择会员重新处理");
        }

        /// <summary>
        /// 聊天室/群组全局禁言
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public async Task<(bool, string)> AllChatRoomBan(string userId, AllChatRoomBanDto cmd)
        {
            //if (string.IsNullOrWhiteSpace(cmd.ChatRoomId))
            //{
            //    MessageBox.Show("聊天室信息不能为空!");
            //}

            if (string.IsNullOrWhiteSpace(cmd.MemberId))
            {
                MessageBox.Show("聊天室会员信息不能为空！");
            }
            ////首先查询web用户表，判断是否是超管 或者是否是管理员
            //var isManager = await Repository.Orm.Select<CzimChatroomManager>()
            //                            //根据传入的管理员id查询管理员信息
            //                            .Where(s => s.ManagerId == userId && s.IsActive)
            //                            .AnyAsync();
            ////查询聊天室主表 因为这里存放着超管信息
            //var isSuperManager = await Repository.Orm.Select<CzimChatroomInfo>()
            //                               //根据传入的web用户id 聊天室id
            //                               .Where(s => s.ManagerId == userId  && s.IsActive)
            //                               .AnyAsync();
            ////如果当前web账号不属于当前聊天室的管理员 或者 不属于当前聊天室的超管
            //if (!isManager && !isSuperManager)
            //{
            //    //则不能禁言
            //    MessageBox.Show("必须是管理员或超管");
            //}

            #region 判断当前web管理员,用户和当前聊天室的环信id有没有，没有就报错

            ////环信管理员id
            //var isEaseManagerId = await Repository.Orm.Select<CzimChatCustomer>()
            //                                  .Where(s => s.UserId == userId)
            //                                  .FirstAsync(s => s.EaseChatCustomerId);
            //if (string.IsNullOrWhiteSpace(isEaseManagerId))
            //{
            //    MessageBox.Show("管理员的环信账号异常，请联系管理员!");
            //}
            ////环信聊天室id
            //var isEaseChatRoomId = await Repository.Orm.Select<CzimChatroomInfo>().
            //                                    Where(s => s.Id == cmd.ChatRoomId)
            //                                    .FirstAsync(s => s.EaseChatRoomId);
            //if (string.IsNullOrWhiteSpace(isEaseChatRoomId))
            //{
            //    MessageBox.Show("当前聊天室的环信账号异常，请联系管理员!");
            //}
            //环信用户id
            //var isEaseCustomerId = await Repository.Orm.Select<CzimMemberInfo>()
            //                                     .Where(s => s.Id == cmd.MemberId)
            //                                     .FirstAsync(s => s.EaseId);
            //if (string.IsNullOrWhiteSpace(isEaseCustomerId))
            //{
            //    MessageBox.Show("当前会员的环信账号异常，请联系管理员！");
            //}

            #endregion 判断当前web管理员,用户和当前聊天室的环信id有没有，没有就报错

            //将小时转换为毫秒数 要计算的小时*这个变量即可
            var calculate = 24 * 60 * 60 * 1000;
            //czim_chatroom_manager 聊天室管理员表 czim_chatroom_info 聊天室主表
            //调用环信 聊天室会员全局禁言接口

            var imInfo = await easeImService.ChatCustomerAllBan(cmd.MemberId, cmd.Day * calculate);
            //判断当前传入的时间

            if (imInfo)
            {
                if (cmd.Day > 0)
                {
                    //将禁言时间等信息存到本地数据库
                    var forbiddenTime = DateTime.Now.AddDays(cmd.Day);
                    var isUpdate = await Repository.Orm.Update<CzimMemberCapital>()
                                           .Set(s => s.ForbiddenTime, forbiddenTime)
                                           .Set(s => s.UDate, DateTime.Now)
                                           .Set(s => s.UUser, userId)
                                           .Where(s => s.MId == cmd.MemberId)
                                           .ExecuteAffrowsAsync();
                    if (isUpdate > 0)
                    {
                        return (true, "禁言成功");
                    }
                    return (true, "禁言失败！");
                }
                else
                {
                    var isUpdate = await Repository.Orm.Update<CzimMemberCapital>()
                                           .Set(s => s.ForbiddenTime, DateTime.MinValue)
                                           .Set(s => s.UDate, DateTime.Now)
                                           .Set(s => s.UUser, userId)
                                           .Where(s => s.MId == cmd.MemberId)
                                           .ExecuteAffrowsAsync();
                    if (isUpdate > 0)
                    {
                        return (true, "解禁成功");
                    }
                    return (true, "解禁失败！");
                }
            }
            return (false, "环信返回值异常，请联系管理员！");
        }

        ///撤回单聊消息
        /// <summary>
        /// 撤回单聊消息
        /// </summary>
        /// <param name="ai">管理员信息</param>
        /// <param name="cmd">聊天消息</param>
        /// <returns></returns>
        public async Task<(bool, string)> DoRecallOneAsync(AccountInfo ai, ChatMessageDto cmd)
        {
            var doDate = DateTime.Now;

            // 调用环信消息撤回接口
            var result = await this.easeImService.MessagesRecallAsync(cmd.MessageContent, cmd.ReceiveId, "chat");

            if (result)
            {
                return (true, "");
            }

            return (false, "撤回异常！");
        }

        /// <summary>
        /// 管理员撤回群聊消息
        /// </summary>
        /// <param name="ai">管理员信息</param>
        /// <param name="cmd">聊天消息</param>
        /// <returns></returns>
        public async Task<(bool, string)> DoRecallChanAsync(AccountInfo ai, ChatMessageDto cmd)
        {
            // 查询是否是管理员
            var isManager = await this.Repository.Orm.Select<CzimChatroomManager>()
                                        .Where(w => w.ManagerId == ai.UserId && w.IsActive && w.ChatRoomId == cmd.ReceiveId)
                                        .AnyAsync();
            var isCustomer = await this.Repository.Orm.Select<CzimChatCustomer>()
                                        .Where(s => s.UserId == ai.Id && s.IsActive)
                                        .AnyAsync();
            if (!ai.IsAdministrator &&
                !isManager &&
                !isCustomer)
            {
                MessageBox.Show("非后台管理员，不可以撤回消息");
                return default;
            }

            var doDate = DateTime.Now;

            // 调用环信消息撤回接口
            var result = await this.easeImService.MessagesRecallAsync(cmd.MessageContent, cmd.ReceiveId, "groupchat");

            if (result)
            {
                return (true, "");
            }

            return (false, "撤回异常！");
        }

        /// <summary>
        /// 查询所有用户的头像和昵称
        /// </summary>
        /// <param name="userId">当前用户id</param>
        /// <param name="targetIds">目标id</param>
        /// <returns></returns>
        public async Task<List<ChatContactsVo>> GetUserExtAsync(string userId, List<string> targetIds)
        {
            // 所有会员信息
            var memberInfo = await this.Repository.Orm.Select<CzimMemberInfo>()
                                            .Where(w => targetIds.Contains(w.Id))
                                            .ToListAsync(t => new ChatContactsVo
                                            {
                                                TargetId = t.Id,
                                                NickName = t.NickName,
                                                Avatar = t.Avatar,
                                                CzimId = t.Id,
                                                CzType = (int)CzTypeEnum.SingleChat,
                                            });
            // 所有客服信息
            var userInfo = await this.Repository.Orm.Select<CzimChatCustomer>()
                                            .Where(w => targetIds.Contains(w.UserId))
                                            .ToListAsync(t => new ChatContactsVo
                                            {
                                                TargetId = t.UserId,
                                                NickName = t.NickName,
                                                Avatar = t.Avatar,
                                                CzimId = t.UserId,
                                                CzType = (int)CzTypeEnum.SingleChat
                                            });
            var targetList = new List<ChatContactsVo>();
            targetList.AddRange(memberInfo);
            targetList.AddRange(userInfo);

            return targetList.DistinctBy(d => d.TargetId).ToList();
        }

        /// <summary>
        /// 聊天室查询所有聊天的背景和说明
        /// </summary>
        /// <param name="userId">当前用户id</param>
        /// <param name="targetIds">目标id</param>
        /// <returns></returns>
        public async Task<List<ChatContactsVo>> GetChatRoomExtAsync(string userId, List<string> targetIds)
        {
            // 所有聊天室信息
            var chatroomInfo = await this.Repository.Orm.Select<CzimChatroomInfo>()
                                            .Where(w => targetIds.Contains(w.EaseChatRoomId))
                                            .ToListAsync(t => new ChatContactsVo
                                            {
                                                TargetId = t.EaseChatRoomId,
                                                NickName = t.ChatRoomName,
                                                Avatar = t.ChatRoomImg,
                                                CzimId = t.Id,
                                                CzType = (int)CzTypeEnum.ChatRoom,
                                                IsProhibit = t.IsProhibit,
                                                Maxusers = t.UserNum
                                            });

            var groupInfo = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                            .Where(w => targetIds.Contains(w.EaseChatgroupId))
                                            .ToListAsync(t => new ChatContactsVo
                                            {
                                                TargetId = t.EaseChatgroupId,
                                                NickName = t.GroupName,
                                                Avatar = t.GroupImg,
                                                CzimId = t.Id,
                                                CzType = (int)CzTypeEnum.GroupChat,
                                                IsProhibit = t.IsProhibit,
                                                Maxusers = t.Maxusers
                                            });
            List<ChatContactsVo> ccvs = new List<ChatContactsVo>();
            if (chatroomInfo?.Count > 0)
            {
                ccvs.AddRange(chatroomInfo);
            }

            if (groupInfo?.Count > 0)
            {
                ccvs.AddRange(groupInfo);
            }
            return ccvs.DistinctBy(d => d.TargetId).ToList();
        }

        /// <summary>
        /// 获取客服相关所有聊天室信息
        /// </summary>
        /// <param name="userId">管理员id</param>
        /// <param name="csd">聊天室内容</param>
        /// <returns></returns>
        public async Task<PageViewModel> GetChatRoomListAsync(string userId, ChatRoomSearchDto csd)
        {
            var result = await this.Repository.Orm.Select<CzimChatroomInfo, CzimChatroomManager>()
                                                    .LeftJoin(l => l.t1.Id == l.t2.ChatRoomId)
                                                    .Where(w => w.t2.ManagerId == userId && w.t2.IsActive && w.t1.IsActive)
                                                    .WhereIf(!string.IsNullOrWhiteSpace(csd.ChatroomName), w => w.t1.ChatRoomName.Contains(csd.ChatroomName))
                                                    .Count(out var total)
                                                    .ToListAsync(t => new CustomerChatRoomVo
                                                    {
                                                        ChatRoomId = t.t1.Id,
                                                        TargetId = t.t1.EaseChatRoomId,
                                                        ChatRoomImg = t.t1.ChatRoomImg,
                                                        ChatRoomName = t.t1.ChatRoomName,
                                                        ChatRoomNotice = t.t1.ChatRoomNotice,
                                                        IsActive = t.t1.IsActive,
                                                        IsProhibit = t.t1.IsProhibit,
                                                        Level = t.t1.Level,
                                                        ManagerId = t.t1.ManagerId,
                                                        UserNum = t.t1.UserNum,
                                                        EaseChatRoomId = t.t1.EaseChatRoomId,
                                                        CzType = (int)CzTypeEnum.ChatRoom
                                                    });
            return await this.Repository.AsPageViewModelAsync(result, csd.Page, csd.Size, total);
        }

        // 获取好友列表
        /// <summary>
        /// 获取所有的联系人列表
        /// </summary>
        /// <param name="userId">后台用户id</param>
        /// <param name="page">当前页</param>
        /// <param name="size">页面大小</param>
        /// <returns></returns>
        public async Task<PageViewModel> ContactsListAsync(string userId, int page, int size)
        {
            var memberInfo = await this.Repository.ContactsListAsync(userId, page, size);

            List<ChatContactsVo> contactsVos = new();
            if (memberInfo.Item2?.Count > 0)
            {
                memberInfo.Item2.ForEach(m =>
                {
                    contactsVos.Add(new ChatContactsVo()
                    {
                        NickName = string.IsNullOrWhiteSpace(m.NickName) ? $"临时会员_{m.BsvNumber}" : m.NickName,
                        Avatar = m.Avatar,
                        TargetId = m.Id,
                        CzimId = m.Id,
                        CzType = (int)CzTypeEnum.SingleChat,
                    });
                });
            }

            return await this.Repository.AsPageViewModelAsync(contactsVos.DistinctBy(d => d.TargetId).ToList(), page, size, memberInfo.Item1);
        }

        /// <summary>
        /// 预设聊天信息列表
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="crsd">预设消息模糊字段</param>
        /// <returns></returns>
        public async Task<List<string>> PresetListAsync(string userId, ChatPresetSearchDto crsd)
        {
            var result = await this.Repository.Orm.Select<CzimChatPreset>()
                                    .WhereIf(!string.IsNullOrWhiteSpace(crsd?.Msg), w => w.Context.Contains(crsd.Msg) ||
                                                                                     w.Abbre.Contains(crsd.Msg) ||
                                                                                     w.Vague.Contains(crsd.Msg))
                                    .Where(w => w.IsActive)
                                    .ToListAsync(t => t.Context);

            return result;
        }

        /// <summary>
        /// 管理员添加好友
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="crsd">目标id</param>
        /// <returns></returns>
        public async Task<bool> AddFriendListAsync(string userId, AddFriendDto crsd)
        {
            // 判断是否好友关系
            var _hasF = await this.Repository.Orm.Select<CzimChatFriendList>()
                                        .Where(w => (w.UserId == userId && w.MemberId == crsd.TargetId) ||
                                                    w.UserId == crsd.TargetId && w.MemberId == userId)
                                        .Where(w => w.IsActive)
                                        .AnyAsync();

            if (_hasF)
            {
                return true;
            }

            // 添加好友关系
            CzimChatFriendList czimChatFriendList = new CzimChatFriendList()
            {
                CDate = DateTime.Now,
                UDate = DateTime.Now,
                CrtDate = DateTime.Now,
                IsActive = true,
                MemberId = crsd.TargetId,
                UserId = userId
            };

            var result = await this.Repository.Orm.Insert<CzimChatFriendList>(czimChatFriendList).ExecuteAffrowsAsync();

            return result > 0;
        }

        /// <summary>
        /// 获取客服所在的所有群信息
        /// </summary>
        /// <param name="userId">管理员id</param>
        /// <param name="csd">聊天室内容</param>
        /// <returns></returns>
        public async Task<PageViewModel> GetChatGroupListAsync(string userId, ChatgroupSearchDto csd)
        {
            //var result = await this.Repository.Orm.Select<CzimChatgroupInfo, CzimChatgroupMember>()
            //                                        .LeftJoin(l => l.t1.Id == l.t2.ChatgroupId)
            //                                        .Where(w => (w.t2.MemberId == userId && w.t2.IsManager) || w.t1.OwnId == userId)
            //                                        .Where(w => w.t2.IsActive && w.t1.IsActive)
            //                                        .WhereIf(!string.IsNullOrWhiteSpace(csd.ChatgroupName), w => w.t1.GroupName.Contains(csd.ChatgroupName))
            //                                        .Distinct().ToList(t=>t.t1.Id)
            //                                        .Count(out var total)
            //                                        .ToListAsync(t => new CustomerChatgroupVo
            //                                        {
            //                                            AllowInvites = t.t1.AllowInvites,
            //                                            Announcement = t.t1.Announcement,
            //                                            CrtDate = t.t1.CDate,
            //                                            EaseChatgroupId = t.t1.EaseChatgroupId,
            //                                            Description = t.t1.Description,
            //                                            GroupImg = t.t1.GroupImg,
            //                                            GroupName = t.t1.GroupName,
            //                                            Id = t.t1.Id,
            //                                            IsProhibit = t.t1.IsProhibit,
            //                                            OwnId = t.t1.OwnId,
            //                                            UserNum = t.t1.UserNum,
            //                                            TargetId = t.t1.EaseChatgroupId,
            //                                            CzType = (int)CzTypeEnum.GroupChat,
            //                                        });

            var result = await this.Repository.GetChatGroupListAsync(userId, csd.Page, csd.Size);

            var total = result.Item1;

            List<CustomerChatgroupVo> ccv = new List<CustomerChatgroupVo>();

            if (result.Item2?.Count > 0)
            {
                foreach (var item in result.Item2)
                {
                    if (item != null)
                    {
                        ccv.Add(
                                new CustomerChatgroupVo
                                {
                                    AllowInvites = item.AllowInvites,
                                    Announcement = item.Announcement,
                                    CrtDate = item.CDate,
                                    EaseChatgroupId = item.EaseChatgroupId,
                                    Description = item.Description,
                                    GroupImg = item.GroupImg,
                                    GroupName = item.GroupName,
                                    Id = item.Id,
                                    IsProhibit = item.IsProhibit,
                                    OwnId = item.OwnId,
                                    UserNum = item.UserNum,
                                    TargetId = item.EaseChatgroupId,
                                    CzType = (int)CzTypeEnum.GroupChat,
                                });
                    }
                }
            }
            return await this.Repository.AsPageViewModelAsync(ccv, csd.Page, csd.Size, total);
        }

        /// <summary>
        /// 查询所有群的聊天的背景和说明
        /// </summary>
        /// <param name="userId">当前用户id</param>
        /// <param name="targetIds">目标id</param>
        /// <returns></returns>
        public async Task<List<ChatContactsVo>> GetChatGroupExtAsync(string userId, List<string> targetIds)
        {
            // 所有群信息
            var chatgroupnfo = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                            .Where(w => targetIds.Contains(w.EaseChatgroupId))
                                            .ToListAsync(t => new ChatContactsVo
                                            {
                                                TargetId = t.EaseChatgroupId,
                                                NickName = t.GroupName,
                                                Avatar = t.GroupImg,
                                                CzimId = t.Id,
                                                CzType = (int)CzTypeEnum.GroupChat,
                                            });
            return chatgroupnfo.Distinct().ToList();
        }

        /// <summary>
        /// 查询当前群所有成员信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="sesd">群组信息</param>
        /// <returns></returns>
        public async Task<List<ChatContactsVo>> GetGroupMemberAsync(string userId, ChatgroupSearchDto sesd)
        {
            var groupMember = await this.Repository.Orm.Select<CzimChatgroupMember, CzimMemberInfo, CzimChatCustomer>()
                                            .LeftJoin(l => l.t1.MemberId == l.t2.Id && l.t2.IsActive)
                                            .LeftJoin(l => l.t1.MemberId == l.t3.UserId && l.t3.IsActive)
                                            .Where(w => sesd.ChatgroupId.Equals(w.t1.ChatgroupId) || w.t1.EaseChatgroupId == sesd.ChatgroupId)
                                            .Where(w => w.t1.IsActive)
                                            .Page(sesd.Page, 10)
                                            .ToListAsync(t => new ChatContactsVo
                                            {
                                                TargetId = t.t2 == null ? t.t3.UserId : t.t2.Id,
                                                NickName = t.t2 == null ? t.t3.NickName : t.t2.NickName,
                                                Avatar = t.t2 == null ? t.t3.Avatar : t.t2.Avatar,
                                                CzType = 0,
                                                CzimId = t.t2 == null ? t.t3.UserId : t.t2.Id,
                                            });

            return groupMember.DistinctBy(d => d.TargetId).ToList();
        }

        //
    }
}