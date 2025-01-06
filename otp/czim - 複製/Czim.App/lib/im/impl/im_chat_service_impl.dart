import 'package:easy_chat/config/global_config.dart';
import 'package:easy_chat/ext/im_message_ext.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/im/im_chat_service.dart';
import 'package:easy_chat/models/friend.dart';
import 'package:easy_chat/models/params/revoke_message_params.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import '../../export.dart';

class IMChatServiceImpl extends IMChatService {
  final GlobalController _globalController = Get.find();

  @override
  Future<List<EMConversation>?> conversations() async {
    try {
      var localConvs =
          await EMClient.getInstance.chatManager.loadAllConversations();
      if (localConvs.isEmpty) {
        localConvs =
            await EMClient.getInstance.chatManager.getConversationsFromServer();
      }
       localConvs.forEach((element) {
         logger.d('conv ==> ${element.name} ${element.toJson()}');
       });
      return localConvs;
    } on EMError catch (e) {
      logger.e('操作失败，原因是: $e');
    }
  }

  @override
  Future<EMConversation?> createConversation(String sessionId) async {
    try {
      // sessionId: 会话对应环信id, 如果是群组或者聊天室，则为群组id或者聊天室id
      return await EMClient.getInstance.chatManager.getConversation(sessionId);
    } on EMError catch (e) {
      logger.e('操作失败，原因是: $e');
    }
  }

  @override
  Future<List<EMMessage?>?> loadMessages(EMConversation conv,
      {String messageId = ''}) async {
    var list = await conv.loadMessages(
        startMsgId: messageId, loadCount: GlobalConfig.pageSize);
    for (var element in list) {
      if (element.attributes['readTime'] == null ||
          element.attributes['readTime'] == 0) {
        element.attributes['readTime'] = DateTime.now().millisecondsSinceEpoch;
        try {
          await conv.updateMessage(element);
          if(element.direction == EMMessageDirection.RECEIVE) {
            await readMessage(conv, element.msgId ?? '');
            await EMClient.getInstance.chatManager.sendMessageReadAck(element);
          }
        } catch (e) {
          logger.e(e);
        }
      }
      if(element.isFired(_globalController.userId)) {
        await conv.deleteMessage(element.msgId ?? '');
        if(conv.type == EMConversationType.Chat) {
          await memberRestClient.revokeMessage(RevokeMessageParams(
            receiveId: conv.id,
            msgId: element.msgId
          ));
        } else {
          await memberRestClient.revokeRoomMessage(RevokeMessageParams(
              receiveId: conv.id,
              msgId: element.msgId
          ));
        }
      }
      logger.d('消息 ==> ${element.toJson()}');
    }
    list.removeWhere(
        (EMMessage element) => element.isFired(_globalController.userId));
    try {
      await fetchMessageExt(list);
    } catch (e) {
      logger.e('获取消息的ext失败');
    }
    return list;
    // if(list.isNotEmpty && messageId == '') {
    //   return list;
    // }
    // try {
    //   EMCursorResult<EMMessage?> result = await EMClient.getInstance.chatManager
    //       .fetchHistoryMessages(conv.id,
    //           startMsgId: messageId,
    //           pageSize: GlobalConfig.pageSize,
    //           type: conv.type!);
    //   try {
    //     await fetchMessageExt(result.data);
    //   } catch (e) {
    //     logger.e('获取消息的ext失败');
    //   }
    //   result.data?.forEach((msg) async {
    //     // if (conv.type == EMConversationType.Chat) {
    //     //   msg?.attributes = {
    //     //     'nickName': conv.ext!['nickName'],
    //     //     'avatar': conv.ext!['avatar']
    //     //   };
    //     // }
    //     // await sendMessageReadAck(msg!);
    //     bool res = false;
    //     if (msg!.direction == EMMessageDirection.RECEIVE) {
    //       res = await conv.markMessageAsRead(msg.msgId!);
    //     }
    //     if (msg.attributes['readTime'] == null ||
    //         msg.attributes['readTime'] == 0) {
    //       msg.attributes['readTime'] = DateTime.now().millisecondsSinceEpoch;
    //       logger.d('更新消息到本地 ==> ${msg.toJson()}');
    //       await conv.updateMessage(msg);
    //       await readMessage(conv, msg.msgId ?? '');
    //     }
    //     if(msg.isFired(_globalController.userId)) {
    //       await conv.deleteMessage(msg.msgId ?? '');
    //     }
    //     // logger.d('loadMessages: ${result.data?.length}');
    //     // logger.d('message: $res === ${msg?.toJson()}');
    //   });
    //   result.data?.removeWhere(
    //           (EMMessage? element) => element!.isFired(_globalController.userId));
    //   return result.data;
    // } on EMError catch (e) {
    //   logger.e('操作失败，原因是: $e');
    // }
    // return List.empty();
  }

  @override
  Future<bool> readMessage(EMConversation conv, String messageId) {
    try {
      return conv.markMessageAsRead(messageId);
    } on EMError catch (e) {
      logger.e('操作失败，原因是: $e');
    }
    return Future.value(false);
  }

  @override
  Future? readAllMessage(EMConversation conv) async {
    try {
      await conv.markAllMessagesAsRead();
      await EMClient.getInstance.chatManager.sendConversationReadAck(conv.id);
      logger.d('会话已读');
    } on EMError catch (e) {
      logger.e('操作失败，原因是: $e');
    }
  }

  @override
  Future? sendMessageReadAck(EMMessage message) async {
    try {
      // if(message.needGroupAck && message.chatType != EMMessageChatType.ChatRoom) {
      //   logger.d('发送消息已读回执');
      // }
      if (message.direction == EMMessageDirection.RECEIVE) {
        await EMClient.getInstance.chatManager.sendMessageReadAck(message);
      }
      logger.d('发送消息已读回执');
    } on EMError catch (e) {
      logger.e('操作失败，原因是: $e');
    }
  }

  // @override
  // Future? sendConversationReadAck(EMConversation conv) async {
  //   return conv.markAllMessagesAsRead();
  // }

  @override
  Future? sendMessage(EMMessage message) async {
    try {

      String pushContent = '离线推送内容部分';
      switch (message.body?.type) {
        case EMMessageBodyType.TXT:
          var body = message.body as EMTextMessageBody;
          pushContent = body.content ?? '';
          break;
        case EMMessageBodyType.IMAGE:
          pushContent = '[图片]';
          break;
        case EMMessageBodyType.VIDEO:
          pushContent = '[视频]';
          break;
        case EMMessageBodyType.FILE:
          pushContent = '[文件]';
          break;
        case EMMessageBodyType.VOICE:
          pushContent = '[语音]';
          break;
        case EMMessageBodyType.LOCATION:
          pushContent = '[位置]';
          break;
        case EMMessageBodyType.CUSTOM:
          var params = (message.body as EMCustomMessageBody).params;
          if (params?['action'] == 'shake') {
            pushContent =
            '抖了你一下';
          }
          break;
        default:
          pushContent = '';
      }
      var pushAttr = {
        'em_push_name': _globalController.userInfo?.showName,
        'em_push_content': pushContent,
        // 'em_push_sound': 1,
        // 'em_force_notification': true
      };
      message.attributes['em_apns_ext'] = pushAttr;
      await EMClient.getInstance.chatManager.sendMessage(message);
    } on EMError catch (e) {
      logger.e('操作失败，原因是: $e');
    }
  }

  @override
  void addListener(EMChatManagerListener listener) {
    EMClient.getInstance.chatManager.addListener(listener);
  }

  @override
  void removeListener(EMChatManagerListener listener) {
    EMClient.getInstance.chatManager.removeListener(listener);
  }

  @override
  Future? fetchMessageExt(List<EMMessage?>? messages) async {
    if (messages?.isNotEmpty ?? false) {
      final unGetUserExts = <String>{};
      for (var msg in messages!) {
        Friend? ext = _globalController.userExtCache[msg!.from];
        if (ext == null) {
          //说明缓存没有
          unGetUserExts.add(msg.from!);
        } else {
          // msg.attributes;
          msg.attributes['avatar'] = ext.avatar ?? '';
          msg.attributes['nickName'] = ext.nickName ?? msg.from;
        }
      }
      List<Friend>? list;
      if (unGetUserExts.isNotEmpty) {
        list = await memberRestClient.fetchUsers(unGetUserExts.toList());
      } else {
        list = <Friend>[];
      }
      logger.d('unGetUserExts: $unGetUserExts');
      final newValue = <String, Friend>{};
      list?.forEach((element) {
        newValue[element.targetId!] = element;
        for (var msg in messages) {
          if (msg?.from == element.targetId) {
            msg?.attributes ??= {};
            msg?.attributes['avatar'] = element.avatar ?? '';
            msg?.attributes['nickName'] = element.nickName ?? msg.from;
          }
        }
      });
      _globalController.userExtCache.addAll(newValue);
      logger.d('当前缓存的数据：${_globalController.userExtCache}');
    }
  }
}
