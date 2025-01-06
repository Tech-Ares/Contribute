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
      <el-form-item label="机器人" name="memberType">
        <el-radio-group v-model="form.memberType">
          <el-radio :label="1">是</el-radio>
          <el-radio :label="2">否</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="数量" prop="addCount" v-if="form.memberType == 1">
        <el-input-number v-model="form.addCount" :min="0"> </el-input-number>
      </el-form-item>
      <el-form-item label="性别" prop="gender" v-if="form.memberType == 1">
        <el-radio-group v-model="form.gender">
          <el-radio border :label="1">男</el-radio>
          <el-radio border :label="2">女</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="昵称" prop="nickName" v-if="form.memberType == 2">
        <el-input
          v-model="form.nickName"
          placeholder="昵称"
          clearable
        ></el-input>
      </el-form-item>
      <el-form-item
        label="App登录名"
        prop="loginName"
        v-if="form.memberType == 2"
      >
        <el-input
          v-model="form.loginName"
          placeholder="请输入App登录名"
          clearable
        ></el-input
      ></el-form-item>
      <el-form-item label="登录密码" prop="pwd" v-if="form.memberType == 2">
        <el-input
          v-model="form.pwd"
          type="password"
          placeholder="请输入App登录密码"
          clearable
        ></el-input
      ></el-form-item>
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
        add: "新增会员",
      },
      visible: false,
      isSaveing: false,
      loading: false,
      //表单数据
      form: {
        addCount: 0,
        gender: 0,
        memberType: "",
        nickName: "",
        loginName: "",
        pwd: "",
      },
      rules: {
        addCount: [{ required: true, message: "请输入数量" }],
        gender: [{ required: true, message: "请选择性别" }],
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
          var res = await this.$API.appMember.addRobot.post(this.form);
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
    setData() {
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
