<template>
  <el-dialog v-model="dialogVisible" :title="tableStatus.title" width="50%" destroy-on-close>
    <el-card shadow="never">
      <div class="kxTable-table" v-loading="tableStatus.loading">
        <el-table :data="memberList" ref="kxTable" style="width: 100%" :height="350" stripe>
          <el-table-column prop="nickName" label="昵称" width="90" fixed="left" />
          <el-table-column prop="avatar" label="头像" width="90">
            <template #default="scope">
              <el-avatar :src="scope.row.avatar" :size="40"></el-avatar>
            </template>
          </el-table-column>
          <el-table-column prop="isManager" label="权限" width="100">
            <template #default="scope">
              <el-tag v-if="scope.row.isManager == 1" type="warning">管理员</el-tag>
              <el-tag v-else-if="scope.row.isManager == 0">会员</el-tag>
            </template>
          </el-table-column>
          <el-table-column prop="isForbidden" label="禁言状态" width="90">
            <template #default="scope">
              <el-tag v-if="scope.row.isForbidden == 1" type="warning">是</el-tag>
              <el-tag v-else-if="scope.row.isForbidden == 0">否</el-tag>
            </template>
          </el-table-column>
          <el-table-column prop="loginName" label="登录名" />
          <el-table-column prop="joinDate" label="进群时间" />
          <template #empty>
            <el-empty description="暂无数据" :image-size="100"></el-empty>
          </template>
        </el-table>
      </div>
    </el-card>
    <template #footer>
      <div class="kxTable-pagination-box">
        <!-- 分页器 -->
        <el-pagination
          background
          :small="true"
          layout="total, sizes, prev, pager, next, jumper"
          :total="total"
          :page-sizes="[10, 20, 50, 100, 200]"
          v-model:currentPage="queryMember.page"
          @current-change="handleCurrentChange"
          @size-change="handleSizeChange"
        ></el-pagination>
        <!-- 刷新 -->
        <div class="kxTable-do">
          <el-button @click="refresh" icon="el-icon-refresh" circle style="margin-left: 15px"></el-button>
        </div>
      </div>
    </template>
  </el-dialog>
</template>
<script setup>
import { ref, getCurrentInstance, reactive, computed } from 'vue';
import { useStore } from 'vuex';
import { ElMessage } from 'element-plus';

// 获取全局API
const { appContext } = getCurrentInstance()
// 引入 Vuex
const store = useStore()
const dialogVisible = ref(false)

// 聊天室控制器
const queryMember = reactive({
  size: 10,
  page: 1,
  chatroomName: computed(() => store.state.user.currentChat.chatRoomName),
  chatRoomId: computed(() => store.state.user.currentChat.chatRoomId)
})

// 群组控制器
const queryGroupMember = reactive({
  size: 10,
  page: 1,
  groupName: computed(() => store.state.user.currentChat.chatRoomName || store.state.user.currentChat.groupName),
  groupId: computed(() => store.state.user.currentChat.chatRoomId || store.state.user.currentChat.chatgroupId)
})

// 表格状态管理
const tableStatus = reactive({
  loading: false,
  title: '',
  mode: ''
})

// 打开成员表格
const open = (mode) => {
  tableStatus.mode = mode
  if (mode == 'chatroom') {
    tableStatus.title = '聊天室成员'
  } else if (mode == 'group') {
    tableStatus.title = '群聊成员'
  } else {
    return ElMessage.error('访问被拒绝')
  }
  dialogVisible.value = true
}

// 表格实例
const kxTable = ref()
const memberList = ref([])
const total = ref(0)

// 获取当前聊天室成员方法
const fetchChatroomMemberList = async () => {
  tableStatus.loading = true
  const res = await appContext.config.globalProperties.$API.imApi.getChatroomMember.post(queryMember)
  console.log(res)
  tableStatus.loading = false
  if (res.code !== 0) {
    return ElMessage.error('获取群成员列表失败')
  }
  memberList.value = res.data.dataSource
  total.value = res.data.count
  kxTable.value.$el.querySelector('.el-table__body-wrapper').scrollTop = 0
}

// 获取当前群组成员方法
const fetchGroupMemberList = async () => {
  tableStatus.loading = true
  const res = await appContext.config.globalProperties.$API.imApi.getGroupMemberList.post(queryGroupMember)
  tableStatus.loading = false
  console.log(res);
  memberList.value = res.data.rows
  total.value = res.data.total
}

// 翻页控制
const handleCurrentChange = (val) => {
  if (tableStatus.mode == 'chatroom') {
    queryMember.page = val
    fetchChatroomMemberList()
  } else {
    queryGroupMember.page = val
    fetchGroupMemberList()
  }
}
// 每页条数控制
const handleSizeChange = (val) => {
  if (tableStatus.mode == 'chatroom') {
    queryMember.size = val;
    queryMember.page = 1;
    fetchChatroomMemberList()
  } else {
    queryGroupMember.size = val
    queryGroupMember.page = 1
    fetchGroupMemberList()
  }
}
// 刷新表格
const refresh = () => {
  if (tableStatus.mode == 'chatroom') {
    fetchChatroomMemberList()
  } else {
    fetchGroupMemberList()
  }
}

defineExpose({
  open,
  fetchChatroomMemberList,
  fetchGroupMemberList
})
</script>
<style lang="scss" scoped>
.kxTable-table {
  height: 100%;
}
.kxTable-pagination-box {
  display: flex;
  justify-content: center;
}
</style>