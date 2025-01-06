<template>
  <el-drawer v-model="visible" size="50%" :with-header="false">
    <el-tabs v-model="activeTab" @tab-click="tabEvent" type="card">
      <el-tab-pane label="详情页面" name="addGroup">
        <el-descriptions :column="3" border v-loading="loading">
          <el-descriptions-item
            label="登录名"
            label-align="right"
            align="center"
            >{{ form.data.loginName }}</el-descriptions-item
          >
          <el-descriptions-item label="头像" label-align="right" align="center">
            <el-avatar
              :src="form.data.avatar"
              :width="35"
              :height="35"
              shape="square"
            />
          </el-descriptions-item>
          <el-descriptions-item label="会员" label-align="right" align="center"
            ><span v-if="form.data.isMember == true"
              ><el-tag size="small">是</el-tag></span
            >
            <span v-if="form.data.isMember == false"
              ><el-tag size="small" type="danger">否</el-tag></span
            ></el-descriptions-item
          >
          <el-descriptions-item
            label="昵称"
            label-align="right"
            align="center"
            >{{ form.data.nickName }}</el-descriptions-item
          >
          <el-descriptions-item label="性别" label-align="right" align="center"
            ><template v-if="form.data.gender == 0">---</template
            ><span v-if="form.data.gender == 1"
              ><el-icon color="#409EFF"><el-icon-male /></el-icon
            ></span>
            <span v-if="form.data.gender == 2"
              ><el-icon color="#F56C6C"><el-icon-male /></el-icon></span
          ></el-descriptions-item>
          <el-descriptions-item
            label="出生日期"
            label-align="right"
            align="center"
            >{{ form.data.birthday }}</el-descriptions-item
          >
          <el-descriptions-item
            label="留言"
            label-align="right"
            align="center"
            >{{ form.data.introduce }}</el-descriptions-item
          >
        </el-descriptions>
      </el-tab-pane>
      <el-tab-pane label="登录日志" lazy name="addFriend">
        <el-container>
          <el-main class="nopadding">
            <div class="kxTable-table" v-loading="kxloading">
              <!-- 数据表 -->
              <el-table
                :data="tableData"
                ref="kxTable"
                row-key="id"
                style="width: 98%"
                :height="height == 'auto' ? null : '100%'"
                stripe
              >
                <!-- <el-table-column prop="loginName" label="登录账号" /> -->
                <el-table-column prop="loginIP" label="登录IP" />
                <el-table-column prop="loginOS" label="操作系统" />
                <el-table-column prop="loginModel" label="登录机型" />
                <el-table-column prop="loginTime" label="登录时间" />
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
        size: 10,
        page: 1,
      },
      kxloading: false,
      emptyText: "暂无数据",
      //详情数据
      loading: false,
      form: {
        data: [],
      },
      activeTab: "addGroup",
      id: "",
    };
  },
  mounted() {
    // this.getGroup()
  },
  methods: {
    // 根据id查询当前用户数据
    async findMember() {
      this.loading = true;
      const res = await this.$API.appMember.findMember.post({
        memberId: this.id,
      });
      console.log(res, "详情");
      this.loading = false;
      if (res.code !== 0) {
        return this.$message.warning("获取数据失败");
      }
      this.form.data = res.data;
      //   console.log(this.form.data);
    },
    // 获取登录日志
    async getData() {
      this.kxloading = true;
      const res = await this.$API.appMember.findLogin.post({
        memberId: this.id,
        size: this.searchList.size,
        page: this.searchList.page,
      });
      console.log(res, "登录日志");
      this.kxloading = false;
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
      this.getData(this.id);
    },
    // // 本地更新数据
    // handleSuccess() {
    //   this.refresh();
    // },
    //显示
    open() {
      this.visible = true;
      return this;
    },
    //表单注入数据
    setData(id) {
      this.id = id;
      this.findMember();
      this.getData();
      // this.getData();
      // this.getData(this.id);
      //可以和上面一样单个注入，也可以像下面一样直接合并进去
      //Object.assign(this.form, data)
    },
    //标签页控制
    tabEvent(tab, event) {
      console.log(tab, event);
      if (tab.props.name === "addGroup") {
        this.findMember(this.id);
      }
      if (tab.props.name === "addFriend") {
        this.getData(this.id);
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
.el-descriptions {
  margin-left: 20px;
  width: 95%;
}
.el-icon svg {
  height: 1.5em;
  width: 1.5em;
}
</style>
