<template>
  <el-drawer title="群内成员" v-model="visible" size="50%">
    <el-container>
      <el-header>
        <div class="left-panel">
          <!-- 添加 -->
          <el-button type="primary" icon="el-icon-plus" @click="add(this.id)"
            >添加成员</el-button
          >
          <!-- 删除 -->
          <!-- <el-button
            type="danger"
            plain
            icon="el-icon-delete"
            :disabled="selection.length == 0"
            @click="batch_del"
            >批量删除</el-button
          > -->
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
            <!-- <el-table-column type="selection" width="50"></el-table-column> -->
            <!-- <el-table-column type="index" label="#" width="50" /> -->
            <el-table-column prop="nickName" label="昵称" width="100" />
            <el-table-column prop="avatar" label="头像"
              ><template #default="scope"
                ><el-avatar
                  :src="scope.row.avatar"
                  :size="30"
                ></el-avatar></template
            ></el-table-column>
            <el-table-column prop="isManager" label="身份">
              <template #default="scope">
                <template v-if="scope.row.isManager">
                  <span
                    ><el-tag size="small" type="warning">管理员</el-tag></span
                  >
                </template>
                <template v-if="!scope.row.isManager">
                  <span><el-tag size="small">用户</el-tag></span>
                </template>
              </template>
            </el-table-column>
            <el-table-column prop="isForbidden" label="是否禁言" width="100">
              <template #default="scope">
                <template v-if="scope.row.isForbidden == 1">
                  <span
                    ><el-tag size="small" type="warning">已禁言</el-tag></span
                  >
                </template>
                <template v-if="scope.row.isForbidden == 0">
                  <span
                    ><el-tag size="small" type="success">未禁言</el-tag></span
                  >
                </template>
              </template>
            </el-table-column>
            <el-table-column prop="joinDate" label="进群时间" />
            <el-table-column fixed="right" label="操作" width="140">
              <template #default="scope">
                <!-- <template v-if="!scope.row.isManager"> -->
                <el-popconfirm
                  title="确定要将该会员移除当前群聊吗"
                  @confirm="delChat(scope.row.memberId)"
                >
                  <template #reference>
                    <el-link type="danger">移除</el-link>
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
  <!-- 添加成员模态窗 -->
  <el-dialog v-model="chatVisible" title="添加成员">
    <el-container>
      <el-header>
        <div class="left-panel">
          <!-- 添加 -->
          <el-button
            type="primary"
            icon="el-icon-plus"
            :disabled="selection.length == 0"
            @click="batch_add"
            >批量添加</el-button
          >
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
            @selection-change="selectionChange"
            stripe
          >
            <el-table-column type="selection" width="50"></el-table-column>
            <el-table-column type="index" label="#" width="50" />
            <el-table-column prop="nickName" label="名称" />
            <el-table-column prop="avatar" label="头像" width="100">
              <template #default="scope">
                <el-avatar :src="scope.row.avatar" :size="30"></el-avatar>
              </template>
            </el-table-column>
            <el-table-column prop="introduce" label="个性签名" />
            <el-table-column prop="cDate" label="创建时间" />
            <el-table-column fixed="right" label="操作" width="140">
              <template #default="scope">
                <!-- <template v-if="!scope.row.isManager"> -->
                <el-popconfirm
                  title="确定要将该会员加入当前群聊吗"
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
import Image from "../../../assets/icons/Image.vue";
export default {
  components: { Image },
  emits: ["success", "closed"],
  data() {
    return {
      visible: false,
      isSaveing: false,
      tableData: [],
      height: "100%",
      total: 0,
      searchList: {
        groupId: "",
        size: 10,
        page: 1,
      },
      loading: false,
      emptyText: "暂无数据",

      //可加入会员
      selection: [],
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
    // 获取群内成员
    async findForm() {
      this.loading = true;
      const res = await this.$API.groupChat.findMember.post({
        groupId: this.id,
        size: this.searchList.size,
        page: this.searchList.page,
      });
      console.log(res, "getmember");
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
    //添加/打开加入群聊(对话框)
    add() {
      this.chatVisible = true;
      this.findAbb();
    },
    //获取所有可加入会员(对话框)
    async findAbb() {
      this.joinloading = true;
      const res = await this.$API.groupChat.findisMember.post({
        size: this.searchJoin.size,
        page: this.searchJoin.page,
        chatGroupId: this.id,
        nickName: this.searchJoin.nickName,
      });
      console.log(res, "可加入聊天室的会员");
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
    //将会员添加到聊天室
    async joinChat(id) {
      // console.log(userId);
      const res = await this.$API.groupChat.addMember.post({
        groupId: this.id,
        userId: id,
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
    //将会员移除到聊天室
    async delChat(id) {
      console.log(id);
      const res = await this.$API.groupChat.delMember.post({
        groupId: this.id,
        userId: id,
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
          const res = await this.$API.groupChat.delMembers.post({
            memberIds: this.selection.map((item) => item.id),
            chatGroupId: this.id,
          });
          if (res.code !== 0) {
            Promise.reject(res.message);
          }
          this.chatVisible = false;
          this.findForm();
          this.$message.success("操作成功");
        })
        .catch((err) => {
          console.log(err);
        });
    },
    //批量添加
    async batch_add() {
      this.$confirm(
        `确定添加选中的 ${this.selection.length} 项吗？如果删除项中含有子集将会被一并添加`,
        "提示",
        {
          type: "warning",
        }
      )
        .then(async () => {
          this.loading = true;
          const res = await this.$API.groupChat.addMembers.post({
            memberIds: this.selection.map((item) => item.id),
            chatGroupId: this.id,
          });
          if (res.code !== 0) {
            Promise.reject(res.message);
          }
          this.chatVisible = false;
          this.findForm();
          this.$message.success("操作成功");
        })
        .catch((err) => {
          console.log(err);
        });
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
    //表格选择后回调事件(对话框)
    selectionChange(selection) {
      this.selection = selection;
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
      console.log(this.id, "groupId");
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
