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
            v-model="searchList.nickName"
            placeholder="名称"
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
            :disabled="searchList.nickName.length < 1"
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
          <el-table-column
            prop="title"
            label="标题"
            :show-overflow-tooltip="true"
          />
          <el-table-column prop="videoUrl" label="视频链接" />
          <el-table-column prop="liveDate" label="直播日期" />
          <el-table-column prop="endDate" label="结束时间" />
          <el-table-column prop="isActive" label="状态" width="80">
            <template #default="scope">
              <template v-if="scope.row.isActive">
                <span><el-tag size="small" type="success">可用</el-tag></span>
              </template>
              <template v-if="!scope.row.isActive">
                <span><el-tag size="small" type="danger">冻结</el-tag></span>
              </template>
            </template>
          </el-table-column>
          <el-table-column fixed="right" label="操作" width="140">
            <template #default="scope">
              <el-button
                type="text"
                size="small"
                @click="table_edit(scope.row, scope.$index)"
                >修改</el-button
              ><el-divider direction="vertical"></el-divider>
              <template v-if="!scope.row.isActive">
                <el-popconfirm
                  title="确定启用直播吗？"
                  @confirm="table_del(scope.row, scope.$index)"
                >
                  <template #reference>
                    <el-button
                      type="text"
                      size="small"
                      :style="{ color: 'blue' }"
                      >启用直播</el-button
                    >
                  </template>
                </el-popconfirm>
              </template>
              <template v-if="scope.row.isActive">
                <el-popconfirm
                  title="确定冻结直播吗？"
                  @confirm="table_del(scope.row, scope.$index)"
                >
                  <template #reference>
                    <el-button
                      type="text"
                      size="small"
                      :style="{ color: '#f56c6c' }"
                      >冻结直播</el-button
                    >
                  </template>
                </el-popconfirm>
              </template>
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
  <!-- <save-member
    v-if="member.save"
    ref="saveMember"
    @success="handleSuccess"
    @closed="member.save = false"
  ></save-member> -->
</template>

<script>
import saveDialog from "./save.vue";
// import saveMember from "./inMember.vue";

export default {
  name: "loginList",
  components: { saveDialog },
  data() {
    return {
      dialog: {
        save: false,
      },
      member: {
        save: false,
      },
      selection: [],
      tableData: [],
      height: "100%",
      total: 0,
      searchList: {
        nickName: "",
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
    // 获取直播列表
    async getData() {
      this.loading = true;
      const res = await this.$API.liveStream.findList.post(this.searchList);
      console.log(res);
      this.loading = false;
      if (res.code !== 0) {
        return this.$message.error("获取数据失败");
      }
      this.tableData = res.data.rows;
      this.searchList.size = res.data.pageSize;
      this.total = res.data.total;
      this.searchList.page = res.data.page;
      console.log(this.tableData);
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
        this.$refs.saveDialog.open("edit").setData(row);
      });
    },
    // 添加好友
    // table_member(row) {
    //   this.member.save = true;
    //   this.$nextTick(() => {
    //     this.$refs.saveMember.open("edit").setData(row.userId);
    //   });
    // },
    //启用/禁用账户
    async table_del(row) {
      this.loading = true;
      const res = await this.$API.liveStream.deleteActive.post({
        id: row.id,
        isActive: !row.isActive,
      });
      this.loading = false;
      if (res.code == 0) {
        this.getData();
        this.$message.success("操作成功");
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