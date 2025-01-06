using KUX.Infrastructure;
using KUX.Models.ApiResultManage;
using KUX.Models.DTO;
using KUX.Models.Entities.Framework;
using KUX.Models.KuxAdmin.Request.SysUser;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.Framework;
using KUX.Services.Admin.ServicesAdmin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUX.Services.Admin.Framework;

/// <summary>
/// 系统账号服务
/// </summary>
public class SysUserService : AdminBaseService<SysUserRepository>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository">用户仓储</param>
    public SysUserService(SysUserRepository repository) : base(repository)
    {
    }

    /// <summary>
    /// 获取列表数据
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<PageViewModel> FindListAsync(UserSearchDto search)
    {
        var query = await this.Repository.Orm.Select<SysUser, SysOrganization, SysUserRole>()
                                                        .LeftJoin(l => l.t1.OrganizationId == l.t2.Id)
                                                        .LeftJoin(l => l.t1.Id == l.t3.UserId)
                                                     .WhereIf(!string.IsNullOrWhiteSpace(search.OrganizationId), w => w.t1.OrganizationId == search.OrganizationId)
                                                     .WhereIf(!string.IsNullOrWhiteSpace(search?.Name), w => w.t1.Name.Contains(search.Name))
                                                     .WhereIf(!string.IsNullOrWhiteSpace(search?.LoginName), w => w.t1.LoginName.Contains(search.LoginName))
                                                     .OrderByDescending(w => w.t1.CDate)
                                                     .Page(search.Page, search.Size)
                                                     .Count(out var total)
                                                     .ToListAsync(w => new
                                                     {
                                                         w.t1.Id,
                                                         w.t1.Name,
                                                         w.t1.LoginName,
                                                         Roles = string.Join(",", this.Repository.Orm.Select<SysRole>()
                                                                                            .Where(n => n.Id == w.t3.RoleId)
                                                                                            .ToList(t => t.Name)),
                                                         OrganizationName = w.t2.Name,
                                                         w.t1.Phone,
                                                         w.t1.Email,
                                                         UDate = w.t1.UDate.ToString("yyyy-MM-dd"),
                                                         CDate = w.t1.CDate.ToString("yyyy-MM-dd")
                                                     });

        return await this.Repository.AsPageViewModelAsync(query, search.Page, search.Size, total);
    }

    /// <summary>
    /// 根据id数组删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task DeleteListAsync(List<string> ids)
    {
        var mbshow = "";
        foreach (var item in ids)
        {
            var userModel = await this.Repository.FindAsync(item);
            if (userModel.IsDelete == 2)
            {
                mbshow += $"{userModel.Name}、";
                continue;
            }
            // 删除角色
            await this.Repository.Orm.Delete<SysUserRole>().Where(w => w.UserId == item).ExecuteAffrowsAsync();

            // 删除岗位
            await this.Repository.Orm.Delete<SysUserPost>().Where(w => w.UserId == item).ExecuteAffrowsAsync();

            // 用户删除
            await this.Repository.DeleteAsync(userModel);
        }

        if (!string.IsNullOrWhiteSpace(mbshow))
        {
            MessageBox.Show($"{mbshow} 等用户不能删除！");
        }
    }

    /// <summary>
    /// 查询表单数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, object>> FindFormAsync(string id)
    {
        var res = new Dictionary<string, object>();

        var userForm = await this.Repository.FindAsync(id);

        var form = (await this.Repository.FindAsync(id)).NullSafe();
        //角色信息
        var roleIds = await this.Repository.Orm.Select<SysUserRole>()
                                             .Where(w => w.UserId == id)
                                             .ToListAsync(t => new
                                             {
                                                 t.RoleId
                                             });
        // 所有角色信息
        var allRoleList = await this.Repository.Orm.Select<SysRole>()
                                            .Where(w => w.IsActive)
                                            .ToListAsync(t => new
                                            {
                                                label = t.Name,
                                                value = t.Id
                                            });

        //岗位信息
        var postIds = await this.Repository.Orm.Select<SysUserPost>()
                                .Where(w => w.UserId == id)
                                .ToListAsync(t => new
                                {
                                    t.PostId,
                                });
        var allPostList = await this.Repository.Orm.Select<SysPost>()
                                        .Where(w => w.IsActive)
                                        .OrderBy(w => w.Number)
                                        .ToListAsync(t => new
                                        {
                                            label = t.Name,
                                            value = t.Id
                                        });

        res[nameof(id)] = string.IsNullOrWhiteSpace(id) ? "" : id;
        res[nameof(form)] = form;
        res[nameof(roleIds)] = roleIds;
        res[nameof(allRoleList)] = allRoleList;
        // 当前用户岗位信息
        res[nameof(postIds)] = postIds;
        // 所有岗位列表
        res[nameof(allPostList)] = allPostList;
        return res;
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="userId">管理员id</param>
    /// <param name="form">用户表单</param>
    /// <returns></returns>
    public async Task<bool> SaveFormAsync(string userId, SysUserFormDto form)
    {
        var model = form.Form;

        if (string.IsNullOrWhiteSpace(model.Id))
        {
            model.Id = Guid.NewGuid().ToString("N");
            model.IsActive = true;
            model.CDate = DateTime.Now;
            model.UDate = DateTime.Now;
        }

        if (string.IsNullOrWhiteSpace(model.Password))
        {
            MessageBox.Show("密码不能为空！");
        }

        if (string.IsNullOrWhiteSpace(model.Id))
        {
            // 默认密码
            model.Password = string.IsNullOrWhiteSpace(model.Password) ? "admin@888".Md5Encrypt() : model.Password.Md5Encrypt();
        }

        if (await this.Repository.Select.AnyAsync(w => w.LoginName == model.LoginName && w.Id != model.Id))
        {
            MessageBox.Show("登录账号名称已存在!");
        }

        // 新增或保存用户信息
        model = await this.Repository.InsertOrUpdateAsync(model);

        if (string.IsNullOrWhiteSpace(model?.Id))
        {
            MessageBox.Show("账户保存异常！");
        }

        //变更用户角色
        if (form.RoleIds.Count > 0)
        {
            await this.Repository.Orm.Delete<SysUserRole>().Where(w => w.UserId == model.Id).ExecuteAffrowsAsync();//.DeleteAsync(w => w.UserId == model.Id);

            // 用户角色列表
            List<SysUserRole> sysUserRoles = new List<SysUserRole>();
            foreach (var item in form.RoleIds)
            {
                var sysUserRole = new SysUserRole()
                {
                    RoleId = item,
                    UserId = model.Id,
                    CDate = DateTime.Now,
                    UDate = DateTime.Now,
                    CUser = userId,
                    UUser = userId
                };
                sysUserRoles.Add(sysUserRole);
            }

            if (sysUserRoles?.Count > 0)
            {
                // 保存用户权限
                await this.Repository.Orm.Insert<SysUserRole>(sysUserRoles).ExecuteAffrowsAsync();
            }
        }

        //处理岗位信息
        if (form.PostIds.Count > 0)
        {
            // 所有岗位信息
            var sysUserPosts = new List<SysUserPost>();

            await this.Repository.Orm.Delete<SysUserPost>().Where(w => w.UserId == model.Id).ExecuteAffrowsAsync();//.DeleteAsync(w => w.UserId == model.Id);
            foreach (var item in form.PostIds)
            {
                sysUserPosts.Add(new SysUserPost()
                {
                    PostId = item,
                    UserId = model.Id,
                    CDate = DateTime.Now,
                    UDate = DateTime.Now,
                    CUser = userId,
                    UUser = userId
                });

                //var sysUserPost = sysUserPosts.FirstOrDefault(w => w.PostId == item).NullSafe();

                //sysUserPost.Id = Guid.NewGuid().ToString("N");
                //sysUserPost.PostId = item;
                //sysUserPost.UserId = model.Id;
            }

            if (sysUserPosts?.Count > 0)
            {
                await this.Repository.Orm.Insert<SysUserPost>(sysUserPosts).ExecuteAffrowsAsync();//.InsertAsync(sysUserPost);
            }
        }

        return true;
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<byte[]> ExportExcelAsync(UserSearchDto search)
    {
        search.Page = 1;
        search.Size = int.MaxValue - 1;

        var tableViewModel = await this.FindListAsync(search);
        return this.ExportExcelByPageViewModel(tableViewModel, null, "Id");
    }

    /// <summary>
    /// 对 部门树 加工树结构
    /// </summary>
    /// <param name="tree"></param>
    /// <returns></returns>
    public async Task<List<Dictionary<string, object>>> GetSysDepartmentTreeAsync(IEnumerable<SysOrganization> tree)
    {
        var res = new List<Dictionary<string, object>>();

        foreach (var item in tree)
        {
            res.Add(new Dictionary<string, object>()
            {
                ["key"] = item.Id,
                ["title"] = item.Name,
                ["children"] = item.Children.Count > 0 ? await this.GetSysDepartmentTreeAsync(item.Children) : null
            });
        }

        return res;
    }
}