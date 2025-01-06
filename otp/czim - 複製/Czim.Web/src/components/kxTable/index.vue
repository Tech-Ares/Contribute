<!--
 * @Descripttion: 数据表格组件
 * @version: 1.0
 * @Author: 王昶 - 酷象信息
 * @Date: 2022年1月18日16:31
 * @LastEditors: 王昶
 * @LastEditTime: 2022年1月18日16:31
-->
<template>
  <div class="kxTable" v-loading="loading">
    <div class="kxTable-table">
      <el-table
        v-bind="$attrs"
        :data="tableData"
        ref="kxTable"
        :row-key="rowKey"
        style="width: 100%"
        :height="tableHeight"
        :stripe="stripe"
        :border="border"
      >
        <slot></slot>
        <template #empty>
          <el-empty :description="emptyText" :image-size="100"></el-empty>
        </template>
      </el-table>
    </div>
    <div class="kxTable-pagination-box" v-if="!hidePagination">
      <el-pagination
        background
        :small="paginationSize"
        :layout="layout"
        :total="totalNum"
        :page-sizes="pageSizes"
        v-model:currentPage="currentPage"
        @current-change="handleCurrentChange"
        @size-change="handleSizeChange"
      ></el-pagination>
      <div class="kxTable-do">
        <el-button @click="refresh" icon="el-icon-refresh" circle style="margin-left:15px"></el-button>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: 'kxTable',
  props: {
    rowKey: {
      type: String,
      default: ''
    },
    loading: {
      type: Boolean,
      default: false
    },
    data: {
      type: Array,
      default: () => []
    },
    page: {
      default: 1
    },
    total: {
      type: Number,
      default: 0
    },
    pageSizes: {
      type: Array,
      default: () => [10, 20, 30, 50]
    },
    layout: {
      type: String,
      default: "total, sizes, prev, pager, next, jumper"
    },
    stripe: {
      type: Boolean,
      default: false
    },
    border: {
      type: Boolean,
      default: false
    },
    paginationSize: {
      type: Boolean,
      default: true
    },
    hidePagination: { type: Boolean, default: false },
  },
  watch: {
    //监听从props里拿到值了
    data() {
      console.log(this.rowKey);
      this.tableData = this.data;
      this.totalNum = this.total;
    },
  },
  data() {
    return {
      tableData: [],
      totalNum: 0,
      currentPage: 1,
      emptyText: "暂无数据",
      tableHeight: '100%',
    }
  },
  methods: {
    handleCurrentChange(val) {
      this.$emit('handleCurrentChange', val)
      this.$refs.kxTable.$el.querySelector('.el-table__body-wrapper').scrollTop = 0
    },
    handleSizeChange(val) {
      this.$emit('handleSizeChange', val)
      this.$refs.kxTable.$el.querySelector('.el-table__body-wrapper').scrollTop = 0
    },
    refresh() {
      this.$emit('refresh')
      this.$refs.kxTable.$el.querySelector('.el-table__body-wrapper').scrollTop = 0
    },
    selectionChange() {
      this.$refs.kxTable.clearSelection();
      this.$emit('selectionChange')
    }
  },
}
</script>
<style lang="scss" scoped>
.kxTable-table {
  // height: calc(100% - 50px);
  height: 100%;
}
.kxTable-pagination-box {
  height: 50px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 15px;
}
</style>
