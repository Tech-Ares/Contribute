<template>
  <el-dialog
    :title="titleMap[mode]"
    v-model="visible"
    :width="500"
    destroy-on-close
    @closed="$emit('closed')"
  >
    <el-form
      :model="form"
      ref="dialogForm"
      :rules="rules"
      label-width="100px"
      label-position="left"
      v-loading="loading"
    >
      <el-form-item label="模糊查询" prop="vague">
        <el-input
          v-model="form.vague"
          placeholder="请输入模糊查询"
          clearable
        ></el-input>
      </el-form-item>
      <el-form-item label="缩略" prop="abbre">
        <el-input
          v-model="form.abbre"
          placeholder="请输入缩略"
          clearable
        ></el-input>
      </el-form-item>
      <el-form-item label="消息内容" prop="context">
        <el-input
          v-model="form.context"
          type="textarea"
          placeholder="请输入消息内容"
          clearable
        ></el-input>
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
        edit: "编辑预设消息",
        add: "新增预设消息",
      },
      visible: false,
      isSaveing: false,
      loading: false,
      //表单数据
      form: {
        id: "",
        vague: "",
        abbre: "",
        context: "",
      },
      rules: {
        vague: [{ required: true, message: "请输入模糊查询" }],
        abbre: [{ required: true, message: "请输入缩略" }],
        context: [{ required: true, message: "请输入消息内容" }],
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
          var res = await this.$API.preset.saveForm.post(this.form);
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

      return this;
    },
    //表单注入数据
    setData(row) {
      console.log(row, "row");
      this.form.id = row.id;
      this.form.abbre = row.abbre;
      this.form.vague = row.vague;
      this.form.context = row.context;
      //   this.findForm();
      //   this.findCustomer();
      // this.getmanager();
      //可以和上面一样单个注入，也可以像下面一样直接合并进去
      //Object.assign(this.form, data)
    },
  },
};
</script>

<style>
.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}
.avatar-uploader .el-upload:hover {
  border-color: #409eff;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  text-align: center;
}
.avatar-uploader-icon svg {
  margin-top: 74px; /* (178px - 28px) / 2 - 1px */
}
.avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>
