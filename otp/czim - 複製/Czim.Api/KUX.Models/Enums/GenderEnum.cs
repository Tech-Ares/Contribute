using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.Enums
{
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum GenderEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = 0,

        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Man = 1,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        WoMen = 2,

        /// <summary>
        /// 未说明
        /// </summary>
        [Description("未说明")]
        None = 9
    }
}
