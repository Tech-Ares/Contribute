import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/widget_im_avatar.dart';
import 'package:easy_chat/models/friend.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'blacklist_logic.dart';

class BlacklistPage extends GetView<BlacklistLogic> {
  const BlacklistPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
        appBar: BrnAppBar(
          title: '黑名单',
        ),
        body: controller.volatile(
          (state) => ListView.separated(
            itemBuilder: _itemBuilder,
            itemCount: controller.getItemCount,
            separatorBuilder: (BuildContext context, int index) => BrnLine(),
          ),
        ));
  }

  Widget _itemBuilder(BuildContext context, int index) {
    Friend item = controller.indexOfItem(index);
    return ListTile(
      leading: imUserAvatar(item.avatar, 36, name: item.nickName),
      title: Text(
        item.nickName ?? item.targetId ?? '-',
        style: Get.textTheme.subtitle1,
      ),
      onTap: () => item.onClick(),
    );
  }
}
