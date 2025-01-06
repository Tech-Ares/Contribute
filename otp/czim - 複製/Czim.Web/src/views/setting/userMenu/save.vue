<template>
  <el-container v-loading="cloading">
    <el-header v-if="menuTree.length">
      当前操作角色：{{ currentAuditData.name }}
      <el-button type="primary" @click="onSave">保存</el-button>
    </el-header>
    <el-main class="nopadding">
      <el-tree
        ref="menu"
        node-key="menuId"
        :data="menuTree"
        :props="defaultProps"
        default-expand-all
        highlight-current
        check-strictly
        empty-text="点击操作查看角色菜单权限"
      >
        <template #default="{ node, data }">
          <div class="custom-tree-node el-tree-node__label">
            <span class="label">{{ node.label }}</span>
            <div class="tree-node-checkbox">
              <el-checkbox v-for="item in data.functions" :key="item.funcId" v-model="item.isSelected">{{ item.title }}</el-checkbox>
            </div>
          </div>
        </template>
      </el-tree>
    </el-main>
  </el-container>
</template>
<script setup>
import { ElMessage } from 'element-plus';
import { getCurrentInstance, defineExpose, ref } from 'vue';

// 获取全局API
const { appContext } = getCurrentInstance()

// loading
const cloading = ref(false)

// 菜单树配置
const defaultProps = {
  children: 'children',
  label: 'title',
}

// 菜单树
const menuTree = ref([])

// 选中的节点
// const checkedMenu = ref([])

// 根据 roleId 获取树
const getTree = async (id) => {
  cloading.value = true
  const res = await appContext.config.globalProperties.$API.sysRole.getRoleMenuById.get(id)
  cloading.value = false
  console.log(res);
  if (res.code !== 0) {
    return ElMessage.warning('获取数据失败')
  }
  menuTree.value = res.data.list
  // checkedMenu.value = res.data.selectMenu
}

// 提交更新后的 tree 给服务器
const onSave = async () => {
  cloading.value = true
  console.log(menuTree.value);
  const res = await appContext.config.globalProperties.$API.sysRole.updateRoleMenu.post({ roleId: currentAuditData.value.id, roleMenuFunctionList: menuTree.value })
  cloading.value = false
  console.log(res);
  if (res.code !== 0) {
    return ElMessage.warning('操作失败')
  }
  ElMessage.success('更新权限成功')
}

// 当前用户信息
const currentAuditData = ref({})

// 从父组件获取当前用户信息
const setData = (data) => {
  currentAuditData.value = data
}

// 暴露子组件方法给父组件调用
defineExpose({
  getTree,
  setData
})
</script>
<style lang="scss" scoped>
.el-main {
  padding: 0 !important;
}
.el-tree {
  width: 100%;
  :deep(.el-tree-node__content) {
    min-height: 50px;
  }
  .custom-tree-node {
    display: flex;
    flex: 1;
    align-items: center;
    justify-content: space-between;
    font-size: 14px;
    padding-right: 24px;
    height: 100%;
    .label {
      display: flex;
      align-items: center;
      height: 100%;
      color: rgb(15, 109, 185);
    }
    .tree-node-checkbox {
      display: flex;
      justify-content: flex-end;
      :deep(.el-checkbox) {
        width: 40px;
      }
    }
  }
}
</style>
