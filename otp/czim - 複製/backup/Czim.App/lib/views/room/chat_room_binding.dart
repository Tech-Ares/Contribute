import 'package:get/get.dart';

import 'chat_room_logic.dart';

class ChatRoomBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => ChatRoomLogic());
  }
}
