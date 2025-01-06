<template>
  <el-container>
    <el-header>
      <div class="left-panel">
        <!-- 添加 -->
        <el-button type="primary" icon="el-icon-plus" @click="add(this.id)"
          >添加群聊</el-button
        >
      </div>
    </el-header>
    <el-main class="nopadding">
      <div class="kxTable-table" v-loading="loading">
        <!--群聊数据表 -->
        <el-table
          :data="tableData"
          ref="kxTable"
          row-key="ownId"
          style="width: 98%"
          :height="height == 'auto' ? null : '100%'"
          stripe
        >
          <el-table-column prop="groupName" label="名称" />
          <el-table-column prop="groupImg" label="封面">
            <template #default="scope"
              ><el-avatar :src="scope.row.groupImg" :size="30"></el-avatar
            ></template>
          </el-table-column>
          <el-table-column
            prop="description"
            label="群聊公告"
            :show-overflow-tooltip="true"
          />

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
  <!-- 加入群聊模态窗 -->
  <el-dialog v-model="chatVisible" title="加入群聊">
    <el-container>
      <el-header>
        <div class="left-panel">
          <span><a>查询群聊</a></span>
        </div>
        <div class="right-panel">
          <div class="right-panel-search">
            <!-- 搜索框 -->
            <el-input
              v-model="searchJoin.chatGroupName"
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
              :disabled="searchJoin.chatGroupName.length < 1"
            ></el-button>
          </div>
        </div>
      </el-header>
      <el-main class="nopadding">
        <div class="join-table" v-loading="joinloading">
          <el-table
            :data="tableJoin"
            ref="joinTable"
            row-key="id"
            style="width: 100%"
            :height="joinheight == 'auto' ? null : '100%'"
            stripe
          >
            <el-table-column prop="groupName" label="名称" />
            <el-table-column prop="groupImg" label="封面">
              <template #default="scope">
                <el-avatar :src="scope.row.groupImg" :size="30"></el-avatar>
              </template>
            </el-table-column>
            <el-table-column prop="description" label="群聊描述" />
            <el-table-column prop="crtDate" label="创建时间" />
            <el-table-column fixed="right" label="操作" width="140">
              <template #default="scope">
                <!-- <template v-if="!scope.row.isManager"> -->
                <el-popconfirm
                  title="确定要加入群聊吗"
                  @confirm="joinChat(scope.row.id)"
                >
                  <template #reference>
                    <el-link type="primary">加入</el-link>
                  </template>
                </el-popconfirm>
                <!-- </template> -->
              </template>
            </el-table-column>
            <template #empty>
              <el-empty :description="emptyText" :image-size="100"></el-empty>
            </template>
          </el-table>
        </div>
        <div class="joinTable-pagination-box">
          <!-- 分页器 -->
          <el-pagination
            background
            :small="true"
            layout="total, sizes, prev, pager, next, jumper"
            :total="jointotal"
            :page-sizes="[10, 20, 50, 100]"
            v-model:currentPage="searchJoin.page"
            @current-change="JoinCurrentChange"
            @size-change="JoinSizeChange"
          ></el-pagination>
          <!-- 刷新 -->
          <div class="kxTable-do">
            <el-button
              @click="refreshJoin"
              icon="el-icon-refresh"
              circle
              style="margin-left: 15px"
            ></el-button>
          </div>
        </div>
      </el-main>
    </el-container>
    <template #footer>
      <el-button @click="chatVisible = false">取 消</el-button>
      <el-button type="primary" @click="chatVisible = false">保 存</el-button>
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
      // 群聊记录表
      tableData: [],
      height: "100%",
      total: 0,
      searchList: {
        size: 10,
        page: 1,
        memberId: "",
      },
      emptyText: "暂无数据",

      //添加群聊
      chatVisible: false,
      joinloading: false,
      tableJoin: [],
      joinheight: "100%",
      jointotal: 0,
      searchJoin: {
        size: 10,
        page: 1,
        chatGroupName: "",
        memberId: "",
      },
      joinText: "暂无数据",

      //   id: "",
    };
  },
  props: {
    memberId: {
      type: String,
      required: true,
    },
  },
  mounted() {
    // this.getGroup()
    this.getGroupData();
  },
  methods: {
    // 获取加群列表(群)
    async getGroupData() {
      this.loading = true;
      const res = await this.$API.appMember.findGroup.post({
        size: this.searchList.size,
        page: this.searchList.page,
        memberId: this.memberId,
      });
      console.log(res, "群聊记录");
      this.loading = false;
      if (res.code !== 0) {
        return this.$message.error("获取数据失败");
      }
      this.tableData = res.data.rows;
      this.searchList.size = res.data.pageSize;
      this.total = res.data.total;
      this.searchList.page = res.data.page;
      // console.log(this.tableData);
      //   return this;
    },
    // 翻页控制(群)
    handleCurrentChange(val) {
      this.searchList.page = val;
      this.getGroupData();
    },
    // 每页条数控制（群）
    handleSizeChange(val) {
      this.searchList.size = val;
      this.searchList.page = 1;
      this.getGroupData();
    },
    // 刷新表格（群）
    refresh() {
      // this.$refs.kxTable.clearSelection();
      this.getGroupData();
    },
    //添加/打开加入群聊(对话框)
    add() {
      this.chatVisible = true;
      this.findAbb();
    },
    //获取可加入的群聊(对话框)
    async findAbb() {
      this.joinloading = true;
      const res = await this.$API.groupChat.findGroup.post({
        size: this.searchJoin.size,
        page: this.searchJoin.page,
        chatGroupName: this.searchJoin.chatGroupName,
        memberId: this.memberId,
      });
      console.log(res, "可加入群聊数据");
      this.joinloading = false;
      if (res.code !== 0) {
        return this.$message.error("获取数据失败");
      }
      this.tableJoin = res.data.rows;
      this.searchJoin.size = res.data.pageSize;
      this.jointotal = res.data.total;
      this.searchJoin.page = res.data.page;
      // console.log(this.tableData);
    },
    //将会员添加到群聊(对话框)
    async joinChat(id) {
      // console.log(userId);
      const res = await this.$API.appMember.joinGroup.post({
        memberId: this.memberId,
        chatgroupId: id,
        pullId: "",
      });
      if (res.code == 0) {
        // this.$emit("success", this.findService);
        this.chatVisible = false;
        this.getGroupData();
        this.$message.success("操作成功");
      } else {
        this.$alert(res.message, "提示", { type: "error" });
      }
      this.findAbb();
    },
    // 翻页控制(对话框)
    JoinCurrentChange(val) {
      this.searchJoin.page = val;
      this.findAbb();
    },
    // 每页条数控制（对话框）
    JoinSizeChange(val) {
      this.searchJoin.size = val;
      this.searchJoin.page = 1;
      this.findAbb();
    },
    // 刷新表格（对话框）
    refreshJoin() {
      // this.$refs.kxTable.clearSelection();
      this.findAbb();
    },
    // 搜索(对话框)
    searchRole() {
      this.findAbb();
    },
    // 重置搜索（对话框）
    resetSearch() {
      for (let key in this.searchJoin) {
        console.log(key);
        if (key == "size") {
          this.searchJoin[key] = 10;
        } else if (key == "page") {
          this.searchJoin[key] = 1;
        } else {
          this.searchJoin[key] = "";
        }
      }
      this.findAbb();
    },
    //显示
    open() {
      this.visible = true;
      return this;
    },
    //表单注入数据
    // setData(id) {
    //   this.id = id;
    //   this.getGroupData();
    //   this.findFriend();
    //   console.log(this.id, "群id");
    // },
    //标签页控制
    // tabEvent(tab, event) {
    //   console.log(tab, event, "标签页");
    //   if (tab.props.name === "addGroup") {
    //     this.getGroupData();
    //   }
    //   if (tab.props.name === "addFriend") {
    //     this.findFriend();
    //   }
    //   if (tab.props.name === "addChat") {
    //     this.getGroupData();
    //   }
    // },
  },
};
</script>
<style lang="scss" scoped>
.kxTable-table {
  height: calc(100% - 50px);
  margin-left: 20px;
}
.kxTable-pagination-box {
  height: 50px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 15px;
}
.el-tabs {
  margin-top: 30px;
  .el-tab-pane {
    height: calc(100vh - 100px);
  }
}
.join-table {
  height: 400px;
}
.joinTable-pagination-box {
  height: 50px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 15px;
}
</style>