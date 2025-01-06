import 'package:get/get.dart';

import 'my_groups_logic.dart';

class MyGroupsBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => MyGroupsLogic());
  }
}
