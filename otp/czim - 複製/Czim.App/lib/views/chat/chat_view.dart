import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/global/i_style.dart';
import 'package:flutter/material.dart';
import 'package:flutter_chat_types/flutter_chat_types.dart' as types;
import 'package:flutter_chat_ui/flutter_chat_ui.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'chat_logic.dart';

class ChatPage extends GetView<ChatLogic> {
  final GlobalKey _globalKey = GlobalKey();

  @override
  String? get tag => '$_globalKey';

  ChatPage({Key? key}) : super(key: key) {
    Get.lazyPut(() => ChatLogic(), tag: '$_globalKey');
  }

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      backgroundColor: Get.theme.backgroundColor,
      appBar: BrnAppBar(
        title: controller.title,
        actions: BrnIconAction(
          child: const Icon(Icons.more_horiz_outlined),
          themeData: BrnAppBarConfig.dark()
            ..backgroundColor = Colors.blueAccent,
          //自定义action的点击
          iconPressed: () => controller.onChatMore(),
        ),
      ),
      body: SafeArea(
        bottom: false,
        child: Obx(() => Chat(
              messages: controller.conversationMessages,
              onMessageTap: controller.handleMessageTap,
              onMessageLongPress: controller.handleMessageLongPress,
              onPreviewDataFetched: controller.handlePreviewDataFetched,
              onMessageStatusTap: controller.handleMessageStatus,
              onSendPressed: controller.handleSendPressed,
              onAudioCompleted: controller.handleAudioSend,
              customMessageBuilder: _customMessageBuilder,
              showUserAvatars: true,
              onAvatarLongPress: controller.handleAvatarLongPressed,
              onAvatarTap: controller.handleAvatarTap,
              user: controller.user,
              onEndReachedThreshold: 1,
              onEndReached: controller.handleEndReached,
              dateLocale: 'zh',
              isLastPage: controller.isLastPage,
              l10n: const ChatL10nZhCN(),
              showUserNames: controller.showName,
              // onMessageLongPress: controller.onMessageLongPress,
              theme: DefaultChatTheme(
                receivedMessageBodyTextStyle: TextStyle(
                  color: neutral0,
                  fontSize: controller.fontSize,
                  fontWeight: FontWeight.w500,
                  height: 1.5,
                ),
                sentMessageBodyTextStyle: TextStyle(
                  color: neutral7,
                  fontSize: controller.fontSize,
                  fontWeight: FontWeight.w500,
                  height: 1.5,
                ),
                inputBorderRadius: BorderRadius.zero,
                primaryColor: Get.theme.bottomAppBarColor,
                userAvatarImageBackgroundColor: IColors.primarySwatch,
                inputBackgroundColor: Colors.white,
                inputTextColor: controller.isFire
                    ? Get.theme.bottomAppBarColor
                    : Colors.black87,
              ),
              attachments: controller.attachments,
              shakeAnimationController: controller.shakeAnimationController,
              fireNow: controller.fireNow,
              onFirePressed: () => controller.showFireDialog(),
              onMessageFirePress: controller.showFirePop,
              decoration: controller.decoration,
              focusNode: controller.focusNode,
              textEditingController: controller.textEditingController,
              headers: {
                'Authorization': 'Bearer ${controller.imToken}'
              },
              // stateWrapper: (types.Message message, Widget child) =>
              //     controller.wrapMessageState(message, child),
            )),
      ),
    );
  }

  Widget _customMessageBuilder(types.CustomMessage customMessage,
      {required int messageWidth}) {
    return Text('${customMessage.metadata?['tip']}');
  }
}
