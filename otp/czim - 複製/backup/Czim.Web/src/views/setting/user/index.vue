<template>
  <!-- <el-container> -->
  <!-- <el-aside width="200px" v-loading="showGrouploading">
			<el-container>
				<el-header>
					<el-input placeholder="输入关键字进行过滤" v-model="groupFilterText" clearable></el-input>
				</el-header>
				<el-main class="nopadding">
					<el-tree ref="group" class="menu" node-key="id" :data="group" :current-node-key="''" :highlight-current="true" :expand-on-click-node="false" :filter-node-method="groupFilterNode" @node-click="groupClick"></el-tree>
				</el-main>
			</el-container>
  </el-aside>-->
  <el-container>
    <el-header>
      <div class="left-panel">
        <el-button type="primary" icon="el-icon-plus" @click="add"></el-button>
        <el-button
          type="danger"
          plain
          icon="el-icon-delete"
          :disabled="selection.length == 0"
          @click="batch_del"
        ></el-button>
      </div>
      <div class="right-panel">
        <div class="right-panel-search">
          <el-input v-model="searchList.name" placeholder="用户名" clearable></el-input>
          <el-input v-model="searchList.loginName" placeholder="登录账号" clearable></el-input>
          <el-button type="primary" icon="el-icon-search" @click="getData"></el-button>
          <el-button
            type="info"
            plain
            icon="el-icon-refresh"
            @click="resetSearch"
            :disabled="
              searchList.name.length < 1 && searchList.loginName.length < 1
            "
          ></el-button>
        </div>
      </div>
    </el-header>
    <el-main class="nopadding">
      <!-- <scTable ref="table" :apiObj="apiObj" @selection-change="selectionChange" stripe remoteSort remoteFilter>
						<el-table-column type="selection" width="50"></el-table-column>
						<el-table-column label="ID" prop="id" width="80" sortable='custom'></el-table-column>
						<el-table-column label="头像" width="80" column-key="filterAvatar" :filters="[{text: '已上传', value: '1'}, {text: '未上传', value: '0'}]">
							<template #default="scope">
								<el-avatar :src="scope.row.avatar" size="small"></el-avatar>
							</template>
						</el-table-column>
						<el-table-column label="登录账号" prop="userName" width="150" sortable='custom' column-key="filterUserName" :filters="[{text: '系统账号', value: '1'}, {text: '普通账号', value: '0'}]"></el-table-column>
						<el-table-column label="姓名" prop="name" width="150" sortable='custom'></el-table-column>
						<el-table-column label="所属角色" prop="groupName" width="200" sortable='custom'></el-table-column>
						<el-table-column label="加入时间" prop="date" width="150" sortable='custom'></el-table-column>
						<el-table-column label="操作" fixed="right" align="right" width="140">
							<template #default="scope">
								<el-button type="text" size="small" @click="table_show(scope.row, scope.$index)">查看</el-button>
								<el-button type="text" size="small" @click="table_edit(scope.row, scope.$index)">编辑</el-button>
								<el-popconfirm title="确定删除吗？" @confirm="table_del(scope.row, scope.$index)">
									<template #reference>
										<el-button type="text" size="small">删除</el-button>
									</template>
								</el-popconfirm>
							</template>
						</el-table-column>
      </scTable>-->
      <div class="kxTable" v-loading="loading">
        <kxTable
          :data="tableData"
          ref="kxTable"
          rowKey="id"
          :total="total"
          :loading="loading"
          :stripe="true"
          @handleCurrentChange="handleCurrentChange"
          @handleSizeChange="handleSizeChange"
          @refresh="refresh"
          @selection-Change="selectionChange"
        >
          <el-table-column type="selection" width="50"></el-table-column>
          <el-table-column type="index" label="#" width="50" />
          <el-table-column prop="name" label="用户名称" width="150" />
          <el-table-column prop="loginName" label="登录名" width="150" />
          <el-table-column prop="roles" label="所属角色" />
          <el-table-column prop="organizationName" label="所属组织" />
          <el-table-column prop="phone" label="手机号" />
          <el-table-column fixed="right" label="操作" width="140">
            <template #default="scope">
              <el-button type="text" size="small" @click="table_edit(scope.row, scope.$index)">编辑</el-button>
              <el-divider direction="vertical"></el-divider>
              <el-popconfirm title="确定删除吗？" @confirm="table_del(scope.row, scope.$index)">
                <template #reference>
                  <el-button type="text" size="small" :style="{ color: '#f56c6c' }">删除</el-button>
                </template>
              </el-popconfirm>
            </template>
          </el-table-column>
        </kxTable>
      </div>
      <!-- <el-table
          :data="tableData"
          ref="kxTable"
          row-key="id"
          style="width: 100%"
          :height="height == 'auto' ? null : '100%'"
          stripe
          @selection-change="selectionChange"
        >
          <el-table-column type="selection" width="50"></el-table-column>
          <el-table-column type="index" label="#" width="50" />
          <el-table-column prop="name" label="用户名称" width="150" />
          <el-table-column prop="loginName" label="登录名" width="150" />
          <el-table-column prop="roles" label="所属角色" />
          <el-table-column prop="organizationName" label="所属组织" />
          <el-table-column prop="phone" label="手机号" />
          <el-table-column fixed="right" label="操作" width="140">
            <template #default="scope">
              <el-button
                type="text"
                size="small"
                @click="table_edit(scope.row, scope.$index)"
                >编辑</el-button
              >
              <el-divider direction="vertical"></el-divider>
              <el-popconfirm
                title="确定删除吗？"
                @confirm="table_del(scope.row, scope.$index)"
              >
                <template #reference>
                  <el-button
                    type="text"
                    size="small"
                    :style="{ color: '#f56c6c' }"
                    >删除</el-button
                  >
                </template>
              </el-popconfirm>
            </template>
          </el-table-column>
          <template #empty>
            <el-empty :description="emptyText" :image-size="100"></el-empty>
          </template>
        </el-table> -->
      <!-- </div> -->
      <!-- <div class="kxTable-pagination-box">
        <el-pagination
          background
          :small="true"
          layout="total, sizes, prev, pager, next, jumper"
          :total="total"
          :page-sizes="[10, 20, 50, 100]"
          v-model:currentPage="searchList.page"
          @current-change="handleCurrentChange"
          @size-change="handleSizeChange"
        ></el-pagination>
        <div class="kxTable-do">
          <el-button
            @click="refresh"
            icon="el-icon-refresh"
            circle
            style="margin-left: 15px"
          ></el-button>
        </div>
      </div> -->
    </el-main>
  </el-container>
  <save-dialog
    v-if="dialog.save"
    ref="saveDialog"
    @success="handleSuccess"
    @closed="dialog.save = false"
  ></save-dialog>
</template>

<script>
import saveDialog from "./save";

export default {
  name: "user",
  components: {
    saveDialog,
  },
  data() {
    return {
      dialog: {
        save: false,
      },
      showGrouploading: false,
      selection: [],
      tableData: [],
      height: "100%",
      total: 0,
      searchList: {
        name: "",
        loginName: "",
        organizationId: "",
        size: 10,
        page: 1,
      },
      loading: false,
      emptyText: "暂无数据",
    };
  },
  watch: {
    groupFilterText(val) {
      this.$refs.group.filter(val);
    },
  },
  mounted() {
    // console.log('触发');
    this.getData();
  },
  methods: {
    // 获取用户列表
    async getData() {
      this.loading = true;
      const res = await this.$API.sysUser.findList.post(this.searchList);
      console.log(res);
      this.loading = false;
      if (res.code !== 0) {
        return this.$message.error("获取数据失败");
      }
      this.tableData = res.data.rows;
      this.searchList.size = res.data.pageSize;
      this.total = res.data.total;
      this.searchList.page = res.data.page;
    },
    // 翻页控制
    handleCurrentChange(val) {
      this.searchList.page = val;
      this.getData();
    },
    // 每页条数控制
    handleSizeChange(val) {
      this.searchList.size = val;
      this.searchList.page = 1;
      this.getData();
    },
    // 刷新表格
    refresh() {
      // this.$refs.kxTable.clearSelection();
      this.getData();
    },
    //添加
    add() {
      this.dialog.save = true;
      this.$nextTick(() => {
        this.$refs.saveDialog.open().setData("");
      });
    },
    //编辑
    table_edit(row) {
      this.dialog.save = true;
      this.$nextTick(() => {
        this.$refs.saveDialog.open("edit").setData(row.id);
      });
    },
    //删除角色
    async table_del(row) {
      this.loading = true;
      const res = await this.$API.sysUser.deleteUser.post([row.id]);
      this.loading = false;
      if (res.code == 0) {
        this.getData();
        this.$message.success("删除成功");
      } else {
        this.$alert(res.message, "提示", { type: "error" });
      }
    },
    //批量删除
    async batch_del() {
      this.$confirm(
        `确定删除选中的 ${this.selection.length} 项吗？如果删除项中含有子集将会被一并删除`,
        "提示",
        {
          type: "warning",
        }
      )
        .then(async () => {
          this.loading = true;
          const res = await this.$API.sysUser.deleteUser.post(
            this.selection.map((item) => item.id)
          );
          if (res.code !== 0) {
            Promise.reject(res.message);
          }
          this.getData();
          this.$message.success("操作成功");
        })
        .catch((err) => {
          console.log(err);
        });
    },
    //表格选择后回调事件
    selectionChange(selection) {
      this.selection = selection;
    },
    // 搜索
    searchRole() {
      this.getData();
    },
    // 重置搜索
    resetSearch() {
      for (let key in this.searchList) {
        console.log(key);
        if (key == "size") {
          this.searchList[key] = 10;
        } else if (key == "page") {
          this.searchList[key] = 1;
        } else {
          this.searchList[key] = "";
        }
      }
      this.getData();
    },
    //加载树数据
    // async getGroup() {
    // 	this.showGrouploading = true;
    // 	var res = await this.$API.system.role.list.get();
    // 	this.showGrouploading = false;
    // 	var allNode = { id: '', label: '所有' }
    // 	res.data.unshift(allNode);
    // 	this.group = res.data;
    // },
    //树过滤
    // groupFilterNode(value, data) {
    // 	if (!value) return true;
    // 	return data.label.indexOf(value) !== -1;
    // },
    //树点击事件
    // groupClick(data) {
    // 	var params = {
    // 		groupId: data.id
    // 	}
    // 	this.$refs.table.reload(params)
    // },
    //搜索
    // upsearch() {
    // 	this.$refs.table.upData(this.search)
    // },
    //本地更新数据
    handleSuccess() {
      this.refresh();
      // if (mode == 'add') {
      // 	data.id = new Date().getTime()
      // 	this.$refs.table.tableData.unshift(data)
      // } else if (mode == 'edit') {
      // 	this.$refs.table.tableData.filter(item => item.id === data.id).forEach(item => {
      // 		Object.assign(item, data)
      // 	})
      // }
    },
  },
};
</script>

<style lang="scss" scoped>
.kxTable {
  height: 100%;
}
:deep(.kxTable-table) {
  height: calc(100% - 50px);
}
:deep(.kxTable-pagination-box) {
  height: 50px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 15px;
}
</style>
