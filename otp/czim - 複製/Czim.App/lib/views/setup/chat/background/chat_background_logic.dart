import 'package:bruno/bruno.dart';
import 'package:easy_chat/common/image_processor.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/chat_background.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:easy_chat/views/chat/chat_logic.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'chat_background_state.dart';

class ChatBackgroundLogic extends GetxController {
  final ChatBackgroundState state = ChatBackgroundState();
  final GlobalController _globalController = Get.find();
  final String? sessionId = Get.arguments;

  selectFromGallery() => _selectImage(0);

  selectFromCamera() => _selectImage(1);

  void _selectImage(int type) async {
    List<AssetEntity>? assetEntities = await selectPhoto(type, size: 1);
    if (assetEntities == null || assetEntities.isEmpty) {
      logger.w('所选内容为空！');
      return;
    }
    var originFile = await assetEntities[0].originFile;
    if (!(originFile?.existsSync() ?? false)) {
      logger.w('图片不存在！');
      return;
    }
    _globalController.saveChatBackground(
        chatBackground:
            ChatBackground.file(originFile!.path, sessionId: sessionId));
    try {
      ChatLogic chatLogic = Get.find();
      chatLogic.reloadBackground();
    } catch (e) {
      logger.e('当前没有打开聊天页面，无需更新背景');
    }
  }

  toSelectBackground() =>
      Get.toNamed(PageRoutes.backgroundSelect, arguments: sessionId);

  setupAll() {
    BrnDialogManager.showConfirmDialog(Get.context!,
        title: '提示',
        cancel: '取消',
        confirm: '确定',
        message: '是否将选择的背景应用到所有的聊天场景？', onConfirm: () {
      _globalController.setupAllChatBackground();
      Get.back();
    }, onCancel: () {
      Get.back();
    });
  }
}
