import 'dart:ui';

import 'package:easy_chat/global/i_style.dart';
import 'package:easy_chat/mock/mock_data.dart';
import 'package:easy_chat/utils/string_util.dart';
import 'package:flutter/material.dart';
import 'package:flutter_chat_ui/flutter_chat_ui.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

Widget convAvatar(EMConversation conversation) {
  // if (conversation.type == EMConversationType.GroupChat) {
  //   return SvgPicture.asset(
  //     Utils.getSvgPath('ic_group_avatar_default'),
  //     width: 48,
  //   );
  // } else {
    return imUserAvatar(conversation.ext?['avatar'], 48, name: conversation.name);
  // }
}

Widget imUserAvatar(String? url, double size,
    {double border = 0,
    Color? borderColor,
    String? imgRes,
    String? name,
    double? fontSize}) {
  final initials = getUserInitialsByName(name);
  final color = getUserNameColor(
    name ?? '',
    IColors.avatarColors,
  );
  logger.d('url ==> $url, name ==> $name');
  return Container(
    width: size,
    height: size,
    decoration: ShapeDecoration(
      color: IColors.hLineColor,
      shape: CircleBorder(
        side: BorderSide(
          width: border,
          color: borderColor ?? commonConfig.defaultAvatarBorderColor,
        ),
      ),
    ),
    child: ClipOval(
      child: ((url?.isEmpty ?? true))
          ? SizedBox(
              width: size,
              height: size,
              child: CircleAvatar(
                  backgroundColor: color,
                  radius: 16,
                  child: Text(
                    initials ?? '',
                    style: Get.textTheme.headline4
                        ?.copyWith(color: Colors.white, fontSize: fontSize),
                  )),
            )
          : CachedNetworkImage(
              height: size,
              width: size,
              imageUrl: url ?? MockData.avatarUlr,
              fit: BoxFit.cover,
              placeholder: (BuildContext context, String url) =>
                  SvgPicture.asset(
                      Utils.getSvgPath(commonConfig.defaultAvatarRes),
                      color: Colors.white,
                      width: size,
                      height: size),
              errorWidget: (BuildContext context, String url, dynamic error) =>
                  SvgPicture.asset(
                      Utils.getSvgPath(commonConfig.defaultAvatarRes),
                      width: size,
                      height: size),
            ),
    ),
  );
}

Widget imGroupAvatar(EMGroup emGroup, {double size = 40}) {
  return SvgPicture.asset(Utils.getSvgPath('ic_group_avatar_default'),
      width: size);
}
