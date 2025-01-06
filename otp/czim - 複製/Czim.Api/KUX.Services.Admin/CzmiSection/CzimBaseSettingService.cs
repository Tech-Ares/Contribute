using KUX.Models.ApiResultManage;
using KUX.Models.CzimAdmin.Request.CzimBaseSetting;
using KUX.Models.CzimSection;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.CzimSection;
using KUX.Services.Admin.ServicesAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Services.Admin.CzmiSection
{
    /// <summary>
    /// 基础设置服务
    /// </summary>
    public class CzimBaseSettingService : AdminBaseService<CzimBaseSettingRepository>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="repository">仓储</param>
        public CzimBaseSettingService(CzimBaseSettingRepository repository) : base(repository)
        {

        }
        //修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto">保存/修改对象</param>
        /// <returns></returns>
        public async Task<(bool,string)> SaveOrUpdateAsync(CzimBaseSettingAddDto dto)
        {
            //表单id为空
            if (string.IsNullOrWhiteSpace(dto.Id))
            {
                MessageBox.Show("id不能为空");
            }

            var result = await Repository.Orm.Update<CzimBaseSetting>()
                                           .Where(s => s.Id == dto.Id)
                                           .Set(s => s.TypeValue, dto.TypeValue)
                                           .ExecuteAffrowsAsync();
            if (result>0)
            {
                return (true,"修改成功！");
            }
            return (false,"修改失败！");
        }
        //查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageViewModel> FindListAsync(CzimBaseSettingSearchDto dto)
        {
            var result =await Repository.Orm.Select<CzimBaseSetting>()
                                 .Page(dto.Page, dto.Size)
                                 .Count(out var total)
                                 .ToListAsync(
                s=>new
                {
                    s.TypeName,
                    s.TypeCode,
                    s.TypeValue,
                    s.Id,
                    s.IsActive
                }
                );
            return await Repository.AsPageViewModelAsync(result,dto.Page,dto.Size,total);
        }

    }
}
