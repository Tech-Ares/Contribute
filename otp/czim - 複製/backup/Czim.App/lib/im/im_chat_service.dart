
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

abstract class IMChatService {

  ///会话列表
  ///第一次从服务器拿数据，后面直接从本地拿
  Future<List<EMConversation>?> conversations();

  ///创建会话
  Future<EMConversation?> createConversation(String sessionId);

  ///获取会话消息
  Future<List<EMMessage?>?> loadMessages(EMConversation conv, {String messageId});

  ///消息标记已读
  Future<bool> readMessage(EMConversation conv, String messageId);

  ///会话的所有消息标记已读
  Future? readAllMessage(EMConversation conv);

  ///发送消息
  Future? sendMessage(EMMessage message);

  ///发送已读回执
  Future? sendMessageReadAck(EMMessage message);

  /// 发送会话已读 [conversationId]为会话Id
  // Future? sendConversationReadAck(String convId);

  ///添加监听器
  void addListener(EMChatManagerListener listener);

  ///添加监听器
  void removeListener(EMChatManagerListener listener);

  ///获取消息发送者的昵称和头像
  Future? fetchMessageExt(List<EMMessage?>? messages);
}