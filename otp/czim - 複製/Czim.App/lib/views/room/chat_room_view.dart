import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_pull_refresh.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/widget_im_avatar.dart';
import 'package:easy_chat/mock/mock_data.dart';
import 'package:easy_chat/models/chat_room.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/widget/get_widget_image.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'chat_room_logic.dart';

class ChatRoomPage extends GetView<ChatRoomLogic> {
  const ChatRoomPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: 'chatroom_list'.tr,
      ),
      body: controller.volatile((state) => BasePullToRefresh(
            refreshController: controller.refreshController,
            onRefresh: () => controller.onRefresh(),
            child: ListView.separated(
                itemBuilder: _itemBuilder,
                separatorBuilder: (_, __) => BrnLine(),
                itemCount: controller.getItemCount),
          )),
    );
  }

  Widget _itemBuilder(BuildContext context, int index) {
    var chatRoom = controller.indexOfItem<ChatRoom>(index);
   return ListTile(
     onTap: () => controller.openConv(index),
     leading: imUserAvatar(chatRoom?.avatar, 40, name: '${chatRoom?.nickName}'),
     title: Text('${chatRoom?.nickName}'),
   );
  }
}
