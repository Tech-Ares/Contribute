import 'dart:io';

import 'package:bruno/bruno.dart';
import 'package:easy_chat/export.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/params/im_group_info_params.dart';
import 'package:easy_chat/models/params/scan_code_params.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'scan_state.dart';

class ScanLogic extends BaseController {
  final ScanState _state = ScanState();
  final GlobalKey qrKey = GlobalKey(debugLabel: 'QR');
  final GlobalController _globalController = Get.find();
  late QRViewController qrController;

  void onQRViewCreated(QRViewController qrController) {
    this.qrController = qrController;
    qrController.scannedDataStream.listen((scanData) {
      _state.barCode.value = scanData;
      if (_parseQRCode(scanData.code)) {
        qrController.pauseCamera();
      }
      // BrnDialogManager.showConfirmDialog(Get.context,
      //     title: '识别到二维码',
      //     message: scanData.code,
      //     cancel: '取消',
      //     confirm: '确认');
    });
  }

  @override
  void onPaused() {
    if (Platform.isAndroid) {
      qrController.pauseCamera();
    } else if (Platform.isIOS) {
      qrController.resumeCamera();
    }
  }

  bool _parseQRCode(String? code) {
    if (code == null) {
      return false;
    }
    logger.d('识别到二维码:$code');
    try {
      List<String> keyValue = code.split('?')[1].split('=');
      switch (keyValue[0]) {
        case 'memberId':
          BrnDialogManager.showConfirmDialog(Get.context!,
              title: '识别到用户信息，是否添加为好友 ？',
              cancel: '取消',
              confirm: '确定',
              onCancel: () => Get.back(),
              onConfirm: () => _doAddFriend(keyValue[1]));
          break;
        case 'groupId':
          BrnDialogManager.showConfirmDialog(Get.context!,
              title: '识别到群组信息，是否加入该群？',
              cancel: '取消',
              confirm: '确定',
              onCancel: () => Get.back(),
              onConfirm: () => _doJoinTeam(keyValue[1]));
          break;
        case 'roomId':
          BrnDialogManager.showConfirmDialog(Get.context!,
              title: '识别到聊天室信息，是否加入？',
              cancel: '取消',
              confirm: '确定',
              onCancel: () => Get.back(),
              onConfirm: () => _doJoinRoom(keyValue[1]));
          break;
      }
      // Get.offAndToNamed(PageRoutes.memberHome, arguments: memberId);
      return true;
    } catch (e) {
      qrController.resumeCamera();
      ToastUtil.error('无效二维码');
      return false;
    }
  }

  _doAddFriend(String value) => _realAction(value, 0);

  _doJoinTeam(String value) => _realAction(value, 1);

  _doJoinRoom(String value) => _realAction(value, 2);

  _realAction(String id, int type) async {
    try {
      Get.back();
      await memberRestClient
          .scanCode(ScanCodeParams(qrCode: id, czType: type));
      EMConversationType convType = EMConversationType.Chat;
      String name = '';
      switch (type) {
        case 0:
          convType = EMConversationType.Chat;
          var emUsers = await imManager.imUserService.fetchAllUsers([id]);
          await _globalController.fetchUserExt(emUsers);
          name = emUsers[0].nickName!;
          break;
        case 1:
          convType = EMConversationType.GroupChat;
          var group = await memberRestClient.imGroupInfo(IMGroupInfoParams(targetId: id));
          name = group.groupName ?? '';
          break;
        case 2:
          convType = EMConversationType.ChatRoom;
          break;
      }
      EMConversation? conversation = await EMClient.getInstance.chatManager
          .getConversation(id, convType);
      conversation?.name = name;
      Get.offAndToNamed(PageRoutes.chat, arguments: conversation);
    } catch (e) {
      qrController.resumeCamera();
      rethrow;
    }
  }
}
