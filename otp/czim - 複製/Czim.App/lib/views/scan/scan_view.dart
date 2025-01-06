import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/views/scan/scan_logic.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

class ScanPage extends GetView<ScanLogic> {
  const ScanPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    var scanArea = (MediaQuery.of(context).size.width < 400 ||
        MediaQuery.of(context).size.height < 400)
        ? 150.0
        : 300.0;
    return BaseScaffold(
      appBar: BrnAppBar(
        title: 'scan'.tr,
      ),
      body: QRView(
        key: controller.qrKey,
        onQRViewCreated: controller.onQRViewCreated,
        overlay: QrScannerOverlayShape(
            borderColor: Colors.red,
            borderRadius: 10,
            borderLength: 30,
            borderWidth: 10,
            cutOutSize: scanArea),
        onPermissionSet: (ctrl, p) => _onPermissionSet(context, ctrl, p),
      ),
    );
  }

  void _onPermissionSet(BuildContext context, QRViewController ctrl, bool p) {
    logger.d('${DateTime.now().toIso8601String()}_onPermissionSet $p');
    if (!p) {
      ToastUtil.error('没有相机权限');
    }
  }


}
