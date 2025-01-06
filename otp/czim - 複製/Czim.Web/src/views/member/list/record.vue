<template>
  <el-drawer v-model="visible" size="50%" :with-header="false" destroy-on-close>
    <el-tabs v-model="activeTab" @tab-click="tabEvent" type="card">
      <el-tab-pane label="群聊记录" name="addGroup">
        <login-info v-model:memberId="id" ref="login"></login-info>
      </el-tab-pane>
      <el-tab-pane label="好友记录" lazy name="addFriend">
        <el-container>
          <el-header>
            <div class="left-panel">
              <!-- 添加 -->
              <el-button type="primary" icon="el-icon-plus" @click="addfriend"
                >添加好友</el-button
              >
            </div>
          </el-header>
          <el-main class="nopadding">
            <div class="kxTable-table" v-loading="friendloading">
              <!-- 数据表 -->
              <el-table
                :data="friendData"
                ref="kxTable"
                row-key="id"
                style="width: 98%"
                :height="friendheight == 'auto' ? null : '100%'"
                stripe
              >
                <el-table-column prop="nickName" label="昵称" />
                <el-table-column prop="avatar" label="头像">
                  <template #default="scope"
                    ><el-avatar :src="scope.row.avatar" :size="30"></el-avatar
                  ></template>
                </el-table-column>
                <el-table-column prop="manTime" label="服务人次" />
                <el-table-column prop="crtDate" label="加入时间" />

                <template #empty>
                  <el-empty
                    :description="emptyText"
                    :image-size="100"
                  ></el-empty>
                </template>
              </el-table>
            </div>
            <div class="kxTable-pagination-box">
              <!-- 分页器 -->
              <el-pagination
                background
                :small="true"
                layout="total, sizes, prev, pager, next, jumper"
                :total="friendtotal"
                :page-sizes="[10, 20, 50, 100]"
                v-model:currentPage="searchFriend.page"
                @current-change="friendCurrentChange"
                @size-change="friendSizeChange"
              ></el-pagination>
              <!-- 刷新 -->
              <div class="kxTable-do">
                <el-button
                  @click="refreshFriend"
                  icon="el-icon-refresh"
                  circle
                  style="margin-left: 15px"
                ></el-button>
              </div>
            </div>
          </el-main>
        </el-container>
      </el-tab-pane>
      <el-tab-pane label="聊天室记录" lazy name="addChat">
        <el-container>
          <el-header>
            <div class="left-panel">
              <!-- 添加 -->
              <el-button
                type="primary"
                icon="el-icon-plus"
                @click="add(this.id)"
                >添加聊天室</el-button
              >
            </div>
          </el-header>
          <el-main class="nopadding">
            <div class="kxTable-table" v-loading="loading">
              <!--加聊天室 数据表 -->
              <el-table
                :data="tableData"
                ref="kxTable"
                row-key="id"
                style="width: 98%"
                :height="height == 'auto' ? null : '100%'"
                stripe
              >
                <el-table-column prop="chatRoomName" label="名称" />
                <el-table-column prop="chatRoomImg" label="封面">
                  <template #default="scope"
                    ><el-avatar
                      :src="scope.row.chatRoomImg"
                      :size="30"
                    ></el-avatar
                  ></template>
                </el-table-column>
                <el-table-column
                  prop="chatRoomNotice"
                  label="聊天室公告"
                  :show-overflow-tooltip="true"
                />

                <template #empty>
                  <el-empty
                    :description="emptyText"
                    :image-size="100"
                  ></el-empty>
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
      </el-tab-pane>
    </el-tabs>
  </el-drawer>

  <!-- 加入聊天室模态窗 -->
  <el-dialog v-model="chatVisible" title="加入聊天室">
    <el-container>
      <el-header>
        <div class="left-panel">
          <span><a>查询聊天室</a></span>
        </div>
        <div class="right-panel">
          <div class="right-panel-search">
            <!-- 搜索框 -->
            <el-input
              v-model="searchJoin.chatroomName"
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
              :disabled="searchJoin.chatroomName.length < 1"
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
            <el-table-column prop="chatRoomName" label="名称" />
            <el-table-column prop="chatRoomImg" label="封面">
              <template #default="scope">
                <el-avatar :src="scope.row.chatRoomImg" :size="30"></el-avatar>
              </template>
            </el-table-column>
            <el-table-column prop="crtDate" label="创建时间" />
            <el-table-column fixed="right" label="操作" width="140">
              <template #default="scope">
                <!-- <template v-if="!scope.row.isManager"> -->
                <el-popconfirm
                  title="确定要加入聊天室吗"
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
  <!-- 添加好友对话框 -->
  <el-dialog v-model="friendVisible" title="添加客服好友">
    <el-container>
      <el-header>
        <div class="left-panel">
          <span><a>查询客服</a></span>
        </div>
        <div class="right-panel">
          <div class="right-panel-search">
            <!-- 搜索框 -->
            <el-input
              v-model="service.nickName"
              placeholder="名称"
              clearable
            ></el-input>
            <!-- 查询 -->
            <el-button
              type="primary"
              icon="el-icon-search"
              @click="searchService"
            ></el-button>
            <!-- 重置 -->
            <el-button
              type="info"
              plain
              icon="el-icon-refresh"
              @click="resetService"
              :disabled="service.nickName.length < 1"
            ></el-button>
          </div>
        </div>
      </el-header>
      <el-main class="nopadding">
        <div class="join-table" v-loading="kfloading">
          <!-- 数据表 -->
          <el-table
            :data="serviceData"
            ref="kxTable"
            row-key="id"
            style="width: 100%"
            :height="height == 'auto' ? null : '100%'"
            stripe
            @selection-change="selectionChange"
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
                  <span><el-tag size="small">客服</el-tag></span>
                </template>
              </template>
            </el-table-column>
            <el-table-column fixed="right" label="操作" width="140">
              <template #default="scope">
                <el-popconfirm
                  title="确定要添加为好友吗"
                  @confirm="addService(scope.row.userId)"
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
            :total="servicetotal"
            :page-sizes="[10, 20, 50, 100]"
            v-model:currentPage="searchList.page"
            @current-change="serviceChange"
            @size-change="serviceSizeChange"
          ></el-pagination>
          <!-- 刷新 -->
          <div class="kxTable-do">
            <el-button
              @click="servicerefresh"
              icon="el-icon-refresh"
              circle
              style="margin-left: 15px"
            ></el-button>
          </div>
        </div>
      </el-main>
    </el-container>

    <template #footer>
      <el-button @click="friendVisible = false">取 消</el-button>
      <el-button type="primary" @click="friendVisible = false">保 存</el-button>
    </template>
  </el-dialog>
</template>

<script>
import loginInfo from "./joinChat";
export default {
  components: { loginInfo },
  emits: ["success", "closed"],
  data() {
    return {
      visible: false,
      isSaveing: false,
      loading: false,
      // 加聊天室记录表
      tableData: [],
      height: "100%",
      total: 0,
      searchList: {
        size: 10,
        page: 1,
        memberId: "",
      },
      emptyText: "暂无数据",

      // 好友记录表
      friendloading: false,
      friendData: [],
      friendheight: "100%",
      friendtotal: 0,
      searchFriend: {
        size: 10,
        page: 1,
        memberId: "",
      },
      // friendText: "暂无数据",

      //添加聊天室
      chatVisible: false,
      joinloading: false,
      tableJoin: [],
      joinheight: "100%",
      jointotal: 0,
      searchJoin: {
        size: 10,
        page: 1,
        chatroomName: "",
        memberId: "",
      },
      joinText: "暂无数据",

      // 添加好友
      friendVisible: false,
      kfloading: false,
      serviceData: [],
      servicetotal: 0,
      service: {
        size: 10,
        page: 1,
        memberId: "",
        nickName: "",
      },
      // 标签页
      activeTab: "addGroup",
      id: "",
    };
  },

  mounted() {
    // this.getGroup()
  },
  methods: {
    // 获取聊天室列表(聊天室)
    async getData() {
      this.loading = true;
      const res = await this.$API.appMember.findJoin.post({
        size: this.searchList.size,
        page: this.searchList.page,
        memberId: this.id,
      });
      console.log(res, "聊天室记录");
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
    // 翻页控制(聊天室)
    handleCurrentChange(val) {
      this.searchList.page = val;
      this.getData();
    },
    // 每页条数控制（聊天室）
    handleSizeChange(val) {
      this.searchList.size = val;
      this.searchList.page = 1;
      this.getData();
    },
    // 刷新表格（聊天室）
    refresh() {
      // this.$refs.kxTable.clearSelection();
      this.getData();
    },
    //添加/打开加入聊天室(对话框)
    add() {
      this.chatVisible = true;
      this.findAbb();
    },
    //获取可加入的聊天室(对话框)
    async findAbb() {
      this.joinloading = true;
      const res = await this.$API.chatRoom.findAdd.post({
        size: this.searchJoin.size,
        page: this.searchJoin.page,
        chatroomName: this.searchJoin.chatroomName,
        memberId: this.id,
      });
      console.log(res, "可加入聊天室数据");
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
    //将会员添加到聊天室(对话框)
    async joinChat(id) {
      // console.log(userId);
      const res = await this.$API.appMember.joinChat.post({
        memberId: this.id,
        chatRoomId: id,
        pullId: "",
      });
      if (res.code == 0) {
        this.$emit("success", this.findService);
        this.chatVisible = false;
        this.getData();
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
    // 获取用户好友列表
    async findFriend() {
      this.friendloading = true;
      const res = await this.$API.appMember.findFriend.post({
        size: this.searchFriend.size,
        page: this.searchFriend.page,
        memberId: this.id,
      });
      console.log(res, "好友记录");
      this.friendloading = false;
      if (res.code !== 0) {
        return this.$message.error("获取数据失败");
      }
      this.friendData = res.data.rows;
      this.searchFriend.size = res.data.pageSize;
      this.friendtotal = res.data.total;
      this.searchFriend.page = res.data.page;
      // console.log(this.tableData);
    },
    // 翻页控制（好友）
    friendCurrentChange(val) {
      this.searchFriend.page = val;
      this.findFriend();
    },
    // 每页条数控制（好友）
    friendSizeChange(val) {
      this.searchFriend.size = val;
      this.searchFriend.page = 1;
      this.findFriend();
    },
    // 刷新表格（好友）
    refreshFriend() {
      // this.$refs.kxTable.clearSelection();
      this.findFriend();
    },

    // 打开会员添加好友(对话框)
    addfriend() {
      this.friendVisible = true;
      this.findService();
    },
    //获取可添加的客服(对话框)
    async findService() {
      this.kfloading = true;
      const res = await this.$API.appMember.findService.post({
        size: this.service.size,
        page: this.service.page,
        memberId: this.id,
        nickName: this.service.nickName,
      });
      console.log(res, "可添加的客服");
      this.kfloading = false;
      if (res.code !== 0) {
        return this.$message.error("获取数据失败");
      }
      this.serviceData = res.data.rows;
      this.service.size = res.data.pageSize;
      this.servicetotal = res.data.total;
      this.service.page = res.data.page;
      // console.log(this.serviceData);
    },
    //会员添加好友(对话框)
    async addService(id) {
      // console.log(userId);
      const res = await this.$API.appMember.addService.post({
        userId: id,
        memberId: this.id,
      });
      if (res.code == 0) {
        this.$emit("success", this.findService);
        this.friendVisible = false;
        this.findFriend();
        this.$message.success("操作成功");
      } else {
        this.$alert(res.message, "提示", { type: "error" });
      }
    },
    // 翻页控制
    serviceChange(val) {
      this.service.page = val;
      this.findService();
    },
    // 每页条数控制
    serviceSizeChange(val) {
      this.service.size = val;
      this.service.page = 1;
      this.findService();
    },
    // 刷新表格
    servicerefresh() {
      this.findService();
    },
    // 搜索
    searchService() {
      this.findService();
    },
    // 重置搜索
    resetService() {
      for (let key in this.service) {
        console.log(key);
        if (key == "size") {
          this.service[key] = 10;
        } else if (key == "page") {
          this.service[key] = 1;
        } else {
          this.service[key] = "";
        }
      }
      this.findService();
    },

    //显示
    open() {
      this.visible = true;
      return this;
    },
    //表单注入数据
    setData(id) {
      this.id = id;
      this.getData();
      this.findFriend();
      console.log(this.id, "id");
    },
    //标签页控制
    tabEvent(tab, event) {
      console.log(tab, event, "标签页");
      if (tab.props.name === "addGroup") {
        this.$refs.login.getGroupData();
      }
      if (tab.props.name === "addFriend") {
        this.findFriend();
      }
      if (tab.props.name === "addChat") {
        this.getData();
      }
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
