import 'package:get/get.dart';

import 'generate_setup_logic.dart';

class GenerateSetupBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => GenerateSetupLogic());
  }
}
