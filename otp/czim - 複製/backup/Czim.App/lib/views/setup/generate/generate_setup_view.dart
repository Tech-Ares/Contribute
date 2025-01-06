import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'generate_setup_logic.dart';

class GenerateSetupPage extends GetView<GenerateSetupLogic> {
  const GenerateSetupPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: '通用设置',
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            ListTile(
              title: const Text('聊天字体大小'),
              trailing: const Icon(Icons.chevron_right),
              onTap: () => controller.setupChatFontSize(),
            ),
            BrnLine(),
            // ListTile(
            //   title: const Text('多语言'),
            //   trailing: const Icon(Icons.chevron_right),
            //   onTap: () => {},
            // ),
            // BrnLine(),
            ListTile(
              title: const Text('聊天背景设置'),
              trailing: const Icon(Icons.chevron_right),
              onTap: () => Get.toNamed(PageRoutes.backgroundSetup),
            ),
            BrnLine(
              height: 10,
            ),
            // ListTile(
            //   title: const Text('在导航栏中显示通话记录'),
            //   trailing: Obx(
            //     () => CupertinoSwitch(
            //       value: controller.isShowHistory,
            //       onChanged: (bool value) => controller.onShowHistory(value),
            //     ),
            //   ),
            // ),
            // BrnLine(),
            ListTile(
              title: const Text('黑名单'),
              trailing: const Icon(Icons.chevron_right),
              onTap: () => Get.toNamed(PageRoutes.blacklist),
            ),
            // BrnLine(),
            // ListTile(
            //   title: const Text('推送设置'),
            //   trailing: const Icon(Icons.chevron_right),
            //   onTap: () => {},
            // ),
          ],
        ),
      ),
    );
  }
}
