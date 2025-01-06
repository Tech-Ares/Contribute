import 'package:get/get.dart';

import 'chat_room_setup_logic.dart';

class ChatRoomSetupBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => ChatRoomSetupLogic());
  }
}
