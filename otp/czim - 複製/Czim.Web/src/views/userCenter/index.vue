<template>
  <el-main>
    <el-row :gutter="15">
      <el-col :lg="8">
        <el-card shadow="never">
          <div class="user-info">
            <div class="user-info-top">
              <el-avatar v-if="userInfo.avatar" :size="80" :src="userInfo.avatar"></el-avatar>
              <el-avatar v-else :size="80">{{ userInfo.nickName.substring(0,1) }}</el-avatar>
              <h2>{{ userInfo.nickName || "-" }}</h2>
              <!-- <p>{{ form.about || "无签名" }}</p> -->
              <el-button
                type="primary"
                round
                icon="el-icon-collection-tag"
                size="medium"
                >Administrator</el-button
              >
            </div>
            <div class="user-info-main">
              <!-- <ul>
                <li>
                  <label>
                    <el-icon><el-icon-user /> </el-icon
                  ></label>
                  <span>CrabIM@qq.com</span>
                </li>
                <li>
                  <label
                    ><el-icon><el-icon-present /></el-icon></label
                  ><span>2022-01-01</span>
                </li>
                <li>
                  <label
                    ><el-icon><el-icon-male /></el-icon></label
                  ><span>男</span>
                </li>
                <li>
                  <label
                    ><el-icon><el-icon-location /></el-icon></label
                  ><span>中国/上海/张江</span>
                </li>
                <li>
                  <label
                    ><el-icon><el-icon-office-building /></el-icon></label
                  ><span>集团/上海分公司/软件研发部/摸鱼组</span>
                </li>
                <li>
                  <label
                    ><el-icon><el-icon-coin /></el-icon></label
                  ><span>超级管理员</span>
                </li>
              </ul> -->
            </div>
            <div class="user-info-bottom">
              <h2>当前账号权限</h2>
              <el-space wrap>
                <el-tag v-auth="'user.add'">user.add</el-tag>
                <el-tag v-auth="'user.edit'">user.edit</el-tag>
                <el-tag v-if="$AUTH('user.delete')">user.delete</el-tag>
                <el-tag v-if="$AUTH('list.add')">list.add</el-tag>
                <el-tag v-if="$AUTH('list.edit')">list.edit</el-tag>
                <el-tag v-if="$AUTH('list.delete')">list.delete</el-tag>
              </el-space>
            </div>
          </div>
        </el-card>
      </el-col>
      <el-col :lg="16">
        <el-card shadow="never">
          <el-tabs tab-position="top">
            <el-tab-pane :label="$t('user.settings')">
              <el-form
                ref="form"
                :model="form"
                label-width="120px"
                style="margin-top: 20px"
              >
                <el-form-item :label="$t('user.nightmode')">
                  <el-switch
                    v-model="config.theme"
                    active-value="dark"
                    inactive-value="default"
                    inline-prompt
                    active-icon="el-icon-moon"
                    inactive-icon="el-icon-sunny"
                  ></el-switch>
                  <div class="el-form-item-msg">
                    {{ $t("user.nightmode_msg") }}
                  </div>
                </el-form-item>
                <el-form-item label="主题颜色">
                  <el-color-picker
                    v-model="config.colorPrimary"
                    :predefine="colorList"
                    >></el-color-picker
                  >
                </el-form-item>
                <el-form-item :label="$t('user.language')">
                  <el-select v-model="config.lang">
                    <el-option label="简体中文" value="zh-cn"></el-option>
                    <el-option label="English" value="en"></el-option>
                    <el-option label="日本語" value="ja"></el-option>
                  </el-select>
                  <div class="el-form-item-msg">
                    {{ $t("user.language_msg") }}
                  </div>
                </el-form-item>
              </el-form>
            </el-tab-pane>
            <el-tab-pane :label="$t('user.dynamic')">
              <el-timeline style="margin-top: 20px; padding-left: 10px">
                <el-timeline-item
                  v-for="(activity, index) in activities"
                  :key="index"
                  :timestamp="activity.timestamp"
                  placement="top"
                >
                  <div class="activity-item">
                    <el-avatar
                      class="avatar"
                      :size="24"
                      src="img/avatar.jpg"
                    ></el-avatar>
                    <label>{{ activity.operate }}</label
                    ><el-tag v-if="activity.mod" size="mini">{{
                      activity.mod
                    }}</el-tag
                    >{{ activity.describe }}
                  </div>
                </el-timeline-item>
              </el-timeline>
            </el-tab-pane>
            <el-tab-pane :label="$t('user.info')">
              <el-form
                ref="form"
                :model="form"
                label-width="80px"
                style="width: 460px; margin-top: 20px"
              >
                <el-form-item label="账号">
                  <el-input v-model="form.user" disabled></el-input>
                  <div class="el-form-item-msg">
                    账号信息用于登录，系统不允许修改
                  </div>
                </el-form-item>
                <el-form-item label="姓名">
                  <el-input v-model="form.name"></el-input>
                </el-form-item>
                <!-- <el-form-item label="性别">
                  <el-select v-model="form.sex" placeholder="请选择">
                    <el-option label="保密" value="0"></el-option>
                    <el-option label="男" value="1"></el-option>
                    <el-option label="女" value="2"></el-option>
                  </el-select>
                </el-form-item>
                <el-form-item label="个性签名">
                  <el-input v-model="form.about" type="textarea"></el-input>
                </el-form-item> -->
                <el-form-item label="头像" prop="avatar">
                  <!-- 上传组件 -->
                  <el-upload
                    action="#"
                    class="avatar-uploader"
                    :show-file-list="false"
                    :http-request="handleUpload"
                    :before-upload="beforeAvatarUpload"
                  >
                    <img class="avatar" v-if="userInfo.avatar" :src="userInfo.avatar" />
                    <el-icon v-else class="avatar-uploader-icon"
                      ><el-icon-plus
                    /></el-icon>
                  </el-upload>
                </el-form-item>
                <el-form-item>
                  <el-button type="primary" @click="saveForm">保存</el-button>
                </el-form-item>
              </el-form>
            </el-tab-pane>
          </el-tabs>
        </el-card>
      </el-col>
    </el-row>
  </el-main>
</template>

<script>
import colorTool from "@/utils/color";

export default {
  name: "userCenter",
  data() {
    return {
      activities: [
        // {
        //   operate: "更改了",
        //   mod: "系统配置",
        //   describe: "systemName 为 SCUI",
        //   type: "edit",
        //   timestamp: "刚刚",
        // },
        // {
        //   operate: "删除了",
        //   mod: "用户",
        //   describe: "USER",
        //   type: "del",
        //   timestamp: "5分钟前",
        // },
        // {
        //   operate: "禁用了",
        //   mod: "用户",
        //   describe: "USER",
        //   type: "del",
        //   timestamp: "5分钟前",
        // },
        // {
        //   operate: "创建了",
        //   mod: "用户",
        //   describe: "USER",
        //   type: "add",
        //   timestamp: "5分钟前",
        // },
        // {
        //   operate: "审核了",
        //   mod: "用户",
        //   describe: "lolowan 为 通过",
        //   type: "add",
        //   timestamp: "10分钟前",
        // },
        // {
        //   operate: "登录",
        //   mod: "",
        //   describe: "成功",
        //   type: "do",
        //   timestamp: "1小时前",
        // },
      ],
      form: {
        user: "81883387@qq.com",
        name: '',
        sex: "1",
        about: "正所谓富贵险中求",
        avatar: "",
      },
      colorList: [
        "#409EFF",
        "#009688",
        "#536dfe",
        "#ff5c93",
        "#c62f2f",
        "#fd726d",
      ],
      config: {
        lang: this.$TOOL.data.get("APP_LANG") || this.$CONFIG.LANG,
        theme: this.$TOOL.data.get("APP_THEME") || "default",
        colorPrimary:
          this.$TOOL.data.get("APP_COLOR") || this.$CONFIG.COLOR || "#409EFF",
      },
    };
  },
  computed: {
    userInfo() {
      return this.$store.state.user.userSecret
    }
  },
  watch: {
    "config.theme"(val) {
      document.body.setAttribute("data-theme", val);
      this.$TOOL.data.set("APP_THEME", val);
    },
    "config.lang"(val) {
      this.$i18n.locale = val;
      this.$TOOL.data.set("APP_LANG", val);
    },
    "config.colorPrimary"(val) {
      document.documentElement.style.setProperty("--el-color-primary", val);
      for (let i = 1; i <= 9; i++) {
        document.documentElement.style.setProperty(
          `--el-color-primary-light-${i}`,
          colorTool.lighten(val, i / 10)
        );
      }
      document.documentElement.style.setProperty(
        `--el-color-primary-darken-1`,
        colorTool.darken(val, 0.1)
      );
      this.$TOOL.data.set("APP_COLOR", val);
    },
  },
  //路由跳转进来 判断from是否有特殊标识做特殊处理
  beforeRouteEnter(to, from, next) {
    next((vm) => {
      if (from.is) {
        //删除特殊标识，防止标签刷新重复执行
        delete from.is;
        //执行特殊方法
        vm.$alert("路由跳转过来后含有特殊标识，做特殊处理", "提示", {
          type: "success",
          center: true,
        })
          .then(() => {})
          .catch(() => {});
      }
    });
  },
  methods: {
    // 保存
    async saveForm() {
      // console.log(id, "id");
      // this.loading = true;
      const res = await this.$API.customerService.upInfo.post({
        avatar: this.form.avatar,
        nickName: this.form.name,
      });
      console.log(res);
      // this.loading = false;
      if (res.code !== 0) {
        return this.$message.warning("获取数据失败");
      } else {
        this.$message.success("操作成功");
      }
      // this.form = res.data;
      // console.log(this.form);
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
      this.form.avatar = res.data[0].imageUrl;
      console.log(res);
    },
  },
};
</script>

<style scoped>
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
.el-card {
  margin-bottom: 15px;
}
.activity-item {
  font-size: 13px;
  color: #999;
  display: flex;
  align-items: center;
}
.activity-item label {
  color: #333;
  margin-right: 10px;
}
.activity-item .el-avatar {
  margin-right: 10px;
}
.activity-item .el-tag {
  margin-right: 10px;
}

[data-theme="dark"] .user-info-bottom {
  border-color: var(--el-border-color-base);
}
[data-theme="dark"] .activity-item label {
  color: #999;
}
</style>
