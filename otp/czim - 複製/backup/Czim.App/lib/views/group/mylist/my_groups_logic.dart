import 'package:easy_chat/export.dart';
import 'package:easy_chat/models/im_group.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'my_groups_state.dart';

class MyGroupsLogic extends BaseController<List<IMGroup>?> {
  final MyGroupsState _state = MyGroupsState();

  @override
  Future<List<IMGroup>?> Function() get requestFunc =>
      () => memberRestClient.groups();

  @override
  void onReady() {
    onRefresh();
    super.onReady();
  }

  openConv(int index) async {
    IMGroup group = indexOfItem(index);
    EMConversation? conv = await EMClient.getInstance.chatManager
        .getConversation(group.targetId!, EMConversationType.GroupChat);
    conv?.name = group.groupName ?? '';
    logger.d('EMConversation ==> ${conv?.toJson()}');
    Get.toNamed(PageRoutes.chat, arguments: conv);
  }


}
