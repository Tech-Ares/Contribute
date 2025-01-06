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
      <el-form-item label="客服" prop="userId">
        <el-select v-model="form.userId" placeholder="请选择客服">
          <el-option
            v-for="item in managerOptions"
            :key="item.value"
            :value="item.value"
            :label="item.label"
          >
            {{ item.label }}
          </el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="昵称" prop="nickName">
        <el-input
          v-model="form.nickName"
          placeholder="请输入昵称"
          clearable
        ></el-input>
      </el-form-item>
      <el-form-item label="推广员" name="isAgent">
        <el-radio-group v-model="form.isAgent">
          <el-radio :label="true">是</el-radio>
          <el-radio :label="false">否</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="头像" prop="avatar">
        <!-- 上传组件 -->
        <el-upload
          action="#"
          class="avatar-uploader"
          :show-file-list="false"
          :http-request="handleUpload"
          :before-upload="beforeAvatarUpload"
        >
          <img class="avatar" v-if="form.avatar" :src="form.avatar" />
          <el-icon v-else class="avatar-uploader-icon"
            ><el-icon-plus
          /></el-icon>
        </el-upload>
      </el-form-item>
      <el-form-item label="App登录名" prop="loginName" v-if="form.isAgent">
        <el-input
          v-model="form.loginName"
          placeholder="请输入App登录名"
          clearable
        ></el-input
      ></el-form-item>
      <el-form-item label="登录密码" prop="passWord" v-if="form.isAgent">
        <el-input
          v-model="form.passWord"
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
        add: "新增客服账号",
      },
      visible: false,
      isSaveing: false,
      loading: false,
      //表单数据
      form: {
        userId: "",
        nickName: "",
        avatar: "",
        isAgent: false,
        loginName: "",
        passWord: "",
      },
      managerOptions: [],
      file: {},
      rules: {
        userId: [
          { required: true, message: "请选择管理员", trigger: "change" },
        ],
        nickName: [{ required: true, message: "请输入昵称" }],
        avatar: [{ required: true, message: "请上传头像" }],
        loginName: [{ required: true, message: "请填写登录名" }],
        passWord: [{ required: true, message: "请输入密码" }],
      },
    };
  },
  mounted() {},

  methods: {
    // 获取管理员
    async findCustomer() {
      const res = await this.$API.customerService.findCustomer.get();
      console.log(res, "findCustomer");
      this.loading = false;
      if (res.code !== 0) {
        return this.$message.warning("获取数据失败");
      }
      this.managerOptions = res.data;
      console.log(this.managerOptions);
    },
    // 表单提交方法
    submit() {
      this.$refs.dialogForm.validate(async (valid) => {
        if (valid) {
          this.isSaveing = true;
          var res = await this.$API.customerService.saveForm.post(this.form);
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
    // 图片上传前
    beforeAvatarUpload(file) {
      this.file = file;
      console.log(file);
      const isJPG = file.type === "image/jpeg";
      const isLt2M = file.size / 1024 / 1024 < 2;

      if (!isJPG) {
        this.$message.error("Avatar picture must be JPG format!");
      }
      if (!isLt2M) {
        this.$message.error("Avatar picture size can not exceed 2MB!");
      }
      return isJPG && isLt2M;
    },
    // 图片上传
    async handleUpload() {
      const configs = {
        headers: { "Content-Type": "multipart/form-data" },
      };
      const formData = new FormData();
      formData.append("file", this.file);
      const res = await this.$API.fileUpload.upload.post("", formData, configs);
      this.form.avatar = res.data[0].imageUrl;
      console.log(res);
    },
    //显示
    open(mode = "add") {
      this.mode = mode;
      this.visible = true;
      this.findCustomer();
      return this;
    },
    //表单注入数据
    setData() {
      // this.findForm(id);
      this.findCustomer();
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
