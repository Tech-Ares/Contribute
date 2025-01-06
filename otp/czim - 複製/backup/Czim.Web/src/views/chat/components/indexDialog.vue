<template>
  <el-dialog v-model="dialogVisible" title="禁言用户" width="35%" destroy-on-close @close="closeDialog">
    <strong>提示: 操作禁言后，该用户在一段时间内将无法在该群聊(聊天室)发言。禁言天数填0点击执行，将为用户解除禁言。</strong>
    <el-descriptions v-loading="loadingCtrl.loading">
      <el-descriptions-item label="昵称:">{{ forbidUser.nickName }}</el-descriptions-item>
      <el-descriptions-item label="状态:">
        <el-tag type="danger" v-if="forbidUser.info.result === 1">已禁言</el-tag>
        <el-tag type="success" v-else-if="forbidUser.info.result === 0">正常</el-tag>
        <el-tag type="danger" v-else>此用户已被移出群聊(聊天室)</el-tag>
      </el-descriptions-item>
      <el-descriptions-item
        v-if="forbidUser.info.result === 1"
        label="剩余时间:"
      >{{ forbidUser.info.time }}</el-descriptions-item>
    </el-descriptions>
    <el-form
      ref="forbidForm"
      :model="groupForm"
      label-position="left"
      :rules="rules"
      v-if="dialogMode == 'banGroup'"
    >
      <el-form-item label="禁言范围：" prop="range">
        <el-radio-group v-model="groupForm.range">
          <el-radio-button label="current">当前</el-radio-button>
          <el-radio-button label="global">全局</el-radio-button>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="禁言天数：" prop="day">
        <el-input-number v-model="groupForm.day" :step="1" :min="0" />
      </el-form-item>
    </el-form>
    <el-form
      ref="forbidForm"
      :model="form"
      label-position="left"
      :rules="rules"
      v-if="dialogMode == 'banRoom'"
    >
      <el-form-item label="禁言范围：" prop="range">
        <el-radio-group v-model="form.range">
          <el-radio-button label="current">当前</el-radio-button>
          <el-radio-button label="global">全局</el-radio-button>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="禁言天数：" prop="day">
        <el-input-number v-model="form.day" :step="1" :min="0" />
      </el-form-item>
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="closeDialog">取消</el-button>
        <el-button
          type="primary"
          v-if="dialogMode == 'banRoom' && forbidUser.info.result !== 2"
          @click="handleForbid"
          :loading="loadingCtrl.handle"
        >执行</el-button>
        <el-button
          type="primary"
          v-if="dialogMode == 'banGroup' && forbidUser.info.result !== 2"
          @click="handleGroupForbid"
        >执行</el-button>
      </span>
    </template>
  </el-dialog>
</template>
<script setup>
import { ElMessage } from 'element-plus';
import { reactive, ref, getCurrentInstance, computed } from 'vue';
import { useStore } from 'vuex';

// 获取全局API
const { appContext } = getCurrentInstance()

// 获取store
const store = useStore()

const rules = reactive({
  day: [
    {
      required: true,
      message: '请输入禁言时长',
      trigger: 'blur',
    },
    {
      pattern: /(^0$)|(^[1-9]+[0-9]*$)/,
      message: '请输入大于0的整数',
      trigger: 'blur',
    }
  ],
  range: [
    {
      required: true,
      message: '请选择',
      trigger: 'blur',
    },
  ]
})

const dialogVisible = ref(false)
const dialogMode = ref('')
const loadingCtrl = reactive({
  loading: false,
  handle: false
})
// const loading = ref(false)
const open = (item, mode = 'banRoom') => {
  dialogMode.value = mode
  forbidUser.nickName = item.ext.nickName
  if (mode == 'banRoom') {
    form.memberId = item.from
  } else {
    groupForm.memberId = item.from
  }
  dialogVisible.value = true
}

const forbidForm = ref()

const form = reactive({
  chatRoomId: computed(() => store.state.user.currentChat.chatRoomId || ''),
  memberId: '',
  day: 0,
  range: 'current'
})

const groupForm = reactive({
  groupId: computed(() => store.state.user.currentChat.chatRoomId || store.state.user.currentChat.chatgroupId),
  memberId: '',
  day: 0,
  range: 'current'
})

const forbidUser = reactive({
  info: {},
  nickName: ''
})

// 获取聊天室用户禁言状态
const getUserStatus = async () => {
  loadingCtrl.loading = true
  const res = await appContext.config.globalProperties.$API.imApi.getForbidState.post({ chatRoomId: form.chatRoomId, memberId: form.memberId })
  loadingCtrl.loading = false
  if (res.code !== 0) {
    return ElMessage.error('获取用户状态失败')
  }
  console.log(res)
  forbidUser.info = res.data
}

// 聊天室禁言操作
const handleForbid = async () => {
  loadingCtrl.handle = true
  if (groupForm.range === 'current') {
    try {
      await forbidForm.value.validate()
      const res = await appContext.config.globalProperties.$API.imApi.memberForbidden.post(form)
      console.log(res)
      if (form.day == 0) {
        ElMessage.success(`已为 用户：${forbidUser.nickName} 解除禁言`)
      } else if (form.day > 0) {
        ElMessage.success(`禁言 用户：${forbidUser.nickName} ${form.day}天`)
      }
      closeDialog()
    } catch (error) {
      console.log(error)
    } finally {
      loadingCtrl.handle = false
    }
  } else {
    handleGlobalForbid()
  }
}

// 获取群聊用户禁言状态
const getGroupUserStatus = async () => {
  loadingCtrl.loading = true
  const res = await appContext.config.globalProperties.$API.imApi.getForbidStateGroup.post({ chatGroupId: groupForm.groupId, memberId: groupForm.memberId })
  loadingCtrl.loading = false
  if (res.code !== 0) {
    return ElMessage.error('获取群聊用户状态失败')
  }
  console.log(res)
  forbidUser.info = res.data
}

// 群聊禁言操作
const handleGroupForbid = async () => {
  loadingCtrl.handle = true
  if (groupForm.range === 'current') {
    try {
      await forbidForm.value.validate()
      const res = await appContext.config.globalProperties.$API.imApi.memberForbiddenGroup.post(groupForm)
      console.log(res)
      if (groupForm.day == 0) {
        ElMessage.success(`已为 用户：${forbidUser.nickName} 解除禁言`)
      } else if (groupForm.day > 0) {
        ElMessage.success(`禁言 用户：${forbidUser.nickName} ${groupForm.day}天`)
      }
      closeDialog()
    } catch (error) {
      console.log(error)
    } finally {
      loadingCtrl.handle = false
    }
  } else {
    handleGlobalForbid()
  }
}

// 全局禁言！！！！
const handleGlobalForbid = async () => {
  if (dialogMode.value === 'banGroup') {
    try {
      await forbidForm.value.validate()
      const res = await appContext.config.globalProperties.$API.imApi.globalProhibit.post({ memberId: groupForm.memberId, day: groupForm.day })
      console.log(res)
      if (groupForm.day == 0) {
        ElMessage.success(`已为 用户：${forbidUser.nickName} 解除全局禁言`)
      } else if (groupForm.day > 0) {
        ElMessage.success(`全局禁言 用户：${forbidUser.nickName} ${groupForm.day}天`)
      }
      closeDialog()
    } catch (error) {
      console.log(error)
    } finally {
      loadingCtrl.handle = false
    }
  } else {
    try {
      await forbidForm.value.validate()
      const res = await appContext.config.globalProperties.$API.imApi.globalProhibit.post({ memberId: form.memberId, day: form.day })
      console.log(res)
      if (form.day == 0) {
        ElMessage.success(`已为 用户：${forbidUser.nickName} 解除全局禁言`)
      } else if (form.day > 0) {
        ElMessage.success(`全局禁言 用户：${forbidUser.nickName} ${form.day}天`)
      }
      closeDialog()
    } catch (error) {
      console.log(error)
    } finally {
      loadingCtrl.handle = false
    }
  }
}

// 关闭时清空数据
const closeDialog = () => {
  form.day = 0
  form.range = 'current'
  groupForm.day = 0
  groupForm.range = 'current'
  dialogVisible.value = false
}

defineExpose({
  open,
  getUserStatus,
  getGroupUserStatus
})
</script>
<style lang="scss" scoped>
.el-descriptions {
  margin-top: 40px;
}
.el-form {
  margin-top: 40px;
  .el-form-item {
    align-items: center;
  }
}
</style>