import config from "@/config"
import http from "@/utils/request"

export default {
    findList: {
        url: `${config.API_URL}/czimliveinfo/getliveinfolist`,
        name: "分页获取直播列表",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    deleteActive: {
        url: `${config.API_URL}/czimliveinfo/isActive`,
        name: "激活/失效",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    saveForm: {
        url: `${config.API_URL}/czimliveinfo/saveorupdateliveinfo`,
        name: "新建保存",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },


}