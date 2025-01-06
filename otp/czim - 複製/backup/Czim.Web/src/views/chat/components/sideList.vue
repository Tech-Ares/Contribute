<template>
  <el-tabs type="border-card" v-model="activeTab" @tab-click="switchTab">
    <el-tab-pane name="recent" lazy>
      <template #label>
        <el-badge :value="listData.recentTabNotice" :hidden="activeTab == 'recent' || listData.recentTabNotice === 0">最近</el-badge>
      </template>
      <el-scrollbar
        class="scroll-content"
        ref="scrollbar"
        v-loading="loadingCtrl.recent"
        element-loading-text="正在获取最近联系人..."
      >
        <transition-group name="el-zoom-in-center">
          <div
            class="contact-card"
            @click="selectTarget(item)"
            :class="{ select: currenChat.targetId == item.targetId }"
            v-for="item in listData.recents"
            :key="item.targetId"
          >
            <el-badge :value="item.unread_num" :hidden="item.unread_num == 0" :max="10">
              <el-avatar
                v-if="item.avatar && item.avatar.length > 0"
                shape="circle"
                :size="40"
                :src="item.avatar"
              ></el-avatar>
              <el-avatar
                v-else
                shape="circle"
                :size="40"
              >{{ item.nickName.substring(0, 1) || item.chatRoomName.substring(0, 1) || item.groupName.substring(0, 1) }}</el-avatar>
            </el-badge>
            <div class="details">
              <span class="user-name" v-text="item.nickName || item.chatRoomName"></span>
              <span class="type group" v-if="item.czType === 1">群聊</span>
              <span class="type room" v-if="item.czType === 2">聊天室</span>
            </div>
          </div>
        </transition-group>
        <el-empty
          v-if="!listData.recents || listData.recents.length == 0"
          description="暂无联系人"
          :image-size="100"
        ></el-empty>
      </el-scrollbar>
    </el-tab-pane>
    <el-tab-pane label="好友" name="contact" lazy>
      <el-scrollbar
        class="scroll-content"
        ref="scrollbar"
        v-loading="loadingCtrl.contact"
        element-loading-text="正在获取好友..."
      >
        <transition-group name="el-zoom-in-center">
          <div
            class="contact-card"
            @click="selectTarget(item)"
            :class="{ select: currenChat.targetId == item.targetId }"
            v-for="item in listData.contacts"
            :key="item.targetId"
          >
            <el-badge :value="item.unRead" :max="10">
              <el-avatar v-if="item.avatar" shape="circle" :size="40" :src="item.avatar"></el-avatar>
              <el-avatar v-else shape="circle" :size="40">{{ item.nickName.substring(0, 1) }}</el-avatar>
            </el-badge>
            <div class="details">
              <span class="user-name" v-text="item.nickName"></span>
            </div>
          </div>
        </transition-group>
        <el-empty
          v-if="!listData.contacts || listData.contacts.length == 0"
          description="暂无好友"
          :image-size="100"
        ></el-empty>
      </el-scrollbar>
      <div class="kxTable-pagination-box">
        <el-pagination
          class="kxTable-pagination"
          background
          :small="true"
          layout="total, prev, pager, next"
          :total="pageCtrl.contactTotal"
          v-model:currentPage="pageCtrl.contact.page"
          @current-change="handleChange"
        ></el-pagination>
        <div class="kxTable-do">
          <el-button
            @click="refresh('contact')"
            icon="el-icon-refresh"
            circle
            style="margin-left:15px"
          ></el-button>
        </div>
      </div>
    </el-tab-pane>
    <el-tab-pane label="群聊" name="chatgroup" lazy>
      <el-scrollbar
        class="scroll-content"
        ref="scrollbar"
        v-loading="loadingCtrl.chatgroup"
        element-loading-text="正在获取群聊..."
      >
        <transition-group name="el-zoom-in-center">
          <div
            class="contact-card"
            v-for="item in listData.chatgroups"
            :key="item.targetId"
            @click="selectTarget(item)"
            :class="{ select: currenChat.targetId == item.targetId }"
          >
            <el-avatar
              shape="square"
              :size="40"
              :src="item.groupImg || `//cube.elemecdn.com/9/c2/f0ee8a3c7c9638a54940382568c9dpng.png`"
            ></el-avatar>
            <div class="details">
              <span class="user-name" v-text="item.groupName"></span>
            </div>
          </div>
        </transition-group>
        <el-empty v-if="listData.chatgroups.length == 0" description="暂无群聊" :image-size="100"></el-empty>
      </el-scrollbar>
      <div class="kxTable-pagination-box">
        <el-pagination
          background
          :small="true"
          layout="total, prev, pager, next"
          :total="pageCtrl.chatgroupTotal"
          v-model:currentPage="pageCtrl.chatgroup.page"
          @current-change="handleChange"
        ></el-pagination>
        <div class="kxTable-do">
          <el-button
            @click="refresh('chatgroup')"
            icon="el-icon-refresh"
            circle
            style="margin-left:15px"
          ></el-button>
        </div>
      </div>
    </el-tab-pane>
    <el-tab-pane label="聊天室" name="chatroom" lazy>
      <el-scrollbar
        class="scroll-content"
        ref="scrollbar"
        v-loading="loadingCtrl.chatroom"
        element-loading-text="正在获取聊天室..."
      >
        <transition-group name="el-zoom-in-center">
          <div
            class="contact-card"
            v-for="item in listData.chatrooms"
            :key="item.targetId"
            @click="selectTarget(item)"
            :class="{ select: currenChat.targetId == item.targetId }"
          >
            <el-avatar
              shape="square"
              :size="40"
              :src="item.chatRoomImg || `//cube.elemecdn.com/9/c2/f0ee8a3c7c9638a54940382568c9dpng.png`"
            ></el-avatar>
            <div class="details">
              <span class="user-name" v-text="item.chatRoomName"></span>
            </div>
          </div>
        </transition-group>
        <el-empty v-if="listData.chatrooms.length == 0" description="暂无聊天室" :image-size="100"></el-empty>
      </el-scrollbar>
      <div class="kxTable-pagination-box">
        <el-pagination
          background
          :small="true"
          layout="total, prev, pager, next"
          :total="pageCtrl.chatroomTotal"
          v-model:currentPage="pageCtrl.chatroom.page"
          @current-change="handleChange"
        ></el-pagination>
        <div class="kxTable-do">
          <el-button
            @click="refresh('chatroom')"
            icon="el-icon-refresh"
            circle
            style="margin-left:15px"
          ></el-button>
        </div>
      </div>
    </el-tab-pane>
    <!-- <el-tab-pane label="我的" name="me" lazy>
      <my-info></my-info>
    </el-tab-pane> -->
  </el-tabs>
</template>
<script setup>
import WebIM from "@/utils/WebIM"
// import myInfo from "./myInfo.vue"
import { ElMessage } from 'element-plus';
import { computed, getCurrentInstance, reactive } from 'vue';
import { useStore } from "vuex";

// 获取全局API
const { appContext } = getCurrentInstance()

// 引入 Store
const store = useStore()

// loading统一控制
const loadingCtrl = reactive({
  recent: false,
  contact: false,
  chatgroup: false,
  chatroom: false
})

// 默认 Tab
const activeTab = computed(() => store.state.user.activeTab || 'recent')

// 切换TAB
const switchTab = (pane) => {
  console.log(pane);
  if (pane.props) {
    if (pane.props.name == 'recent') {
      store.commit('setActiveTab', 'recent')
      debounce(getRecent, 600, true)()
    } else if (pane.props.name == 'contact') {
      store.commit('setActiveTab', 'contact')
      debounce(getContacts, 600, true)()
    } else if (pane.props.name == 'chatgroup') {
      store.commit('setActiveTab', 'chatgroup')
      debounce(getChatgroups, 600, true)()
    } else if (pane.props.name == 'chatroom') {
      store.commit('setActiveTab', 'chatroom')
      debounce(getChatrooms, 600, true)()
    } else if (pane.props.name == 'me') {
      store.commit('setActiveTab', 'me')
      debounce(getMyInfo, 600, true)()
    } else {
      return ElMessage.error('参数错误: 不存在此选项卡！')
    }
  } else {
    if (pane == 'recent') {
      store.commit('setActiveTab', 'recent')
      debounce(getRecent, 600, true)()
    } else if (pane == 'contact') {
      store.commit('setActiveTab', 'contact')
      debounce(getContacts, 600, true)()
    } else if (pane == 'chatgroup') {
      store.commit('setActiveTab', 'chatgroup')
      debounce(getChatgroups, 600, true)()
    } else if (pane == 'chatroom') {
      store.commit('setActiveTab', 'chatroom')
      debounce(getChatrooms, 600, true)()
    } else if (pane == 'me') {
      store.commit('setActiveTab', 'me')
      debounce(getMyInfo, 600, true)()
    } else {
      return ElMessage.error('参数错误: 不存在此选项卡！')
    }
  }
}

// 列表数据存储
const listData = reactive({
  recents: computed(() => store.state.user.recent),
  contacts: computed(() => store.state.user.contact),
  chatrooms: computed(() => store.state.user.chatroom),
  chatgroups: computed(() => store.state.user.chatgroup),
  recentTabNotice: computed(() => store.state.user.recentTabNotice)
})

// 分页控制器
const pageCtrl = reactive({
  contact: {
    page: 1,
    size: 10,
  },
  contactTotal: 0,
  chatroom: {
    page: 1,
    size: 10,
  },
  chatroomTotal: 0,
  chatgroup: {
    page: 1,
    size: 10,
  },
  chatgroupTotal: 0,
})

// 翻页控制
const handleChange = (val) => {
  if (activeTab.value == 'contact') {
    pageCtrl.contact.page = val;
    getContacts();
  }
  if (activeTab.value == 'chatgroup') {
    pageCtrl.chatgroup.page = val;
    getChatgroups()
  }
  if (activeTab.value == 'chatroom') {
    pageCtrl.chatroom.page = val;
    getChatrooms()
  }
}


// 刷新
const refresh = (type) => {
  if (type == 'contact') {
    debounce(getContacts, 800, true)()
  }
  if (type == 'chatgroup') {
    debounce(getChatgroups, 800, true)()
  }
  if (type == 'chatroom') {
    debounce(getChatrooms, 800, true)()
  }
}

// 获取最近联系人列表
const getRecent = async () => {
  try {
    loadingCtrl.recent = true
    const res = await WebIM.conn.getSessionList()
    if (!res.data.channel_infos || res.data.channel_infos.length == 0) {
      throw new Error('无最近联系人')
    }
    console.log(res);
    let arr = res.data.channel_infos
    console.log(arr);
    // 数据转换
    let chatroomArr = []
    let contactArr = []
    // let totalUnread = 0
    arr.forEach((item, index, arr) => {
      arr[index].meta.payload = JSON.parse(arr[index].meta.payload)
      if (arr[index].channel_id.includes('@conference')) {
        chatroomArr.push(arr[index].channel_id.match(/_(\S*)@/)[1])
        arr[index].targetId = arr[index].channel_id.match(/_(\S*)@/)[1]
        arr[index].isChatroom = true
      } else {
        contactArr.push(arr[index].channel_id.match(/_(\S*)@/)[1])
        arr[index].targetId = arr[index].channel_id.match(/_(\S*)@/)[1]
        arr[index].isChatroom = false
      }
      // totalUnread += arr[index].unread_num
    });
    console.log(arr)
    console.log(chatroomArr)
    console.log(contactArr)
    // store.commit('setRecentTabNotice', totalUnread)
    await Promise.all([
      appContext.config.globalProperties.$API.imApi.getContactById.post({ targetIds: contactArr }),
      appContext.config.globalProperties.$API.imApi.getChatroomById.post({ targetIds: chatroomArr })
    ])
      .then((res) => {
        console.log(res)
        // 通过czType来判断目标类型 0 联系人、 1 群 、 2 聊天室 
        Object.keys(res).forEach(key => {
          if (res[key].data) {
            res[key].data.forEach(item => {
              arr.some(target => {
                if (target.targetId == item.targetId && target.isChatroom) {
                  target.avatar = item.avatar
                  target.chatRoomName = item.nickName
                  target.chatRoomId = item.czimId
                  target.czType = item.czType
                  target.isProhibit = item.isProhibit
                  target.maxusers = item.maxusers
                  return true
                } else if (target.targetId == item.targetId && !target.isChatroom) {
                  target.avatar = item.avatar
                  target.nickName = item.nickName
                  target.czType = item.czType
                }
              })
            })
          }
        })
        console.log(arr);
      })
    // commit to Vuex
    store.commit('setRecent', arr)
  } catch (err) {
    console.log(err);
  } finally {
    loadingCtrl.recent = false
  }
}

// 获取好友列表
const getContacts = async () => {
  loadingCtrl.contact = true
  const res = await appContext.config.globalProperties.$API.imApi.getContacts.post(pageCtrl.contact)
  console.log(res);
  loadingCtrl.contact = false
  if (res.code !== 0) {
    return ElMessage.warning('获取联系人失败')
  }
  store.commit('setContact', res.data.rows)
  pageCtrl.contactTotal = res.data.total
}

// 获取群聊列表
const getChatgroups = async () => {
  loadingCtrl.chatgroup = true
  const res = await appContext.config.globalProperties.$API.imApi.getChatGroup.post(pageCtrl.chatgroup)
  console.log(res);
  loadingCtrl.chatgroup = false
  if (res.code !== 0) {
    return ElMessage.warning('获取群聊失败')
  }
  // listData.chatgroups = res.data.rows
  store.commit('setChatgroup', res.data.rows)
  pageCtrl.chatgroupTotal = res.data.total
}

// 获取聊天室列表
const getChatrooms = async () => {
  loadingCtrl.chatroom = true
  const res = await appContext.config.globalProperties.$API.imApi.getChatroom.post(pageCtrl.chatroom)
  console.log(res);
  loadingCtrl.chatroom = false
  if (res.code !== 0) {
    return ElMessage.warning('获取聊天室失败')
  }
  store.commit('setChatroom', res.data.rows)
  pageCtrl.chatroomTotal = res.data.total
}

// 获取个人信息
const getMyInfo = async () => {
  const res = await WebIM.conn.fetchUserInfoById(store.state.user.userSecret.userId)
  console.log(res);
}

const currenChat = computed(() => store.state.user.currentChat || {})

const emit = defineEmits(['get-current-msg'])

// 选择聊天目标 同步到 VUEX
const selectTarget = (item) => {
  if (currenChat.value.targetId === item.targetId) {
    return;
  }
  // 设置当前聊天目标
  store.commit('setCurrentChat', item)
  if (activeTab.value == 'recent') {
    // 从服务器拉取最近30条聊天记录
    emit('get-current-msg', item.targetId, item.czType)
    store.dispatch('channelClearUnread', item)
  }
  if (activeTab.value == 'contact') {
    emit('get-current-msg', item.targetId, item.czType)
  }
  if (activeTab.value == 'chatgroup') {
    emit('get-current-msg', item.targetId, item.czType)
  }
  if (activeTab.value == 'chatroom') {
    emit('get-current-msg', item.targetId, item.czType)
  }
}

let timeout
function debounce(func, wait, immediate) {
  return function () {
    const context = this;
    const args = [...arguments];
    if (timeout) {
      clearTimeout(timeout);
    }
    if (immediate) {
      const callNow = !timeout;
      timeout = setTimeout(() => {
        timeout = null;
      }, wait)
      if (callNow) func.apply(context, args)
    }
    else {
      timeout = setTimeout(() => {
        func.apply(context, args)
      }, wait);
    }
  }
}

defineExpose({
  getContacts,
  getRecent,
  switchTab
})
</script>
<style lang="scss" scoped>
.el-tabs {
  height: 100%;
  border: 0;
  :deep(.el-tabs__content) {
    height: calc(100% - 40px);
    padding-left: 0;
    padding-right: 0;
    .el-tab-pane {
      height: calc(100% - 50px);
      .scroll-content {
        padding: 0 12px;
      }
    }
  }
}
:deep(.el-badge) {
  .el-badge__content {
    top: 10px;
    right: 3px;
  }
}

.contact-card {
  display: flex;
  border: 1px solid #e6e6e6;
  border-radius: 5px;
  min-height: 60px;
  padding: 10px;
  margin-bottom: 10px;
  align-items: center;
  cursor: pointer;
  position: relative;
  overflow: hidden;
  .details {
    flex: 1;
    display: flex;
    justify-content: space-between;
    margin-left: 10px;
    .type {
      position: absolute;
      bottom: 0;
      right: 0;
      width: 40px;
      height: 20px;
      border-top-left-radius: 5px;
      text-align: center;
      line-height: 20px;
      color: #fff;
      font-size: 10px;
      // border-top: 30px solid red;
      // border-left: 30px solid transparent;
    }
    .type.group {
      background: #409eff;
    }
    .type.room {
      background: #1abc9c;
    }
  }
  &:hover {
    box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);
    transition: 0.3s;
  }
}
.contact-card.select {
  // border: 1px solid #22b130;
  box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);
  background: linear-gradient(to right, #fff, #e0eafc, #cfdef3);
}

[data-theme="dark"] .contact-card.select {
  background: linear-gradient(to right, #83a4d4, #b6fbff);
  color: black;
}
.kxTable-pagination-box {
  height: 50px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 15px;
  border-top: 1px solid #e6e6e6;
}
</style>