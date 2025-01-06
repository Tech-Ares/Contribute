<template>
  <el-container>
    <!-- <el-header> </el-header> -->
    <el-main class="nopadding">
      <div class="kxTable-table" v-loading="loading">
        <el-form :model="form" ref="form" size="large" border>
          <el-form-item
            :label="`${item.typeName}：`"
            v-for="item in form"
            :key="item.id"
          >
            <el-switch
              v-model="item.typeValue"
              active-value="1"
              inactive-value="0"
              active-color="#13ce66"
              inactive-color="#ff4949"
              active-text="开启"
              inactive-text="关闭"
              inline-prompt
              @click="saveForm(item)"
          /></el-form-item>
        </el-form>
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
</template>

<script>
export default {
  name: "loginList",
  components: {},
  data() {
    return {
      form: [],
      height: "100%",
      total: 0,
      searchList: {
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
    // 获取基础配置数据
    async getData() {
      this.loading = true;
      const res = await this.$API.baseSetting.findList.post(this.searchList);
      console.log(res);
      this.loading = false;
      if (res.code !== 0) {
        return this.$message.error("获取数据失败");
      }
      this.form = res.data.rows;
      this.searchList.size = res.data.pageSize;
      this.total = res.data.total;
      this.searchList.page = res.data.page;
      console.log(this.form, "form");
    },
    // 修改
    async saveForm(item) {
      const res = await this.$API.baseSetting.saveForm.post({
        id: item.id,
        typeValue: item.typeValue,
      });
      if (res.code == 0) {
        this.$emit("success", this.form, this.mode);
        this.visible = false;
        this.$message.success("操作成功");
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
      // this.$refs.kxTable.clearSelection();
      this.getData();
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
  background-color: #f5f5f8;
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
.el-form {
  margin-left: 200px;
  width: 600px;
  // border: 0px solid #000;
}
.el-form-item {
  margin-left: 100px;
  padding: 20px 20px 10px 20px;
}
.el-switch {
  margin-left: 20px;
  padding: 20px 10px 20px 10px;
}
</style>