import config from "@/config"
import http from "@/utils/request"

export default {
    findList: {
        url: `${config.API_URL}/czimchatpreset/findlist`,
        name: "分页获取所有预设列表",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    saveForm: {
        url: `${config.API_URL}/czimchatpreset/saveform`,
        name: "保存/修改预设消息",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    isActive: {
        url: `${config.API_URL}/czimchatpreset/isactiveform`,
        name: "激活/失效",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    }


}