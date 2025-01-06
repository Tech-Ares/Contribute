<template>
  <el-row>
    <el-col :lg="24">
      <el-card shadow="never">
        <div class="user-info">
          <div class="user-info-top">
            <el-avatar :size="80" src="img/avatar.jpg"></el-avatar>
            <!-- <h2>{{ form.name || "-" }}</h2>
            <p>{{ form.about || "无签名" }}</p>-->
            <h2>-</h2>
            <p>无签名</p>
            <el-button
              type="primary"
              round
              icon="el-icon-edit"
              size="medium"
            >修改个人信息</el-button>
          </div>
          <div class="user-info-main" v-if="!status.isEdit">
            <ul>
              <li>
                <label>
                  <el-icon>
                    <el-icon-user />
                  </el-icon>
                </label>
                <span>81883387@qq.com</span>
              </li>
              <li>
                <label>
                  <el-icon>
                    <el-icon-present />
                  </el-icon>
                </label>
                <span>2022-01-01</span>
              </li>
              <li>
                <label>
                  <el-icon>
                    <el-icon-male />
                  </el-icon>
                </label>
                <span>男</span>
              </li>
              <li>
                <label>
                  <el-icon>
                    <el-icon-location />
                  </el-icon>
                </label>
                <span>中国/上海/金山</span>
              </li>
              <li>
                <label>
                  <el-icon>
                    <el-icon-office-building />
                  </el-icon>
                </label>
                <span>集团/上海分公司/软件研发部/摸鱼组</span>
              </li>
              <li>
                <label>
                  <el-icon>
                    <el-icon-coin />
                  </el-icon>
                </label>
                <span>超级管理员</span>
              </li>
            </ul>
          </div>
        </div>
      </el-card>
    </el-col>
  </el-row>
</template>
<script setup>
// import WebIM from "@/utils/WebIM"
import { reactive } from 'vue';
// import { onMounted, reactive } from 'vue';
// import { useStore } from "vuex";

// 状态管理
const status = reactive({
  isEdit: false,
  loading: false
})

//
// const store = useStore()

// onMounted(() => {
//   loadUserInfo()
// })

// 获取用户属性
// const loadUserInfo = async () => {
//   let user = store.state.user.user.username
//   const res = await WebIM.conn.fetchUserInfoById(user)
//   console.log(res);
// }

</script>
<style lang="scss" scoped>
.user-info {
  padding: 20px 25px;
}
</style>