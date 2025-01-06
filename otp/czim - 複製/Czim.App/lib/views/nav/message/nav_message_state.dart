import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

class NavMessageState {

  RxList<EMConversation> conversations = RxList.empty();
  RxBool newMessageReceived = false.obs;
  RxBool newMessageVibrate = false.obs;
}
