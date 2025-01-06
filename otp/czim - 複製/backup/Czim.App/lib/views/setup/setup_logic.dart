import 'package:bruno/bruno.dart';
import 'package:easy_chat/models/params/invite_code_params.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import '../../export.dart';
import 'setup_state.dart';

class SetupLogic extends GetxController {
  final SetupState state = SetupState();

  get cacheSize => state.cacheSize.value;

  set cacheSize(size) => state.cacheSize.value = size;

  @override
  void onInit() {
    _calcCacheSize();
    super.onInit();
  }

  void clearCache() {
    BrnDialogManager.showConfirmDialog(Get.context!,
        title: '是否清除缓存内容',
        // warning: '删除后无法恢复',
        confirm: '确认',
        cancel: '取消',
        onCancel: () => Get.back(),
        onConfirm: () async {
          _clearCache();
        });
  }

  Future? logout() async {
    BrnDialogManager.showConfirmDialog(Get.context!,
        title: 'logout_tip'.tr,
        cancel: 'cancel'.tr,
        confirm: 'logout'.tr,
        onCancel: () => Get.back(),
        onConfirm: () async {
          await imManager.imClientService.logout();
          Get.offAllNamed(PageRoutes.login);
        });
  }

  void _calcCacheSize() async {
    var _newSize = await CacheUtil.loadCache();
    cacheSize = _newSize;
  }

  void _clearCache() async {
    Get.back();
    try {
      BrnLoadingDialog.show(Get.context!, content: '清理中...');
      String? _newSize = await CacheUtil.clearCache(Get.context);
      if (_newSize != null) {
        cacheSize = _newSize;
      }
    } catch (e) {
      logger.e(e);
    } finally {
      BrnLoadingDialog.dismiss(Get.context!);
    }
  }

  fillInviteCode() async {
    BrnMiddleInputDialog(
        title: '填写邀请码',
        cancelText: '取消',
        confirmText: '确定',
        maxLength: 100,
        maxLines: 1,
        hintText: '请输入邀请人账号',
        barrierDismissible: false,
        inputEditingController: TextEditingController(),
        textInputAction: TextInputAction.done,
        onConfirm: (value) => _doFillInviteCode(value),
        onCancel: () {
          Get.back();
        }).show(Get.context!);
  }

  _doFillInviteCode(String value) async {
    await memberRestClient
        .fillInviteCode(InviteCodeParams(referralCode: value));
    Get.back();
  }
}
