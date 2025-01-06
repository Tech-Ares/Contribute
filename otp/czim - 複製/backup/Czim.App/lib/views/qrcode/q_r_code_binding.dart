import 'package:get/get.dart';

import 'q_r_code_logic.dart';

class QRCodeBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => QRCodeLogic());
  }
}
