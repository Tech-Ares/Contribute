import 'dart:typed_data';

import 'package:bruno/bruno.dart';
import 'package:easy_chat/config/global_config.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/im_group_info.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:image_gallery_saver/image_gallery_saver.dart';

import 'q_r_code_state.dart';

class QRCodeLogic extends GetxController {
  final QRCodeState state = QRCodeState();
  final GlobalController _globalController = Get.find();
  final GlobalKey paintKey = GlobalKey();
  final IMGroupInfo? group = Get.arguments;

  get getQrCode =>
      '${GlobalConfig.qrCode}?${isGroup ? 'groupId=${group!.easeChatgroupId}' : 'memberId=${_globalController.userId}'}';

  String get desc => isGroup ? '快来加群一起畅聊吧' : '我在使用XChat，扫我加好友';

  get nickname =>
      isGroup ? group!.groupName : _globalController.userInfo?.showName;

  String? get avatar =>
      isGroup ? group!.groupImg : _globalController.userInfo?.avatar;

  get isGroup => state.isGroup.value;

  Future _createImage() async {
    try {
      BrnLoadingDialog.show(Get.context!);
      final Uint8List? data = await _globalController.capturePng(paintKey);
      if (data != null) {
        // final tempDir = await getTemporaryDirectory();
        // final file = await File('${tempDir.path}/invite.jpg').create();
        // file.writeAsBytesSync(data);
        final result = await ImageGallerySaver.saveImage(
            Uint8List.fromList(data),
            quality: 80,
            name: '我的名片');
        ToastUtil.show('已保存到相册');
      }
    } catch (e) {
      logger.e(e);
      ToastUtil.error('$e');
    } finally {
      BrnLoadingDialog.dismiss(Get.context!);
    }
  }

  saveImage() => _createImage();

  @override
  void onInit() {
    state.isGroup.value = group != null;
    super.onInit();
  }
}
