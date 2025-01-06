import 'package:easy_chat/models/authorization.dart';
import 'package:easy_chat/models/user_info.dart';
import 'package:get/get.dart';

class GlobalState {
  Rx<UserInfo?> userInfo = Rx<UserInfo?>(null);
  Rx<Authorization?> authorization = Rx<Authorization?>(null);
  ///0:连接中
  ///1：连接成功
  ///2：连接失败
  RxInt imConnectState = 0.obs;
  RxBool refreshContact = false.obs;
  RxBool refreshConvs = false.obs;
  RxString currentConv = ''.obs;
  RxBool appPause = false.obs;
}
