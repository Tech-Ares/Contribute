<template>
  <el-container>
    <el-aside width="350px">
      <el-container>
        <el-header>
          <div class="right-panel-search">
            <el-input placeholder="用户名称" v-model="searchList.name" clearable></el-input>
            <el-button type="primary" icon="el-icon-search" @click="getData"></el-button>
            <el-button
              type="info"
              plain
              icon="el-icon-refresh"
              @click="resetSearch"
              :disabled="searchList.name.length < 1"
            ></el-button>
          </div>
        </el-header>
        <el-main class="nopadding">
          <div class="kxTable-table" v-loading="loading">
            <el-table
              :data="tableData"
              ref="kxTable"
              row-key="id"
              style="width: 100%"
              :height="height == 'auto' ? null : '100%'"
              stripe
            >
              <el-table-column prop="name" label="用户名称" width="90" />
              <el-table-column prop="remark" label="说明" />
              <el-table-column fixed="right" label="操作" width="50">
                <template #default="scope">
                  <el-button
                    type="text"
                    size="small"
                    @click="table_edit(scope.row, scope.$index)"
                  >操作</el-button>
                </template>
              </el-table-column>
              <template #empty>
                <el-empty :description="emptyText" :image-size="100"></el-empty>
              </template>
            </el-table>
          </div>
          <div class="kxTable-pagination-box">
            <el-pagination
              background
              :small="true"
              layout="total, sizes, prev, pager, next"
              :total="total"
              :page-sizes="[10, 20, 50, 100]"
              v-model:currentPage="searchList.page"
              @current-change="handleCurrentChange"
              @size-change="handleSizeChange"
            ></el-pagination>
            <div class="kxTable-do">
              <el-button @click="refresh" icon="el-icon-refresh" circle style="margin-left:15px"></el-button>
            </div>
          </div>
        </el-main>
      </el-container>
    </el-aside>
    <el-container>
      <el-main class="nopadding" ref="main">
        <save ref="saveCom"></save>
      </el-main>
    </el-container>
  </el-container>
</template>
<script setup>
import { getCurrentInstance, onMounted, reactive, ref } from 'vue'
import { ElMessage } from 'element-plus'
import save from './save'

// 获取全局API
const { appContext } = getCurrentInstance()
// ref 获取组件实例
const kxTable = ref()
const saveCom = ref()

// loading
let loading = ref(false)

// 死数据
const height = '100%'
const emptyText = "暂无数据"

// 表格数据 
const tableData = ref([])

// 总条数
const total = ref(0)

// 查询对象
let searchList = reactive({
  name: '',
  size: 10,
  page: 1
})

// 加载用户列表
const getData = async () => {
  loading.value = true;
  const res = await appContext.config.globalProperties.$API.sysRole.getSysRole.post(searchList)
  console.log(res);
  loading.value = false;
  if (res.code !== 0) {
    return ElMessage.error("获取数据失败");
  }
  tableData.value = res.data.rows;
  searchList.size = res.data.pageSize
  total.value = res.data.total
  searchList.page = res.data.page
}

onMounted(() => {
  getData()
})

// 获取用户菜单，ref 调用子组件方法
const table_edit = (row) => {
  saveCom.value.getTree(row.id)
  saveCom.value.setData(row)
}

// 翻页控制
const handleCurrentChange = (val) => {
  searchList.page = val;
  getData();
}
// 每页条数控制
const handleSizeChange = (val) => {
  searchList.size = val;
  searchList.page = 1;
  getData();
}
// 刷新表格
const refresh = () => {
  kxTable.value.clearSelection();
  getData();
}
// 重置搜索
const resetSearch = () => {
  for (let key in searchList) {
    console.log(key);
    if (key == "size") {
      searchList[key] = 10;
    } else if (key == "page") {
      searchList[key] = 1;
    } else {
      searchList[key] = '';
    }
  }
  getData();
}
</script>

<style lang="scss" scoped>
.kxTable-table {
  height: calc(100% - 50px);
}
.kxTable-pagination-box {
  height: 50px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 15px;
}
</style>
