using KUX.Models.CzimAdmin.Request.CzimChatPreset;
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
    /// 预设聊天消息服务
    /// </summary>
    public class CzimChatPresetService : AdminBaseService<CzimChatPresetRepository>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="repository">仓储</param>
        public CzimChatPresetService(CzimChatPresetRepository repository) : base(repository)
        {

        }
        /// <summary>
        /// 保存/修改
        /// </summary>
        /// <returns></returns>
        public async Task<(bool,string)> SaveFormAsync(string userid,CzimChatPresetAddDto dto)
        {
            var dateNow = DateTime.Now;
            CzimChatPreset entity = new ()
            {
                Context = dto.Context,
                Abbre = dto.Abbre,
                Vague = dto.Vague,
                IsActive = true,
                CUser = userid,
                UUser = userid,
                CDate = dateNow,
                UDate = dateNow
            };
            if (string.IsNullOrEmpty(dto.Id))
            {
                entity.Id = Guid.NewGuid().ToString("N");
                var res = await this.Repository.Orm.Insert(entity).ExecuteAffrowsAsync();
                if (res>0)
                {
                    return (true,"新增成功");
                }
                else
                {
                    return (true, "新增失败");
                }
            }
            else
            {
                var res = await this.Repository.Orm.Update<CzimChatPreset>()
                    .Set(s=>s.Context,dto.Context)
                    .Set(s=>s.Abbre,dto.Abbre)
                    .Set(s=>s.Vague,dto.Vague)
                    .Where(s=>s.Id==dto.Id).ExecuteAffrowsAsync();
                if (res > 0)
                {
                    return (true, "修改成功");
                }
                else
                {
                    return (true, "修改失败");
                }
            }

        }
        /// <summary>
        /// 激活/失效
        /// </summary>
        /// <param name="userId">管理员id</param>
        /// <param name="gad">商品信息</param>
        /// <returns></returns>
        public async Task<(bool, string)> IsActiveFormAsync(string userId, CzimChatPresetActiveDto gad)
        {
            var result = await this.Repository.Orm.Update<CzimChatPreset>()
                                    .Set(s => s.IsActive, gad.IsActive)
                                    .Set(s => s.UUser, userId)
                                    .Where(w => w.Id == gad.Id)
                                    .ExecuteAffrowsAsync();

            if (result > 0)
            {
                return (true, gad.IsActive ? "激活成功" : "失效成功");
            }

            return (false, "操作失败，请选择商品重新处理");
        }
        /// <summary>
        /// 获取所有聊天预设信息
        /// </summary>
        /// <param name="gsd"></param>
        /// <returns></returns>
        public async Task<PageViewModel> FindListAsync(PresetSearchDto gsd)
        {
            var query = await this.Repository.Orm.Select<CzimChatPreset>()
                .WhereIf(!string.IsNullOrEmpty(gsd.Context), s => s.Context.Contains(gsd.Context))
                .Count(out var total)
                .Page(gsd.Page, gsd.Size)
                .ToListAsync(s=>new
                {
                    s.Context,
                    s.Abbre,
                    s.Vague,
                    s.Id,
                    s.IsActive
                });
            return await this.Repository.AsPageViewModelAsync(query, gsd.Page, gsd.Size, total);
        }
    }
}
