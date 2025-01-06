import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:easy_chat/views/member/select/select_member_logic.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import '../../../../export.dart';
import 'chat_room_setup_state.dart';

class ChatRoomSetupLogic extends BaseController<EMChatRoom> {
  final ChatRoomSetupState _state = ChatRoomSetupState();
  final GlobalController _globalController = Get.find();

  final roomId = Get.arguments;

  bool get isManager => _globalController.userInfo?.isAgent ?? false;

  @override
  Future<EMChatRoom> Function() get requestFunc => () async {
        EMChatRoom chatRoom =
            await imManager.imRoomService.getChatRoomInfo(roomId);
        List<String> allMembers = [];
        allMembers.add(chatRoom.owner!);
        chatRoom.adminList?.forEach((element) {
          if (element is String) {
            allMembers.add(element);
          }
        });
        chatRoom.memberList?.forEach((element) {
          if (element is String) {
            allMembers.add(element);
          }
        });
        _state.memberIds.clear();
        _state.memberIds.addAll(allMembers);
        var members = await imManager.imUserService.fetchAllUsers(allMembers);
        await _globalController.fetchUserExt(members);
        _state.roomMembers.clear();
        _state.roomMembers.addAll(members);
        return chatRoom;
      };

  get itemCount => _state.roomMembers.length + (isManager ? 2 : 0);

  int get memberCount => state?.memberCount ?? 0;

  get groupName => state?.name ?? '';

  get nickname => '';

  get notice => state?.announcement;

  get isTop => _state.isTop.value;

  get isMute => _state.isMute.value;
  set isMute(value) => _state.isMute.value = value;

  @override
  void onDataLoaded() {
    logger.d('adminList ==> ${state?.adminList}');
    logger.d('memberList ==> ${state?.memberList}');
    logger.d('name ==> ${state?.name}');
    super.onDataLoaded();
  }

  @override
  void onInit() {
    isMute = _globalController.isConvNotify(roomId);
    super.onInit();
  }

  @override
  void onReady() {
    onRefresh();
    super.onReady();
  }

  EMUserInfo indexOfMember(int index) => _state.roomMembers[index];

  bool isMember(int index) => index < itemCount - (isManager ? 2 : 0);

  bool isRemove(int index) => index == itemCount - (isManager ? 1 : 0);

  toggleTop(bool value) => _state.isTop.toggle();

  clearHistory() {}

  toggleMute(bool value) {
    _state.isMute.toggle();
    _globalController.changeNotify(roomId, value);
  }

  exit() {}

  roomMembers() => Get.toNamed(PageRoutes.selectMember,
      arguments: {'type': SelectType.roomMembers, 'id': roomId});

  addMember() => Get.toNamed(PageRoutes.selectMember,
          arguments: {'type': SelectType.addRoomMember, 'id': roomId})
      ?.then((value) => onRefresh());

  updateMembers({bool remove = false}) {
    Get.toNamed(PageRoutes.selectMember, arguments: {
      'type': remove ? SelectType.removeRoomMember : SelectType.addRoomMember,
      'id': roomId,
      'selected': _state.memberIds
    });
  }

  toSelectBackground() => Get.toNamed(PageRoutes.backgroundSetup, arguments: roomId);
}
