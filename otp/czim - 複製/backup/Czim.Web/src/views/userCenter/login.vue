<template>
  <div class="login_bg">
    <div class="login_adv" style="background-image: url(img/auth_banner.jpg)">
      <div class="login_adv__title">
        <h2>xchat</h2>
        <h4>{{ $t("login.slogan") }}</h4>
        <!-- <p>{{ $t("login.describe") }}</p> -->
        <!-- <div>
          <span>
            <el-icon>
              <sc-icon-vue />
            </el-icon>
          </span>
          <span>
            <el-icon class="add">
              <el-icon-plus />
            </el-icon>
          </span>
          <span>
            <el-icon>
              <el-icon-eleme-filled />
            </el-icon>
          </span>
        </div>-->
      </div>
      <div class="login_adv__bottom">© {{ $CONFIG.APP_NAME }} {{ $CONFIG.APP_VER }}</div>
    </div>
    <div class="login_main">
      <div class="login_config">
        <el-button
          :icon="config.theme == 'dark' ? 'el-icon-sunny' : 'el-icon-moon'"
          circle
          type="info"
          @click="configTheme"
        ></el-button>
        <el-dropdown trigger="click" placement="bottom-end" @command="configLang">
          <el-button circle>
            <svg
              xmlns="http://www.w3.org/2000/svg"
              xmlns:xlink="http://www.w3.org/1999/xlink"
              aria-hidden="true"
              role="img"
              width="1em"
              height="1em"
              preserveAspectRatio="xMidYMid meet"
              viewBox="0 0 512 512"
            >
              <path
                d="M478.33 433.6l-90-218a22 22 0 0 0-40.67 0l-90 218a22 22 0 1 0 40.67 16.79L316.66 406h102.67l18.33 44.39A22 22 0 0 0 458 464a22 22 0 0 0 20.32-30.4zM334.83 362L368 281.65L401.17 362z"
                fill="currentColor"
              />
              <path
                d="M267.84 342.92a22 22 0 0 0-4.89-30.7c-.2-.15-15-11.13-36.49-34.73c39.65-53.68 62.11-114.75 71.27-143.49H330a22 22 0 0 0 0-44H214V70a22 22 0 0 0-44 0v20H54a22 22 0 0 0 0 44h197.25c-9.52 26.95-27.05 69.5-53.79 108.36c-31.41-41.68-43.08-68.65-43.17-68.87a22 22 0 0 0-40.58 17c.58 1.38 14.55 34.23 52.86 83.93c.92 1.19 1.83 2.35 2.74 3.51c-39.24 44.35-77.74 71.86-93.85 80.74a22 22 0 1 0 21.07 38.63c2.16-1.18 48.6-26.89 101.63-85.59c22.52 24.08 38 35.44 38.93 36.1a22 22 0 0 0 30.75-4.9z"
                fill="currentColor"
              />
            </svg>
          </el-button>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item
                v-for="item in lang"
                :key="item.value"
                :command="item"
                :class="{ selected: config.lang == item.value }"
              >{{ item.name }}</el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </div>
      <div class="login-form">
        <div class="login-header">
          <div class="logo">
            <img :alt="$CONFIG.APP_NAME" src="/img/x-chat.png" />
            <label>{{ $CONFIG.APP_NAME }}</label>
          </div>
          <h2>{{ $t("login.signInTitle") }}</h2>
        </div>
        <el-form ref="loginForm" :model="ruleForm" :rules="rules" label-width="0" size="large">
          <el-form-item prop="user">
            <el-input
              v-model="ruleForm.user"
              prefix-icon="el-icon-user"
              clearable
              :placeholder="$t('login.userPlaceholder')"
            >
              <!-- <template #append>
                <el-select v-model="userType" style="width: 130px">
                  <el-option :label="$t('login.admin')" value="admin"></el-option>
                  <el-option :label="$t('login.user')" value="user"></el-option>
                </el-select>
              </template>-->
            </el-input>
          </el-form-item>
          <el-form-item prop="password">
            <el-input
              v-model="ruleForm.password"
              prefix-icon="el-icon-lock"
              clearable
              show-password
              :placeholder="$t('login.PWPlaceholder')"
            ></el-input>
          </el-form-item>
          <el-form-item style="margin-bottom: 10px">
            <el-row>
              <el-col :span="12">
                <el-checkbox :label="$t('login.rememberMe')" v-model="ruleForm.autologin"></el-checkbox>
              </el-col>
              <el-col :span="12" style="text-align: right">
                <el-button type="text">{{ $t("login.forgetPassword") }}？</el-button>
              </el-col>
            </el-row>
          </el-form-item>
          <el-form-item>
            <el-button
              type="primary"
              style="width: 100%"
              :loading="islogin"
              round
              @click="login"
            >{{ $t("login.signIn") }}</el-button>
          </el-form-item>
        </el-form>

        <!-- <el-divider>{{ $t("login.signInOther") }}</el-divider>

        <div class="login-oauth">
          <el-button size="small" type="success" icon="sc-icon-wechat" circle></el-button>
        </div>-->
      </div>
    </div>
  </div>
</template>

<script>
// import WebIM from "@/utils/WebIM";

export default {
  data() {
    return {
      userType: "admin",
      ruleForm: {
        user: "",
        password: "",
        autologin: false,
      },
      rules: {
        user: [
          {
            required: true,
            message: this.$t("login.userError"),
            trigger: "blur",
          },
        ],
        password: [
          {
            required: true,
            message: this.$t("login.PWError"),
            trigger: "blur",
          },
        ],
      },
      islogin: false,
      config: {
        lang: this.$TOOL.data.get("APP_LANG") || this.$CONFIG.LANG,
        theme: this.$TOOL.data.get("APP_THEME") || "default",
      },
      lang: [
        {
          name: "简体中文",
          value: "zh-cn",
        },
        {
          name: "English",
          value: "en",
        },
        {
          name: "日本語",
          value: "ja",
        },
      ],
    };
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
  },
  created() {
    this.$TOOL.data.remove("TOKEN");
    this.$TOOL.data.remove("USER_INFO");
    this.$TOOL.data.remove("IM_USERINFO");
    this.$TOOL.data.remove("MENU");
    this.$TOOL.data.remove("grid");
    this.$store.commit("clearViewTags");
    this.$store.commit("clearKeepLive");
    this.$store.commit("clearIframeList");
  },
  methods: {
    // 登陆方法
    async login() {
      let validate = await this.$refs.loginForm.validate().catch(() => { });
      if (!validate) {
        return false;
      }

      this.islogin = true;
      let data = {
        username: this.ruleForm.user,
        password: this.ruleForm.password,
        // password: this.$TOOL.crypto.MD5(this.ruleForm.password),
        //this.$TOOL.crypto.MD5(this.ruleForm.password),
      };
      //获取后台 token
      let user = await this.$API.auth.token.post(data);
      this.islogin = false;
      console.log(user);
      if (user.code !== 0) {
        this.$message.warning(user.message);
        return false;
      }
      this.$TOOL.data.set("TOKEN", user.data.token)
      // 存一套账号密码 给环信登录使用
      this.$store.commit('setUserSecret', user.data.userInfo)
      let msgList = this.$TOOL.data.get(`${user.data.userInfo.userId}_IM_MSGLIST`)
      if (!msgList) {
        let list = {
          contact: {},
          groupchat: {},
          chatroom: {}
        }
        this.$TOOL.data.set(`${user.data.userInfo.userId}_IM_MSGLIST`, list);
      }
      //获取菜单
      const userMenu = await this.$API.sysMenu.myMenus.get();
      console.log(userMenu);
      if (userMenu.code == 0) {
        if (userMenu.data.menu.length == 0) {
          this.islogin = false;
          this.$alert(
            "当前用户无任何菜单权限，请联系系统管理员",
            "无权限访问",
            {
              type: "error",
              center: true,
            }
          );
          return false;
        }
        this.$TOOL.data.set("MENU", userMenu.data.menu);
      } else {
        this.islogin = false;
        this.$message.warning(userMenu.message);
        return false;
      }
      this.$router.replace({
        path: "/",
      });
      this.$message.success("Login Success 登录成功");
      this.islogin = false;
    },
    configTheme() {
      this.config.theme = this.config.theme == "default" ? "dark" : "default";
    },
    configLang(command) {
      this.config.lang = command.value;
    },
  },
};
</script>

<style scoped>
.login_bg {
  width: 100%;
  height: 100%;
  background: #fff;
  display: flex;
}
.login_adv {
  width: 33.33333%;
  background-color: #555;
  background-size: cover;
  background-position: center center;
  background-repeat: no-repeat;
  position: relative;
}
.login_adv__title {
  color: #fff;
  padding: 40px;
}
.login_adv__title h2 {
  font-size: 40px;
}
.login_adv__title h4 {
  font-size: 18px;
  margin-top: 10px;
  font-weight: normal;
}
.login_adv__title p {
  font-size: 14px;
  margin-top: 10px;
  line-height: 1.8;
  color: rgba(255, 255, 255, 0.6);
}
.login_adv__title div {
  margin-top: 10px;
  display: flex;
  align-items: center;
}
.login_adv__title div span {
  margin-right: 15px;
}
.login_adv__title div i {
  font-size: 40px;
}
.login_adv__title div i.add {
  font-size: 20px;
  color: rgba(255, 255, 255, 0.6);
}
.login_adv__bottom {
  position: absolute;
  left: 0px;
  right: 0px;
  bottom: 0px;
  color: #fff;
  padding: 40px;
  background-image: linear-gradient(transparent, #000);
}

.login_main {
  flex: 1;
  overflow: auto;
  display: flex;
}
.login-form {
  width: 400px;
  margin: auto;
  padding: 20px 0;
}
.login-header {
  margin-bottom: 20px;
}
.login-header .logo {
  display: flex;
  align-items: center;
}
.login-header .logo img {
  width: 35px;
  height: 35px;
  vertical-align: bottom;
  margin-right: 10px;
}
.login-header .logo label {
  font-size: 24px;
}
.login-header h2 {
  font-size: 24px;
  font-weight: bold;
  margin-top: 50px;
}
.login-oauth {
  display: flex;
  justify-content: space-around;
}
.login-form .el-divider {
  margin-top: 40px;
}

.login_config {
  position: absolute;
  top: 20px;
  right: 20px;
}
.el-dropdown-menu__item.selected {
  color: var(--el-color-primary);
}

@media (max-width: 1200px) {
  .login-form {
    width: 340px;
  }
}
@media (max-width: 1000px) {
  .login_main {
    display: block;
  }
  .login-form {
    width: 100%;
    padding: 20px 40px;
  }
  .login_adv {
    display: none;
  }
}
</style>
