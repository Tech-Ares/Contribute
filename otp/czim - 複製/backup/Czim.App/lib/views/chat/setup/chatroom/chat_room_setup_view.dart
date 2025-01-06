import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/widget_im_avatar.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'chat_room_setup_logic.dart';

class ChatRoomSetupPage extends GetView<ChatRoomSetupLogic> {
  const ChatRoomSetupPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: 'room_setup'.tr,
      ),
      body: controller.volatile((state) => SingleChildScrollView(
            child: SafeArea(
              child: Column(
                children: [
                  _header(),
                  ListTile(
                    title: const Text('群成员'),
                    onTap: () => controller.roomMembers(),
                    trailing: Row(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        Text('${controller.memberCount}人'),
                        const Icon(Icons.chevron_right)
                      ],
                    ),
                  ),
                  BrnLine(),
                  ListTile(
                    title: const Text('群名称'),
                    trailing: Row(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        Text('${controller.groupName}'),
                        const Icon(Icons.chevron_right)
                      ],
                    ),
                  ),
                  // BrnLine(),
                  // ListTile(
                  //   title: const Text('我在聊天室的名片'),
                  //   trailing: Row(
                  //     mainAxisSize: MainAxisSize.min,
                  //     children: [
                  //       Text('${controller.nickname}'),
                  //       const Icon(Icons.chevron_right)
                  //     ],
                  //   ),
                  // ),
                  // BrnLine(),
                  // ListTile(
                  //   title: const Text('聊天室二维码'),
                  //   trailing: Row(
                  //     mainAxisSize: MainAxisSize.min,
                  //     children: const [Icon(Icons.chevron_right)],
                  //   ),
                  // ),
                  // BrnLine(),
                  // ListTile(
                  //   title: const Text('聊天室公告'),
                  //   trailing: Row(
                  //     mainAxisSize: MainAxisSize.min,
                  //     children: [
                  //       Text('${controller.notice}'),
                  //       const Icon(Icons.chevron_right)
                  //     ],
                  //   ),
                  // ),
                  // BrnLine(),
                  // ListTile(
                  //   title: const Text('聊天室管理'),
                  //   trailing: Row(
                  //     mainAxisSize: MainAxisSize.min,
                  //     children: const [Icon(Icons.chevron_right)],
                  //   ),
                  // ),
                  BrnLine(
                    height: 10,
                  ),
                  ListTile(
                    onTap: () => controller.toSelectBackground(),
                    title: const Text('当前聊天背景设置'),
                    trailing: Row(
                      mainAxisSize: MainAxisSize.min,
                      children: const [Icon(Icons.chevron_right)],
                    ),
                  ),
                  // BrnLine(),
                  // ListTile(
                  //   title: const Text('查找聊天记录'),
                  //   trailing: Row(
                  //     mainAxisSize: MainAxisSize.min,
                  //     children: const [Icon(Icons.chevron_right)],
                  //   ),
                  // ),
                  // BrnLine(
                  //   height: 10,
                  // ),
                  // Obx(() => ListTile(
                  //       title: const Text('置顶聊天'),
                  //       trailing: CupertinoSwitch(
                  //         onChanged: (bool value) =>
                  //             controller.toggleTop(value),
                  //         value: controller.isTop,
                  //       ),
                  //     )),
                  BrnLine(),
                  Obx(() => ListTile(
                        title: const Text('消息免打扰'),
                        trailing: CupertinoSwitch(
                          onChanged: (bool value) =>
                              controller.toggleMute(value),
                          value: controller.isMute,
                        ),
                      )),
                  // BrnLine(
                  //   height: 10,
                  // ),
                  // ListTile(
                  //   title: const Text('清空聊天记录'),
                  //   onTap: () => controller.clearHistory(),
                  // ),
                  // const SizedBox(
                  //   height: 24,
                  // ),
                  // Container(
                  //   padding: const EdgeInsets.all(24),
                  //   child: BrnBigMainButton(
                  //     bgColor: Colors.red,
                  //     title: '退出',
                  //     onTap: () => controller.exit(),
                  //   ),
                  // )
                ],
              ),
            ),
          )),
    );
  }

  Widget _header() => Obx(() => Container(
        color: BrnThemeConfigurator.instance
            .getConfig()
            .commonConfig
            .dividerColorBase,
        child: GridView.builder(
          shrinkWrap: true,
          padding: EdgeInsets.zero,
          physics: const NeverScrollableScrollPhysics(),
          itemBuilder: _itemBuilder,
          itemCount: controller.itemCount,
          gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
              crossAxisCount: 5, childAspectRatio: 1.1),
        ),
      ));

  Widget _itemBuilder(BuildContext context, int index) {
    Widget content;
    if (controller.isMember(index)) {
      EMUserInfo user = controller.indexOfMember(index);
      content = Column(
        children: [
          imUserAvatar(user.avatarUrl, 40, name: user.nickName ?? user.userId),
          Text(
            user.nickName ?? user.userId,
            maxLines: 1,
            overflow: TextOverflow.ellipsis,
          )
        ],
      );
    } else if (controller.isRemove(index)) {
      content = GestureDetector(
        onTap: () => controller.updateMembers(remove: true),
        child: const Icon(
          Icons.remove_circle_outline,
          size: 40,
          color: Colors.grey,
        ),
      );
    } else {
      content = GestureDetector(
        onTap: () => controller.updateMembers(),
        child: content = const Icon(
          Icons.add_circle_outline,
          size: 40,
          color: Colors.grey,
        ),
      );
    }
    return Container(
      padding: const EdgeInsets.only(top: 8),
      alignment: Alignment.topCenter,
      child: content,
    );
  }
}
