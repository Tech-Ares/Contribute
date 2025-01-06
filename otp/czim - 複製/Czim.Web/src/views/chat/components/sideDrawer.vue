<template>
  <div>
    <el-drawer v-model="drawer" :title="text" direction="rtl" destroy-on-close>
      <el-container v-loading="loadingCtrl.loading">
        <el-main>
          <el-card shadow="always">
            <el-avatar
              :size="40"
              :src="targetInfo.avatar || info.chatroom.chatRoomImg || info.chatgroup.groupImg || `//cube.elemecdn.com/9/c2/f0ee8a3c7c9638a54940382568c9dpng.png`"
            ></el-avatar>
            <div class="info">
              <span
                class="name"
                v-text="targetInfo.nickName || info.chatroom.chatRoomName || info.chatgroup.groupName || '未命名'"
              ></span>
              <p
                class="notice"
                v-if="text == '聊天室信息'"
              >聊天室公告：{{ info.chatroom.chatRoomNotice || info.chatgroup.description }}</p>
              <p class="notice" v-else-if="text == '群聊信息'">群公告：{{ info.chatgroup.description }}</p>
            </div>
          </el-card>
          <ul class="handle-box">
            <li v-if="text == '聊天室信息'" @click="fetchChatroomMember">
              <span>聊天室成员</span>
              <el-icon>
                <el-icon-arrow-right />
              </el-icon>
            </li>
            <li v-if="text == '群聊信息'" @click="fetchGroupMember">
              <span>群聊成员</span>
              <el-icon>
                <el-icon-arrow-right />
              </el-icon>
            </li>
            <li v-if="text == '聊天室信息'">
              <span>聊天室全员禁言</span>
              <el-switch
                v-model="info.chatroom.isProhibit"
                :loading="loadingCtrl.switching"
                :before-change="switchProhibit"
              ></el-switch>
            </li>
            <li v-if="text == '群聊信息'">
              <span>群聊全员禁言</span>
              <el-switch
                v-model="info.chatgroup.isProhibit"
                :loading="loadingCtrl.switching"
                :before-change="switchGroupProhibit"
              ></el-switch>
            </li>
          </ul>
        </el-main>
        <el-footer></el-footer>
      </el-container>
    </el-drawer>
  </div>
  <drawer-dialog ref="drawerDia"></drawer-dialog>
</template>
<script setup>
import { computed, ref, getCurrentInstance, reactive } from 'vue';
import { useStore } from 'vuex';
import { ElMessage } from 'element-plus';
import drawerDialog from './drawerDialog.vue'

// 获取全局API
const { appContext } = getCurrentInstance()

const store = useStore()

const drawer = ref(false)

const text = ref('')

const targetInfo = computed(() => store.state.user.currentChat)

const loadingCtrl = reactive({
  loading: false,
  switching: false
})

const open = (mode = "contact") => {
  if (mode === "contact") {
    text.value = "联系人信息"
    drawer.value = true
  } else if (mode === "chatroom") {
    text.value = "聊天室信息"
    drawer.value = true
    fetchChatroomInfo(targetInfo.value.chatRoomId)
  } else {
    text.value = "群聊信息"
    drawer.value = true
    fetchGroupInfo(targetInfo.value.chatRoomId || targetInfo.value.chatgroupId)
  }
}

const info = reactive({
  chatroom: {},
  chatgroup: {}
})

const fetchChatroomInfo = async (id) => {
  loadingCtrl.loading = true
  const res = await appContext.config.globalProperties.$API.chatRoom.findForm.post(id)
  loadingCtrl.loading = false
  console.log(res);
  if (res.code !== 0) {
    return ElMessage.error('服务器异常，请刷新后重试!')
  }
  info.chatroom = res.data
}

const fetchGroupInfo = async (id) => {
  loadingCtrl.loading = true
  const res = await appContext.config.globalProperties.$API.imApi.getChatgroupById.post(id)
  loadingCtrl.loading = false
  console.log(res);
  if (res.code !== 0) {
    return ElMessage.error('服务器异常，请刷新后重试!')
  }
  info.chatgroup = res.data
}

const drawerDia = ref()

const fetchChatroomMember = () => {
  drawerDia.value.open('chatroom')
  drawerDia.value.fetchChatroomMemberList()
}

const fetchGroupMember = () => {
  drawerDia.value.open('group')
  drawerDia.value.fetchGroupMemberList()
}

// 控制聊天室全员禁言
const switchProhibit = async () => {
  if (targetInfo.value.czType !== 2) {
    ElMessage.error('请选中聊天室后重试!')
    return false
  }
  loadingCtrl.switching = true
  const res = await appContext.config.globalProperties.$API.imApi.chatroomProhibit.post({
    chatRoomId: info.chatroom.id,
    isProhibit: !info.chatroom.isProhibit
  })
  loadingCtrl.switching = false
  console.log(res);
  if (res.code !== 0) {
    ElMessage.error('服务器异常，请刷新后重试!')
    return false
  }
  ElMessage.success('操作成功')
  store.commit('changeProhibit', !info.chatroom.isProhibit)
  return true
}

// 控制群聊全员禁言
const switchGroupProhibit = async () => {
  console.log(targetInfo.value);
  if (targetInfo.value.czType !== 1) {
    ElMessage.error('请选中群聊后重试!')
    return false
  }
  loadingCtrl.switching = true
  const res = await appContext.config.globalProperties.$API.imApi.groupProhibit.post({
    chatGroupId: info.chatgroup.id,
    isProhibit: !info.chatgroup.isProhibit
  })
  loadingCtrl.switching = false
  console.log(res);
  if (res.code !== 0) {
    ElMessage.error('服务器异常，请刷新后重试!')
    return false
  }
  ElMessage.success('操作成功')
  store.commit('changeProhibit', !info.chatgroup.isProhibit)
  return true
}

defineExpose({
  open
})
</script>
<style lang="scss" scoped>
.el-card {
  background: linear-gradient(to right, #fff, #e0eafc, #cfdef3);
  :deep(.el-card__body) {
    display: flex;
    min-height: 100px;
    align-items: center;
    padding: 10px 20px;
    .el-avatar {
      margin-right: 15px;
    }
    .info {
      flex: 1;
      .name {
        font-size: 16px;
      }
      .notice {
        overflow: hidden;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
      }
    }
  }
}

.handle-box {
  list-style: none;
  margin-top: 15px;
  li {
    display: flex;
    justify-content: space-between;
    align-items: center;
    height: 50px;
    border: 1px solid #eee;
    border-radius: 6px;
    margin-bottom: 5px;
    cursor: pointer;
    &:hover {
      background: linear-gradient(to right, #fff, #e0eafc, #cfdef3);
      box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);
      transition: 0.3s;
    }
    span {
      margin-left: 12px;
    }
    .el-icon {
      margin-right: 12px;
    }
    .el-switch {
      margin-right: 12px;
    }
  }
}
[data-theme="dark"] .el-card {
  background: linear-gradient(to right, #83a4d4, #b6fbff);
  color: black;
}
[data-theme="dark"] .handle-box li:hover {
  background: linear-gradient(to right, #83a4d4, #b6fbff);
  color: black;
}
</style>
