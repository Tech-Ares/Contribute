using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimBaseSetting
{
    /// <summary>
    /// 基础对象
    /// </summary>
    public class CzimBaseSettingAddDto
    {
        /// <summary>
        /// 表单id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 配置值
        /// </summary>
        public string TypeValue { get; set; }
    }
}
