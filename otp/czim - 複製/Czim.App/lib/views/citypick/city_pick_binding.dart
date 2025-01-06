import 'package:get/get.dart';

import 'city_pick_logic.dart';

class CityPickBinding extends Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => CityPickLogic());
  }
}
