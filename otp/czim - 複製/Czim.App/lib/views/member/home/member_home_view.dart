import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/widget_im_avatar.dart';
import 'package:easy_chat/global/i_style.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'member_home_logic.dart';

class MemberHomePage extends GetView<MemberHomeLogic> {
  const MemberHomePage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        actions: BrnIconAction(
          child: const Icon(Icons.more_horiz_outlined),
          themeData: BrnAppBarConfig.dark()
            ..backgroundColor = Colors.blueAccent,
          //自定义action的点击
          iconPressed: () => controller.showMorePop(),
        ),
      ),
      body: controller.volatile((state) => SafeArea(
              child: Column(
            children: [
              Expanded(
                  child: Column(
                children: [
                  const SizedBox(
                    height: 100,
                  ),
                  imUserAvatar(controller.avatar, 100,
                      name: controller.username, fontSize: 40),
                  const SizedBox(
                    height: 24,
                  ),
                  Row(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Text(
                        controller.username,
                        style: Get.textTheme.headline3,
                      ),
                      SvgPicture.asset(
                        Utils.getSvgPath(
                            'icon_${controller.isFemale ? 'female' : 'male'}'),
                        width: 20,
                        color: controller.isFemale
                            ? IColors.primarySwatch
                            : IColors.color_2,
                      ),
                    ],
                  ),
                  const SizedBox(
                    height: 6,
                  ),
                  Text('简介: ${controller.intro}',
                      style: Get.textTheme.subtitle1),
                  const SizedBox(
                    height: 6,
                  ),
                  // Text('地区: ${controller.userArea}',
                  //     style: Get.textTheme.subtitle1),
                  // const SizedBox(
                  //   height: 40,
                  // ),
                ],
              )),
              Padding(
                padding: const EdgeInsets.all(24),
                child: Column(
                  children: [
                    if (!controller.isFriend)
                      BrnBigMainButton(
                        title: '加为好友',
                        onTap: () => controller.addFriend(),
                      ),
                    const SizedBox(
                      height: 10,
                    ),
                    BrnBigGhostButton(
                      title: '发送消息',
                      onTap: () => controller.chatNow(),
                    ),
                  ],
                ),
              ),
            ],
          ))),
    );
  }
}
