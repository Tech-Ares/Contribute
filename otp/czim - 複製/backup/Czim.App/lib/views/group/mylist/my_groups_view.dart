import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_pull_refresh.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/widget_im_avatar.dart';
import 'package:easy_chat/mock/mock_data.dart';
import 'package:easy_chat/models/im_group.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'my_groups_logic.dart';

class MyGroupsPage extends GetView<MyGroupsLogic> {
  const MyGroupsPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: 'my_group'.tr,
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

  Widget _itemBuilder(BuildContext context, int index) => ListTile(
    onTap: () => controller.openConv(index),
        leading: imUserAvatar(controller.indexOfItem<IMGroup>(index)?.avatar, 32),
        title: Text('${controller.indexOfItem<IMGroup>(index)?.groupName}'),
      );
}
