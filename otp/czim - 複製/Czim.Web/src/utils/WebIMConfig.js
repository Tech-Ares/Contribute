// import websdk from 'easemob-websdk'

const config = {
  socketServer: '//msync-api-61.easemob.com', // socket Server地址
  restServer: '//a61.easemob.com', // rest Server地址
  appKey: '1107220118095349#xchat',
  https: true, // 是否使用https
  isAutoLogin: true, // 如果isAutoLogin设置为false，那么必须手动设置上线，否则无法收消息
  isHttpDNS: true, // 3.0 SDK支持，防止DNS劫持从服务端获取XMPPUrl、restUrl
  isMultiLoginSessions: false, // 是否开启多页面同步收消息，注意，需要先联系商务开通此功能
  isDebug: false, // 打开调试，会自动打印log，在控制台的console中查看log
  autoReconnectNumMax: 2, // 断线重连最大次数,如果autoReconnectNumMax=0，则不会自动连接。
  autoReconnectInterval: 2, // 每次重新连接之间的间隔秒数。仅当autoReconnectMaxNum>=2时有效
  delivery: true, // 是否发送已读回执
  useOwnUploadFun: true // 是否使用自己的上传方式（如将图片文件等上传到自己的服务器，构建消息时只传url）
}

export default config
