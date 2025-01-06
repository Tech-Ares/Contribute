import 'dart:async';

import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/params/register_params.dart';
import 'package:easy_chat/views/register/register_view.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import '../../export.dart';
import 'register_state.dart';

class RegisterLogic extends GetxController {
  final RegisterState state = RegisterState();
  final GlobalController _globalController = GlobalController();

  final TextEditingController nameTextController = TextEditingController();
  final TextEditingController pwdTextController = TextEditingController();
  final TextEditingController codeTextController = TextEditingController();
  final TextEditingController nickTextController = TextEditingController();

  Timer? _timer;

  get canSend => state.countDown.value == 0;

  get sendBtnShow => state.countDown.value == 0 ? '发送验证码' : '${state.countDown}s后发送';

  Future? register() async {
    String username = nameTextController.text.trim();
    String password = pwdTextController.text.trim();
    String code = codeTextController.text.trim();
    String nick = nickTextController.text.trim();
    var machineCode = await _globalController.getMachineCode();
    // var machineCode = '123456';
    await memberRestClient.register(RegisterParams(
        loginName: username,
        nickName: nick,
        meId: machineCode ?? '',
        passWord: password,
        passwordTwo: password,
        vCode: '',
        referralCode: code,
    ));
    // await imManager.imClientService.register(username, password);
    // ToastUtil.success('注册成功！');
    Get.back();
  }

  void sendVerifyCode() {
    String username = nameTextController.text.trim();
    if(username.isEmpty) {
      ToastUtil.error('请输入手机号');
      return;
    }
    state.countDown.value = 60;
    _timer = Timer.periodic(const Duration(seconds: 1), (timer) {
      if (state.countDown > 0) {
        state.countDown--;
      } else {
        _timer?.cancel();
      }
    });
  }

  @override
  void onClose() {
    _timer?.cancel();
    super.onClose();
  }
}
