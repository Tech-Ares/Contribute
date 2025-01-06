import config from "@/config"
import http from "@/utils/request"

export default {
    findList: {
        url: `${config.API_URL}/czimchatcustomer/findcustomerlist`,
        name: "分页获取客服列表",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findCustomer: {
        url: `${config.API_URL}/czimchatcustomer/findcustomeruser`,
        name: "获取所有可成为客服的账号",
        get: async function () {
            return await http.get(this.url);
        }
    },
    saveForm: {
        url: `${config.API_URL}/czimchatcustomer/savecustomer`,
        name: "保存客服信息",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    addAgent: {
        url: `${config.API_URL}/czimchatcustomer/addagent`,
        name: "新增推广员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    deleteActive: {
        url: `${config.API_URL}/czimchatcustomer/customeractive`,
        name: "激活/失效",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    getChat: {
        url: `${config.API_URL}/czimchatcustomer/getchatcustomeroffriend`,
        name: "获取客服好友列表",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    upInfo: {
        url: `${config.API_URL}/czimchatcustomer/updateuserinfo`,
        name: "修改用户信息",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    }


}