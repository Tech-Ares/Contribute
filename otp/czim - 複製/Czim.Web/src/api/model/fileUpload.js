// import config from "@/config"
import http from "@/utils/request"

export default {
    upload: {
        // url: `${config.API_URL}/czimchat/getcontacts`,
        name: "上传图片",
        post: async function (url, data, config) {
            return await http.upload(url, data, config);
        }
    }
}
