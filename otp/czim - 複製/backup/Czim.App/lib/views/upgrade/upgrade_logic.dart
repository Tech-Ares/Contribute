import 'dart:developer' as dev;
import 'dart:io';

import 'package:easy_chat/models/upgrade_version.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:url_launcher/url_launcher.dart';

import 'upgrade_state.dart';

class UpgradeLogic extends GetxController {
  final state = UpgradeState();

  ///app下载后的文件名
  static const String _downloadApkName = 'app.apk';

  ///下载进度 0～1
  RxDouble percent = 0.0.obs;

  ///0：待下载
  ///1：正在下载
  ///2：下载完成
  ///3：下载失败
  RxInt downloadState = 0.obs;
  RxString apkFile = ''.obs;

  bool get showPercent => downloadState.value == 1;

  get showBtns => downloadState.value == 0;

  String get btnStr {
    switch (downloadState.value) {
      case 1:
        return 'downloading'.tr;
      case 2:
        return 'install'.tr;
      case 3:
        return 're_download'.tr;
      default:
        return GetPlatform.isIOS
            ? 'begin_download_ios'.tr
            : 'begin_download_android'.tr;
    }
  }

  get isDownloadProcess => !showPercent;

  bool get downloaded => downloadState.value == 2;

  upgradeApp(UpgradeVersion upgradeVersion) {
    if (GetPlatform.isAndroid) {
      if (downloadState.value == 0 || downloadState.value == 3) {
        _downloadApk(upgradeVersion.apkDownLoadUrl!);
      } else if (downloadState.value == 2) {
        _onClickInstallApk();
      }
    } else {
      if (upgradeVersion.iosLinkUrl != null) {
        launch(upgradeVersion.iosLinkUrl!);
        // AppInstaller.goStore(
        //     upgradeVersion.apkDownLoadUrl ?? '', upgradeVersion.iosLinkUrl!);
      }
    }
  }

  ///
  /// 下载apk包
  ///
  _downloadApk(String url) async {
    if (downloadState.value > 0) {
      dev.log('当前下载状态：${downloadState.value},不能重复下载。');
      return;
    }
    downloadState.value = 1;
    try {
      var dio = Dio();
      Directory tempDir = await getTemporaryDirectory();
      String filePath = '${tempDir.path}/$_downloadApkName';
      await dio.download(url, filePath,
          onReceiveProgress: (int count, int total) {
        if (total == -1) {
          percent.value = 0.0;
        } else {
          percent.value = count / total.toDouble();
        }
        if (percent.value >= 1) {
          downloadState.value = 2;
          apkFile.value = filePath;
          //下载完成，跳转到程序安装界面
          _onClickInstallApk();
        }
      });
    } catch (e) {
      e.printError();
      downloadState.value = 3;
    }
  }

  void _onClickInstallApk() async {
    String filePath = apkFile.value;
    if (filePath.isEmpty) {
      dev.log('make sure the apk file is set');
      return;
    }
    File file = File(filePath);
    if (!file.existsSync()) {
      dev.log('make sure the apk file is exists');
      return;
    }
    AppInstaller.installApk(filePath);
  }

  void _jumpToAppstore(String iosLinkUrl) => launch(iosLinkUrl);
}
