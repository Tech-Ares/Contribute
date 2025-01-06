import 'package:get/get.dart';

import 'setup_logic.dart';

class SetupBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => SetupLogic());
  }
}
