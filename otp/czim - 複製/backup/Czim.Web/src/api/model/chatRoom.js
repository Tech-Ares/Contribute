import config from "@/config"
import http from "@/utils/request"

export default {
    findList: {
        url: `${config.API_URL}/czimchatroom/findchatroomlist`,
        name: "分页获取所有聊天室信息",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findForm: {
        url: `${config.API_URL}/czimchatroom/findform`,
        name: "根据 id 获取用户信息",
        post: async function (id) {
            return await http.post(`${this.url}/${id}`);
        }
    },
    saveForm: {
        url: `${config.API_URL}/czimchatroom/saveform`,
        name: "保存聊天室信息",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    getManager: {
        url: `${config.API_URL}/czimchatroom/getallmanager`,
        name: "获取所有管理员信息",
        get: async function () {
            return await http.get(this.url);
        }
    },
    findMember: {
        url: `${config.API_URL}/czimchatroom/getmemberofchatroomlist`,
        name: "分页获取当前聊天室的所有成员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findManager: {
        url: `${config.API_URL}/czimchatroom/findmanagerlist`,
        name: "分页获取所有管理员列表",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    addManager: {
        url: `${config.API_URL}/czimchatroom/addordelmanager`,
        name: "聊天室添加/移除管理员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    isActive: {
        url: `${config.API_URL}/czimchatroom/isactiveform`,
        name: "失效",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    doDefault: {
        url: `${config.API_URL}/czimchatroom/dodefault`,
        name: "设置默认聊天室",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findAdd: {
        url: `${config.API_URL}/czimchatroom/findchatroomabbre`,
        name: "获取可加入的聊天室",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findisMember: {
        url: `${config.API_URL}/czimchatroom/chatroomaddmember`,
        name: "获取可加入聊天室的会员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },

}