import 'package:easy_chat/export.dart';
import 'package:easy_chat/models/params/update_pwd_params.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'reset_pwd_state.dart';

class ResetPwdLogic extends GetxController {
  final ResetPwdState state = ResetPwdState();

  final TextEditingController nameTextController = TextEditingController();
  final TextEditingController pwdTextController = TextEditingController();
  final TextEditingController pwdConfirmTextController = TextEditingController();

  Future? reset() async {
    final oldPwd = nameTextController.text.trim();
    final newPwd = pwdTextController.text.trim();
    final newPwd2 = pwdConfirmTextController.text.trim();
    await memberRestClient.restPassword(UpdatePwdParams(
      oldPwd: oldPwd,
      newPwd: newPwd,
      newPwd2: newPwd2
    ));
    Get.back();
  }
}
