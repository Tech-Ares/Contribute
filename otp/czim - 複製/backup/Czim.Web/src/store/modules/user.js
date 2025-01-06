import WebIM from "@/utils/WebIM"
import tool from '@/utils/tool';
import dayjs from 'dayjs'
import { ElLoading, ElNotification } from 'element-plus';

function errorHandler(e) {
  if (e.type === 602) {
    return ElNotification.error({
      title: '参数错误',
      message: `图片发送失败，你不属于当前群或聊天室`
    })
  } else if (e.type === 603) {
    return ElNotification.error({
      title: '参数错误',
      message: `您已被管理员禁言`
    })
  } else if (e.type === 605) {
    return ElNotification.error({
      title: '参数错误',
      message: `当前群(聊天室)不存在`
    })
  } else if (e.type === 501) {
    return ElNotification.error({
      title: '警告',
      message: `发送失败，消息包含敏感词`
    })
  } else if (e.type === 502) {
    return ElNotification.error({
      title: '警告',
      message: `发送失败，设置的自定义拦截捕获`
    })
  } else if (e.type === 503) {
    return ElNotification.error({
      title: '警告',
      message: `未知错误`
    })
  } else if (e.type === 504) {
    return ElNotification.warning({
      title: '提示',
      message: `超出消息撤回时间`
    })
  } else if (e.type === 505) {
    return ElNotification.warning({
      title: '提示',
      message: `未开通消息撤回`
    })
  } else if (e.type === 506) {
    return ElNotification.warning({
      title: '提示',
      message: `您被禁止在该群组或聊天室发言`
    })
  } else {
    return ElNotification.warning({
      title: '未知错误',
      message: `请联系环信`
    })
  }
}

export default {
  state: {
    user: tool.data.get('IM_USERINFO'),
    userSecret: tool.data.get('USER_INFO'),
    mute: tool.data.get('MUTE') || false,
    currentChat: {},
    currentMsg: [],
    recent: tool.data.get('USER_RECENT'),
    contact: [],
    chatgroup: [],
    chatroom: [],
    activeTab: tool.data.get('ACTIVE_TAB'),
    recentTabNotice: 0,
  },
  // vuex计算属性
  getters: {
    fetchCurrentMsg(state) {
      return state.currentMsg;
    }
  },
  // vuex 同步任务
  mutations: {
    setMute(state, payload) {
      state.mute = payload
      tool.data.set('MUTE', payload)
    },
    setRecentTabNotice (state, payload) {
      state.recentTabNotice += payload
    },
    reduceRecentTabNotice (state, payload) {
      state.recentTabNotice -= payload
      if (state.recentTabNotice < 0) {
        state.recentTabNotice = 0
      }
    },
    setUser(state, payload) {
      state.user = payload
      tool.data.set('IM_USERINFO', payload)
    },
    // 登录保存环信登录账号密码
    setUserSecret(state, payload) {
      state.userSecret = payload
      tool.data.set('USER_INFO', payload)
    },
    // 设置最近联系人列表
    setRecent(state, payload) {
      state.recent = payload
      tool.data.set('USER_RECENT', payload)
    },
    // 设置好友列表
    setContact(state, payload) {
      state.contact = payload
    },
    // 设置群列表
    setChatgroup(state, payload) {
      state.chatgroup = payload
    },
    // 设置聊天室列表
    setChatroom(state, payload) {
      state.chatroom = payload
    },
    // 设置未读 ---仅适用于联系人
    setSessionUnread(state, payload) {
      console.log(payload);
      const { from, to, type, time, ext } = payload
      console.log(from, to, type, time, ext);
      state.recent && state.recent.some(target => {
        if (type == 'chatroom' && target.targetId == to) {
          if (!target.unread_num) {
            target.unread_num = 1
          } else {
            target.unread_num += 1
          }
          return true
        } else if (type == 'chat' && target.targetId == from) {
          console.log('用户');
          if (!target.unread_num) {
            target.unread_num = 1
          } else {
            target.unread_num += 1
          }
          return true
        } else if (type == 'groupchat' && target.targetId == to) {
          if (!target.unread_num) {
            target.unread_num = 1
          } else {
            target.unread_num += 1
          }
          return true
        }
      })
    },
    // 清除未读
    clearUnread(state, payload) {
      state.contact.some(target => {
        if (target.targetId == payload) {
          delete target.unRead
          return true
        }
      })
    },
    // 清除最近联系人未读
    clearSessionUnread(state, payload) {
      const { targetId } = payload
      state.recent && state.recent.some(target => {
        if (target.targetId == targetId) {
          target.unread_num = 0
          return true
        }
      })
      tool.data.set('USER_RECENT', state.recent)
    },
    // 设置当前激活的面板
    setActiveTab(state, payload) {
      state.activeTab = payload
      tool.data.set('ACTIVE_TAB', payload)
    },
    // 设置当前聊天对象
    setCurrentChat(state, payload) {
      state.currentChat = payload
    },
    // 设置当前聊天记录
    setCurrentMsg(state, payload) {
      state.currentMsg = payload
    },
    // 更新总聊天记录
    updateMsgList(state, payload) {
      const msgList = tool.data.get(`${state.userSecret.userId}_IM_MSGLIST`)
      console.log(msgList);
      console.log(payload);
      if (!msgList) {
        let list = {
          contact: {},
          groupchat: {},
          chatroom: {}
        }
        tool.data.set(`${state.userSecret.userId}_IM_MSGLIST`, list)
      }
      // 如果本地存储没有对应聊天记录，就创建一个
      if (!msgList[payload.chatType][payload.chatId]) {
        msgList[payload.chatType][payload.chatId] = [payload]
        // if (payload.chatType === 'groupchat') {
        //   const newObj = {

        //   }
        // }
      } else {
        // 有对应聊天记录就往里追加
        msgList[payload.chatType][payload.chatId] = [...msgList[payload.chatType][payload.chatId], payload]
        // 如果缓存的聊天记录 > 80 条, 清除溢出的聊天记录
        if (msgList[payload.chatType][payload.chatId].length > 80) {
          // let len = msgList[payload.chatType][payload.chatId].length - 80
          msgList[payload.chatType][payload.chatId].splice(0, 10)
          console.log(msgList[payload.chatType][payload.chatId].length, '删除后');
        }
        // 收消息时，如果发信人为当前聊天对象，就更新 currentMsg
        if (state.currentChat.targetId == payload.chatId) {
          state.currentMsg = [...state.currentMsg, payload]
        }
      }
      console.log(msgList)
      // 最后更新本地存储
      tool.data.set(`${state.userSecret.userId}_IM_MSGLIST`, msgList)
    },
    // 更新消息状态
    updateMsgStatus(state, payload) {
      // 单聊
      if (state.currentChat.nickName) {
        const { mid, from, type } = payload
        const msgList = tool.data.get(`${state.userSecret.userId}_IM_MSGLIST`)
        console.log(msgList);
        if (state.currentMsg) {
          state.currentMsg.some(msg => {
            if (msg.mid == mid) {
              msg.status = type
              msgList["contact"][from] = state.currentMsg
              console.log(msgList["contact"][from], '11')
              return true
            }
          })
          tool.data.set(`${state.userSecret.userId}_IM_MSGLIST`, msgList)
        }
      }
      // 目前已读只支持单聊
    },
    // 联系人正在输入状态 - 支持单聊
    updateTyping(state, payload) {
      if (!state.currentChat.typing) {
        state.currentChat.typing = payload
      } else {
        delete state.currentChat.typing
      }
    },
    // 消息撤回
    onRecall(state, payload) {
      console.log(payload)
      const { to, mid } = payload
      const msgList = tool.data.get(`${state.userSecret.userId}_IM_MSGLIST`)
      if (Object.keys(msgList).length > 0) {
        Object.keys(msgList).forEach(key => {
          if (msgList[key][to]) {
            console.log(msgList[key][to], '>>')
            msgList[key][to].some(msg => {
              if (msg.mid == mid) {
                msg.status = 'recall'
                msg.recallMsg = '该消息已被撤回'
                if (to == state.currentChat.targetId) {
                  state.currentMsg = msgList[key][to]
                }
                return true
              }
            })
          }
        })
      }
      tool.data.set(`${state.userSecret.userId}_IM_MSGLIST`, msgList)
    },
    // 更改 currentChat 禁言状态
    changeProhibit(state, payload) {
      if (state.currentChat.czType !== 0) {
        state.currentChat.isProhibit = payload
      }
    }
  },
  actions: {
    // 注销
    onLogout() {
      WebIM.conn.close()
    },
    getLoginUserInfo(context, payload) {
      const { userId } = payload
      WebIM.conn.fetchUserInfoById(userId).then((res) => {
        console.log(res.data[userId])
        let user_detail = res.data[userId];
        res.data[userId] && context.commit('setUserDetaild', user_detail)
      })
    },
    // 发送文本消息
    onSendText(context, payload) {
      const { chatType, chatId, msg, mid, ext } = payload
      // 设定消息类型
      let type
      if (chatType === "contact") {
        type = 'singleChat'
      } else if (chatType === "chatroom") {
        type = 'chatroom'
      } else {
        type = 'groupchat'
      }
      // 设定消息类型
      // const type = chatType == 'contact' ? 'singleChat' : chatType
      let msgType = new WebIM.message('txt', mid)
      let msgContent = {
        msg,               // 消息内容
        to: chatId,        // 接收消息对象（用户id）
        chatType: type,          // 设置为单聊/群聊
        ext,                                  //扩展消息
        success: (id, serverMsgId) => {
          // id 为本地生成的 UniqueId  serverMsgId 为服务器消息id
          console.log('send private text Success', id, serverMsgId)
          payload.mid = serverMsgId
          context.commit('updateMsgList', payload)
        },
        fail: (e) => {
          console.log("Send private text error", e)
          errorHandler(e)
          // if (e.type === 602) {
          //   ElMessage.error(`消息发送失败，你不属于当前群或聊天室`)
          // }
        }
      }
      msgType.set(msgContent);
      console.log(msgType.body)
      msgType.body.msgConfig = { allowGroupAck: true }
      WebIM.conn.send(msgType.body)
    },
    // 通过文件上传发送图片
    onSendImg(context, payload) {
      const { chatType, chatId, msg, mid } = payload
      // 设定消息类型
      let type
      if (chatType === "contact") {
        type = 'singleChat'
      } else if (chatType === "chatroom") {
        type = 'chatroom'
      } else {
        type = 'groupchat'
      }
      // const type = chatType == 'contact' ? 'singleChat' : 'chatRoom'
      let msgType = new WebIM.message('img', mid)
      msgType.set({
        to: chatId,
        chatType: type,
        msg,
        body: {
          type: 'file',
          url: msg,
        },
        ext: {
          nickName: context.state.userSecret.nickName,
          avatar: context.state.userSecret.avatar
        },
        success: (id, serverMsgId) => {
          // id 为本地生成的 UniqueId  serverMsgId 为服务器消息id
          console.log('send private img Success', id, serverMsgId)
          payload.mid = serverMsgId
          context.commit('updateMsgList', payload)
        },
        fail: (e) => {
          console.log("Send private img error", e)
          errorHandler(e)
          // if (e.type == 602) {
          //   ElNotification.error({
          //     title: '参数错误',
          //     message: `图片发送失败，你不属于当前群或聊天室`
          //   })
          // }
        }
      });
      console.log(msgType.body);
      WebIM.conn.send(msgType.body)
    },
    // 通过剪贴板发送图片
    onSendPasteImg(context, payload) {
      const { chatType, chatId, msg, mid } = payload
      // 设定消息类型
      let type
      if (chatType === "contact") {
        type = 'singleChat'
      } else if (chatType === "chatroom") {
        type = 'chatroom'
      } else {
        type = 'groupchat'
      }
      // const type = chatType == 'contact' ? 'singleChat' : 'chatRoom'
      let msgType = new WebIM.message('img', mid)
      msgType.set({
        file: { data: payload.data.file, url: msg },
        to: chatId,
        chatType: type,
        ext: {
          nickName: context.state.userSecret.nickName,
          avatar: context.state.userSecret.avatar
        },
        onFileUploadError: function (error) {
          console.log('上传失败', error)
        },
        onFileUploadComplete: function (data) {
          console.log('上传成功', data)
          payload.msg = data.uri
        },
        success: function (id, serverMsgId) {
          console.log('发送成功', id, serverMsgId)
          payload.mid = serverMsgId
          context.commit('updateMsgList', payload)
        },
        fail: function (e) {
          errorHandler(e)
          console.log("发送失败", e) //如禁言、拉黑后发送消息会失败
          // if (e.type == 602) {
          //   ElNotification.error({
          //     title: '参数错误',
          //     message: `图片发送失败，你不属于当前群或聊天室`
          //   })
          // }
        }
      });
      console.log(msgType.body)
      WebIM.conn.send(msgType.body)
    },
    // 拉取指定目标的聊天记录 —— 消息漫游
    fetchHistoryMsg(context, payload) {
      console.log(context, payload);
      const { id, type } = payload
      const msgList = tool.data.get(`${context.state.userSecret.userId}_IM_MSGLIST`)
      if (type == 'contact') {
        if (!msgList[type][id]) {
          const loadingInstance = ElLoading.service({
            target: '.message-box',
            fullscreen: false,
            text: '正在同步数据...'
          })
          const options = {
            queue: context.state.currentChat.targetId,
            isGroup: false,
            count: 30,
            success: (res) => {
              //获取拉取成功的历史消息
              const historyMsg = res.map(item => {
                if (item.contentsType == 'TEXT') {
                  return {
                    bySelf: item.to == context.state.userSecret.userId ? false : true,
                    chatId: item.to == context.state.userSecret.userId ? item.from : item.from,
                    chatType: "contact",
                    ext: item.ext,
                    mid: item.id,
                    msg: item.data,
                    time: dayjs(parseInt(item.time)).format('YYYY-MM-DD HH:mm:ss'),
                    type: 'text'
                  }
                } else if (item.contentsType == 'IMAGE') {
                  return {
                    bySelf: item.to == context.state.userSecret.userId ? false : true,
                    chatId: item.to == context.state.userSecret.userId ? item.from : item.to,
                    chatType: "contact",
                    ext: item.ext,
                    mid: item.id,
                    msg: item.url,
                    time: dayjs(parseInt(item.time)).format('YYYY-MM-DD HH:mm:ss'),
                    type: 'image'
                  }
                } else if (item.contentsType == 'CUSTOM' && item.customEvent == 'shake') {
                  return {
                    bySelf: item.to == context.state.userSecret.userId ? false : true,
                    chatId: item.to == context.state.userSecret.userId ? item.from : item.to,
                    chatType: "contact",
                    ext: item.ext,
                    mid: item.id,
                    msg: '抖动消息',
                    time: dayjs(parseInt(item.time)).format('YYYY-MM-DD HH:mm:ss'),
                    type: 'shake'
                  }
                } else if (item.contentsType == 'VOICE') {
                  return {
                    bySelf: item.to == context.state.userSecret.userId ? false : true,
                    chatId: item.to == context.state.userSecret.userId ? item.from : item.to,
                    chatType: "contact",
                    ext: item.ext,
                    mid: item.id,
                    msg: item.url,
                    time: dayjs(parseInt(item.time)).format('YYYY-MM-DD HH:mm:ss'),
                    type: 'voice'
                  }
                }
              })
              console.log(historyMsg);
              context.commit('setCurrentMsg', historyMsg)
              msgList[type][id] = historyMsg
              tool.data.set(`${context.state.userSecret.userId}_IM_MSGLIST`, msgList)
              const dom = document.getElementById('scrollbar')
              if (dom) {
                console.log(dom);
                setTimeout(() => {
                  console.log('执行了');
                  loadingInstance.close()
                  dom.scrollTop = dom.scrollHeight
                }, 0);
              }
            },
            fail: (err) => {
              console.log(err)
              errorHandler(err)
            }
          }
          WebIM.conn.mr_cache = []
          WebIM.conn.fetchHistoryMessages(options)
        } else {
          context.commit('setCurrentMsg', msgList[type][id])
          const dom = document.getElementById('scrollbar')
          if (dom) {
            console.log(dom);
            setTimeout(() => {
              console.log('执行了');
              dom.scrollTop = dom.scrollHeight
            }, 0);
          }
        }
      } else if (type == 'chatroom') {
        if (!msgList[type][id]) {
          const loadingInstance = ElLoading.service({
            target: '.message-box',
            fullscreen: false,
            text: '正在同步数据...'
          })
          const options = {
            queue: context.state.currentChat.targetId,
            isGroup: true,
            count: 30,
            success: (res) => {
              console.log(res);
              //获取拉取成功的历史消息
              const historyMsg = res.map(item => {
                if (item.contentsType == 'TEXT') {
                  return {
                    bySelf: item.from == context.state.userSecret.userId ? true : false,
                    chatId: item.to,
                    from: item.from,
                    chatType: item.type == 'chatroom' ? "chatroom" : "contact",
                    ext: item.ext,
                    mid: item.id,
                    msg: item.data,
                    time: dayjs(parseInt(item.time)).format('YYYY-MM-DD HH:mm:ss'),
                    type: 'text'
                  }
                } else if (item.contentsType == 'IMAGE') {
                  return {
                    bySelf: item.from == context.state.userSecret.userId ? true : false,
                    chatId: item.to,
                    from: item.from,
                    chatType: item.type == 'chatroom' ? "chatroom" : "contact",
                    ext: item.ext,
                    mid: item.id,
                    msg: item.url,
                    time: dayjs(parseInt(item.time)).format('YYYY-MM-DD HH:mm:ss'),
                    type: 'image'
                  }
                } else if (item.contentsType == 'CUSTOM' && item.customEvent == 'shake') {
                  return {
                    bySelf: item.from == context.state.userSecret.userId ? true : false,
                    chatId: item.to,
                    from: item.from,
                    chatType: item.type == 'chatroom' ? "chatroom" : "contact",
                    ext: item.ext,
                    mid: item.id,
                    msg: '抖动消息',
                    time: dayjs(parseInt(item.time)).format('YYYY-MM-DD HH:mm:ss'),
                    type: 'shake'
                  }
                } else if (item.contentsType == 'VOICE') {
                  return {
                    bySelf: item.from == context.state.userSecret.userId ? true : false,
                    chatId: item.to,
                    from: item.from,
                    chatType: item.type == 'chatroom' ? "chatroom" : "contact",
                    ext: item.ext,
                    mid: item.id,
                    msg: item.url,
                    time: dayjs(parseInt(item.time)).format('YYYY-MM-DD HH:mm:ss'),
                    type: 'voice'
                  }
                }
              })
              console.log(historyMsg);
              context.commit('setCurrentMsg', historyMsg)
              msgList[type][id] = historyMsg
              tool.data.set(`${context.state.userSecret.userId}_IM_MSGLIST`, msgList)
              const dom = document.getElementById('scrollbar')
              if (dom) {
                console.log(dom);
                setTimeout(() => {
                  console.log('执行了');
                  loadingInstance.close()
                  dom.scrollTop = dom.scrollHeight
                }, 0);
              }
            },
            fail: (err) => {
              console.log(err)
              errorHandler(err)
            }
          }
          WebIM.conn.mr_cache = []
          WebIM.conn.fetchHistoryMessages(options)
          let dom = document.getElementById('scrollbar')
          if (dom) {
            console.log(dom);
            setTimeout(() => {
              loadingInstance.close() && (dom.scrollTop = dom.scrollHeight)
            }, 100);
          }
        } else {
          context.commit('setCurrentMsg', msgList[type][id])
          const dom = document.getElementById('scrollbar')
          if (dom) {
            console.log(dom);
            setTimeout(() => {
              console.log('执行了');
              dom.scrollTop = dom.scrollHeight
            }, 0);
          }
        }
      } else if (type == 'groupchat') {
        if (!msgList[type][id]) {
          const loadingInstance = ElLoading.service({
            target: '.message-box',
            fullscreen: false,
            text: '正在同步数据...'
          })
          const options = {
            queue: context.state.currentChat.targetId,
            isGroup: true,
            count: 30,
            success: (res) => {
              console.log(res);
              //获取拉取成功的历史消息
              const historyMsg = res.map(item => {
                if (item.contentsType == 'TEXT') {
                  return {
                    bySelf: item.from == context.state.userSecret.userId ? true : false,
                    chatId: item.to,
                    from: item.from,
                    chatType: 'groupchat',
                    ext: item.ext,
                    mid: item.id,
                    msg: item.data,
                    time: dayjs(parseInt(item.time)).format('YYYY-MM-DD HH:mm:ss'),
                    type: 'text'
                  }
                } else if (item.contentsType == 'IMAGE') {
                  return {
                    bySelf: item.from == context.state.userSecret.userId ? true : false,
                    chatId: item.to,
                    from: item.from,
                    chatType: 'groupchat',
                    ext: item.ext,
                    mid: item.id,
                    msg: item.url,
                    time: dayjs(parseInt(item.time)).format('YYYY-MM-DD HH:mm:ss'),
                    type: 'image'
                  }
                } else if (item.contentsType == 'CUSTOM' && item.customEvent == 'shake') {
                  return {
                    bySelf: item.from == context.state.userSecret.userId ? true : false,
                    chatId: item.to,
                    from: item.from,
                    chatType: 'groupchat',
                    ext: item.ext,
                    mid: item.id,
                    msg: '抖动消息',
                    time: dayjs(parseInt(item.time)).format('YYYY-MM-DD HH:mm:ss'),
                    type: 'shake'
                  }
                } else if (item.contentsType == 'VOICE') {
                  return {
                    bySelf: item.from == context.state.userSecret.userId ? true : false,
                    chatId: item.to,
                    from: item.from,
                    chatType: 'groupchat',
                    ext: item.ext,
                    mid: item.id,
                    msg: item.url,
                    time: dayjs(parseInt(item.time)).format('YYYY-MM-DD HH:mm:ss'),
                    type: 'voice'
                  }
                }
              })
              console.log(historyMsg);
              context.commit('setCurrentMsg', historyMsg)
              msgList[type][id] = historyMsg
              tool.data.set(`${context.state.userSecret.userId}_IM_MSGLIST`, msgList)
              const dom = document.getElementById('scrollbar')
              if (dom) {
                console.log(dom);
                setTimeout(() => {
                  console.log('执行了');
                  loadingInstance.close()
                  dom.scrollTop = dom.scrollHeight
                }, 0);
              }
            },
            fail: (err) => {
              console.log(err)
              errorHandler(err)
            }
          }
          WebIM.conn.mr_cache = []
          WebIM.conn.fetchHistoryMessages(options)
          let dom = document.getElementById('scrollbar')
          if (dom) {
            setTimeout(() => {
              loadingInstance.close() && (dom.scrollTop = dom.scrollHeight)
            }, 100);
          }
        } else {
          console.log('else');
          context.commit('setCurrentMsg', msgList[type][id])
          const dom = document.getElementById('scrollbar')
          if (dom) {
            setTimeout(() => {
              console.log('执行了');
              dom.scrollTop = dom.scrollHeight
            }, 0);
          }
        }
      }
    },
    // 发送整个会话已读回执
    channelClearUnread(context, payload) {
      console.log(context, payload)
      const { targetId, czType } = payload
      let msgType = new WebIM.message('channel', WebIM.conn.getUniqueId())
      if (czType === 0) {
        msgType.set({
          to: targetId
        })
      } else {
        msgType.set({
          to: targetId,
          chatType: 'groupChat'
        });
      }
      WebIM.conn.send(msgType.body)
      context.commit('reduceRecentTabNotice', payload.unread_num)
      context.commit('clearSessionUnread', payload)
    }
  }
}