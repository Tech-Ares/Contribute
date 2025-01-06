<template>
  <audio ref="voiceMsg"></audio>
  <el-container>
    <el-aside>
      <side-list ref="sideList" @getCurrentMsg="loadCurrentMsg"></side-list>
    </el-aside>
    <el-main class="nopadding">
      <el-header>
        <div class="current-chat">
          <span class="name">
            {{
              currentChat.nickName || currentChat.chatRoomName || currentChat.groupName
            }}
            <i
              v-if="Object.keys(currentChat).length > 0 && currentChat.czType !== 0"
            >({{ onlineNum }}/{{ currentChat.maxusers }})</i>
          </span>
          <transition name="el-zoom-in-center">
            <span v-show="currentChat.czType == 0 && currentChat.typing">对方正在输入...</span>
          </transition>
          <transition name="el-zoom-in-center">
            <el-alert
              v-if="currentChat.czType !== 0 && currentChat.isProhibit"
              title="管理员已开启全员禁言"
              type="warning"
              effect="dark"
              show-icon
            ></el-alert>
          </transition>
        </div>
        <el-icon>
          <el-icon-setting @click="openDrawer" />
        </el-icon>
      </el-header>
      <div class="message-box" ref="scrollbar" id="scrollbar" @scroll="scrolling">
        <!-- <transition-group name="fade"> -->
        <template v-for="(item, index) in fetchCurrentMsg" :key="item.mid">
          <div class="msg" :class="{ byself: item.bySelf }" v-if="item.status !== 'recall'">
            <p class="nickname" v-text="item.ext.nickName || ''"></p>
            <div class="msg-main">
              <!-- 头像 -->
              <div class="ava">
                <el-avatar
                  shape="circle"
                  :size="30"
                  :src="
                    item.ext.avatar ||
                    `//cube.elemecdn.com/9/c2/f0ee8a3c7c9638a54940382568c9dpng.png`
                  "
                  fit="fill"
                ></el-avatar>
              </div>
              <!-- 消息内容 -->
              <div
                class="msg-content"
                :style="{
                  background:
                    item.type == 'text' && item.bySelf
                      ? '#9eea6a'
                      : '#f8f8f8',
                }"
              >
                <el-image
                  v-if="item.type == 'image'"
                  :src="item.msg"
                  :preview-src-list="srcList"
                  @click="preview(item.msg)"
                >
                  <template #placeholder>
                    <div class="image-slot">
                      Loading
                      <span class="dot">...</span>
                    </div>
                  </template>
                </el-image>
                <div
                  class="text"
                  v-else-if="
                    item.ext.fireTime && item.ext.fireTime !== 'null'
                  "
                >
                  <el-button type="danger" @click="readBurnMsg(item)">点击查看阅后即焚消息</el-button>
                </div>
                <div class="audio" v-else-if="item.type == 'voice'" @click="playAudio(item.msg)">
                  <el-icon>
                    <el-icon-video-play />
                  </el-icon>语音消息
                </div>
                <p class="text" v-else-if="item.type == 'text'" v-text="item.msg" v-copy></p>
                <p class="text" v-else-if="item.type == 'shake'" v-text="item.msg"></p>
              </div>
              <div class="status" v-if="currentChat.czType === 0">
                <el-tag v-show="item.status == 'sending'">已发送</el-tag>
                <el-tag v-show="item.status == 'read'" type="success">已读</el-tag>
              </div>
              <div class="more">
                <el-dropdown trigger="click">
                  <span class="el-dropdown-link">
                    <el-icon>
                      <el-icon-operation />
                    </el-icon>
                  </span>
                  <template #dropdown>
                    <el-dropdown-menu>
                      <el-dropdown-item @click="recall(item)">撤回消息</el-dropdown-item>
                      <el-dropdown-item
                        v-if="currentChat.czType !== 0"
                        :disabled="item.bySelf"
                        @click="ban(item)"
                      >禁言用户</el-dropdown-item>
                      <el-dropdown-item v-if="!item.bySelf" @click="addFriend(item)">加为好友</el-dropdown-item>
                    </el-dropdown-menu>
                  </template>
                </el-dropdown>
              </div>
            </div>
            <!-- 时间 -->
            <p class="time" v-if="index % 8 === 0" v-text="item.time"></p>
          </div>
          <div v-else class="recall">------{{ item.recallMsg }}------</div>
        </template>
        <!-- </transition-group> -->
        <el-empty
          v-if="!currentChat.targetId"
          description="选择联系人开始聊天"
          :image-size="200"
          image="/img/x-chat.png"
        ></el-empty>
      </div>
      <el-footer>
        <send-box ref="sendBox" @toBottom="scrollBottomSmooth"></send-box>
      </el-footer>
    </el-main>
  </el-container>
  <!-- 详情抽屉 -->
  <side-drawer ref="drawer"></side-drawer>
  <!-- 禁言弹窗 -->
  <index-dialog ref="forbid"></index-dialog>
  <!-- 阅后即焚弹窗 -->
  <burn-dialog ref="burn" @burnMsg="handleBurn"></burn-dialog>
</template>
<script>
import sideList from "./components/sideList.vue";
import sideDrawer from "./components/sideDrawer.vue";
import sendBox from "./components/sendBox.vue";
import indexDialog from "./components/indexDialog.vue";
import burnDialog from "./components/burnDialog.vue";
import { mapGetters } from "vuex";
import WebIM from "@/utils/WebIM"

export default {
  name: "chat",
  data() {
    return {
      onlineNum: 0,
      srcList: [],
      voiceSrc: "",
      scrollPosition: 0,
      timer: null,
    };
  },
  computed: {
    ...mapGetters(["fetchCurrentMsg"]),
    currentChat() {
      return this.$store.state.user.currentChat;
    }
  },
  watch: {
    fetchCurrentMsg() {
      this.$nextTick(() => this.scrollBottom());
    },
    // 监听滚动条位置
    scrollPosition: {
      handler(val) {
        if (val && val > 800) {
          if (this.$refs.sendBox.showToBottom) {
            return;
          }
          this.$refs.sendBox.backToBottom(true);
        } else {
          if (!this.$refs.sendBox.showToBottom) {
            return;
          }
          this.$refs.sendBox.backToBottom(false);
        }
      }
    },
  },
  created() {
    this.watchPaste();
  },
  mounted() {
    this.loadCurrentList();
  },
  methods: {
    // 获取好友列表
    loadCurrentList() {
      const active = this.$TOOL.data.get('ACTIVE_TAB') || 'recent'
      this.$refs.sideList.switchTab(active);
    },
    // 提取当前聊天记录
    loadCurrentMsg(id, czType) {
      console.log(id, czType);
      if (!this.currentChat) {
        return;
      }
      if (id && czType === 0) {
        console.log(0);
        this.$store.dispatch("fetchHistoryMsg", { id, type: "contact" });
      } else if (id && czType === 1) {
        console.log(1);
        this.$store.dispatch("fetchHistoryMsg", { id, type: "groupchat" });
        this.getGroupInfo()
      } else if (id && czType === 2) {
        console.log(2);
        this.$store.dispatch("fetchHistoryMsg", { id, type: "chatroom" });
      } else {
        return this.$message.warning("参数错误，获取聊天记录失败")
      }
    },
    // 新消息触底
    scrollBottom() {
      this.$refs.scrollbar.scrollTop = this.$refs.scrollbar.scrollHeight;
    },
    // 触底动画
    scrollBottomSmooth() {
      this.$refs.scrollbar.scrollTo({
        top: this.$refs.scrollbar.scrollHeight,
        behavior: "smooth",
      });
    },
    // 监听滚动
    scrolling() {
      if (this.timer) {
        clearTimeout(this.timer);
      }
      this.timer = setTimeout(() => {
        this.watchScroll();
      }, 300);
    },
    // 监听滚动方法
    watchScroll() {
      if (this.$refs.scrollbar) {
        let max = this.$refs.scrollbar.scrollHeight;
        let current = this.$refs.scrollbar.scrollTop;
        this.scrollPosition = max - current;
      }
    },
    // 打开右侧设置抽屉
    openDrawer() {
      if (!Object.keys(this.currentChat).length) {
        return this.$message.warning("请选择聊天目标后重试");
      }
      if (this.currentChat.czType === 2) {
        this.$refs.drawer.open("chatroom");
      } else if (this.currentChat.czType === 0) {
        this.$refs.drawer.open();
      } else if (this.currentChat.czType === 1) {
        this.$refs.drawer.open("groupchat");
      } else {
        return this.$message.error("参数错误，访问被拒绝");
      }
    },
    // 消息撤回
    async recall(item) {
      // 撤回群聊消息
      if (this.currentChat.czType == 1 || this.currentChat.czType == 2) {
        if (item.bySelf) {
          console.log("撤回自己消息");
          const res = await this.$API.imApi.recallMsg.post({
            chatType: 0,
            receiveId: item.chatId,
            messageType: 1,
            messageContent: item.mid,
          });
          console.log(res);
          if (res.code !== 0) {
            return this.$message.warning("消息撤回失败");
          }
          this.$store.commit("onRecall", {
            bySelf: item.bySelf,
            to: item.chatId,
            mid: item.mid,
          });
        } else {
          console.log("撤回他人消息");
          const res = await this.$API.imApi.recallMsg.post({
            chatType: 0,
            receiveId: item.chatId,
            messageType: 1,
            messageContent: item.mid,
          });
          console.log(res);
          if (res.code !== 0) {
            return this.$message.warning("消息撤回失败");
          }
        }
        // 撤回单聊消息
      } else if (this.currentChat.czType == 0) {
        if (item.bySelf) {
          // 调整自己发送的消息，不调用服务器撤回
          console.log("单聊撤回自己消息");
          if (!item.ext.fireTime) {
            const res = await this.$API.imApi.recallOne.post({
              chatType: 0,
              receiveId: item.chatId,
              messageType: 1,
              messageContent: item.mid
            })
            console.log(res);
            if (res.code !== 0) {
              return this.$message.warning('消息撤回失败')
            }
            this.$store.commit("onRecall", {
              bySelf: item.bySelf,
              to: item.chatId,
              mid: item.mid,
            });
          } else if (item.ext.fireTime && item.ext.fireTime > 0) {
            this.$store.commit("onRecall", {
              bySelf: item.bySelf,
              to: item.chatId,
              mid: item.mid,
            });
          }
        } else {
          console.log("单聊撤回他人消息");
          const res = await this.$API.imApi.recallOne.post({
            chatType: 0,
            receiveId: item.chatId,
            messageType: 1,
            messageContent: item.mid,
          });
          console.log(res);
          if (res.code !== 0) {
            return this.$message.warning("消息撤回失败");
          }
          this.$store.commit("onRecall", {
            bySelf: item.bySelf,
            to: item.chatId,
            mid: item.mid,
          });
        }
      }
    },
    // 阅读阅后即焚消息
    readBurnMsg(msg) {
      console.log(msg);
      this.$refs.burn.open(msg);
      this.$refs.burn.countDown();
    },
    handleBurn(msg) {
      console.log(msg);
      this.recall(msg);
    },
    // 禁言
    ban(item) {
      if (item.bySelf) {
        return this.$message.warning("不可以禁言自己");
      }
      console.log(item);
      if (this.currentChat.czType == 1) {
        this.$refs.forbid.open(item, "banGroup");
        this.$refs.forbid.getGroupUserStatus()
      } else if (this.currentChat.czType == 2) {
        this.$refs.forbid.open(item, "banRoom");
        this.$refs.forbid.getUserStatus();
      }
    },
    // 加为好友
    async addFriend(item) {
      const { from } = item
      const res = await this.$API.imApi.addFriendById.post({ targetId: from })
      if (res.code !== 0) {
        return this.$message.warning("服务器异常，建立好友关系失败");
      }
      // console.log(res);
      this.$message.success("建立好友关系成功")
    },
    // 播放语音
    playAudio(url) {
      this.$refs.voiceMsg.src = url;
      this.$refs.voiceMsg.play();
    },
    // 监听剪贴板
    watchPaste() {
      document.addEventListener("paste", (e) => {
        if (e.clipboardData || e.originalEvent) {
          //某些chrome版本使用的是event.originalEvent,需要测试调整
          let clipboardData = e.clipboardData || e.originalEvent.clipboardData;
          console.log(clipboardData);
          if (clipboardData.items) {
            // for chrome
            let items = clipboardData.items;
            console.log(items, "it");
            let blob = null;
            // 循环剪贴板内容，并提取图片
            for (let i = 0; i < items.length; i++) {
              if (items[i].type.match("^image/") && items[i].kind === "file") {
                //getAsFile() firefox ie11 并不支持
                blob = items[i].getAsFile();
                break;
              }
            }
            if (!blob) {
              return;
            }
            console.log(blob);
            // 将粘贴的图片传输给 sendBox
            this.$refs.sendBox.setPasteFile(blob);
          }
        }
      });
    },
    // 图片预览方法
    preview(url) {
      this.srcList[0] = url;
    },
    // 获取群组信息
    async getGroupInfo() {
      try {
        const options = {
          groupId: this.currentChat.targetId
        }
        const res = await WebIM.conn.getGroupInfo(options)
        this.onlineNum = res.data[0].affiliations_count
        console.log(res);
      } catch (error) {
        console.log(error);
      }
    }
  },
  components: {
    sideList,
    sendBox,
    sideDrawer,
    indexDialog,
    burnDialog,
  },
};
</script>
<style lang="scss" scoped>
.el-header {
  font-size: 14px;
  .current-chat {
    flex: 1;
    display: flex;
    align-items: center;
    margin-right: 10px;
    .name {
      margin-right: 10px;
      min-width: 120px;
    }
    .el-alert {
      width: 90%;
    }
  }
  .el-icon {
    margin-left: 10px;
    font-size: 1.5rem;
  }
}
.message-box {
  display: flex;
  flex-direction: column;
  height: calc(100% - 280px);
  overflow: auto;
  width: 100%;
  padding: 0 15px;
  box-sizing: border-box;
  position: relative;
  .el-alert {
    position: absolute;
    width: 70%;
    top: 5px;
    left: 15%;
    z-index: 2000;
  }
}
.msg {
  display: flex;
  flex-direction: column;
  margin-bottom: 10px;
  .nickname {
    color: rgb(163, 163, 163);
    margin-bottom: 5px;
  }
  .msg-main {
    display: flex;
    .ava {
      margin: 0 10px;
    }
    .msg-content {
      background-color: #f8f8f8;
      color: #000;
      max-width: 300px;
      padding: 10px;
      box-sizing: border-box;
      border-radius: 10px;
      .image-slot {
        width: 300px;
        height: 400px;
        display: flex;
        justify-content: center;
        align-items: center;
        font-size: 16px;
      }
      .text {
        line-height: 20px;
      }
      .audio {
        font-size: 14px;
        display: flex;
        align-items: center;
        justify-content: space-around;
        width: 100px;
        cursor: pointer;
      }
      &:hover {
        box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);
        transition: 0.3s;
      }
    }
    .status {
      align-self: center;
      padding: 5px;
    }
    .more {
      align-self: center;
      opacity: 0;
      padding: 5px;
    }
  }
  .time {
    margin-left: 50px;
    height: 30px;
    line-height: 30px;
    color: rgb(163, 163, 163);
  }
  &:nth-child(1) {
    margin-top: 10px;
  }
  &:hover .more {
    opacity: 1;
    transition: all 0.5s;
  }
}

.msg.byself {
  .nickname {
    margin-left: 0;
    margin-right: 10px;
    align-self: flex-end;
    display: none;
  }
  .msg-main {
    flex-direction: row-reverse;
    .msg-content {
      background-color: #9eea6a;
    }
  }
  .time {
    margin-left: 0;
    margin-right: 50px;
    align-self: flex-end;
  }
}

.recall {
  display: flex;
  justify-content: center;
  margin-bottom: 10px;
  color: #adadad;
}

// .fade-enter-from,
// .fade-leave-to {
//   opacity: 0;
//   // transform: translateY(30px);
// }
// .fade-enter-active,
// .fade-leave-active {
//   transition: opacity .2s;
// }
</style>