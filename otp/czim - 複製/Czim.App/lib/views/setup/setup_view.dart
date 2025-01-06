import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'setup_logic.dart';

class SetupPage extends GetView<SetupLogic> {
  const SetupPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: 'setup'.tr,
      ),
      body: Column(
        children: [
          Expanded(
              child: SingleChildScrollView(
            child: Column(
              children: [
                _itemView(
                    title: 'reset_pwd'.tr,
                    onTap: () => Get.toNamed(PageRoutes.resetPwd)),
                BrnLine(height: 10,),
                _itemView(
                    title: '填写邀请码'.tr,
                    onTap: () => controller.fillInviteCode()),
                BrnLine(height: 10,),
                _itemView(
                    title: 'message_setup'.tr,
                    onTap: () => Get.toNamed(PageRoutes.messageSetup)),
                BrnLine(),
                _itemView(
                    title: 'clear_cache'.tr,
                    trailing: Obx(() => Text(
                      controller.cacheSize,
                      style: Get.textTheme.subtitle1,
                    )),
                    onTap: () => controller.clearCache()),
                BrnLine(height: 10,),
                _itemView(
                    title: 'generate_setup'.tr,
                    onTap: () => Get.toNamed(PageRoutes.generateSetup)),
              ],
            ),
          )),
          SafeArea(
              child: Padding(
            padding: const EdgeInsets.all(16),
            child: BrnBigMainButton(
              bgColor: Get.theme.bottomAppBarColor,
              title: 'logout'.tr,
              onTap: () => controller.logout(),
            ),
          )),
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
