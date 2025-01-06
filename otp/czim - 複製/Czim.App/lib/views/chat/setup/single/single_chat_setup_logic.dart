import 'package:easy_chat/export.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:easy_chat/views/member/select/select_member_logic.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'single_chat_setup_state.dart';

class SingleChatSetupLogic extends BaseController<EMUserInfo?> {
  final SingleChatSetupState _state = SingleChatSetupState();
  final GlobalController _globalController = GlobalController();
  final memberId = Get.arguments['id'];
  final memberName = Get.arguments['name'];

  @override
  Future<EMUserInfo?> Function() get requestFunc =>
          () => imManager.imUserService.fetchUMemberInfo(memberId);

  String? get avatar => state?.avatarUrl;

  get isTop => _state.isTop.value;

  get isMute => _state.isMute.value;

  set isMute(value) => _state.isMute.value = value;

  @override
  void onDataLoaded() {
    logger.d(state?.toJson());
    super.onDataLoaded();
  }

  @override
  void onInit() {
    isMute = _globalController.isConvNotify(memberId);
    super.onInit();
  }

  @override
  void onReady() {
    onRefresh();
    super.onReady();
  }

  setupBackground() =>
      Get.toNamed(PageRoutes.backgroundSetup, arguments: memberId);

  searchHistory() => Get.toNamed(PageRoutes.search);

  top() {}

  onTop(bool value) => _state.isTop.toggle();

  onMute(bool value) {
    _state.isMute.toggle();
    _globalController.changeNotify(memberId, value);
  }

  createGroup() =>
      Get.toNamed(PageRoutes.selectMember, arguments: {
        'type': SelectType.createGroup,
        'selected': <String>[memberId]
      });
}
