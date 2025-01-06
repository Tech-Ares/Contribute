import config from "@/config"
import http from "@/utils/request"

export default {
	/**
	 * 登录获取token
	 */
	token: {
		url: `${config.API_URL}/account/check`,
		name: "登录获取TOKEN",
		post: async function (data = {}) {
			return await http.post(this.url, data);
		}
	}
}
