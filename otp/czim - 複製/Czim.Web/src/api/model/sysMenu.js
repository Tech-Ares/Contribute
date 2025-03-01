import config from "@/config"
import http from "@/utils/request"

export default {
	myMenus: {
		url: `${config.API_URL}/sysuser/menu`,
		name: "获取用户菜单",
		get: async function () {
			return await http.post(this.url);
		}
	},
	menuList: {
		url: `${config.API_URL}/sysmenu/list`,
		name: "获取菜单",
		get: async function () {
			return await http.get(this.url);
		}
	},
	menuDelete: {
		url: `${config.API_URL}/sysmenu/deletelist`,
		name: "删除菜单",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	menuSave: {
		url: `${config.API_URL}/sysmenu/save`,
		name: "新增菜单",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},

	// ------------------- scui遗留 -----------------------
	dic: {
		tree: {
			url: `${config.API_URL}/system/dic/tree`,
			name: "获取字典树",
			get: async function(){
				return await http.get(this.url);
			}
		},
		list: {
			url: `${config.API_URL}/system/dic/list`,
			name: "字典明细",
			get: async function(params){
				return await http.get(this.url, params);
			}
		},
		get: {
			url: `${config.API_URL}/system/dic/get`,
			name: "获取字典数据",
			get: async function(params){
				return await http.get(this.url, params);
			}
		}
	},
	role: {
		list: {
			url: `${config.API_URL}/system/role/list`,
			name: "获取角色列表",
			get: async function(params){
				return await http.get(this.url, params);
			}
		}
	},
	user: {
		list: {
			url: `${config.API_URL}/system/user/list`,
			name: "获取用户列表",
			get: async function(params){
				return await http.get(this.url, params);
			}
		}
	},
	app: {
		list: {
			url: `${config.API_URL}/system/app/list`,
			name: "应用列表",
			get: async function(){
				return await http.get(this.url);
			}
		}
	},
	log: {
		list: {
			url: `${config.API_URL}/system/log/list`,
			name: "日志列表",
			get: async function(params){
				return await http.get(this.url, params);
			}
		}
	},
	table: {
		list: {
			url: `${config.API_URL}/system/table/list`,
			name: "表格列管理列表",
			get: async function(params){
				return await http.get(this.url, params);
			}
		},
		info: {
			url: `${config.API_URL}/system/table/info`,
			name: "表格列管理详情",
			get: async function(params){
				return await http.get(this.url, params);
			}
		}
	}
}
