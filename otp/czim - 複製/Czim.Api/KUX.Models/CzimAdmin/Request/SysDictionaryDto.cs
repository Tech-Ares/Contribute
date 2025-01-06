using KUX.Models.Entities.Framework;
using System.Collections.Generic;

namespace KUX.Models.DTO;

/// <summary>
/// 系统字典Dto
/// </summary>
public class SysDictionaryDto : SysDictionary
{
    /// <summary>
    /// 主键
    /// </summary>
    public string Key
    {
        get
        {
            return Id;
        }
    }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title
    {
        get
        {
            return Name;
        }
    }

    /// <summary>
    /// 子列表
    /// </summary>
    public List<SysDictionaryDto> Children { get; set; }
}