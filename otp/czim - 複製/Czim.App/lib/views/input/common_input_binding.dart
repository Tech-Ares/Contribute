import 'package:get/get.dart';

import 'common_input_logic.dart';

class CommonInputBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => CommonInputLogic());
  }
}
