import config from "@/config"
import http from "@/utils/request"

export default {
  getContacts: {
		url: `${config.API_URL}/czimchat/getcontacts`,
		name: "分页获取联系人列表",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	getChatroom: {
		url: `${config.API_URL}/czimchat/getchatroom`,
		name: "分页获取聊天室列表",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	getChatGroup: {
		url: `${config.API_URL}/czimchat/getchatgroup`,
		name: "分页获取群聊列表",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	recallMsg: {
		url: `${config.API_URL}/czimchat/recallmessage`,
		name: "撤回群聊消息",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	recallOne: {
		url: `${config.API_URL}/czimchat/recallone`,
		name: "撤回单聊消息",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	getPresetMsg: {
		url: `${config.API_URL}/czimchat/getpreset`,
		name: "获取所有预设消息",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	globalProhibit: {
		url: `${config.API_URL}/czimchat/allchatroomban`,
		name: "全局禁言!!!!!!",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	chatroomProhibit: {
		url: `${config.API_URL}/czimchat/chatroomprohibit`,
		name: "聊天室全员禁言",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	groupProhibit: {
		url: `${config.API_URL}/czimchat/chatgroupprohibit`,
		name: "群聊全员禁言",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	memberForbidden: {
		url: `${config.API_URL}/czimchat/memberforbidden`,
		name: '聊天室会员个体禁言',
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	memberForbiddenGroup: {
		url: `${config.API_URL}/czimchat/memberforbiddengroup`,
		name: '群聊会员个体禁言',
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	getChatroomMember: {
		url: `${config.API_URL}/czimchatroom/getallmember`,
		name: '获取聊天室的所有成员',
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	getForbidState: {
		url: `${config.API_URL}/czimchatroom/getforbidstate`,
		name: '聊天室获取指定用户禁言状态',
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	getForbidStateGroup: {
		url: `${config.API_URL}/czimchatgroup/getforbidstate`,
		name: '群聊获取指定用户禁言状态',
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	getContactById: {
		url: `${config.API_URL}/czimchat/getuserext`,
		name: '根据id获取用户信息',
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	getChatroomById: {
		url: `${config.API_URL}/czimchat/getchatroomext`,
		name: '根据id获取聊天室信息',
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	getGroupMember: {
		url: `${config.API_URL}/czimchat/getgroupmember`,
		name: '获取群聊 @ 列表',
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	getChatgroupById: {
		url: `${config.API_URL}/czimchatgroup/findchatgroup`,
		name: '根据id获取群组信息',
		post: async function(id){
			return await http.post(`${this.url}/${id}`);
		}
	},
	getGroupMemberList: {
		url: `${config.API_URL}/czimchatgroup/getmemberofchatgrouplist`,
		name: '获取群成员列表',
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	addFriendById: {
		url: `${config.API_URL}/czimchat/addfriend`,
		name: '通过 ID 添加好友',
		post: async function(data){
			return await http.post(this.url, data);
		}
	}
}