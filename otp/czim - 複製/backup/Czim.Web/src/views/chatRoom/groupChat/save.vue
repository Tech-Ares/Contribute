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
      <el-form-item label="管理员" prop="ownId">
        <el-select v-model="form.ownId" placeholder="选择管理员">
          <el-option
            v-for="item in managerOptions"
            :key="item.value"
            :value="item.label"
            :label="item.value"
          >
            {{ item.value }}
          </el-option>
        </el-select>
      </el-form-item>
      <!-- -------------------------------------------- -->
      <el-form-item label="群组类型" prop="isPublic">
        <el-radio-group v-model="form.isPublic">
          <el-radio :label="true">公开群</el-radio>
          <el-radio :label="false">私有群</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="需要审批" prop="memberSonly">
        <el-radio-group v-model="form.memberSonly">
          <el-radio :label="true">是</el-radio>
          <el-radio :label="false">否</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="允许邀请" prop="allowInvites">
        <el-radio-group v-model="form.allowInvites">
          <el-radio :label="true">是</el-radio>
          <el-radio :label="false">否</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="成员上限" prop="maxusers">
        <el-input-number v-model="form.maxusers" :min="0"> </el-input-number>
      </el-form-item>
      <el-form-item label="群名称" prop="groupName">
        <el-input
          v-model="form.groupName"
          placeholder="群名称"
          clearable
        ></el-input>
      </el-form-item>
      <el-form-item label="群描述" prop="description">
        <el-input
          v-model="form.description"
          placeholder="群描述"
          clearable
        ></el-input>
      </el-form-item>
      <el-form-item label="群公告" prop="announcement">
        <el-input
          v-model="form.announcement"
          type="textarea"
          placeholder="群公告"
          clearable
        ></el-input>
      </el-form-item>
      <el-form-item label="群封面" prop="groupImg">
        <!-- 上传组件 -->
        <el-upload
          action="#"
          class="avatar-uploader"
          :show-file-list="false"
          :http-request="handleUpload"
          :before-upload="beforeAvatarUpload"
        >
          <img class="avatar" v-if="form.groupImg" :src="form.groupImg" />
          <el-icon v-else class="avatar-uploader-icon"
            ><el-icon-plus
          /></el-icon>
        </el-upload>
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
        add: "新增",
      },
      visible: false,
      isSaveing: false,
      loading: false,

      //表单数据
      form: {
        groupName: "",
        groupImg: "",
        announcement: "",
        description: "",
        memberSonly: "",
        allowInvites: "",
        maxusers: 0,
        ownId: "",
        isPublic: "",
      },
      managerOptions: [],
      file: {},
      rules: {
        ownId: [{ required: true, message: "请选择群主", trigger: "change" }],
        isPublic: [{ required: true, message: "请选择群组类型" }],
        memberSonly: [
          { required: true, message: "请选择是否需要审批才可加入" },
        ],
        allowInvites: [{ required: true, message: "请选择是否允许邀请" }],
        maxusers: [{ required: true, message: "请输入成员上限" }],
        groupName: [{ required: true, message: "请输入群名称" }],
        description: [{ required: true, message: "请输入群描述" }],
        groupImg: [{ required: true, message: "请上传群封面" }],
      },
    };
  },
  mounted() {},

  methods: {
    // 根据id查询当前群数据
    async findForm(id) {
      // console.log(id, "id");
      this.loading = true;
      const res = await this.$API.groupChat.findForm.post(id);
      console.log(res, "获取当前数据");
      this.loading = false;
      if (res.code !== 0) {
        return this.$message.warning("获取数据失败");
      }
      this.form = res.data;
      // console.log(this.form);
    },
    // 获取管理员
    async getmanager() {
      const res = await this.$API.chatRoom.getManager.get();
      console.log(res, "getmanager");
      this.loading = false;
      if (res.code !== 0) {
        return this.$message.warning("获取数据失败");
      }
      let opts = res.data.map((item) => ({
        value: item.managerName,
        label: item.managerId,
      }));
      console.log(opts, "opts");
      this.managerOptions = opts;
      console.log(this.managerOptions);
    },
    // 表单提交方法
    submit() {
      this.$refs.dialogForm.validate(async (valid) => {
        if (valid) {
          this.isSaveing = true;
          var res = await this.$API.groupChat.createChat.post(this.form);
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
      // const isJPG = file.type === "image/jpeg";
      const isLt2M = file.size / 1024 / 1024 < 2;

      // if (!isJPG) {
      //   this.$message.error("Avatar picture must be JPG format!");
      // }
      if (!isLt2M) {
        this.$message.error("Avatar picture size can not exceed 2MB!");
      }
      // return isJPG && isLt2M;
      return isLt2M;
    },
    // 图片上传
    async handleUpload() {
      const configs = {
        headers: { "Content-Type": "multipart/form-data" },
      };
      const formData = new FormData();
      formData.append("file", this.file);
      const res = await this.$API.fileUpload.upload.post("", formData, configs);
      this.form.groupImg = res.data[0].imageUrl;
      console.log(res);
    },
    //显示
    open(mode = "add") {
      this.mode = mode;
      this.visible = true;
      this.getmanager();
      return this;
    },
    //表单注入数据
    setData(id) {
      this.findForm(id);

      console.log(id, "id");
      // this.getmanager();
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
