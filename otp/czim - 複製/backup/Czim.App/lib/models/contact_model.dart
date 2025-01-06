import 'package:azlistview/azlistview.dart';
import 'package:bruno/bruno.dart' show BrnLoadingDialog;
import 'package:easy_chat/export.dart';
import 'package:easy_chat/models/friend.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';
import 'package:lpinyin/lpinyin.dart';

class ContactModel with ISuspensionBean {
  EMUserInfo? _userInfo;
  Friend? friend;
  String? _showName;
  String? svgPath;
  Color? bgColor;
  ContactType? contactType;
  bool isSelect = false;

  ContactModel.fromUserId(String userId)
      : _eid = userId,
        _isCustom = false;

  ContactModel.fromAt(String userId, String name,)
      : _eid = userId,
      _isCustom = false,
        _showName = name;

  ContactModel.fromUserInfo(EMUserInfo userInfo)
      : _userInfo = userInfo,
        _eid = userInfo.userId,
        _isCustom = false;

  ContactModel.fromFriend(this.friend)
      : _eid = friend?.targetId,
        _isCustom = false;

  ContactModel.custom(String name,
      [this.contactType, this.svgPath, this.bgColor])
      : _eid = name,
        _firstLetter = '↑',
        _isCustom = true;

  final String? _eid;

  final bool _isCustom;
  String? _firstLetter;

  bool get isCustom => _isCustom;

  String get username => _userInfo?.userId ?? '';

  String get showName {
    if (_showName != null) return _showName!;
    if (friend != null) {
      _showName = friend?.nickName ?? friend?.targetId ?? _eid;
    } else {
      _showName = _userInfo?.nickName ?? _userInfo?.userId ?? _eid;
    }
    if (_showName?.isEmpty ?? false) {
      _showName = _eid;
    }
    return _showName ?? '';
  }

  String? get contactId => _eid;

  String get firstLetter {
    if (_firstLetter == null) {
      if (_isCustom) {
        return '☆';
      }
      String str =
          PinyinHelper.getShortPinyin(showName.substring(0, 1)).toUpperCase();
      if (!RegExp(r'[A-Z]').hasMatch(str)) {
        str = '#';
      }
      _firstLetter = str;
    }
    return _firstLetter ?? '↑';
  }

  String? get avatar => friend?.avatar ?? _userInfo?.avatarUrl;

  @override
  String getSuspensionTag() => firstLetter;

  Future? onClick(BuildContext context) async {
    if (contactType != null) {
      switch (contactType!) {
        case ContactType.friends:
          Get.toNamed(PageRoutes.newFriend);
          break;
        case ContactType.groups:
          Get.toNamed(PageRoutes.groupList);
          break;
        case ContactType.tags:
          break;
        case ContactType.rooms:
          Get.toNamed(PageRoutes.roomList);
          break;
        case ContactType.customer:
          try {
            BrnLoadingDialog.show(Get.context!);
            var customer = await memberRestClient.customer();
            if ((customer?.isNotEmpty ?? false) &&
                customer?[0].targetId != null) {
              EMConversation? conv = await EMClient.getInstance.chatManager
                  .getConversation(customer![0].targetId!);
              conv?.name = customer[0].nickName ?? customer[0].targetId!;
              conv?.ext = {
                'nickName': customer[0].nickName ?? customer[0].targetId!,
                'avatar': customer[0].avatar ?? ''
              };
              logger.d('conv: ${conv?.toJson()}');
              Get.offAndToNamed(PageRoutes.chat, arguments: conv);
            }
          } catch (e) {
            logger.e(e);
            BrnLoadingDialog.dismiss(Get.context!);
          }
          break;
      }
    } else {
      EMConversation? conv = await EMClient.getInstance.chatManager
          .getConversation(friend?.targetId ?? _userInfo?.userId ?? '');
      conv?.name = friend?.nickName ?? friend?.targetId ?? '-';
      // if(conv?.type == EMConversationType.Chat) {
      //   Get.toNamed(PageRoutes.memberHome, arguments: conv?.id);
      // } else {
      //   Get.toNamed(PageRoutes.chat, arguments: conv);
      // }
      Get.toNamed(PageRoutes.chat, arguments: conv);
    }
  }
}

enum ContactType {
  friends,
  groups,
  rooms,
  tags,
  customer,
}
