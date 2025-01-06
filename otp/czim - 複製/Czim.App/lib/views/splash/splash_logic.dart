import 'dart:async';

import 'package:easy_chat/export.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

class SplashLogic extends BaseController {
  final int second = 3;
  final GlobalController _globalLogic = Get.find<GlobalController>();

  Timer? _timer;

  int? machineVerified;

  @override
  Future<void> onReady() async {
    _timer = Timer.periodic(
      Duration(seconds: second),
      (Timer timer) => jumpToIndex(),
    );
    await imManager.imClientService.init();
    await _loginAccount();
    super.onReady();
  }

  Future? jumpToIndex() async {
    _timer?.cancel();
    bool isLogin = EMClient.getInstance.isLoginBefore;
    String? token = _globalLogic.imToken;
    logger.d('imToken: $token');
    if(!isLogin || token == null) {
      if(isLogin) {
        await EMClient.getInstance.logout();
      }
      Get.offAndToNamed(PageRoutes.login);
    } else {
      Get.offAndToNamed(PageRoutes.index);
    }
  }

  @override
  void onClose() {
    _timer?.cancel();
    super.onClose();
  }

  Future? _loginAccount() async {
    bool isLogin = EMClient.getInstance.isLoginBefore;
    String? token = _globalLogic.imToken;
    if (!isLogin && token != null) {
      var emUserInfo = await imManager.imUserService.fetchOwnInfo();
      _globalLogic.emUserInfo = emUserInfo;
    }
    final timeInfo = await _globalLogic.getTimeParams();
    await memberRestClient.timeInfo(timeInfo);
  }
}
