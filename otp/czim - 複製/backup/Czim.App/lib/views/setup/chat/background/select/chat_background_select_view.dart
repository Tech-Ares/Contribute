import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/global/i_style.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'chat_background_select_logic.dart';

class ChatBackgroundSelectPage extends GetView<ChatBackgroundSelectLogic> {
  const ChatBackgroundSelectPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: '选择聊天背景',
      ),
      body: Container(
        padding: const EdgeInsets.all(4),
        child: GridView.builder(
            gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
                crossAxisCount: 3, crossAxisSpacing: 4, mainAxisSpacing: 4),
            itemCount: controller.assets.length,
            itemBuilder: _itemBuilder),
      ),
    );
  }

  Widget _itemBuilder(BuildContext context, int index) => Stack(
        alignment: Alignment.bottomRight,
        children: [
          GestureDetector(
            onTap: () => controller.select(index),
            child: Container(
              color: Colors.red,
              width: double.infinity,
              child: Image.asset(
                Utils.getImgPath(controller.assets[index], format: 'jpg'),
                fit: BoxFit.cover,
              ),
            ),
          ),
          Obx(() => Checkbox(
              activeColor: IColors.primarySwatch,
              shape: const CircleBorder(),
              value: controller.isSelected(index),
              onChanged: (v) => controller.select(index)))
        ],
      );
}
