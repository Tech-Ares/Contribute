import 'package:get/get.dart';

import 'member_search_logic.dart';

class MemberSearchBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => MemberSearchLogic());
  }
}
