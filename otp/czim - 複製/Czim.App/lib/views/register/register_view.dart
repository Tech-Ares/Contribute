import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/login_input_text.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'register_logic.dart';

class RegisterPage extends GetView<RegisterLogic> {
  const RegisterPage({Key? key}) : super(key: key);

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
                  preferredSize: Size.fromHeight(kToolbarHeight),
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
                  // const SizedBox(
                  //   height: 24,
                  // ),
                  // SvgPicture.asset(
                  //   Utils.getSvgPath('ic_nav_message'),
                  //   height: 80,
                  // ),
                  // Container(
                  //   margin: const EdgeInsets.all(24),
                  //   child: Text(
                  //     'app_name'.tr,
                  //     style: Get.textTheme.headline3,
                  //   ),
                  // ),
                  // BrnTextInputFormItem(
                  //   controller: controller.nameTextController,
                  //   maxCharCount: 11,
                  //   title: 'username'.tr,
                  //   hint: 'username_hint'.tr,
                  // ),
                  LoginInputText(
                    title: 'username'.tr,
                    hint: 'username_hint'.tr,
                    textEditingController: controller.nameTextController,
                    keyboardType: TextInputType.text,
                    maxLength: 11,
                  ),
                  BrnLine(),
                  const SizedBox(
                    height: 24,
                  ),
                  LoginInputText(
                    title: '昵称',
                    hint: '请输入昵称',
                    textEditingController: controller.nickTextController,
                    // suffix: _sendCodeWidget(),
                    keyboardType: TextInputType.text,
                    maxLength: 16,
                  ),
                  BrnLine(),

                  const SizedBox(
                    height: 24,
                  ),
                  LoginInputText(
                    title: '推荐码',
                    hint: '请输入推荐码',
                    textEditingController: controller.codeTextController,
                    // suffix: _sendCodeWidget(),
                    keyboardType: TextInputType.text,
                    maxLength: 6,
                  ),
                  BrnLine(),

                  const SizedBox(
                    height: 24,
                  ),
                  LoginInputText(
                      title: 'password'.tr,
                      hint: 'password_hint'.tr,
                      textEditingController: controller.pwdTextController,
                      keyboardType: TextInputType.visiblePassword,
                      obscureText: true),
                  BrnLine(),
                  // BrnTextInputFormItem(
                  //   controller: controller.pwdTextController,
                  //   inputType: BrnInputType.PWD,
                  //   title: 'password'.tr,
                  //   hint: 'password_hint'.tr,
                  // ),
                  const SizedBox(
                    height: 40,
                  ),
                  BrnBigMainButton(
                    //TODO
                    borderRadius: const BorderRadius.all(Radius.circular(24)),
                    title: 'register'.tr,
                    onTap: () => controller.register(),
                  ),
                ],
              ),
            )
          ],
        ),
      ),
    );
  }

  _sendCodeWidget() => Obx(() => BrnSmallMainButton(
        title: controller.sendBtnShow,
        isEnable: controller.canSend,
        fontSize: 12,
        fontWeight: FontWeight.normal,
        onTap: () => controller.sendVerifyCode(),
      ));
}
