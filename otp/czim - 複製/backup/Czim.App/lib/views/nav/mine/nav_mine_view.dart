import 'dart:io';

import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_pull_refresh.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/widget_im_avatar.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'nav_mine_logic.dart';

class NavMinePage extends GetView<NavMineLogic> {
  const NavMinePage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      // appBar: BrnEmptyAppBar(0),
      body: BasePullToRefresh(
        onRefresh: () => controller.onRefresh(),
        enableLoadMore: false,
        refreshController: controller.refreshController,
        child: SafeArea(
          child: Column(
            children: [
              const SizedBox(
                height: 20,
              ),
              Obx(() => ListTile(
                    title: Text(
                      '${controller.username}',
                      style: Get.textTheme.headline3,
                    ),
                    contentPadding: const EdgeInsets.symmetric(horizontal: 24),
                    subtitle: Row(
                      children: [
                        Text('XChat ID: ${controller.memberId}'),
                        const SizedBox(
                          width: 8,
                        ),
                        Obx(() => Visibility(
                            visible: controller.isManager,
                            child: BrnStateTag(
                              tagText: '推广员',
                              tagState: TagState.waiting,
                            )))
                      ],
                    ),
                    trailing: imUserAvatar(controller.avatar, 50,
                        name: controller.username),
                  )),
              BrnLine(),
              _itemView(
                  svgPath: 'ic_mine_profile',
                  title: 'profile'.tr,
                  onTap: () => controller.onProfile()),
              BrnLine(
                height: 12,
              ),
              _itemView(
                  svgPath: 'ic_mine_qrcode',
                  title: 'my_qr_code'.tr,
                  onTap: () => controller.onQRCode()),
              BrnLine(
                height: 12,
              ),
              _itemView(
                  svgPath: 'ic_mine_setup',
                  title: 'setup'.tr,
                  onTap: () => controller.onSetup()),
              BrnLine(),
              _itemView(
                  svgPath: 'ic_mine_feedback',
                  title: 'feedback'.tr,
                  onTap: () => controller.onFeedback()),
              BrnLine(),
              _itemView(
                  svgPath: 'ic_mine_about',
                  title: 'about'.tr,
                  onTap: () => controller.onAbout()),
              // Text('${controller.imToken}'),
              // Image(
              //     image: NetworkImage(
              //         'https://a61-cn.easemob.com/1107220118095349/xchat/chatfiles/74cb5e30-78ed-11ec-a07c-c97a4346b6b2',
              //         headers: {
              //           'Authorization': 'Bearer ${controller.imToken}'
              //         }
              //     )),
              Expanded(child: Container()),
              // Obx(() => Padding(
              //   padding: const EdgeInsets.only(bottom: 24),
              //   child: Text(controller.foundation, style: Get.textTheme.bodyText2,),
              // ))
            ],
          ),
        ),
      ),
    );
  }

  Widget _itemView(
      {required String svgPath,
      required String title,
      GestureTapCallback? onTap}) {
    return ListTile(
      onTap: onTap,
      leading: SvgPicture.asset(
        Utils.getSvgPath(svgPath),
        width: 24,
        height: 24,
      ),
      minVerticalPadding: 0,
      contentPadding: const EdgeInsets.symmetric(horizontal: 24),
      title: Transform(
        transform: Matrix4.translationValues(-16, 0.0, 0.0),
        child: Text(title),
      ),
      trailing: const Icon(Icons.chevron_right),
    );
  }
}
