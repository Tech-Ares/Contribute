import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

class GroupChatSetupState {

  RxList<EMUserInfo> groupMembers = RxList.empty();
  RxList<String> memberIds = RxList.empty();
  RxBool isTop = false.obs;
  RxBool isMute = false.obs;
}
