import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/login_input_text.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'login_logic.dart';

class LoginPage extends GetView<LoginLogic> {
  const LoginPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      // appBar: BrnAppBar(
      //   leading: Container(),
      //     title: 'login'.tr),
      body: SingleChildScrollView(
        child: Column(
          children: [
            Image.asset(Utils.getImgPath('bg_login')),
            Container(
              width: double.infinity,
              margin: EdgeInsets.only(right: 16.w),
              alignment: Alignment.topRight,
              child: Image.asset(Utils.getImgPath('ic_logo'), width: 160.w,),
            ),
            Container(
              padding: const EdgeInsets.all(24),
              child: Column(
                children: [
                  // const SizedBox(height: 24,),
                  // SvgPicture.asset(Utils.getSvgPath('ic_nav_message'), height: 80,),
                  // Container(
                  //   margin: const EdgeInsets.all(24),
                  //   child: Text('login'.tr, style: Get.textTheme.headline3,),
                  // ),
                  LoginInputText(
                    title: 'username'.tr,
                    hint: 'username_hint'.tr,
                    textEditingController: controller.nameTextController,
                    keyboardType: TextInputType.text,
                    maxLength: 11,
                  ),
                  BrnLine(),
                  SizedBox(height: 24.h,),
                  LoginInputText(
                      title: 'password'.tr,
                      hint: 'password_hint'.tr,
                      textEditingController: controller.pwdTextController,
                      keyboardType: TextInputType.visiblePassword,
                      obscureText: true
                  ),
                  BrnLine(),
                  const SizedBox(height: 24,),
                  Row(
                    children: [
                      Expanded(child: BrnBigGhostButton(
                        //TODO
                        borderRadius: const BorderRadius.all(Radius.circular(24)),
                        title: 'register'.tr,
                        onTap: () => Get.toNamed(PageRoutes.register),
                      )),
                      const SizedBox(width: 24,),
                      Expanded(child: BrnBigMainButton(
                        //TODO
                        borderRadius: const BorderRadius.all(Radius.circular(24)),
                        title: 'login'.tr,
                        onTap: () => controller.login(context),
                      )),
                    ],
                  )
                ],
              ),
            )
          ],
        ),
      ),
    );
  }
}
