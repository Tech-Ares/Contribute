using KUX.Models.ApiResultManage;
using KUX.Models.CzimAdmin.Request.CzimLiveInfo;
using KUX.Models.CzimSection;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.CzimSection;
using KUX.Services.Admin.ServicesAdmin;
using System;
using System.Threading.Tasks;

namespace KUX.Services.Admin.CzmiSection
{
    /// <summary>
    /// 直播服务
    /// </summary>
    public class CzimLiveInfoService : AdminBaseService<CzimLiveInfoReposity>
    {
        //问题:修改时为什么不能直接填充对象
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="repository">仓储</param>
        public CzimLiveInfoService(CzimLiveInfoReposity repository) : base(repository)
        {
        }

        /// <summary>
        /// 增加/修改直播
        /// </summary>
        /// <param name="userId">web后台管理id</param>
        /// <param name="dto">直播对象</param>
        /// <returns>成功/失败</returns>
        public async Task<(bool, string)> SaveOrUpdateLiveInfoAsync(string userId, CzimLiveInfoSaveDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                MessageBox.Show("直播标题是必填项");
            }
            var entity = new CzimLiveInfo()
            {
                Title=dto.Title,
                LiveDate = dto.LiveDate,
                EndDate = dto.EndDate,
                VideoUrl = dto.VideoUrl,
                IsActive = true,
                CrtDate=DateTime.Now,//直播间的创建时间 
                CDate = DateTime.Now,
                IsExamine=1,//审核状态 目前是写死的 要写成活的得将这个去掉 还有下面的时间 
                ExamineDate=DateTime.Now,//审核时间 写死 
                CUser = userId,
                CrtMemberId=userId //创作人id
            };
            //如果id为空 则新增
            if (string.IsNullOrWhiteSpace(dto.Id))
            {
                var isAdd = await Repository.Orm.Insert(entity).ExecuteAffrowsAsync();
                if (isAdd > 0)
                {
                    return (true, "添加成功");
                }
                return (false, "添加失败！");
            }
            //修改
            else
            {
                if (string.IsNullOrWhiteSpace(dto.Id))
                {
                    MessageBox.Show("id不能为空");
                }
                entity.Id = dto.Id;
                entity.UDate = DateTime.Now;
                entity.UUser = userId;
                
                var isUpdate = await Repository.Orm.Update<CzimLiveInfo>()
                                               .Where(s=>s.Id==dto.Id)
                                               .Set(s=>s.Title,dto.Title)
                                               .Set(s=>s.LiveDate,dto.LiveDate)
                                               .Set(s=>s.EndDate,dto.EndDate)
                                               .Set(s=>s.VideoUrl,dto.VideoUrl)
                                               .ExecuteAffrowsAsync();
                if (isUpdate > 0)
                {
                    return (true, "修改成功！");
                }
                return (false, "修改失败");
            }
        }

        //启动禁用
        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="userId">web账户id</param>
        /// <param name="dto">启用/禁用对象</param>
        /// <returns></returns>
        public async Task<(bool, string)> IsActiveAsync(string userId, LiveInfoIsActiveDto dto)
        {
            var IsActive = await Repository.Orm.Update<CzimLiveInfo>()
                                        .Set(s => s.IsActive, dto.IsActive)
                                        .Set(s => s.UDate, DateTime.Now)
                                        .Set(s => s.UUser, userId)
                                        .Where(s => s.Id == dto.Id)
                                        .ExecuteAffrowsAsync();
            if (IsActive > 0)
            {
                return dto.IsActive ? (true, "启用成功") : (true, "禁用成功");
            }
            return (false, "请重新选择一条数据！");
        }

        /// <summary>
        /// 查询所有直播列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageViewModel> GetLiveInfoListAsync(LiveInfoSearchDto dto)
        {
            var result = await Repository.Orm.Select<CzimLiveInfo>()
                                             .WhereIf(!string.IsNullOrWhiteSpace(dto.Title), s => s.Title == dto.Title)
                                             .WhereIf(dto.LiveDate != DateTime.MinValue, s => s.LiveDate >= dto.LiveDate)
                                             .WhereIf(dto.EndDate != DateTime.MinValue, s => s.EndDate <= dto.EndDate)
                                             .Page(dto.Page, dto.Size)
                                             .Count(out var total)
                                             .ToListAsync(s => new
                                             {
                                                 s.Title,
                                                 s.Id,
                                                 s.LiveDate,
                                                 s.EndDate,
                                                 s.VideoUrl,
                                                 s.Content,
                                                 s.HeadPicture,
                                                 s.PushFlowUrl,
                                                 s.PlayUrl,
                                                 s.IsActive
                                             });
            var pageResult = await Repository.AsPageViewModelAsync(result, dto.Page, dto.Size, total);
            return pageResult;
        }
    }
}