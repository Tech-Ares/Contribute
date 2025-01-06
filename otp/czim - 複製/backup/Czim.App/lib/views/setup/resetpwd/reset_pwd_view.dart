import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/login_input_text.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'reset_pwd_logic.dart';

class ResetPwdPage extends GetView<ResetPwdLogic> {
  const ResetPwdPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      // appBar: BrnAppBar(title: 'register'.tr),
      body: SingleChildScrollView(
        child: Column(
          children: [
            Stack(
              children: [
                Image.asset(Utils.getImgPath('bg_login')),
                PreferredSize(
                  preferredSize: const Size.fromHeight(kToolbarHeight),
                  child: BrnAppBar(
                    themeData: BrnAppBarConfig.dark(),
                    backgroundColor: Colors.transparent,
                    showDefaultBottom: false,
                  ),
                )
              ],
            ),
            Container(
              width: double.infinity,
              margin: EdgeInsets.only(right: 16.w),
              alignment: Alignment.topRight,
              child: Image.asset(
                Utils.getImgPath('ic_logo'),
                width: 160.w,
              ),
            ),
            Container(
              padding: const EdgeInsets.all(24),
              child: Column(
                children: [
                  LoginInputText(
                    title: '原密码'.tr,
                    hint: '请输入原密码'.tr,
                    textEditingController: controller.nameTextController,
                      keyboardType: TextInputType.visiblePassword,
                      obscureText: true
                  ),
                  BrnLine(),

                  const SizedBox(
                    height: 24,
                  ),
                  LoginInputText(
                    title: '新密码',
                    hint: '请输入新密码',
                    textEditingController: controller.pwdTextController,
                    // suffix: _sendCodeWidget(),
                      keyboardType: TextInputType.visiblePassword,
                      obscureText: true
                  ),
                  BrnLine(),

                  const SizedBox(
                    height: 24,
                  ),
                  LoginInputText(
                      title: '确认新密码'.tr,
                      hint: '请再次输入新密码'.tr,
                      textEditingController: controller.pwdConfirmTextController,
                      keyboardType: TextInputType.visiblePassword,
                      obscureText: true),
                  BrnLine(),
                  const SizedBox(
                    height: 40,
                  ),
                  BrnBigMainButton(
                    //TODO
                    borderRadius: const BorderRadius.all(Radius.circular(24)),
                    title: 'confirm'.tr,
                    onTap: () => controller.reset(),
                  ),
                ],
              ),
            )
          ],
        ),
      ),
    );
  }
}
