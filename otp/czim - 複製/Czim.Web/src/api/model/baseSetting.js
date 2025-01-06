import config from "@/config"
import http from "@/utils/request"

export default {
    saveForm: {
        url: `${config.API_URL}/czimbasesetting/saveorupdate`,
        name: "修改",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },
    findList: {
        url: `${config.API_URL}/czimbasesetting/findlist`,
        name: "查询",
        post: async function (data) {
            return await http.post(this.url, data);
        }
    },

}