import 'package:easy_chat/views/nav/contact/nav_contact_logic.dart';
import 'package:easy_chat/views/nav/live/nav_live_logic.dart';
import 'package:easy_chat/views/nav/message/nav_message_logic.dart';
import 'package:easy_chat/views/nav/mine/nav_mine_logic.dart';
import 'package:get/get.dart';

import 'index_logic.dart';

class IndexBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => IndexLogic());
    Get.lazyPut(() => NavMessageLogic());
    Get.lazyPut(() => NavContactLogic());
    Get.lazyPut(() => NavLiveLogic());
    Get.lazyPut(() => NavMineLogic());
  }
}
