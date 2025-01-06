import 'package:bruno/bruno.dart';
import 'package:easy_chat/export.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/friend.dart';
import 'package:easy_chat/models/params/add_friend_params.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:easy_chat/views/member/search/member_search_logic.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'member_home_state.dart';

class MemberHomeLogic extends BaseController<Friend?> {
  final MemberHomeState _state = MemberHomeState();
  final GlobalController _globalController = Get.find();
  final String memberId = Get.arguments;

  String get username => state?.nickName ?? state?.targetId ?? '-';

  String get userId => state?.targetId ?? '-';

  String? get avatar => state?.avatar;

  String get userArea => '未知';

  bool get isFriend => _state.friend.value?.isFriend ?? false;

  get intro => _state.friend.value?.introduce ??'-';

  get isFemale => _state.friend.value?.gender == 2;

  Future? chatNow() async {
    EMConversation? conv =
        await EMClient.getInstance.chatManager.getConversation(memberId);
    conv?.name = _state.friend.value?.nickName ?? memberId;
    Get.toNamed(PageRoutes.chat, arguments: conv);
  }

  @override
  Future<Friend?> Function() get requestFunc => () async {
        // EMUserInfo? emUserInfo =
        //     await imManager.imUserService.fetchUMemberInfo(memberId);
        var friend = await memberRestClient.getMemberInfo(memberId);
        _state.friend.value = friend;
        // emUserInfo?.nickName = friend?.nickName;
        // emUserInfo?.avatarUrl = friend?.avatar;
        return friend;
      };

  @override
  void onReady() {
    logger.d('memberId ==> $memberId');
    onRefresh();
    super.onReady();
  }

  static const actionsStr = <String>[
    '解除好友关系',
    '拉黑',
    '取消拉黑',
    '举报',
  ];

  Future? showMorePop() async {
    List<BrnCommonActionSheetItem> actions = [];
    if (isFriend) {
      actions.add(BrnCommonActionSheetItem(
        actionsStr[0],
        actionStyle: BrnCommonActionSheetItemStyle.alert,
      ));
    }
    bool isInBlack = await isMemberInBlack();
    actions.add(BrnCommonActionSheetItem(
      isInBlack ? actionsStr[2] : actionsStr[1],
      actionStyle: BrnCommonActionSheetItemStyle.normal,
    ));
    actions.add(BrnCommonActionSheetItem(
      actionsStr[3],
      actionStyle: BrnCommonActionSheetItemStyle.normal,
    ));

    // 展示actionSheet
    showModalBottomSheet(
        context: Get.context!,
        backgroundColor: Colors.transparent,
        builder: (BuildContext context) {
          return BrnCommonActionSheet(
            actions: actions,
            title: username,
            clickCallBack: (
              int index,
              BrnCommonActionSheetItem actionEle,
            ) {
              switch (actionsStr.indexOf(actionEle.title)) {
                case 0:
                  _doDeleteFriend();
                  break;
                case 1:
                case 2:
                  _doBlacklist();
                  break;
                case 3:
                  _doReport();
              }
            },
          );
        });
  }

  addFriend() {
    var textEditingController = TextEditingController();
    BrnMiddleInputDialog(
        title: '添加${state?.nickName}为好友',
        // message: '确定将${state?.nickName}添加为好友吗',
        hintText: '备注信息（选填）',
        cancelText: '取消',
        confirmText: '确定',
        maxLength: 100,
        maxLines: 1,
        barrierDismissible: true,
        inputEditingController: textEditingController,
        textInputAction: TextInputAction.done,
        onConfirm: (value) => _doAddFriend(value),
        onCancel: () {
          Get.back();
        }).show(Get.context!);
  }

  _doAddFriend(String value) async {
    Get.back();
    await memberRestClient.addFriend(AddFriendParams(
      targetId: memberId,
      remarks: value,
    ));
    _globalController.refreshContactAction();
    _state.friend.value?.isFriend = true;
    onRefresh();
    _refreshSearchList();
  }

  void _doDeleteFriend() {
    ToastUtil.success('删除好友');
  }

  void _doBlacklist() async {
    try {
      bool isInBlack = await isMemberInBlack();
      if (isInBlack) {
        await EMClient.getInstance.contactManager
            .removeUserFromBlockList(memberId);
        ToastUtil.success('已从黑名单中移除');
      } else {
        await EMClient.getInstance.contactManager.addUserToBlockList(memberId);
        ToastUtil.success('已添加到黑名单');
      }
    } on EMError catch (e) {
      logger.e('操作失败，原因是: $e');
    }
  }

  Future? isMemberInBlack() async {
    List<String?> blacklist =
        await EMClient.getInstance.contactManager.getBlockListFromServer();
    return blacklist.contains(memberId);
  }

  void _doReport() {
    ToastUtil.success('举报');
  }

  ///通过搜索进入的需要刷新搜索列表的tag
  void _refreshSearchList() {
    try {
      MemberSearchLogic memberSearchLogic = Get.find();
      var friend = memberSearchLogic.state
          ?.firstWhere((element) => element.targetId == memberId);
      if (friend != null) {
        friend.isFriend = true;
      }
      memberSearchLogic.refresh();
    } catch (e) {
      logger.e('不是通过search进去的，刷新搜索列表失败...');
    }
  }
}
