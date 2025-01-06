import 'package:azlistview/azlistview.dart';
import 'package:bruno/bruno.dart' show BrnPopupListWindow;
import 'package:easy_chat/export.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/contact_model.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'nav_contact_state.dart';

class NavContactLogic extends BaseController<List<ContactModel>>
    implements EMContactEventListener {
  final NavContactState _state = NavContactState();
  final GlobalController _globalController = Get.find();
  final actionKey = GlobalKey();

  @override
  Future<List<ContactModel>> Function() get requestFunc => () async {
        // final members =
        //     await imManager.imContactService.getAllContactFromServer();
        // if (members.isEmpty) {
        //   return [];
        // }
        final members = await memberRestClient.friends();
        final imMembers =
            members?.map((e) => ContactModel.fromFriend(e)).toList();
        // final imMembers = await imManager.imUserService
        //     .fetchUserInfo(members.map((e) => e ?? '').toList());
        final contactResult = <ContactModel>[];
        if (imMembers?.isNotEmpty ?? false) {
          contactResult.addAll(imMembers!);
        }
        _handleSortContactMembers(contactResult);
        contactResult.insertAll(0, _state.contactHeaders);
        return contactResult;
      };

  @override
  void onInit() {
    _globalController.refreshContact.listen((v) {
      onRefresh();
    });
    super.onInit();
  }

  @override
  void onReady() {
    imManager.imContactService.addContactListener(this);
    onRefresh();
    super.onReady();
  }

  @override
  void onClose() {
    imManager.imContactService.removeContactListener(this);
    super.onClose();
  }

  void _handleSortContactMembers(List<ContactModel> list) {
    if (list.isEmpty) return;
    // A-Z sort.
    SuspensionUtil.sortListBySuspensionTag(list);
    // show sus tag.
    SuspensionUtil.setShowSuspensionStatus(list);
  }

  void popMoreAction(BuildContext context) {
    BrnPopupListWindow.showButtonPanelPopList(context, actionKey,
        onItemClick: _onActionClick, data: ['添加好友', '扫一扫']);
  }

  _onActionClick(int index, String item) {
    switch (index) {
      case 0:
        return Get.toNamed(PageRoutes.addFriend);
      case 1:
        return Get.toNamed(PageRoutes.scan);
    }
  }

  /// 被[userName]添加为好友
  @override
  void onContactAdded(String? userName) {
    ToastUtil.success('$userName已添加你为好友');
    onRefresh();
  }

  /// 被[userName]从好友中删除
  @override
  void onContactDeleted(String? userName) {
    ToastUtil.success('$userName已将你删除好友');
    onRefresh();
  }

  /// 收到[userName]的好友申请，原因是[reason]
  @override
  void onContactInvited(String? userName, String? reason) {
    ToastUtil.success('收到$userName的好友申请：$reason');
  }

  /// 发出的好友申请被[userName]同意
  @override
  void onFriendRequestAccepted(String? userName) {
    ToastUtil.success('$userName已同意了你的好友申请');
  }

  /// 发出的好友申请被[userName]拒绝
  @override
  void onFriendRequestDeclined(String? userName) {
    ToastUtil.success('$userName拒绝了你的好友申请');
  }
}
