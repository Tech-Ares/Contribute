using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.ApiResultManage
{
    /// <summary>
    /// 消息返回码
    /// </summary>
    public enum ApiResultCodeEnum
    {
        /// <summary>
        /// 接口不存在
        /// </summary>
        NotFount = -4,

        /// <summary>
        /// 程序错误
        /// </summary>
        Error = -3,

        /// <summary>
        /// 未授权
        /// </summary>
        UnAuth = -2,

        /// <summary>
        /// 警告
        /// </summary>
        Warn = -1,

        /// <summary>
        /// 成功
        /// </summary>
        Ok = 0,
    }
}
