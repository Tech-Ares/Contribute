import 'package:bruno/bruno.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'city_pick_logic.dart';

class CityPickPage extends GetView<CityPickLogic> {
  const CityPickPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BrnSingleSelectCityPage(
      appBarTitle: '选择地区',
      hotCityList: const [],
      emptyImgRes: Image.asset(Utils.getImgPath('ic_placeholder_empty'), width: 200,),
      onValueChanged: (BrnSelectCityModel city) => Get.back(result: city),
    );
  }
}
