import 'package:easy_chat/models/upgrade_params.dart';
import 'package:easy_chat/models/upgrade_version.dart';
import 'package:flutter/material.dart';
import 'package:flutter_beautiful_popup/main.dart';
import 'package:getwidget/getwidget.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import '../../export.dart';
import 'upgrade_logic.dart';

Future checkUpgrade({bool showMsg = false}) async {
  logger.i('开始获取最新版本信息');
  final PackageInfo packageInfo = await PackageInfo.fromPlatform();
  int system = 0;
  if (GetPlatform.isAndroid) {
    system = 1;
  } else if (GetPlatform.isIOS) {
    system = 2;
  }
  final int code = int.parse(packageInfo.buildNumber);
  try {
    final upgradeVersion = await commonRestClient.upgrade(UpgradeParams(
      phoneSystem: system,
      code: code,
      version: packageInfo.version,
    ));
    // final upgradeVersion = UpgradeVersion.test();
    fetchUpgradeDialog(upgradeVersion, showMsg: showMsg);
  } finally {}
}

/// app版本更新弹窗
fetchUpgradeDialog(UpgradeVersion? upgradeVersion,
    {bool showMsg = false}) async {
  if (GetPlatform.isAndroid || GetPlatform.isIOS) {
    PackageInfo packageInfo = await PackageInfo.fromPlatform();
    if (upgradeVersion != null &&
        upgradeVersion.shouldUpgrade(packageInfo.version)) {
      final BeautifulPopup pop =
          BeautifulPopup(context: Get.context!, template: TemplateGreenRocket);
      Get.put(UpgradeLogic());
      pop.show(
          title: '发现新版本',
          content: Text(
            upgradeVersion.content ?? '暂无更新内容',
            style: Get.textTheme.bodyText2,
          ),
          barrierDismissible: false,
          close: const Text(''),
          actions: [_UpgradeWidget(pop, upgradeVersion)]);
    } else {
      if (showMsg) {
        ToastUtil.error('当前已是最新版本了');
      }
    }
  }
}

class _UpgradeWidget extends GetView<UpgradeLogic> {
  final BeautifulPopup pop;
  final UpgradeVersion upgradeVersion;

  const _UpgradeWidget(this.pop, this.upgradeVersion, {Key? key})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Obx(() => Column(
          children: [
            Visibility(
                visible: controller.showPercent,
                child: GFProgressBar(
                  percentage: controller.percent.value,
                  alignment: MainAxisAlignment.center,
                  trailing: Text(
                    '${(controller.percent.value * 100).toStringAsFixed(1)}%',
                    textAlign: TextAlign.end,
                    style: Get.textTheme.bodyText2?.copyWith(fontSize: 10),
                  ),
                  backgroundColor: Colors.black12,
                  progressBarColor: const Color(0xff49ceae),
                )),
            Visibility(
                visible: !controller.showPercent,
                child: Row(
                  children: [
                    Visibility(
                      visible: !(upgradeVersion.focus ?? false) &&
                          !controller.downloaded,
                      child: Expanded(
                        child: pop.button(
                            label: 'ignore_version'.tr,
                            flat: true,
                            labelStyle: Get.textTheme.bodyText2!,
                            onPressed: () => Get.back()),
                      ),
                    ),
                    Visibility(
                        visible: !controller.showPercent,
                        child: Expanded(
                            child: pop.button(
                                label: controller.btnStr,
                                onPressed: () =>
                                    controller.upgradeApp(upgradeVersion))))
                  ],
                )),
          ],
        ));
  }
}
