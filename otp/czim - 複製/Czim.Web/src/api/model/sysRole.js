import config from "@/config"
import http from "@/utils/request"

export default {
	getRoleById: {
		url: `${config.API_URL}/sysrole/findform`,
		name: "根据 id 获取角色信息",
		get: async function (id) {
			return await http.get(`${this.url}/${id}`);
		}
	},
	getSysRole: {
		url: `${config.API_URL}/sysrole/findlist`,
		name: "分页获取系统角色列表",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	createOrEditRole: {
		url: `${config.API_URL}/sysrole/saveform`,
		name: "新增角色",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	deleteRole: {
		url: `${config.API_URL}/sysrole/deletelist`,
		name: "删除角色",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	getRoleMenuById: {
		url: `${config.API_URL}/sysrolemenufunction/getrolemenufunctiontree`,
		name: "根据 id 获取菜单功能树",
		get: async function (id) {
			return await http.get(`${this.url}/${id}`);
		}
	},
	updateRoleMenu: {
		url: `${config.API_URL}/sysrolemenufunction/saveform`,
		name: "更新选中角色菜单权限",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	// ---------- SysRoleMenuFunction -----------
	// getSysRole: {
	// 	url: `${config.API_URL}/sysrole/deletelist`,
	// 	name: "获取角色列表",
	// 	post: async function(data){
	// 		return await http.post(this.url, data);
	// 	}
	// },
}
