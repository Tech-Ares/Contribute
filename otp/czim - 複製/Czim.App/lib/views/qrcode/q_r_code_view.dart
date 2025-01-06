import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/widget_im_avatar.dart';
import 'package:easy_chat/global/i_style.dart';
import 'package:easy_chat/mock/mock_data.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'q_r_code_logic.dart';

class QRCodePage extends GetView<QRCodeLogic> {
  const QRCodePage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      backgroundColor: IColors.backgroundColor,
      appBar: PreferredSize(
        preferredSize: const Size.fromHeight(kToolbarHeight),
        child: Obx(() => BrnAppBar(
          title: controller.isGroup ? '群二维码' : 'my_qr_code'.tr,
        )),
      ),
      body: Column(
        children: [
          Expanded(
              child: Center(
                child: RepaintBoundary(
                  key: controller.paintKey,
                  child: BrnShadowCard(
                    padding: const EdgeInsets.all(16),
                    color: Colors.white,
                    shadowColor: IColors.primarySwatch.withAlpha(20),
                    spreadRadius: 4,
                    child: Column(
                      mainAxisSize: MainAxisSize.min,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Row(
                          mainAxisSize: MainAxisSize.min,
                          mainAxisAlignment: MainAxisAlignment.start,
                          children: [
                            imUserAvatar(controller.avatar, 40, name: controller.nickname),
                            const SizedBox(width: 8,),
                            Column(
                              mainAxisSize: MainAxisSize.min,
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                Text(controller.nickname, style: Get.textTheme.headline4,),
                                Text(controller.desc, style: Get.textTheme.subtitle2?.copyWith(fontSize: 10),)
                              ],
                            )
                          ],
                        ),
                        const SizedBox(height: 8,),
                        QrImage(
                          data: controller.getQrCode,
                          version: QrVersions.auto,
                          size: 240,
                        )
                      ],
                    ),
                  ),
                ),
              )),
          SafeArea(
              child: Container(
            padding: const EdgeInsets.all(16),
            child: BrnBigGhostButton(
              title: '保存名片',
              onTap: () => controller.saveImage(),
            ),
          ))
        ],
      ),
    );
  }
}
