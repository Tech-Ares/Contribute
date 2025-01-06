import websdk from "easemob-websdk"
import config from "./WebIMConfig";
import { ElMessage, ElNotification } from 'element-plus';
import store from '@/store'
import dayjs from 'dayjs'
// import tool from '@/utils/tool';
import router from '@/router'

// 初始化IM SDK
let WebIM = {};
WebIM = window.WebIM = websdk;
WebIM.config = config;
WebIM.conn = new WebIM.connection({
  appKey: WebIM.config.appKey,
  isMultiLoginSessions: WebIM.config.isMultiLoginSessions,
  https: WebIM.config.https,
  isAutoLogin: true,
  heartBeatWait: WebIM.config.heartBeatWait,
  autoReconnectNumMax: WebIM.config.autoReconnectNumMax,
  autoReconnectInterval: WebIM.config.autoReconnectInterval,
  isStropheLog: WebIM.config.isStropheLog,
  isDebug: WebIM.config.isDebug,
  delivery: WebIM.config.delivery,
  isHttpDNS: WebIM.config.isHttpDNS,
  useOwnUploadFun: WebIM.config.useOwnUploadFun
});

WebIM.conn.listen({
  //连接成功回调
  onOpened: () => { ElMessage.success('xchat 连接已建立') },
  //连接关闭回调
  onClosed: () => {
    ElMessage.warning('xchat 连接关闭')
  },
  // 收到文本消息
  onTextMessage: (message) => {
    if (!store.state.user.mute) {
      document.getElementById('mute').play()
    }
    console.log(router);
    console.log('文本', message)
    const { from, to, type, time, ext } = message;
    console.log(from, to, type, time, ext);
    const chatId = type !== "chat" ? to : from;
    const typeMap = {
      chat: "contact",
      groupchat: "groupchat",
      chatroom: "chatroom"
    };
    const msgBody = {
      chatType: typeMap[message.type],
      chatId: chatId,
      msg: message.data,
      bySelf: false,
      from,
      to,
      mid: message.id,
      time: dayjs(parseInt(time)).format('YYYY-MM-DD HH:mm:ss'),
      ext,
      type: 'text'
    }
    store.commit('updateMsgList', msgBody)
    // 非 /chat 页面时，全局通知消息
    if (router.currentRoute._value.fullPath !== "/chat") {
      ElNotification.info({
        title: `来自 ${message.ext.nickName} 的新消息`,
        message: message.data,
        offset: 50,
      })
    }
    // 如果消息来自当前聊天目标，并且聊天目标为用户，就更新正在输入状态
    if (from == store.state.user.currentChat.targetId && store.state.user.currentChat.czType === 0) {
      store.commit('updateTyping')
    }
    // 收到非当前聊天目标消息，展示未读消息数
    if (chatId !== store.state.user.currentChat.targetId) {
      store.commit('setRecentTabNotice', 1)
      store.commit('setSessionUnread', message)
    }
  },
  onEmojiMessage: function (message) { console.log('表情', message); },   //收到表情消息
  // 收到图片消息
  onPictureMessage: (message) => {
    if (!store.state.user.mute) {
      document.getElementById('mute').play()
    }
    console.log('图片', message);
    const { from, to, type, time, ext } = message;
    console.log(from, to, type, time);
    const chatId = type !== "chat" ? to : from;
    const typeMap = {
      chat: "contact",
      groupchat: "groupchat",
      chatroom: "chatroom"
    }
    // 组装消息
    const msgBody = {
      chatType: typeMap[message.type],
      chatId: chatId,
      msg: message.url,
      bySelf: false,
      from: message.from,
      mid: message.id,
      time: dayjs(parseInt(time)).format('YYYY-MM-DD HH:mm:ss'),
      type: 'image',
      ext
    }
    store.commit('updateMsgList', msgBody)
    if (router.currentRoute._value.fullPath !== "/chat") {
      ElNotification.info({
        title: `来自 ${message.ext.nickName} 的新消息`,
        message: message.data,
        offset: 50,
      })
    }
    // 收到非当前聊天目标消息，展示未读消息数
    if (chatId !== store.state.user.currentChat.targetId) {
      store.commit('setRecentTabNotice', 1)
      store.commit('setSessionUnread', message)
    }
  },
  //收到文件消息
  onFileMessage: (file) => {
    console.log('文件', file)
  },
  //收到命令消息
  onCmdMessage: (cmd) => {
    console.log('命令', cmd);
    // 接到 对方正在输入状态 且 当前聊天目标和命令发送人相同
    if (cmd && cmd.action == 'TypingBegin' && cmd.from == store.state.user.currentChat.targetId) {
      store.commit('updateTyping', true)
    }
  },
  //收到音频消息
  onAudioMessage: (message) => {
    if (!store.state.user.mute) {
      document.getElementById('mute').play()
    }
    console.log('音频', message)
    const { from, to, type, time, ext, id, url } = message;
    console.log(from, to, type, time);
    const chatId = type !== "chat" ? to : from;
    const typeMap = {
      chat: "contact",
      groupchat: "groupchat",
      chatroom: "chatroom"
    }
    // 组装消息
    const msgBody = {
      chatType: typeMap[message.type],
      chatId,
      msg: url,
      bySelf: false,
      from,
      mid: id,
      time: dayjs(parseInt(time)).format('YYYY-MM-DD HH:mm:ss'),
      ext,
      type: 'voice'
    }
    store.commit('updateMsgList', msgBody)
    ElNotification.info({
      title: `来自 ${message.ext.nickName} 的新消息`,
      message: '语音消息',
      offset: 50,
    })
    // 收到非当前聊天目标消息，展示未读消息数
    if (chatId !== store.state.user.currentChat.targetId) {
      store.commit('setRecentTabNotice', 1)
      store.commit('setSessionUnread', message)
    }
  },
  onLocationMessage: function (message) { console.log('位置', message); },//收到位置消息
  //收到撤回消息回调
  onRecallMessage: (recall) => {
    console.log('撤回', recall)
    store.commit('onRecall', recall)
  },
  //收到消息送达服务器回执
  // onReceivedMessage: (message) => {
  //   console.log('已送达服务器', message)
  // },
  //收到消息送达客户端回执
  // onDeliveredMessage: (message) => {
  //   console.log('已送达客户端', message);
  // },
  //收到消息已读回执
  onReadMessage: (message) => {
    console.log('对方已读', message);
    store.commit('updateMsgStatus', message)
  },
  onMutedMessage: (message) => {
    console.log(message);
  },
  // 掉线处理
  onOffline: (e) => {
    console.log(e);
    ElMessage.error('网络异常，CrabIM 连接断开')
    const options = {
      user: store.state.user.userSecret.userId,
      pwd: store.state.user.userSecret.easePwd,
      appKey: WebIM.config.appKey,
      success: (res) => {
        console.log(res);
        store.commit('setUser', res)
        ElMessage.success('xchat 断线重连成功')
      }
    }
    WebIM.conn.open(options);
  },
  // 错误处理
  onError: (message) => {
    console.log(message);
    if (message.type === 206) {
      ElMessage.error('账号异地登陆，本机被迫下线')
    }
  },
})

export default WebIM;