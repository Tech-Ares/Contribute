import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'message_setup_logic.dart';

class MessageSetupPage extends GetView<MessageSetupLogic> {
  const MessageSetupPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: '消息设置',
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            ListTile(
              title: const Text('接收新消息通知'),
              trailing: Obx(
                    () => CupertinoSwitch(
                  value: controller.isReceiveNewMessage,
                  onChanged: (bool value) => controller.onReceiveNewMessage(value),
                ),
              ),
            ),
            BrnLine(height: 10,),
            // ListTile(
            //   title: const Text('推送消息显示详情'),
            //   trailing: Obx(
            //         () => CupertinoSwitch(
            //       value: controller.isShowMessageDetail,
            //       onChanged: (bool value) => controller.onShowMessageDetail(value),
            //     ),
            //   ),
            // ),
            // BrnLine(
            //   height: 10,
            // ),
            ListTile(
              title: const Text('振动'),
              trailing: Obx(
                    () => CupertinoSwitch(
                  value: controller.isVibration,
                  onChanged: (bool value) => controller.onVibration(value),
                ),
              ),
            ),
            BrnLine(),
            ListTile(
              title: const Text('声音'),
              trailing: Obx(
                    () => CupertinoSwitch(
                  value: controller.isSound,
                  onChanged: (bool value) => controller.onSound(value),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
