import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/widget_im_avatar.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'single_chat_setup_logic.dart';

class SingleChatSetupPage extends GetView<SingleChatSetupLogic> {
  const SingleChatSetupPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: controller.memberName,
      ),
      body: controller.volatile((state) => SingleChildScrollView(
            child: Column(
              children: [
                _header(),
                ListTile(
                  title: const Text('当前聊天背景设置'),
                  trailing: const Icon(Icons.chevron_right),
                  onTap: () => controller.setupBackground(),
                ),
                BrnLine(
                  height: 10,
                ),
                // ListTile(
                //   title: const Text('查找聊天记录'),
                //   trailing: const Icon(Icons.chevron_right),
                //   onTap: () => controller.searchHistory(),
                // ),
                // BrnLine(
                //   height: 10,
                // ),
                // ListTile(
                //   title: const Text('置顶聊天'),
                //   trailing: Obx(
                //     () => CupertinoSwitch(
                //       value: controller.isTop,
                //       onChanged: (bool value) => controller.onTop(value),
                //     ),
                //   ),
                // ),
                // BrnLine(),
                ListTile(
                  title: const Text('消息免打扰'),
                  trailing: Obx(
                    () => CupertinoSwitch(
                      value: controller.isMute,
                      onChanged: (bool value) => controller.onMute(value),
                    ),
                  ),
                ),
                // BrnLine(
                //   height: 10,
                // ),
                // ListTile(
                //   title: const Text('清空聊天记录'),
                //   onTap: () => controller.searchHistory(),
                // ),
              ],
            ),
          )),
    );
  }

  Widget _header() => Container(
        color: BrnThemeConfigurator.instance
            .getConfig()
            .commonConfig
            .dividerColorBase,
        padding: const EdgeInsets.all(12),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Column(
              children: [
                imUserAvatar(controller.avatar, 48, name: controller.memberName),
                Text(controller.memberName)
              ],
            ),
            const SizedBox(
              width: 8,
            ),
            GestureDetector(
                onTap: () => controller.createGroup(),
                child: const Icon(
                  CupertinoIcons.add_circled,
                  size: 48,
                  color: Colors.grey,
                ))
          ],
        ),
      );
}
