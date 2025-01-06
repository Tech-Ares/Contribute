import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/user_info.dart';
import 'package:flutter_chat_types/flutter_chat_types.dart' as types;
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

extension IMMessageExtension on EMMessage {

  types.Message typeMessage() {
    int readTime = 0;
    try {
      readTime = attributes['readTime'] ?? 0;
    } catch (e) {
      // logger.e(e);
    }
    var metadata = Map<String, dynamic>.from(attributes);
    // metadata['emMessage'] = this;
    logger.d(metadata);
    if (body?.type != null) {
      switch (body!.type!) {
        case EMMessageBodyType.TXT:
          return types.TextMessage(
            id: msgId ?? uuid.v4(),
            author: typeUser(),
            createdAt: serverTime,
            remoteId: conversationId,
            updatedAt: readTime,
            status: typeStatus(),
            metadata: metadata,
            text: (body as EMTextMessageBody).content ?? '',
          );
        case EMMessageBodyType.IMAGE:
          var imageBody = body as EMImageMessageBody;
          String? filePath = imageBody.localPath;
          if (filePath?.isEmpty ?? true) {
            filePath = imageBody.remotePath;
          }
          return types.ImageMessage(
            id: msgId ?? uuid.v4(),
            author: typeUser(),
            createdAt: serverTime,
            updatedAt: readTime,
            name: filePath ?? '',
            size: 0,
            uri: filePath ?? '',
            remoteId: conversationId,
            status: typeStatus(),
            metadata: metadata,
          );
        case EMMessageBodyType.VIDEO:
          var videoBody = body as EMVideoMessageBody;
          String? filePath = videoBody.remotePath;
          if (filePath?.isEmpty ?? true) {
            filePath = videoBody.localPath;
          }
          return types.FileMessage(
            id: msgId ?? uuid.v4(),
            author: typeUser(),
            createdAt: serverTime,
            updatedAt: readTime,
            name: filePath ?? '',
            mimeType: 'video/mp4',
            size: 0,
            uri: filePath ?? '',
            remoteId: conversationId,
            status: typeStatus(),
            metadata: metadata,
          );
        case EMMessageBodyType.LOCATION:
          break;
        case EMMessageBodyType.VOICE:
          var voiceBody = body as EMVoiceMessageBody;
          String? filePath = voiceBody.localPath;
          if (filePath?.isEmpty ?? true) {
            filePath = voiceBody.remotePath;
          }
          logger.d('音频文件路径：$filePath');
          return types.FileMessage(
            id: msgId ?? uuid.v4(),
            author: typeUser(),
            createdAt: serverTime,
            updatedAt: readTime,
            mimeType: 'audio/x-aac',
            name: filePath ?? '',
            size: voiceBody.duration ?? 1,
            uri: filePath ?? '',
            remoteId: conversationId,
            status: typeStatus(),
            metadata: metadata,
          );
        case EMMessageBodyType.FILE:
          break;
        case EMMessageBodyType.CMD:
          break;
        case EMMessageBodyType.CUSTOM:
          var messageBody = (body as EMCustomMessageBody);
          var action = messageBody.params?['action'];
          if (action == 'shake') {
            return types.TextMessage(
              id: msgId ?? uuid.v4(),
              author: typeUser(),
              createdAt: serverTime,
              updatedAt: readTime,
              remoteId: conversationId,
              status: typeStatus(),
              metadata: messageBody.params,
              text: direction == EMMessageDirection.SEND ? '抖了Ta一下' : '抖了你一下',
            );
          }
          return types.CustomMessage(
              id: msgId ?? uuid.v4(),
              author: typeUser(),
              createdAt: serverTime,
              updatedAt: readTime,
              remoteId: conversationId,
              status: typeStatus(),
              metadata: messageBody.params);
          break;
      }
    }
    return types.UnsupportedMessage(
      author: typeUser(),
      id: msgId ?? uuid.v4(),
      status: typeStatus(),
    );
  }

  /// 消息详情
  String showInfo() {
    String showInfo = '';
    try {
      if (attributes['fireTime'] > 0) {
        return '[阅后即焚]';
      }
    } catch (e) {
      // logger.e(e);
    }

    switch (body?.type) {
      case EMMessageBodyType.TXT:
        var body = this.body as EMTextMessageBody;
        showInfo = body.content ?? '';
        break;
      case EMMessageBodyType.IMAGE:
        showInfo = '[图片]';
        break;
      case EMMessageBodyType.VIDEO:
        showInfo = '[视频]';
        break;
      case EMMessageBodyType.FILE:
        showInfo = '[文件]';
        break;
      case EMMessageBodyType.VOICE:
        showInfo = '[语音]';
        break;
      case EMMessageBodyType.LOCATION:
        showInfo = '[位置]';
        break;
      case EMMessageBodyType.CUSTOM:
        var params = (body as EMCustomMessageBody).params;
        if (params?['action'] == 'shake') {
          showInfo =
          '抖了${direction == EMMessageDirection.SEND ? 'Ta' : '你'}一下';
        }
        break;
      default:
        showInfo = '';
    }
    return showInfo;
  }

  bool isFired(String userId) {
    // int? fireLeftTime = _getMessageFireLeftTime(this, userId);
    int? fireLeftTime = typeMessage().getMessageFireLeftTime(userId);
    if(fireLeftTime == null) {
      return false;
    }
    return fireLeftTime <= 0;
  }
  int? _getMessageFireLeftTime(EMMessage message, String userId) {
    if(message.attributes['fireTime'] == null) {
      return null;
    }
    int? leftMills;
    try {
      if (message.from == userId) {
        //我自己发的阅后即焚
        leftMills =
            ((message.serverTime) + message.attributes['fireTime'] as int? ??
                0) -
                DateTime.now().millisecondsSinceEpoch;
      } else {
        //TODO 获取已读时间
        logger.d('消息已读时间：${attributes['readTime']}');
        int readTime = attributes['readTime'] ?? 0;
        leftMills = (readTime + attributes['readTime'] as int? ?? 0) -
            DateTime.now().millisecondsSinceEpoch;
      }
    } catch (e) {
      // logger.e(e);
    }
    return leftMills;
  }
}

extension IMTypeMessageExtension on types.Message {
  EMMessage emMessage() {
    switch (type) {
      case types.MessageType.custom:
        return EMMessage.createCustomSendMessage(
            username: remoteId!,
            event: metadata?['action'],
            params: Map<String, String>.from(metadata!));
      case types.MessageType.file:
        String mineType = (this as types.FileMessage).mimeType ?? '';
        logger.d(
            'IMTypeMessageExtension ==> ${(this as types.FileMessage).mimeType}');
        switch (mineType) {
          case 'audio/x-aac':
            return EMMessage.createVoiceSendMessage(
                username: remoteId!,
                filePath: (this as types.FileMessage).uri,
                duration: (this as types.FileMessage).size as int);
          case 'video/mp4':
            return EMMessage.createVideoSendMessage(
                username: remoteId!, filePath: (this as types.FileMessage).uri);
        }
        break;
      case types.MessageType.image:
        return EMMessage.createImageSendMessage(
            username: remoteId!, filePath: (this as types.ImageMessage).uri);
        break;
      case types.MessageType.text:
        var textMessage = (this as types.TextMessage);
        if (textMessage.metadata?['action'] != null) {
          String action = textMessage.metadata?['action'];
          if (action == 'shake') {
            return EMMessage.createCustomSendMessage(
                username: remoteId!,
                event: metadata?['action'],
                params: Map<String, String>.from(metadata!));
          }
        }
        return EMMessage.createTxtSendMessage(remoteId!, textMessage.text);
      case types.MessageType.unsupported:
        break;
    }
    return EMMessage.createTxtSendMessage(remoteId!, '未知消息');
  }

  int? getMessageFireLeftTime(String userId) {
    if(metadata?['fireTime'] == null) {
      return null;
    }
    int? leftMills;
    try {
      if (author.id == userId) {
        //我自己发的阅后即焚
        leftMills =
            ((createdAt ?? DateTime.now().millisecondsSinceEpoch) + metadata?['fireTime'] as int? ??
                0) -
                DateTime.now().millisecondsSinceEpoch;
      } else {
        //TODO 获取已读时间
        // logger.d('消息已读时间：$updatedAt');
        int readTime = updatedAt ?? DateTime.now().millisecondsSinceEpoch;
        leftMills = (readTime + metadata?['fireTime'] as int? ?? 0) -
            DateTime.now().millisecondsSinceEpoch;
        // leftMills = 5000;
      }
    } catch (e) {
      // logger.e(e);
    }
    return leftMills;
  }
}

extension IMAuthorExtension on EMMessage {
  types.User typeUser() {
    String? firstName = from;
    String? imageUrl;
    try {
      firstName = attributes['nickName'];
      if (attributes['avatar']?.isNotEmpty ?? false) {
        imageUrl = attributes['avatar'];
      }
      // logger.d('attributes : $attributes');
      // logger.d('imageUrl : $imageUrl');
    } catch (e) {
      // logger.e(e);
    }
    return types.User(
        id: from ?? '', firstName: firstName ?? from, imageUrl: imageUrl);
  }
}

extension IMMessageStatusExtension on EMMessage {
  types.Status typeStatus() {
    switch (status) {
      case EMMessageStatus.CREATE:
        return types.Status.sending;
      case EMMessageStatus.PROGRESS:
        return types.Status.sending;
      case EMMessageStatus.SUCCESS:
        return hasReadAck ? types.Status.seen : types.Status.sent;
      case EMMessageStatus.FAIL:
        return types.Status.error;
    }
  }
}

extension IMUserInfoExtension on EMUserInfo {
  types.User typeUser() {
    return types.User(
      id: userId,
      imageUrl: avatarUrl,
      firstName: nickName,
    );
  }
}

extension UserInfoExtension on UserInfo {
  types.User typeUser() {
    return types.User(
      id: id ?? '',
      imageUrl: avatar,
      firstName: nickName,
    );
  }
}
