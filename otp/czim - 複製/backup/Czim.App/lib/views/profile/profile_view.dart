import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/widget_im_avatar.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'profile_logic.dart';

class ProfilePage extends GetView<ProfileLogic> {
  const ProfilePage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: 'profile'.tr,
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            _itemView(
                onTap: () => controller.selectAvatar(),
                title: 'avatar'.tr,
                value: Obx(() => imUserAvatar(
                    controller.avatar, 40,
                    name: controller.showName))),
            BrnLine(
              height: 10,
            ),
            Obx(() => _itemView(
                title: 'nickname'.tr,
                value: '${controller.nickname ?? ''}',
                onTap: () => controller.editNickname())),
            BrnLine(),
            _itemView(
                title: 'xchat_id'.tr, value: controller.userInfo?.memberId ?? ''),
            BrnLine(),
            _itemView(
                title: 'my_qr_code'.tr,
                value: const Icon(Icons.qr_code),
                onTap: () => Get.toNamed(PageRoutes.qrcode)),
            BrnLine(
              height: 10,
            ),
            Obx(
              () => _itemView(
                  title: 'gender'.tr,
                  value: controller.genderStr,
                  onTap: () => controller.onGender()),
            ),
            BrnLine(),
            Obx(() => _itemView(
                title: 'birth'.tr,
                value: '${controller.birth ?? ''}',
                onTap: () => controller.onBirth())),
            BrnLine(),
            Obx(() => _itemView(
                title: 'signature'.tr,
                hint: 'signature_empty'.tr,
                value: controller.signature,
                onTap: () => controller.onSignature())),
            // BrnLine(),
            // Obx(() => _itemView(
            //     title: 'area'.tr,
            //     value: controller.city,
            //     onTap: () => controller.onCity())),
          ],
        ),
      ),
    );
  }

  Widget _itemView(
      {required String title,
      dynamic value,
      String hint = '',
      GestureTapCallback? onTap}) {
    return ListTile(
      onTap: onTap,
      title: Text(title),
      trailing: Row(
        mainAxisSize: MainAxisSize.min,
        children: [
          value is String?
              ? Text(
                  value ?? hint,
                  style: Get.textTheme.subtitle1?.copyWith(
                      color: value == null ? Colors.grey : Colors.black),
                )
              : value,
          const Icon(Icons.chevron_right)
        ],
      ),
    );
  }
}
