import 'package:easy_chat/export.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/user_info.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:gogoboom_flutter_getx_start/utils/version_util.dart';

import 'nav_mine_state.dart';

class NavMineLogic extends BaseController<UserInfo> {
  final NavMineState _state = NavMineState();
  final GlobalController _globalController = Get.find();

  String? get avatar => _globalController.userInfo?.avatar;

  String? get username => _globalController.userInfo?.nickName;

  get memberId => _globalController.userInfo?.bsvNumber;

  bool get isManager => _globalController.userInfo?.isAgent ?? false;

  get imToken => _globalController.imToken;

  Future? onProfile() => Get.toNamed(PageRoutes.profile);

  Future? onQRCode() => Get.toNamed(PageRoutes.qrcode);

  Future? onSetup() => Get.toNamed(PageRoutes.setup);

  Future? onFeedback() => Get.toNamed(PageRoutes.feedback);

  Future? onAbout() => Get.toNamed(PageRoutes.about);

  String get foundation => _state.foundation.value;
  set foundation(f) => _state.foundation.value = f;
  @override
  Future<UserInfo> Function() get requestFunc =>
      () => memberRestClient.userinfo();

  @override
  Future? onReady() async {
    await _initFoundation();
    super.onReady();
  }
  @override
  void onDataLoaded() {
    _globalController.loginSuccess(null, state!);
    super.onDataLoaded();
  }

  _initFoundation () async => foundation = await EiaVersion.getFoundation();
}
