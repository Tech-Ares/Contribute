<template>
  <el-dialog title="添加管理员" v-model="visible" @closed="$emit('closed')">
    <div class="kxTable-table" v-loading="loading">
      <el-table
        :data="tableData"
        ref="kxTable"
        row-key="id"
        style="width: 100%"
        :height="height == 'auto' ? null : '100%'"
        stripe
      >
        <el-table-column prop="nickName" label="昵称" />
        <el-table-column prop="avatar" label="头像">
          <template #default="scope">
            <el-avatar :src="scope.row.avatar" :size="30"></el-avatar>
          </template>
        </el-table-column>
        <el-table-column prop="isOwner" label="身份">
          <template #default="scope">
            <template v-if="scope.row.isOwner">
              <span><el-tag size="small">群主</el-tag></span>
            </template>
          </template>
        </el-table-column>
        <el-table-column prop="isManager" label="管理员">
          <template #default="scope">
            <template v-if="scope.row.isManager && scope.row.isOwner == false">
              <span><el-tag size="small" type="success">已添加</el-tag></span>
            </template>
            <template v-if="!scope.row.isManager && scope.row.isOwner == false">
              <span><el-tag size="small" type="danger">未添加</el-tag></span>
            </template>
            <template v-if="scope.row.isOwner">
              <span><el-tag size="small" type="success">已添加</el-tag></span>
            </template>
          </template>
        </el-table-column>
        <el-table-column fixed="right" label="操作" width="140">
          <template #default="scope">
            <template v-if="!scope.row.isManager && scope.row.isOwner == false">
              <el-popconfirm
                title="确定要添加管理员吗"
                @confirm="addManager(scope.row.userId)"
              >
                <template #reference>
                  <el-link type="primary">加入</el-link>
                </template>
              </el-popconfirm>
            </template>
            <template v-if="scope.row.isOwner">群主已存在 </template>
          </template>
        </el-table-column>
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
    <template #footer>
      <el-button @click="visible = false">取 消</el-button>
      <el-button type="primary" :loading="isSaveing" @click="visible = false"
        >保 存</el-button
      >
    </template>
  </el-dialog>
</template>

<script>
export default {
  emits: ["success", "closed"],
  data() {
    return {
      visible: false,
      isSaveing: false,
      loading: false,
      //表单数据
      tableData: [],
      height: "100%",
      total: 0,
      searchList: {
        groupId: "",
        size: 10,
        page: 1,
      },
      emptyText: "暂无数据",
    };
  },
  mounted() {
    // this.getGroup()
  },
  methods: {
    // 获取管理员列表
    async findForm() {
      this.loading = true;
      const res = await this.$API.groupChat.findManager.post(this.searchList);
      console.log(res, "findManager");
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
    //聊天室管理员添加
    async addManager(userId) {
      console.log(userId);
      const res = await this.$API.groupChat.addManager.post({
        groupId: this.searchList.groupId,
        userId: userId,
      });
      if (res.code == 0) {
        this.$emit("success", this.findService);
        this.findForm();
        this.$message.success("操作成功");
      } else {
        this.$alert(res.message, "提示", { type: "error" });
      }
      this.findForm();
    },
    //聊天室管理员移除
    async delManager(userId) {
      console.log(userId);
      const res = await this.$API.groupChat.delManager.post({
        groupId: this.searchList.groupId,
        userId: userId,
      });
      if (res.code == 0) {
        this.$emit("success", this.findService);
        this.findForm();
        this.$message.success("操作成功");
      } else {
        this.$alert(res.message, "提示", { type: "error" });
      }
      this.findForm();
    },
    // 翻页控制
    handleCurrentChange(val) {
      this.searchList.page = val;
      this.findForm();
    },
    // 每页条数控制
    handleSizeChange(val) {
      this.searchList.size = val;
      this.searchList.page = 1;
      this.findForm();
    },
    // 刷新表格
    refresh() {
      this.$refs.kxTable.clearSelection();
      this.findForm();
    },
    //显示
    open() {
      // this.mode = mode;
      this.visible = true;
      return this;
    },
    //表单注入数据
    setData(id) {
      this.searchList.groupId = id;
      console.log(id);
      this.findForm();
      // console.log(this.id, "obj.id");
      // console.log(this.name, "obj.name");
      //可以和上面一样单个注入，也可以像下面一样直接合并进去
      //Object.assign(this.form, data)
    },
  },
};
</script>

<style lang="scss" scoped>
// .kxTable-table {
//   // min-height: 300px;
//   :deep(.el-table__body-wrapper) {
//     height: 100% !important;
//   }
// }
.kxTable-table {
  height: 400px;
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
  height: 40px;
}
</style>