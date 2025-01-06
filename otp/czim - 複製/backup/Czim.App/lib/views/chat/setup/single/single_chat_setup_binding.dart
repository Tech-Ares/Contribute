import 'package:get/get.dart';

import 'single_chat_setup_logic.dart';

class SingleChatSetupBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => SingleChatSetupLogic());
  }
}
