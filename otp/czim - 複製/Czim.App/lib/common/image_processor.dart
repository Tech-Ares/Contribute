import 'dart:io';

import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:path_provider/path_provider.dart';
import 'package:wechat_assets_picker/wechat_assets_picker.dart';
import 'package:wechat_camera_picker/wechat_camera_picker.dart';

Future<List<AssetEntity>?> selectWechatImage(
        {int size = 1,
        List<AssetEntity>? selectedAssets,
        bool enableRecording = false}) =>
    AssetPicker.pickAssets(Get.context!,
        maxAssets: size,
        selectedAssets: selectedAssets,
        requestType: enableRecording ? RequestType.common : RequestType.image);

Future<AssetEntity?> selectWechatCamera() =>
    CameraPicker.pickFromCamera(Get.context!);

Future<File> compressAndGetFile(File? file) async {
  if (file == null || !file.existsSync()) {
    logger.w('所选图片不存在');
    throw ApiError(statusMessage: '所选图片不存在');
  }
  final dir = await getTemporaryDirectory();
  final targetPath = dir.absolute.path + '/.${StringUtils.randomUUID()}.jpg';
  final result = await FlutterImageCompress.compressAndGetFile(
    file.absolute.path,
    targetPath,
    quality: 80,
    minWidth: 480,
    minHeight: 480,
  );
  if (result == null) {
    logger.w('压缩图片失败！');
    throw ApiError(statusMessage: '压缩图片失败');
  }
  logger.d(
      '压缩前文件大小 -> 「${file.lengthSync()}」,压缩后文件大小 -> 「${result.lengthSync()}」');
  return result;
}

Future<List<AssetEntity>?> selectPhoto(int type,
    {int size = 1,
    List<AssetEntity>? selectedAssets,
    bool enableRecording = false}) async {
  List<AssetEntity>? pickedFiles;
  if (type == 0) {
    pickedFiles = await selectWechatImage(
        size: size,
        selectedAssets: selectedAssets,
        enableRecording: enableRecording);
  } else {
    AssetEntity? assetEntity = await CameraPicker.pickFromCamera(Get.context!,
        enableRecording: enableRecording);
    if (assetEntity == null) {
      return null;
    }
    pickedFiles = <AssetEntity>[assetEntity];
  }
  return pickedFiles;
}
