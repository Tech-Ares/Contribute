using KUX.Models.Consts;
using KUX.Models.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KUX.Models.CzimApp.AppBo;

/// <summary>
/// 会员缓存信息
/// </summary>
public class MemberCacheBo
{
    /// <summary>
    /// 会员Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 帐号
    /// </summary>
    public string LoginName { get; set; }

    /// <summary>
    /// 设备id
    /// </summary>
    public string Meid { get; set; }

    /// <summary>
    /// 会员编号
    /// </summary>
    public string BsvNumber { get; set; }

    /// <summary>
    /// 用户昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Mailbox { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public GenderEnum Gender { get; set; }

    /// <summary>
    /// 推荐人编号
    /// </summary>
    public string RNumber { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string Introduce { get; set; }

    /// <summary>
    /// 注册时间
    /// </summary>
    public DateTime RegDate { get; set; }

    /// <summary>
    /// 是否注册会员
    /// </summary>
    public bool IsMember { get; set; }

    /// <summary>
    /// 专属客服ID
    /// </summary>
    public string ExclusiveUserId { get; set; }

    // capital

    /// <summary>
    /// 最后一次登录时间
    /// </summary>
    public DateTime LastLoginDate { get; set; }

    /// <summary>
    /// 资金
    /// </summary>
    public decimal Capital { get; set; }

    /// <summary>
    /// 锁定资金
    /// </summary>
    public decimal CapitalLocking { get; set; }

    /// <summary>
    /// 会员到期时间
    /// </summary>
    public DateTime FrozenTime { get; set; }

    /// <summary>
    /// 剩余观看次数
    /// </summary>
    public int FreeTimesPerDay { get; set; }

    /// <summary>
    /// 是否推广员
    /// </summary>
    public bool IsAgent { get; set; }

    /// <summary>
    /// 当前登录地址
    /// </summary>
    public string LoginCity { get; set; }

    /// <summary>
    /// 登录坐标（纬度）
    /// </summary>
    public decimal LoginLat { get; set; }

    /// <summary>
    /// 登录坐标（经度）
    /// </summary>
    public decimal LoginLong { get; set; }

    /// <summary>
    /// 会员加入的聊天室列表
    /// </summary>
    public List<MemberChatRoomBo> MemberChatRooms { get; set; }

    /// <summary>
    /// 环信用户密码
    /// </summary>
    public string EasePwd
    {
        get
        {
            return Md5Encrypt($"{Id}{CzimConsts.EaseSalt}");
        }
    }

    /// <summary>
    /// MD5加密（当前类使用，不引用基本库）
    /// </summary>
    /// <param name="str">要加密的字符串</param>
    /// <returns></returns>
    private string Md5Encrypt(string str)
    {
        using var md5 = MD5.Create();
        var data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
        var builder = new StringBuilder();
        // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串
        for (int i = 0; i < data.Length; i++)
        {
            builder.Append(data[i].ToString("X2"));
        }
        return builder.ToString();
    }
}