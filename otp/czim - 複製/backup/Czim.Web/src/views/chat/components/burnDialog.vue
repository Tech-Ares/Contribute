<template>
  <el-dialog
    v-model="dialogVisible"
    title="阅后即焚"
    width="30%"
    destroy-on-close
    ref="burnDialog"
    center
  >
    <div class="count-down">
      剩余时间:
      <span class="count-down-num">{{ info.times }}</span> 秒
    </div>
    <p>消息内容: {{ info.msg.msg }}</p>
  </el-dialog>
</template>
<script setup>
import { computed, reactive, ref } from 'vue';

const dialogVisible = ref(false)
const burnDialog = ref()
const info = reactive({
  msg: {},
  times: computed(() => info.msg?.ext.fireTime / 1000 || 0)
})

const open = (msg) => {
  info.msg = msg
  dialogVisible.value = true
}

const emit = defineEmits(['burn-msg'])

const countDown = () => {
  let timer = null
  timer = setInterval(() => {
    info.msg.ext.fireTime -= 1000
    if (info.times <= 0) {
      clearInterval(timer)
      info.msg.msg = '消息已焚毁'
      emit('burn-msg', info.msg)
      dialogVisible.value = false
    }
  }, 1000);
}

defineExpose({
  open,
  countDown
})
</script>
<style lang="scss" scoped>
.count-down {
  display: flex;
  justify-content: center;
  align-items: baseline;
  font-size: 16px;
  .count-down-num {
    font-size: 22px;
  }
}
</style>