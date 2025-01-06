import config from "@/config"
import http from "@/utils/request"

export default {
    findList: {
        url: `${config.API_URL}/czimchatgroup/findchatgrouplist`,
        name: "分页获取所有群聊信息",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findForm: {
        url: `${config.API_URL}/czimchatgroup/findchatgroup`,
        name: "根据 id 获取群信息",
        post: async function (id) {
            return await http.post(`${this.url}/${id}`);
        }
    },
    createChat: {
        url: `${config.API_URL}/czimchatgroup/createchatgroup`,
        name: "创建群聊",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    addManager: {
        url: `${config.API_URL}/czimchatgroup/groupaddmanager`,
        name: "添加管理员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    delManager: {
        url: `${config.API_URL}/czimchatgroup/groupdeletemanager`,
        name: "移除管理员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    addMember: {
        url: `${config.API_URL}/czimchatgroup/groupaddmember`,
        name: "添加单个群组成员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    delMember: {
        url: `${config.API_URL}/czimchatgroup/groupdeletemember`,
        name: "移除单个群组成员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    addMembers: {
        url: `${config.API_URL}/czimchatgroup/groupaddmemberlist`,
        name: "批量添加群组成员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    delMembers: {
        url: `${config.API_URL}/czimchatgroup/groupdeletememberlist`,
        name: "批量添加群组成员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    doDefault: {
        url: `${config.API_URL}/czimchatgroup/dodefault`,
        name: "设置默认聊天室",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findManager: {
        url: `${config.API_URL}/czimchatgroup/findmanagerlist`,
        name: "分页获取所有管理员列表",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findMember: {
        url: `${config.API_URL}/czimchatgroup/getmemberofchatgrouplist`,
        name: "分页获取当前聊天室的所有成员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findisMember: {
        url: `${config.API_URL}/czimchatgroup/chatgroupaddmember`,
        name: "获取可加入聊天室的会员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findGroup: {
        url: `${config.API_URL}/czimchatgroup/findchatgroupabbre`,
        name: "获取会员可加入的群聊",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },



}