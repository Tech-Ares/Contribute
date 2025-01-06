import 'package:easy_chat/export.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/im_group_info.dart';
import 'package:easy_chat/models/params/im_group_info_params.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:easy_chat/views/member/select/select_member_logic.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'group_chat_setup_state.dart';

class GroupChatSetupLogic extends BaseController<IMGroupInfo> {
  final GroupChatSetupState _state = GroupChatSetupState();
  final GlobalController _globalController = Get.find();
  final groupId = Get.arguments;

  @override
  Future<IMGroupInfo> Function() get requestFunc =>
      () async {
        var params = IMGroupInfoParams(targetId: groupId);
        var group = await memberRestClient.imGroupInfo(params);
        var memberList = await memberRestClient.imGroupMembers(params);
        return group..members = memberList.dataList;
      };

  //     () async {
  //   EMGroup group = await imManager.imGroupService.fetchGroupInfo(groupId);
  //   List<String> allMembers = [];
  //   allMembers.add(group.owner!);
  //   group.adminList?.forEach((element) {
  //     if (element is String) {
  //       allMembers.add(element);
  //     }
  //   });
  //   group.memberList?.forEach((element) {
  //     if (element is String) {
  //       allMembers.add(element);
  //     }
  //   });
  //   var members = await imManager.imUserService.fetchAllUsers(allMembers);
  //   await _globalController.fetchUserExt(members);
  //   _state.groupMembers.clear();
  //   _state.groupMembers.addAll(members);
  //   logger.d('当前群成员 ==> $members');
  //   return group;
  // };

  get itemCount => state!.members!.length + 2;

  int get memberCount => state?.count ?? 0;

  get groupName => state?.groupName ?? '';

  get nickname => '';

  get notice => state?.announcement;

  get isTop => _state.isTop.value;

  get isMute => _state.isMute.value;

  @override
  void onDataLoaded() {
    super.onDataLoaded();
  }

  @override
  void onInit() {
    _state.isMute.value = _globalController.isConvNotify(groupId);
    super.onInit();
  }

  @override
  void onReady() {
    onRefresh();
    super.onReady();
  }

  EMUserInfo indexOfMember(int index) => _state.groupMembers[index];

  bool isMember(int index) => index < itemCount - 2;

  bool isRemove(int index) => index == itemCount - 1;

  toggleTop(bool value) => _state.isTop.toggle();

  clearHistory() {}

  toggleMute(bool value) {
    _state.isMute.toggle();
    _globalController.changeNotify(groupId, value);
  }

  groupMembers() => Get.toNamed(PageRoutes.selectMember,
      arguments: {'type': SelectType.groupMembers, 'id': groupId});

  addMember() => Get.toNamed(PageRoutes.selectMember,
          arguments: {'type': SelectType.addRoomMember, 'id': groupId})
      ?.then((value) => onRefresh());

  updateMembers({bool remove = false}) {
    Get.toNamed(PageRoutes.selectMember, arguments: {
      'type': remove ? SelectType.removeGroupMember : SelectType.addGroupMember,
      'id': groupId,
      'selected': _state.memberIds
    })?.then((value) => onRefresh());
  }

  setupBackground() =>
      Get.toNamed(PageRoutes.backgroundSetup, arguments: groupId);

  exit() {}

  toGroupQRCode() => Get.toNamed(PageRoutes.qrcode, arguments: state);
}
