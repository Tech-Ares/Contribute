import 'package:get/get.dart';

import 'member_apply_logic.dart';

class MemberApplyBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => MemberApplyLogic());
  }
}
