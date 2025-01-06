import config from "@/config"
import http from "@/utils/request"

export default {
  findList: {
		url: `${config.API_URL}/sysuser/findlist`,
		name: "分页获取用户列表",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	getUserById: {
		url: `${config.API_URL}/sysuser/findform`,
		name: "根据 id 获取用户信息",
		get: async function (id) {
			return await http.get(`${this.url}/${id}`);
		}
	},
	saveForm: {
		url: `${config.API_URL}/sysuser/saveform`,
		name: "保存用户信息",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	deleteUser: {
		url: `${config.API_URL}/sysuser/deletelist`,
		name: "删除角色",
		post: async function(data){
			return await http.post(this.url, data);
		}
	}
}