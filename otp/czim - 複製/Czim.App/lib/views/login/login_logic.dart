import 'package:bruno/bruno.dart';
import 'package:easy_chat/export.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/authorization.dart';
import 'package:easy_chat/models/params/login_params.dart';
import 'package:easy_chat/models/user_info.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/cupertino.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'login_state.dart';

class LoginLogic extends GetxController {
  final LoginState state = LoginState();
  final GlobalController _globalController = Get.find();
  final TextEditingController nameTextController = TextEditingController();
  final TextEditingController pwdTextController = TextEditingController();

  Future? login(BuildContext context) async {
    // await imManager.imClientService.login('admin2', '123456');
    // Get.offAllNamed(PageRoutes.index);
    FocusScope.of(context).unfocus();
    String username = nameTextController.text.trim();
    String password = pwdTextController.text.trim();
    var machineCode = await _globalController.getMachineCode();
    // await imManager.imClientService.login(username, password);
    if(username.trim().isEmpty) {
      ToastUtil.error('请输入账号');
      return;
    }
    if(password.trim().isEmpty) {
      ToastUtil.error('请输入密码');
      return;
    }
    Authorization authorization = await memberRestClient.login(LoginParams(
        loginName: username,
        meId: machineCode ?? '',
        passWord: password,
        areaCode: ''));
    try {
      BrnLoadingDialog.show(context, content: '登录中...', barrierDismissible: false);
      _globalController.authorization = authorization;
      UserInfo userInfo = await memberRestClient.userinfo();
      //登录环信
      await imManager.imClientService.login(userInfo.id!, userInfo.easePwd!);
      logger.d('imToken: ${EMClient.getInstance.accessToken}');
      // await imManager.imClientService.login('admin', '123456');
      //获取环信的用户信息
      var ownInfo = await imManager.imUserService.fetchOwnInfo();
      _globalController.emUserUpdate(ownInfo);
      await _globalController.loginSuccess(authorization, userInfo);
      _globalController.imToken = EMClient.getInstance.accessToken;
      Get.offAllNamed(PageRoutes.index);
    } catch (e) {
      BrnLoadingDialog.dismiss(context);
      ToastUtil.error('$e');
      logger.e(e);
    }
  }
}
