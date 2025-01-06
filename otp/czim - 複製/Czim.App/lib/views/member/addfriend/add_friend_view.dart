import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/widget_search_outside.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'add_friend_logic.dart';

class AddFriendPage extends GetView<AddFriendLogic> {
  const AddFriendPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: 'add_friend'.tr,
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            SearchOutsideWidget(
              title: 'member_search_hint'.tr,
              onTap: () => Get.toNamed(PageRoutes.memberSearch),
            ),
            ListTile(
              leading: const Icon(CupertinoIcons.qrcode_viewfinder),
              title: Transform(
                transform: Matrix4.translationValues(-16, 0.0, 0.0),
                child: const Text('扫描二维码名片'),
              ),
              onTap: () => Get.toNamed(PageRoutes.scan),
              trailing: const Icon(Icons.chevron_right),
            )
          ],
        ),
      ),
    );
  }
}
