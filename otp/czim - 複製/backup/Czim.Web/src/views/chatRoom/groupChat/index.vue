<template>
  <el-container>
    <el-header>
      <div class="left-panel">
        <!-- 添加 -->
        <el-button type="primary" icon="el-icon-plus" @click="add"></el-button>
        <!-- 删除 -->
        <!-- <el-button
          type="danger"
          plain
          icon="el-icon-delete"
          :disabled="selection.length == 0"
          @click="batch_del"
        ></el-button> -->
      </div>
      <div class="right-panel">
        <div class="right-panel-search">
          <!-- 搜索框 -->
          <el-input
            v-model="searchList.groupName"
            placeholder="群名称"
            clearable
          ></el-input>
          <!-- 查询 -->
          <el-button
            type="primary"
            icon="el-icon-search"
            @click="searchRole"
          ></el-button>
          <!-- 重置 -->
          <el-button
            type="info"
            plain
            icon="el-icon-refresh"
            @click="resetSearch"
            :disabled="searchList.groupName.length < 1"
          ></el-button>
        </div>
      </div>
    </el-header>
    <el-main class="nopadding">
      <div class="kxTable-table" v-loading="loading">
        <!-- 数据表 -->
        <el-table
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
          <el-table-column prop="nickName" label="群主" />

          <el-table-column prop="groupName" label="群名称" />
          <el-table-column prop="groupImg" label="封面"
            ><template #default="scope"
              ><el-image :src="scope.row.groupImg"></el-image></template
          ></el-table-column>
          <el-table-column prop="maxusers" label="人数上限" />
          <el-table-column prop="isPublic" label="公有群">
            <template #default="scope">
              <template v-if="scope.row.isPublic">
                <span><el-tag size="small" type="warning">共有群</el-tag></span>
              </template>
              <template v-if="!scope.row.isPublic">
                <span><el-tag size="small">私有群</el-tag></span>
              </template>
            </template></el-table-column
          >
          <el-table-column prop="isDefault" label="默认">
            <template #default="scope">
              <template v-if="scope.row.isDefault">
                <el-icon color="#EEEE11"> <el-icon-star-filled /></el-icon>
              </template>
              <template v-if="!scope.row.isDefault">
                <el-popconfirm
                  title="确定要设置默认群聊吗？"
                  @confirm="doDefault(scope.row)"
                >
                  <template #reference>
                    <span><el-tag size="small" type="info">默认</el-tag></span>
                  </template>
                </el-popconfirm></template
              >
            </template>
          </el-table-column>
          <el-table-column prop="isActive" label="状态">
            <template #default="scope">
              <template v-if="scope.row.isActive">
                <el-popconfirm
                  title="确定要冻结聊天室吗？群内成员不可恢复！"
                  @confirm="isActive(scope.row)"
                >
                  <template #reference>
                    <span
                      ><el-tag size="small" type="success">可用</el-tag></span
                    >
                  </template>
                </el-popconfirm>
              </template>
              <template v-if="!scope.row.isActive">
                <span><el-tag size="small" type="danger">冻结</el-tag></span>
              </template>
            </template>
          </el-table-column>
          <el-table-column
            prop="announcement"
            label="群公告"
            :show-overflow-tooltip="true"
          />
          <el-table-column fixed="right" label="操作" width="140">
            <template #default="scope">
              <!-- <el-button
                type="text"
                size="small"
                @click="table_edit(scope.row, scope.$index)"
                >修改</el-button
              >
              <el-divider direction="vertical"></el-divider> -->
              <el-button
                type="text"
                size="small"
                @click="table_member(scope.row, scope.$index)"
                >成员</el-button
              >
              <el-divider direction="vertical"></el-divider>
              <el-button
                type="text"
                size="small"
                @click="table_add(scope.row, scope.$index)"
                >管理</el-button
              >
            </template>
          </el-table-column>
          <template #empty>
            <el-empty :description="emptyText" :image-size="100"></el-empty>
          </template>
        </el-table>
      </div>
      <div class="kxTable-pagination-box">
        <!-- 分页器 -->
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
        <!-- 刷新 -->
        <div class="kxTable-do">
          <el-button
            @click="refresh"
            icon="el-icon-refresh"
            circle
            style="margin-left: 15px"
          ></el-button>
        </div>
      </div>
    </el-main>
  </el-container>

  <save-dialog
    v-if="dialog.save"
    ref="saveDialog"
    @success="handleSuccess"
    @closed="dialog.save = false"
  ></save-dialog>
  <save-member
    v-if="member.save"
    ref="saveMember"
    @success="handleSuccess"
    @closed="member.save = false"
  ></save-member>
  <save-addManager
    v-if="addManager.save"
    ref="saveAddManager"
    @success="handleSuccess"
    @closed="addManager.save = false"
  ></save-addManager>
</template>
<script>
import saveDialog from "./save.vue";
import saveMember from "./inMember.vue";
import saveAddManager from "./inManager.vue";
export default {
  name: "list",
  components: { saveDialog, saveAddManager, saveMember },
  data() {
    return {
      dialog: {
        save: false,
      },
      member: {
        save: false,
      },
      addManager: {
        save: false,
      },
      selection: [],
      tableData: [],
      height: "100%",
      total: 0,
      searchList: {
        groupName: "",
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
    this.getData();
  },
  methods: {
    // 获取用户列表
    async getData() {
      this.loading = true;
      const res = await this.$API.groupChat.findList.post(this.searchList);
      console.log(res);
      this.loading = false;
      if (res.code !== 0) {
        return this.$message.error("获取数据失败");
      }
      this.tableData = res.data.rows;
      this.searchList.size = res.data.pageSize;
      this.total = res.data.total;
      this.searchList.page = res.data.page;
      // console.log(this.tableData);
    },
    // 设置默认聊天室
    async doDefault(row) {
      this.loading = true;
      const res = await this.$API.groupChat.doDefault.post({
        id: row.id,
        isActive: !row.isDefault,
      });
      this.loading = false;
      if (res.code == 0) {
        this.getData();
        this.$message.success("设置成功");
      } else {
        this.$alert(res.message, "提示", { type: "error" });
      }
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
      this.$refs.kxTable.clearSelection();
      this.getData();
    },
    //添加
    add() {
      this.dialog.save = true;
      this.$nextTick(() => {
        this.$refs.saveDialog.open();
      });
    },
    // 修改
    table_edit(row) {
      this.dialog.save = true;
      this.$nextTick(() => {
        this.$refs.saveDialog.open("edit").setData(row.id);
      });
    },
    // 详情
    table_member(row) {
      this.member.save = true;
      this.$nextTick(() => {
        this.$refs.saveMember.open("edit").setData(row.id);
      });
    },
    // 管理
    table_add(row) {
      this.addManager.save = true;
      this.$nextTick(() => {
        this.$refs.saveAddManager.open("edit").setData(row.id);
      });
    },
    //激活/冻结账户
    async isActive(row) {
      this.loading = true;
      const res = await this.$API.chatRoom.isActive.post({
        id: row.id,
        isActive: !row.isActive,
      });
      this.loading = false;
      if (res.code == 0) {
        this.getData();
        this.$message.success("冻结成功");
      } else {
        this.$alert(res.message, "提示", { type: "error" });
      }
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
    // 本地更新数据
    handleSuccess() {
      this.refresh();
    },
  },
};
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
.el-image {
  width: 40px;
}
.el-icon svg {
  height: 1.5em;
  width: 1.5em;
}
</style>