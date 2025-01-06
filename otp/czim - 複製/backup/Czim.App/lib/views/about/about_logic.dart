import 'package:easy_chat/views/upgrade/upgrade_view.dart';
import 'package:get/get.dart';
import 'package:gogoboom_flutter_getx_start/utils/version_util.dart';

import 'about_state.dart';

class AboutLogic extends GetxController {
  final AboutState _state = AboutState();

  String get foundation => _state.foundation.value;
  set foundation(f) => _state.foundation.value = f;

  checkVersion() => checkUpgrade(showMsg: true);

  _initFoundation () async => foundation = await EiaVersion.getFoundation();

  @override
  void onInit() {
    _initFoundation();
    super.onInit();
  }
}
