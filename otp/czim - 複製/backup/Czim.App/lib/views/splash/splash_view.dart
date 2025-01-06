import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/global/i_style.dart';
import 'package:flutter/material.dart';
import 'package:getwidget/getwidget.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:animated_text_kit/animated_text_kit.dart';

import 'splash_logic.dart';

class SplashPage extends GetView<SplashLogic> {
  final SplashLogic logic = Get.put(SplashLogic());

  SplashPage({Key? key}) : super(key: key);

  final colorizeColors = [
    Color(0xfffc7a7f),
    Color(0xffcc6d70),
    Color(0xffab8486),
    Color(0xffd7d4d4),
  ];

  final colorizeTextStyle = const TextStyle(
    fontSize: 40.0,
    color: IColors.primarySwatch,
    fontFamily: 'Horizon',
  );

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      body: Stack(
        children: [
          // CachedNetworkImage(
          //   width: ScreenUtil().screenWidth,
          //   height: ScreenUtil().screenHeight,
          //   imageUrl: MockData.avatarUlr,
          //   fit: BoxFit.cover,
          // ),
          Center(
            child: Container(
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  SvgPicture.asset(
                    Utils.getSvgPath('ic_nav_message'),
                    width: 100,
                  ),
                  const SizedBox(
                    height: 12,
                  ),
                  // Text(
                  //   'app_name'.tr,
                  //   style: Get.textTheme.headline3
                  //       ?.copyWith(color: IColors.primarySwatch, fontSize: 28),
                  // ),
                  AnimatedTextKit(
                    animatedTexts: [
                      ColorizeAnimatedText(
                        'app_name'.tr,
                        textStyle: colorizeTextStyle,
                        colors: colorizeColors,
                      ),
                      ColorizeAnimatedText(
                        'app_name'.tr,
                        textStyle: colorizeTextStyle,
                        colors: colorizeColors,
                      ),
                    ],
                    // pause: const Duration(seconds: 3),
                    isRepeatingAnimation: true,
                    onTap: () {
                    },
                  )
                ],
              ),
            ),
          ),
          // Positioned(
          //   top: 32,
          //   right: 32,
          //   child: SafeArea(
          //     child: GFAvatar(
          //       backgroundColor: Colors.black45,
          //       size: 32,
          //       child: GFProgressBar(
          //         percentage: 1,
          //         radius: 48,
          //         circleWidth: 3,
          //         animation: true,
          //         animationDuration: logic.second * 1000,
          //         padding: EdgeInsets.zero,
          //         type: GFProgressType.circular,
          //         backgroundColor: Colors.black26,
          //         progressBarColor: IColors.accentColor,
          //         child: GestureDetector(
          //           onTap: () => logic.jumpToIndex(),
          //           child: Text(
          //             '跳过',
          //             style: Get.textTheme.bodyText2
          //                 ?.copyWith(color: Colors.white, fontSize: 12),
          //           ),
          //         ),
          //       ),
          //     ),
          //   ),
          // )
        ],
      ),
    );
  }
}
