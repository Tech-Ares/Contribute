import 'package:get/get.dart';

import 'group_chat_setup_logic.dart';

class GroupChatSetupBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => GroupChatSetupLogic());
  }
}
