import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/chat_background.dart';
import 'package:easy_chat/views/chat/chat_logic.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'chat_background_select_state.dart';

class ChatBackgroundSelectLogic extends GetxController {
  final ChatBackgroundSelectState _state = ChatBackgroundSelectState();
  final GlobalController _globalController = Get.find();
  final String? sessionId = Get.arguments;

  ChatBackground get chatBackground => _state.chatBackground.value;

  set chatBackground(value) => _state.chatBackground.value = value;

  int get selectedAssetIndex => _state.selectedIndex.value;

  set selectedAssetIndex(int index) => _state.selectedIndex.value = index;

  final testColors = [
    Colors.white,
    Colors.red,
    Colors.green,
    Colors.blue,
    Colors.yellow,
    Colors.greenAccent,
    Colors.deepOrange,
    Colors.deepPurple,
    Colors.lightGreenAccent,
  ];

  final assets = [
    'bg_01',
    'bg_02',
    'bg_03',
    'bg_04',
    // 'bg_05',
    // 'bg_06',
    // 'bg_07',
    // 'bg_08',
    // 'bg_09',
  ];

  isSelected(int index) => selectedAssetIndex == index;

  select(int index) {
    _globalController.saveChatBackground(
        chatBackground: ChatBackground.asset(assets[index], sessionId: sessionId));
    selectedAssetIndex = index;

    try {
      ChatLogic chatLogic = Get.find();
      chatLogic.reloadBackground();
    } catch (e) {
      logger.e('当前没有打开聊天页面，无需更新背景');
    }
  }

  @override
  void onInit() {
    logger.d('loadChatBackground: $sessionId');
    _state.chatBackground.value =
        _globalController.loadChatBackground(sessionId: sessionId);
    if (chatBackground.value == null) {
      selectedAssetIndex = 0;
    } else {
      selectedAssetIndex = assets.indexOf(chatBackground.value ?? '');
    }
    super.onInit();
  }
}
