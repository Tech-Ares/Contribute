import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'chat_background_logic.dart';

class ChatBackgroundPage extends GetView<ChatBackgroundLogic> {
  const ChatBackgroundPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: '聊天背景设置',
      ),
      body: Column(
        children: [
          _itemView(
              title: '选择背景图'.tr,
              onTap: () => controller.toSelectBackground()),
          BrnLine(),
          _itemView(
              title: '从相册中选取'.tr,
              onTap: () => controller.selectFromGallery()),
          BrnLine(),
          _itemView(
              title: '拍一张'.tr,
              onTap: () => controller.selectFromCamera()),
          BrnLine(height: 10,),
          _itemView(
              title: '将选择的背景应用到所有的聊天场景'.tr,
              onTap: () => controller.setupAll()),
        ],
      ),
    );
  }

  Widget _itemView({required String title, GestureTapCallback? onTap, Widget? trailing}) {
    return ListTile(
      onTap: onTap,
      title: Text(title),
      trailing: Row(
        mainAxisSize: MainAxisSize.min,
        children: [
          if(trailing != null)
            trailing,
          const Icon(
            Icons.keyboard_arrow_right_rounded,
          )
        ],
      ),
    );
  }
}
