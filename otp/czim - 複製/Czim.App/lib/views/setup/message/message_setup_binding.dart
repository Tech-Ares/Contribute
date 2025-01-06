import 'package:get/get.dart';

import 'message_setup_logic.dart';

class MessageSetupBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => MessageSetupLogic());
  }
}
