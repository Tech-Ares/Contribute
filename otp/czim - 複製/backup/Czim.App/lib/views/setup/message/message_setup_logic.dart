import 'package:easy_chat/global/global_controller.dart';
import 'package:get/get.dart';

import 'message_setup_state.dart';

class MessageSetupLogic extends GetxController {
  final MessageSetupState _state = MessageSetupState();
  final GlobalController _globalController = GlobalController();

  get isReceiveNewMessage => _state.isReceiveNewMessage.value;

  get isShowMessageDetail => _state.isShowMessageDetail.value;

  get isVibration => _state.isVibration.value;

  get isSound => _state.isSound.value;

  onReceiveNewMessage(bool value) {
    _state.isReceiveNewMessage.toggle();
    _globalController.isGlobalReceive = value;
  }

  onShowMessageDetail(bool value) {
    _state.isShowMessageDetail.toggle();
  }

  onVibration(bool value) {
    _state.isVibration.toggle();
    _globalController.isGlobalVibrate = value;
  }

  onSound(bool value) {
    _state.isSound.toggle();
    _globalController.isGlobalNotify = value;
  }

  @override
  void onInit() {
    _state.isSound.value = _globalController.isGlobalNotify;
    _state.isVibration.value = _globalController.isGlobalVibrate;
    _state.isReceiveNewMessage.value = _globalController.isGlobalReceive;
    super.onInit();
  }
}
