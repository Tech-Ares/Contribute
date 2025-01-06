<template>
  <div class="editor-box">
    <!-- 功能栏 -->
    <div class="editor-bar">
      <ul class="button-box">
        <li>
          <el-tooltip content="发送图片" placement="top-start">
            <el-icon>
              <el-upload
                action
                :show-file-list="false"
                :http-request="handleUpload"
                :before-upload="beforeUpload"
                :disabled="!currentChatValid"
              >
                <sc-icon-image />
              </el-upload>
            </el-icon>
          </el-tooltip>
        </li>
        <li>
          <el-tooltip content="发送表情" placement="top-start">
            <el-icon @click="showEmoji">
              <sc-icon-thumbs-up />
            </el-icon>
          </el-tooltip>
        </li>
        <li v-if="currentChat.czType === 0">
          <el-tooltip content="阅后即焚" placement="top-start">
            <el-icon @click="showBurn">
              <sc-icon-hot />
            </el-icon>
          </el-tooltip>
        </li>
        <li v-if="isShowBurn && currentChat.czType === 0">
          <el-select v-model="fireValue" placeholder="Select">
            <el-option
              v-for="item in fileTimeValue"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </li>
      </ul>
      <transition name="el-fade-in">
        <el-button
          class="tobottom"
          v-show="showToBottom"
          type="info"
          round
          icon="el-icon-arrow-down-bold"
          @click="scrollBottom"
        ></el-button>
      </transition>
      <div class="button-box-right">
        <el-popover
          v-if="currentChat.czType === 1"
          class="at-popover"
          placement="top"
          :width="300"
          trigger="click"
          @show="loadAtList"
          @hide="hideAt"
        >
          <template #reference>
            <el-button type="info">@群成员</el-button>
          </template>
          <ul
            v-loading="loadingCtrl.isLoadAt"
            v-infinite-scroll="loadAtList"
            class="infinite-list"
            :infinite-scroll-delay="600"
            :infinite-scroll-disabled="finished"
            :infinite-scroll-immediate="false"
          >
            <el-empty v-if="atList.length == 0" description="暂无数据" :image-size="50"></el-empty>
            <template v-else>
              <li
                v-for="item in atList"
                :key="item.targetId"
                class="infinite-list-item"
                @click="selectAt(item)"
              >
                <el-avatar
                  shape="circle"
                  :size="40"
                  :src="
                    item.avatar ||
                    `//cube.elemecdn.com/9/c2/f0ee8a3c7c9638a54940382568c9dpng.png`
                  "
                  fit="fill"
                ></el-avatar>
                <span v-text="item.nickName" class="nickName"></span>
              </li>
            </template>
            <p class="finished" v-show="finished">没有更多了</p>
          </ul>
        </el-popover>
        <el-popover
          class="preset-popover"
          v-model:visible="popoverVisible"
          placement="left"
          :width="400"
          trigger="click"
        >
          <template #reference>
            <el-button type="success" :disabled="!currentChatValid" icon="el-icon-collection">预设消息</el-button>
          </template>
          <el-container>
            <el-header>
              <el-input
                v-model="queryPreset"
                @input="fetchPreset"
                placeholder="输入关键字"
                prefix-icon="el-icon-search"
                clearable
              ></el-input>
            </el-header>
            <el-main v-loading="loadingCtrl.isLoadPreset">
              <el-empty v-if="presetList.length == 0" description="暂无匹配项" :image-size="50"></el-empty>
              <el-scrollbar v-else class="presetList-box" ref="scrollbar">
                <template v-for="(item, index) in presetList" :key="index">
                  <div @click="sendPreset(item)" class="listItem" v-html="item"></div>
                </template>
              </el-scrollbar>
            </el-main>
          </el-container>
        </el-popover>
        <el-button
          type="primary"
          icon="el-icon-promotion"
          @click="sendMsg"
          :loading="loadingCtrl.isSending"
          :disabled="!currentChatValid"
        >发送</el-button>
      </div>
    </div>
    <div
      class="editor"
      :contenteditable="currentChatValid"
      ref="editor"
      @keydown.enter.prevent="sendMsg"
      @click="getCursor"
      @keyup="getCursor"
    ></div>
    <!-- emoji 选择框 -->
    <div class="emoji-box" :style="{ display: isShowEmoji === true ? 'block' : 'none' }">
      <template v-for="(key, keyIndex) in Object.keys(emoji)" :key="keyIndex">
        <section v-if="keyIndex === page">
          <span
            @click="insertEmoji(item)"
            class="emoji-list-item"
            v-for="(item, index) in emoji[key]"
            :key="index"
          >{{ item }}</span>
        </section>
      </template>
      <div class="arrow">
        <div>
          <el-icon @click="pre">
            <el-icon-caret-left />
          </el-icon>
        </div>
        <div>
          <el-icon @click="next">
            <el-icon-caret-right />
          </el-icon>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import WebIM from "@/utils/WebIM";
import dayjs from "dayjs";
import { computed, reactive, ref, getCurrentInstance, watch } from "vue";
import { ElMessage } from "element-plus";
import { useStore } from "vuex";

// 引入全局API
const { appContext } = getCurrentInstance();

// 引入vuex
const store = useStore();

const emoji = {
  faces:
    "😀 😁 😂 🤣 😃 😄 😅 😆 😉 😊 😋 😎 😍 🤐 🤑 😲 ☹️ 🙁 😖 😞 😟 😤 😢 😭 😦 😧 😨 😩 🤯 😬 😮‍💨 😰 😱 🥵 🥶 😳 🤪 😵 😵‍💫 🥴 😠 😡 😶‍🌫️ 🙄".split(
      " "
    ),
  hands:
    "👋 🤚 🖐️ ✋ 🖖 👌 🤌 🤏 ✌️ 🤞 🤟 🤘 🤙 👈 👉 👆 🖕 👇 ☝️ 👍 👎 ✊ 👊 🤛 🤜 👏 🙌 👐 🤲 🤝 🙏 ✍️ 💅 🤳 💪 🦾 🦿 🦵 🦶 👂 🦻 👃 🧠 🫀".split(
      " "
    ),
  natural:
    "🌒 🌓 🌔 🌕 🌖 🌗 🌘 🌙 🌚 🌛 🌜 ☀️ 🌝 🌞 ⭐ 🌟 🌠 ☁️ ⛅ ⛈️ 🌤️ 🌥️ 🌦️ 🌧️ 🌨️ 🌩️ 🌪️ 🌫️ 🌬️ 🌈 ☂️ ☔ ⚡ ❄️ ☃️ ⛄ ☄️ 🔥 💧 🌊 🎄 ✨ 🎋 🎍".split(
      " "
    )
};

const fireValue = ref(0);
// 阅后即焚选项，死数据
const fileTimeValue = [
  {
    value: 0,
    label: "关闭",
  },
  {
    value: 5000,
    label: "5 秒",
  },
  {
    value: 30000,
    label: "30 秒",
  },
  {
    value: 60000,
    label: "60 秒",
  },
  {
    value: 300000,
    label: "五分钟",
  },
];

// 阅后即焚和表情显示隐藏状态
const isShowEmoji = ref(false);
const isShowBurn = ref(false);

// 显示隐藏阅后即焚
const showBurn = () => {
  if (!currentChatValid.value) {
    return
  }
  fireValue.value = 0
  isShowBurn.value = !isShowBurn.value;
}

// loading状态控制
const loadingCtrl = reactive({
  isSending: false,
  isLoadPreset: false,
  isLoadAt: false
});

// 获取文本框实例
const editor = ref();

// 消息接收对象
const currentChat = computed(() => store.state.user.currentChat);

// 检测接收对象是否有内容，自动控制按钮的 disabled 属性
const currentChatValid = ref(false)
watch(currentChat, (val) => {
  console.log(val);
  if (Object.keys(val).length <= 0) {
    currentChatValid.value = false
    console.log(currentChatValid.value);
  } else {
    currentChatValid.value = true
    console.log(currentChatValid.value);
  }
}, { immediate: true, deep: true })

const emit = defineEmits(["to-bottom"]);

// 发送消息 文本+表情 / 粘贴图片
const sendMsg = () => {
  loadingCtrl.isSending = true;
  if (!isPasteImg.value) {
    let msg = editor.value.innerText.replace(/[\n\r]$/, "");
    console.log(msg);
    if (!msg) {
      loadingCtrl.isSending = false;
      return ElMessage.warning("消息不能为空");
    }
    let id = WebIM.conn.getUniqueId();
    let type
    // 确定聊天类型
    if (currentChat.value.czType === 0) {
      type = "contact"
    } else if (currentChat.value.czType === 1) {
      type = "groupchat"
    } else if (currentChat.value.czType === 2) {
      type = "chatroom"
    } else {
      return ElMessage.error("未知的聊天类型")
    }
    const payload = {
      chatType: type,
      chatId: currentChat.value.targetId,
      msg,
      bySelf: true,
      time: dayjs().format("YYYY-MM-DD HH:mm:ss"),
      mid: id,
      status: "sending",
      ext: {
        nickName: store.state.user.userSecret.nickName,
        avatar: store.state.user.userSecret.avatar,
      },
      type: "text",
    };
    // 检测@消息
    if (atExt.value.length > 0) {
      let atArr = []
      atExt.value.forEach(item => {
        if (msg.includes(`@${item.nickName}`)) {
          console.log(item.targetId);
          atArr = [...atArr, item.targetId]
        }
      })
      payload.ext.at = atArr
      // @消息 和 阅后即焚不能共存
      fireValue.value = 0
      // delete payload.ext.fireTime
    }
    // 如果是阅后即焚消息，则增加fireTime 字段
    if (fireValue.value > 0) {
      payload.ext.fireTime = fireValue.value;
      // @消息 和 阅后即焚不能共存
      delete payload.ext.at
      atExt.value = []
    }
    console.log(payload);
    store.dispatch("onSendText", payload);
    // emit("update-current-msg", payload);
    editor.value.innerText = "";
    getCursor();
    atExt.value = []
    loadingCtrl.isSending = false;
  } else {
    editor.value.innerText = "";
    getCursor();
    handleUpload();
    isPasteImg.value = false;
  }
}

// 文件对象
const fileObj = ref({})
const isPasteImg = ref(false)

// 上传图片前，检查文件大小及格式
const beforeUpload = (file) => {
  fileObj.value = file
  console.log("文件", file)
  const isJPG =
    file.type === "image/jpeg" ||
    file.type === "image/png" ||
    file.type === "image/jpeg" ||
    file.type === "image/gif";
  const isLt3M = file.size / 1024 / 1024 < 3;
  if (!isJPG) {
    ElMessage.error("不支持的文件格式");
  }
  if (!isLt3M) {
    ElMessage.error("上传文件体积应小于 3MB!");
  }
  return isJPG && isLt3M;
}

// 图片上传
const handleUpload = async () => {
  loadingCtrl.isSending = true;
  let imageName = randomString(32) + ".png";
  const configs = {
    headers: { "Content-Type": "multipart/form-data" },
  };
  const formData = new FormData();
  formData.append("file", fileObj.value, imageName);
  try {
    const res =
      await appContext.config.globalProperties.$API.fileUpload.upload.post(
        "",
        formData,
        configs
      );
    let id = WebIM.conn.getUniqueId();
    let type
    // 确定聊天类型
    if (currentChat.value.czType === 0) {
      type = "contact"
    } else if (currentChat.value.czType === 1) {
      type = "groupchat"
    } else if (currentChat.value.czType === 2) {
      type = "chatroom"
    } else {
      return ElMessage.error("未知的聊天类型")
    }
    const payload = {
      chatType: type,
      chatId: currentChat.value.targetId,
      msg: res.data[0].imageUrl,
      bySelf: true,
      time: dayjs().format("YYYY-MM-DD HH:mm:ss"),
      mid: id,
      type: "image",
      status: "sending",
      ext: {
        nickName: store.state.user.userSecret.nickName,
        avatar: store.state.user.userSecret.avatar,
      },
    };
    console.log(payload);
    store.dispatch("onSendImg", payload);
    // emit("update-current-msg", payload);
  } catch (error) {
    console.log(error);
  } finally {
    loadingCtrl.isSending = false;
  }
}

// 单聊贴图发送
const setPasteFile = (file) => {
  isPasteImg.value = true;
  fileObj.value = file;
  console.log(editor.value);
}

// 存储光标位置
const cursorPosition = ref("");

// 获取当前光标位置方法，在 getCursor 调用
const getCursorPosition = (element) => {
  let caretOffset = 0;
  const doc = element.ownerDocument || element.document;
  const win = doc.defaultView || doc.parentWindow;
  const sel = win.getSelection();
  if (sel.rangeCount > 0) {
    const range = win.getSelection().getRangeAt(0);
    const preCaretRange = range.cloneRange();
    preCaretRange.selectNodeContents(element);
    preCaretRange.setEnd(range.endContainer, range.endOffset);
    caretOffset = preCaretRange.toString().length;
  }
  return caretOffset;
}

// 点击输入框，获取当前光标位置
const getCursor = () => {
  cursorPosition.value = getCursorPosition(editor.value);
  // 重置 isPasteImg 状态
  if (isPasteImg.value && !editor.value.getElementsByTagName("img").length) {
    isPasteImg.value = false;
  }
}

// 设置光标位置方法，在 insertEmoji 调用
const setCursorPosition = (element, cursorPosition) => {
  const range = document.createRange();
  range.setStart(element.firstChild, cursorPosition);
  range.setEnd(element.firstChild, cursorPosition);
  const sel = window.getSelection();
  sel.removeAllRanges();
  sel.addRange(range);
}

const page = ref(0)

const pre = () => {
  // console.log(page.value);
  if (page.value <= 0) {
    page.value = 0
    return
  }
  page.value--
}

const next = () => {
  // console.log(page.value);
  if (page.value == Object.keys(emoji).length - 1) {
    console.log('跳出');
    return
  }
  page.value++
}

// 显示 emoji 框
const showEmoji = () => {
  if (!currentChatValid.value) {
    return
  }
  isShowEmoji.value = !isShowEmoji.value;
}

// 点击 emoji
const insertEmoji = (emoji) => {
  isShowEmoji.value = false;
  const text = editor.value.innerText;
  editor.value.innerText =
    text.slice(0, cursorPosition.value) +
    emoji +
    text.slice(cursorPosition.value, text.length);
  setCursorPosition(editor.value, cursorPosition.value + 1);
  cursorPosition.value = getCursorPosition(editor.value) + 1; //  emoji takes 2 bytes
};

// 模糊查询 预设消息 关键字
const queryPreset = ref("");
const presetList = ref([]);
const popoverVisible = ref(false);

// 查询预设消息
const fetchPreset = async () => {
  if (queryPreset.value == "") {
    return;
  }
  loadingCtrl.isLoadPreset = true;
  const res =
    await appContext.config.globalProperties.$API.imApi.getPresetMsg.post({
      msg: queryPreset.value,
    });
  loadingCtrl.isLoadPreset = false;
  if (res.code !== 0) {
    return ElMessage.warning("服务器异常，请刷新后重试");
  }
  presetList.value = res.data;
  presetList.value = presetList.value.map((msg) => {
    msg = msg.replace(
      queryPreset.value,
      `<span style="color: red;">${queryPreset.value}</span>`
    );
    return msg;
  });
};

// 存储 @群成员 list
const atList = ref([])
const finished = ref(false)
let atListPage = 1
const atExt = ref([])

// 开启 @群成员 list
const loadAtList = async () => {
  loadingCtrl.isLoadAt = true
  const res = await appContext.config.globalProperties.$API.imApi.getGroupMember.post({
    page: atListPage,
    chatgroupId: currentChat.value.targetId
  })
  loadingCtrl.isLoadAt = false
  console.log(res);
  if (res.code !== 0) {
    return ElMessage.error('获取数据失败')
  }
  atList.value = [...atList.value, ...res.data]
  if (res.data.length < 10) {
    finished.value = true
    console.log(atListPage, finished.value);
    return
  }
  // 变更页数，准备下次数据请求
  atListPage += 1
  console.log(atListPage, finished.value);
}

// 关闭 @群成员 list
const hideAt = () => {
  atList.value = []
  atListPage = 1
  finished.value = false
}

// 选中 @群成员
const selectAt = (item) => {
  // 获取输入框内容
  const text = editor.value.innerText
  // 拼接需要放入的内容
  const value = '@' + item.nickName + '\xa0'
  // 在历史光标位置插入内容
  editor.value.innerText =
    text.slice(0, cursorPosition.value) +
    value +
    text.slice(cursorPosition.value, text.length);
  setCursorPosition(editor.value, cursorPosition.value + value.length);
  getCursor()
  atExt.value = [...atExt.value, { nickName: item.nickName, targetId: item.targetId }]
  console.log(atExt.value)
}

const showToBottom = ref(false)

// 显示回到底部
const backToBottom = (status) => {
  showToBottom.value = status;
};

// 触发父组件事件，回到底部
const scrollBottom = () => {
  emit("to-bottom");
};

const sendPreset = (item) => {
  popoverVisible.value = false;
  editor.value.innerHTML = item;
  editor.value.focus();
};

// 随机数方法
const randomString = (len) => {
  len = len || 32;
  let $chars =
    "ABCDEFGHJKMNPQRSTWXYZabcdefhijkmnprstwxyz2345678"; /****默认去掉了容易混淆的字符oOLl,9gq,Vv,Uu,I1****/
  let maxPos = $chars.length;
  let pwd = "";
  for (let i = 0; i < len; i++) {
    pwd += $chars.charAt(Math.floor(Math.random() * maxPos));
  }
  return pwd;
};

defineExpose({
  setPasteFile,
  backToBottom,
  showToBottom,
});
</script>
<style lang="scss" scoped>
.editor-box {
  display: flex;
  flex-direction: column;
  position: relative;
  .editor-bar {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 13px;
    position: relative;
    .button-box {
      display: flex;
      list-style: none;
      align-items: center;
      li {
        display: flex;
        align-items: center;
        margin-right: 10px;
        font-size: 2rem;
      }
    }
    .tobottom {
      position: absolute;
      left: 50%;
      transform: translateX(-50%);
    }
    :deep(.button-box-right) {
      display: flex;
      flex-wrap: nowrap;
      justify-content: space-between;
    }
  }
}

.infinite-list {
  height: 200px;
  overflow: auto;
  .infinite-list-item {
    display: flex;
    align-items: center;
    margin: 5px;
    margin-bottom: 10px;
    cursor: pointer;
    box-shadow: 0 2px 8px 0 rgba(0, 0, 0, 0.1);
    border-radius: 5px;
    padding: 10px 5px;
    .nickName {
      flex: 1;
      margin-left: 20px;
    }
    &:hover {
      background: linear-gradient(to right, #fff, #e0eafc, #cfdef3);
      transition: 0.5s;
    }
  }
  .finished {
    text-align: center;
  }
}

[data-theme="dark"] .infinite-list .infinite-list-item:hover {
  background: linear-gradient(to right, #83a4d4, #b6fbff);
  color: black;
}

.el-popover {
  .presetList-box {
    height: 150px;
  }
  .listItem {
    font-size: 12px;
    border-bottom: 1px solid #e6e6e6;
    margin-bottom: 10px;
    overflow: hidden;
    text-overflow: ellipsis;
    display: -webkit-box;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    cursor: pointer;
    &:hover {
      background: linear-gradient(to right, #fff, #e0eafc, #cfdef3);
      transition: all 0.5s;
    }
  }
}
[data-theme="dark"] .el-popover .listItem:hover {
  color: black;
}
.editor {
  width: 100%;
  height: 150px;
  max-height: 150px;
  overflow: auto;
  border-radius: 4px;
  border: 1px solid #ccc;
  box-sizing: border-box;
  word-break: break-all;
  overflow-wrap: break-word;
  padding: 5px;
  outline: none;
  img {
    width: 400px;
  }
}
.emoji-box {
  width: 300px;
  padding: 10px;
  position: absolute;
  background-color: rgb(236, 236, 236);
  top: -90%;
  border-radius: 10px;
  section {
    .emoji-list-item {
      display: inline-block;
      width: 25px;
      height: 25px;
      font-size: 20px;
      cursor: pointer;
    }
  }
  .arrow {
    display: flex;
    justify-content: center;
    padding-top: 10px;
  }
}
</style>