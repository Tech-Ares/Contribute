import config from "@/config"
import http from "@/utils/request"

export default {
    findList: {
        url: `${config.API_URL}/czimmember/findlist`,
        name: "分页获取用户列表",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findLogin: {
        url: `${config.API_URL}/czimmember/findloginrecordlist`,
        name: "分页获取登录日志列表",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findJoin: {
        url: `${config.API_URL}/czimmember/memberjoinchatroomfindlist`,
        name: "分页获取会员加聊天室记录",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findFriend: {
        url: `${config.API_URL}/czimmember/memberfriendfindlist`,
        name: "分页获取会员好友记录",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findMember: {
        url: `${config.API_URL}/czimmember/findmember`,
        name: "获取会员详情",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    addRobot: {
        url: `${config.API_URL}/czimmember/addrobot`,
        name: "添加机器人",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    joinChat: {
        url: `${config.API_URL}/czimmember/joinchatroom`,
        name: "将成员添加到聊天室",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findService: {
        url: `${config.API_URL}/czimmember/memberaddroomcustomerlist`,
        name: "查询所有可添加的好友",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    addService: {
        url: `${config.API_URL}/czimmember/customeraddfriend`,
        name: "将客服添加为好友",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findAddmember: {
        url: `${config.API_URL}/czimmember/customeraddroommemberlist`,
        name: "查询客服可添加的会员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findGroup: {
        url: `${config.API_URL}/czimmember/memberjoinchatgroupfindlist`,
        name: "获取会员加入群聊记录",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    joinGroup: {
        url: `${config.API_URL}/czimmember/joinchatgroup`,
        name: "将会员添加到群",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    delMember: {
        url: `${config.API_URL}/czimmember/delmember`,
        name: "删除会员",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
}