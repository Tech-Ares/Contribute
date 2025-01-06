<template>
  <el-dialog
    :title="titleMap[mode]"
    v-model="visible"
    :width="600"
    destroy-on-close
    @closed="$emit('closed')"
  >
    <el-form
      :model="form"
      ref="dialogForm"
      :rules="rules"
      label-width="120px"
      label-position="left"
      v-loading="loading"
    >
      <el-form-item label="标题" prop="title">
        <el-input
          v-model="form.title"
          placeholder="请输入直播标题"
          clearable
        ></el-input
      ></el-form-item>
      <el-form-item label="直播链接" prop="videoUrl">
        <el-input
          v-model="form.videoUrl"
          placeholder="请输入直播链接"
          clearable
        ></el-input>
      </el-form-item>
      <el-form-item label="直播起止时间" prop="start_end_time">
        <!-- <span class="demonstration">666</span> -->
        <el-date-picker
          v-model="form.start_end_time"
          type="daterange"
          range-separator="至"
          start-placeholder="开始日期"
          end-placeholder="结束日期"
        >
        </el-date-picker>
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="visible = false">取 消</el-button>
      <el-button
        v-if="mode != 'show'"
        type="primary"
        :loading="isSaveing"
        @click="submit()"
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
      mode: "add",
      titleMap: {
        edit: "编辑",
        add: "新增客服账号",
      },
      visible: false,
      isSaveing: false,
      loading: false,
      //表单数据
      form: {
        id: "",
        title: "",
        videoUrl: "",
        start_end_time: [],
        liveDate: "",
        endDate: "",
      },
      rules: {
        title: [{ required: true, message: "请输入直播标题" }],
        videoUrl: [{ required: true, message: "请输入直播链接" }],
        start_end_time: [
          {
            required: true,
            message: "请输入起止时间",
            trigger: "blur",
          },
        ],
      },
    };
  },
  mounted() {},

  methods: {
    // 表单提交方法
    submit() {
      this.$refs.dialogForm.validate(async (valid) => {
        if (valid) {
          this.isSaveing = true;

          var res = await this.$API.liveStream.saveForm.post({
            id: this.form.id,
            title: this.form.title,
            videoUrl: this.form.videoUrl,
            liveDate: this.form.start_end_time[0],
            endDate: this.form.start_end_time[1],
          });
          this.isSaveing = false;
          if (res.code == 0) {
            this.$emit("success", this.form, this.mode);
            this.visible = false;
            this.$message.success("操作成功");
          } else {
            this.$alert(res.message, "提示", { type: "error" });
          }
        }
      });
    },

    //显示
    open(mode = "add") {
      this.mode = mode;
      this.visible = true;
      //   this.findCustomer();
      return this;
    },
    //表单注入数据
    setData(row) {
      this.form.id = row.id;
      console.log(this.form.id, "id");
      this.form.title = row.title;
      this.form.videoUrl = row.videoUrl;
      this.form.start_end_time[0] = row.liveDate;
      this.form.start_end_time[1] = row.endDate;
      // this.findForm(id);
      //   this.findCustomer();
      // this.getmanager();
      //可以和上面一样单个注入，也可以像下面一样直接合并进去
      //Object.assign(this.form, data)
    },
  },
};
</script>

<style>
</style>
