import 'dart:convert';
import 'dart:io';

import 'package:awesome_notifications/awesome_notifications.dart';
import 'package:bruno/bruno.dart';
import 'package:easy_chat/export.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/friend.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:easy_chat/views/member/select/select_member_logic.dart';
import 'package:flutter/material.dart';
import 'package:flutter_ringtone_player/flutter_ringtone_player.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';
import 'package:vibration/vibration.dart';
import 'package:easy_chat/ext/im_message_ext.dart';

import 'nav_message_state.dart';

class NavMessageLogic extends BaseController<List<EMConversation>?>
    implements EMChatManagerListener {
  final NavMessageState _state = NavMessageState();
  final actionKey = GlobalKey();
  final GlobalController _globalController = Get.find();

  @override
  String? get emptyMsg => '没有聊天内容';

  Worker? _newMessageWorker;
  Worker? _newMessageVibrateWorker;

  @override
  int get getItemCount => _state.conversations.length;

  EMConversation indexOfConv(int index) => _state.conversations[index];

  @override
  Future<List<EMConversation>?> Function() get requestFunc => () async {
        var conversations = await imManager.imChatService.conversations();
        try {
          await _fetchConversationExt(conversations);
          conversations?.forEach((element) {
            logger.d('element: ${element.name}, type : ${element.type}');
          });
        } catch (e) {
          logger.e('获取会话ext失败');
        }
        _state.conversations.clear();
        if (conversations != null) {
          _state.conversations.addAll(conversations);
        }
        return conversations;
      };

  @override
  void onDataLoaded() {
    int totalUnreadCount = 0;
    state?.forEach((element) {
      totalUnreadCount += element.unreadCount ?? 0;
    });
    _globalController.unreadMessageCount.value = totalUnreadCount;
    if (Platform.isIOS) {
      // final int _badgeCount =
      // await AwesomeNotifications().getGlobalBadgeCounter();
      AwesomeNotifications().setGlobalBadgeCounter(totalUnreadCount);
    }
    super.onDataLoaded();
  }

  @override
  void onReady() {
    onRefresh();
    _globalController.refreshConvs.listen((v) {
      onRefresh();
    });
    super.onReady();
  }

  onConversationItem(int index) async {
    await imManager.imChatService.readAllMessage(indexOfItem(index));
    _globalController.saveAtConversation(state![index].id, false);
    Get.toNamed(PageRoutes.chat, arguments: indexOfItem(index))
        ?.then((value) => onRefresh());
  }

  @override
  void onInit() {
    imManager.imChatService.addListener(this);
    _newMessageWorker = debounce(
        _state.newMessageReceived, (v) => _onMessageReceived(),
        time: const Duration(seconds: 1));
    _newMessageVibrateWorker = debounce(
        _state.newMessageVibrate, (v) => _onMessageVibrate(),
        time: const Duration(seconds: 1));
    super.onInit();
  }

  @override
  void onClose() {
    imManager.imChatService.removeListener(this);
    _newMessageWorker?.dispose();
    super.onClose();
  }

  @override
  void onCmdMessagesReceived(List<EMMessage> messages) {
    logger.d('onCmdMessagesReceived');
  }

  @override
  void onConversationRead(String? from, String? to) {
    logger.d('onConversationRead : from => $from, to => $to');
    onRefresh();
  }

  @override
  void onConversationsUpdate() {
    logger.d('onConversationsUpdate');
    onRefresh();
  }

  @override
  void onGroupMessageRead(List<EMGroupMessageAck> groupMessageAcks) {
    logger.d('onGroupMessageRead');
  }

  @override
  void onMessagesDelivered(List<EMMessage> messages) {
    logger.d('onMessagesDelivered');
    onRefresh();
  }

  @override
  void onMessagesRead(List<EMMessage> messages) {
    logger.d('onMessagesRead');
  }

  @override
  void onMessagesRecalled(List<EMMessage> messages) {
    logger.d('onMessagesRecalled');
    onRefresh();
  }

  @override
  void onMessagesReceived(List<EMMessage> messages) {
    logger.d('onCmdMessagesReceived');
    // _onMessageReceived();
    try {
      if (_globalController.checkMessageNotify(messages)) {
        _state.newMessageReceived.toggle();
        if (_globalController.isGlobalVibrate) {
          _state.newMessageVibrate.toggle();
        }
        if(_globalController.isAppPause) {
          _sendNotification(messages);
        }
      }
    } catch (e) {
      logger.e(e);
    }

    try {
      if (_globalController.checkMessageReceive()) {
        if (_globalController.checkMessageNotify(messages, ignoreGlobal: false)) {
          if(_globalController.isAppPause) {
            _sendNotification(messages);
          }
        }
      }
    } catch (e) {
      logger.e(e);
    }
    for (var msg in messages) {
      List<String> atMembers = [];
      try {
        List<String> m = List<String>.from(jsonDecode(msg.attributes['at']));
        atMembers.addAll(m);
      } catch (e) {
        e.printError();
      }
      if (atMembers.contains(_globalController.userId)) {
        //有人@我
        var firstWhereOrNull =
            state!.firstWhereOrNull((element) => element.id == msg.to);
        if (firstWhereOrNull != null) {
          _globalController.saveAtConversation(firstWhereOrNull.id, true);
        }
      }
    }
    onRefresh();
  }

  _onActionClick(int index, String item) {
    switch (index) {
      case 0:
        return Get.toNamed(PageRoutes.selectMember,
            arguments: {'type': SelectType.createGroup});
      case 1:
        return Get.toNamed(PageRoutes.addFriend);
      case 2:
        return Get.toNamed(PageRoutes.scan);
    }
  }

  void popMoreAction(BuildContext context) {
    BrnPopupListWindow.showButtonPanelPopList(context, actionKey,
        onItemClick: _onActionClick, data: ['发起群聊', '添加好友', '扫一扫']);
  }

  _fetchConversationExt(List<EMConversation>? conversations) async {
    if (conversations?.isNotEmpty ?? false) {
      final unGetUserExts = <String>[];
      final unGetRoomExts = <String>[];
      final unGetGroupExts = <String>[];
      for (var element in conversations!) {
        Friend? ext = _globalController.userExtCache[element.id];
        if (ext == null) {
          //说明缓存没有
          if (element.type == EMConversationType.Chat) {
            unGetUserExts.add(element.id);
          } else if (element.type == EMConversationType.ChatRoom) {
            unGetRoomExts.add(element.id);
          } else if (element.type == EMConversationType.GroupChat) {
            unGetGroupExts.add(element.id);
          }
        } else {
          if (ext.nickName != null) {
            element.name = ext.nickName!;
          }
          element.ext ??= {};
          element.ext!['avatar'] = ext.avatar ?? '';
          switch (ext.czType) {
            case 0:
              element.type = EMConversationType.Chat;
              break;
            case 1:
              element.type = EMConversationType.GroupChat;
              break;
            case 2:
              element.type = EMConversationType.ChatRoom;
              break;
          }
        }
      }
      List<Friend>? list;
      if (unGetUserExts.isNotEmpty) {
        list = await memberRestClient.fetchUsers(unGetUserExts);
      } else {
        list = <Friend>[];
      }
      List<Friend>? roomList;
      if (unGetRoomExts.isNotEmpty) {
        roomList = await memberRestClient.fetchChatRooms(unGetRoomExts);
      } else {
        roomList = <Friend>[];
      }
      List<Friend>? groupList;
      if (unGetGroupExts.isNotEmpty) {
        groupList = await memberRestClient.fetchChatGroups(unGetGroupExts);
      } else {
        groupList = <Friend>[];
      }
      logger.d('unGetUserExts: $unGetUserExts');
      logger.d('unGetRoomExts: $unGetRoomExts');
      logger.d('unGetGroupExts: $unGetGroupExts');
      list?.addAll(roomList ?? []);
      list?.addAll(groupList ?? []);
      final newValue = <String, Friend>{};
      list?.forEach((element) {
        newValue[element.targetId!] = element;
        for (var c in conversations) {
          if (c.id == element.targetId) {
            if (element.nickName != null) {
              c.name = element.nickName!;
            }
            c.ext ??= {};
            c.ext!['avatar'] = element.avatar ?? '';
            switch (element.czType) {
              case 0:
                c.type = EMConversationType.Chat;
                break;
              case 1:
                c.type = EMConversationType.GroupChat;
                break;
              case 2:
                c.type = EMConversationType.ChatRoom;
                break;
            }
          }
        }
      });
      _globalController.userExtCache.addAll(newValue);
      logger.d('当前缓存的数据：${_globalController.userExtCache}');
    }
  }

  deleteConv(int index) async {
    var conversation = state![index];
    // BrnDialogManager.showConfirmDialog(Get.context,
    //     title: '是否删除会话？',
    //     warning: '删除后聊天记录将不会保存',
    //     cancel: '取消',
    //     confirm: '删除',
    //     onCancel: () => Get.back(),
    //     onConfirm: () async => _deleteConversation(conversation));
    await _deleteConversation(conversation);
  }

  Future? _deleteConversation(EMConversation conversation) async {
    await EMClient.getInstance.chatManager
        .deleteConversation(conversation.id, true);
    state!.remove(conversation);
    // refresh();
    // Get.back();
  }

  connectStr() {
    switch (_globalController.imConnectState) {
      case 0:
        return '(连接中)';
      case 2:
        return '(连接失败)';
      default:
        return '';
    }
  }

  readAll(int index) async {
    await imManager.imChatService.readAllMessage(indexOfItem(index));
    // await EMClient.getInstance.chatManager.markAllConversationsAsRead();
    // await _state.conversations[index].markAllMessagesAsRead();
    onRefresh();
  }

  void _onMessageReceived() async {
    await FlutterRingtonePlayer.stop();
    FlutterRingtonePlayer.playNotification(volume: 0.2, looping: false);
  }

  void _onMessageVibrate() async {
    if (_globalController.isGlobalVibrate && await Vibration.hasVibrator()) {
      Vibration.vibrate(duration: 200);
    }
  }

  Future? _sendNotification(List<EMMessage>  messages) async {
    logger.d('_sendNotification');
    for (var element in messages) {
      var name = element.attributes['nickName'];
      String info = element.showInfo();
      var notificationId = element.conversationId.hashCode;
      if(element.chatType == EMMessageChatType.Chat) {
        var list = await memberRestClient.fetchUsers([element.conversationId]);
        if(list?.isNotEmpty ?? false) {
          name = list![0].nickName;
        }
      } else if(element.chatType == EMMessageChatType.GroupChat) {
        var list = await memberRestClient.fetchChatGroups([element.conversationId ?? '']);
        if(list?.isNotEmpty ?? false) {
          name = list![0].nickName;
        }
      } else if(element.chatType == EMMessageChatType.ChatRoom) {
        var list = await memberRestClient.fetchChatRooms([element.conversationId ?? '']);
        if(list?.isNotEmpty ?? false) {
          name = list![0].nickName;
        }
      }
      logger.d('_sendNotification ==> $name, $info');
      AwesomeNotifications().createNotification(
          content: NotificationContent(
              id: notificationId,
              channelKey: 'xchat_channel',
              title: name,
              body: info,
            ticker: '$name有新的消息'
          )
      );
    }}



  @override
  void onPaused() {
    _globalController.isAppPause = true;
    super.onPaused();
  }

  @override
  void onResumed() {
    _globalController.isAppPause = false;
    super.onResumed();
  }
}
