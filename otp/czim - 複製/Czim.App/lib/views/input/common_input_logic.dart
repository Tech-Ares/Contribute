import 'package:bruno/bruno.dart';
import 'package:easy_chat/export.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/params/profile_params.dart';
import 'package:easy_chat/models/user_info.dart';
import 'package:flutter/cupertino.dart';
import 'package:get/get.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'common_input_state.dart';

class CommonInputLogic extends GetxController {
  final CommonInputState state = CommonInputState();
  final GlobalController _globalController = Get.find();
  final String? value = Get.arguments['value'];
  final CommonInputType type = Get.arguments['type'];

  final textEditingController = TextEditingController();
  late CommonInputProp prop;

  UserInfo? get userInfo => _globalController.userInfo;

  get maxCount => prop.maxCount;

  getTitle() => prop.title;

  getHint() => prop.hint;

  CommonInputProp _getCommonInputProp() {
    switch (type) {
      case CommonInputType.nickname:
        return CommonInputProp('修改昵称', '请输入昵称', 16, CommonInputType.nickname);
      case CommonInputType.signature:
        return CommonInputProp(
            '个性签名', '请输入个性签名', 16, CommonInputType.signature);
    }
  }

  @override
  void onInit() {
    prop = _getCommonInputProp();
    textEditingController.text = value ?? '';
    super.onInit();
  }

  Future? commit() async {
    String newValue = textEditingController.text.trim();
    BrnLoadingDialog.show(Get.context!);
    switch (prop.type) {
      case CommonInputType.nickname:
        await memberRestClient.update(ProfileParams(
          gender: userInfo?.gender,
          nickName: newValue,
          avatar: userInfo?.avatar,
          introduce: userInfo?.introduce,
        ));
        _globalController.refreshUserInfo();

        // EMUserInfo? newUser = await imManager.imUserService
        //     .updateOwnUserInfo(EMUserInfoType.NickName, newValue);
        // _globalController.emUserUpdate(newUser);
        break;
      case CommonInputType.signature:
        // EMUserInfo? newUser = await imManager.imUserService
        //     .updateOwnUserInfo(EMUserInfoType.Sign, newValue);
        // _globalController.emUserUpdate(newUser);
        await memberRestClient.update(ProfileParams(
          gender: userInfo?.gender,
          nickName: userInfo?.nickName,
          avatar: userInfo?.avatar,
          introduce: newValue,
        ));
        _globalController.refreshUserInfo();
        break;
    }
    BrnLoadingDialog.dismiss(Get.context!);
    Get.back();
  }
}

class CommonInputProp {
  final String title;
  final String hint;
  final int maxCount;
  final CommonInputType type;

  CommonInputProp(this.title, this.hint, this.maxCount, this.type);
}

enum CommonInputType {
  nickname,
  signature,
}
