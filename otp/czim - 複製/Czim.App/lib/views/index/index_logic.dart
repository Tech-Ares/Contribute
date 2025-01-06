import 'dart:ui';

import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/views/upgrade/upgrade_view.dart';
import 'package:flutter/services.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:huawei_push/huawei_push.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';
import 'package:persistent_bottom_nav_bar/persistent-tab-view.dart';

class IndexLogic extends BaseController {
  final _globalLogic = Get.find<GlobalController>();
  final PersistentTabController tabController = PersistentTabController();
  final RxBool isMember = false.obs;
  final RxInt currentIndex = 0.obs;
  final RxString hwPushToken = ''.obs;

  String get unreadMessageCount => '${_globalLogic.unreadMessageCount}';

  get showUnreadCount => _globalLogic.unreadMessageCount > 0;

  VoidCallback? listener;

  @override
  Future onInit() async {
    listener = () {
      currentIndex.value = tabController.index;
      _globalLogic.pageIndex.value = tabController.index;
    };
    tabController.addListener(listener!);
    await initTokenStream();
    super.onInit();
  }

  Future<bool> back() async {
    return true;
  }

  @override
  void onReady() {
    try {
      // checkUpgrade();
    } catch (e) {
      logger.e('$e');
    }
    // _globalLogic.joinAllRooms();
    super.onReady();
  }

  @override
  void onClose() async {
    if (listener != null) {
      tabController.removeListener(listener!);
    }
    await _globalLogic.leaveAllRooms();
    super.onClose();
  }

  @override
  void onPaused() {

    super.onPaused();
  }

  Future<void> initTokenStream() async {
    Push.getTokenStream.listen(_onTokenEvent, onError: _onTokenError);
    await Push.getToken('');
  }

  void _onTokenEvent(String event) {
    // Requested tokens can be obtained here
    hwPushToken.value = event;
    print('TokenEvent: $event');
    // ToastUtil.show(event);
    EMClient.getInstance.pushManager.updateHMSPushToken(event);
  }

  void _onTokenError(Object error) {
    print('TokenErrorEvent: $error');
    // ToastUtil.show('${error}');
  }
}
