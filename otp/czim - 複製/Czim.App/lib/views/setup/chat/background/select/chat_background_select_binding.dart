import 'package:get/get.dart';

import 'chat_background_select_logic.dart';

class ChatBackgroundSelectBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => ChatBackgroundSelectLogic());
  }
}
