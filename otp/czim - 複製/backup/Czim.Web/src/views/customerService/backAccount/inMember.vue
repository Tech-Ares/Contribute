<template>
  <el-drawer title="客服好友" v-model="visible" size="50%">
    <el-container>
      <el-header>
        <div class="left-panel">
          <!-- 添加 -->
          <el-button type="primary" icon="el-icon-plus" @click="add(this.id)"
            >客服添加好友</el-button
          >
        </div>
      </el-header>
      <el-main class="nopadding">
        <div class="kxTable-table" v-loading="loading">
          <el-table
            :data="tableData"
            ref="kxTable"
            row-key="id"
            style="width: 98%"
            :height="height == 'auto' ? null : '100%'"
            stripe
          >
            <el-table-column prop="nickName" label="昵称" />
            <el-table-column prop="avatar" label="头像"
              ><template #default="scope"
                ><el-avatar
                  :src="scope.row.avatar"
                  :size="30"
                ></el-avatar></template
            ></el-table-column>
            <el-table-column prop="isAgent" label="职位">
              <template #default="scope">
                <template v-if="scope.row.isAgent">
                  <span
                    ><el-tag size="small" type="warning">推广员</el-tag></span
                  >
                </template>
                <template v-if="!scope.row.isAgent">
                  <span><el-tag size="small">会员</el-tag></span>
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
  </el-drawer>
  <!-- 添加好友模态窗 -->
  <el-dialog v-model="chatVisible" title="客服添加好友">
    <el-container>
      <el-header>
        <div class="left-panel">
          <span><a>查询会员</a></span>
        </div>
        <div class="right-panel">
          <div class="right-panel-search">
            <!-- 搜索框 -->
            <el-input
              v-model="searchJoin.nickName"
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
              :disabled="searchJoin.nickName.length < 1"
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
            <el-table-column prop="nickName" label="昵称" />
            <el-table-column prop="gender" label="性别">
              <template #default="scope">
                <template v-if="scope.row.gender == 0"> --- </template>
                <template v-if="scope.row.gender == 1">
                  <span
                    ><el-icon color="#409EFF"><el-icon-male /></el-icon></span
                ></template>
                <template v-if="scope.row.gender == 2">
                  <span
                    ><el-icon color="#F56C6C"><el-icon-male /></el-icon></span
                ></template>
              </template>
            </el-table-column>
            <el-table-column prop="isAgent" label="职位">
              <template #default="scope">
                <template v-if="scope.row.isAgent">
                  <span
                    ><el-tag size="small" type="warning">推广员</el-tag></span
                  >
                </template>
                <template v-if="!scope.row.isAgent">
                  <span><el-tag size="small">会员</el-tag></span>
                </template>
              </template>
            </el-table-column>
            <el-table-column
              prop="introduce"
              label="留言"
              :show-overflow-tooltip="true"
            />
            <el-table-column fixed="right" label="操作" width="140">
              <template #default="scope">
                <el-popconfirm
                  title="确定要添加为好友吗"
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
              <el-empty :description="joinText" :image-size="100"></el-empty>
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
          <div class="joinTable-do">
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
      <el-button
        type="primary"
        :loading="isSaveing"
        @click="chatVisible = false"
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
      tableData: [],
      height: "100%",
      total: 0,
      searchList: {
        chatRoomId: "",
        size: 10,
        page: 1,
      },
      loading: false,
      emptyText: "暂无数据",

      //可加入会员
      chatVisible: false,
      joinloading: false,
      tableJoin: [],
      joinheight: "100%",
      jointotal: 0,
      searchJoin: {
        size: 10,
        page: 1,
        chatRoomId: "",
        nickName: "",
      },
      joinText: "暂无数据",

      id: "",
    };
  },
  mounted() {
    // this.getGroup()
  },
  methods: {
    // 获取客服好友
    async findForm() {
      this.loading = true;
      const res = await this.$API.customerService.getChat.post({
        userId: this.id,
        size: this.searchList.size,
        page: this.searchList.page,
      });
      console.log(res, "getfriend");
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
      // this.$refs.kxTable.clearSelection();
      this.findForm();
    },
    //添加/打开(对话框)
    add() {
      this.chatVisible = true;
      this.findAbb();
    },
    //获取所有可加入会员(对话框)
    async findAbb() {
      this.joinloading = true;
      const res = await this.$API.appMember.findAddmember.post({
        size: this.searchJoin.size,
        page: this.searchJoin.page,
        customerId: this.id,
        nickName: this.searchJoin.nickName,
      });
      console.log(res, "可被客服添加的会员");
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
    //将会员添加为客服好友
    async joinChat(id) {
      // console.log(userId);
      const res = await this.$API.appMember.addService.post({
        userId: this.id,
        memberId: id,
      });
      if (res.code == 0) {
        this.$emit("success", this.findService);
        this.chatVisible = false;
        this.findForm();
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
      // this.mode = mode;
      this.visible = true;
      return this;
    },
    //表单注入数据
    setData(id) {
      this.id = id;
      console.log(this.id, "userId");
      // this.name = obj.chatRoomName;
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
