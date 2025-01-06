import 'package:easy_chat/export.dart';
import 'package:easy_chat/models/friend.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'blacklist_state.dart';

class BlacklistLogic extends BaseController<List<Friend>> {
  final BlacklistState _state = BlacklistState();

  @override
  Future<List<Friend>> Function() get requestFunc => () async {
        List<String?> blacklist =
            await EMClient.getInstance.contactManager.getBlockListFromServer();
        final List<Friend>? remoteUsers =
            await memberRestClient.fetchUsers(blacklist);
        Map<String?, Friend?> userMap = {};
        if (remoteUsers != null) {
          userMap = Map<String?, Friend?>.fromIterables(
              remoteUsers.map((e) => e.targetId), remoteUsers.map((e) => e));
        }
        return blacklist
            .map((e) => userMap[e] ?? Friend(e, null, e, '', 0, false, 0))
            .toList();
      };

  @override
  void onReady() {
    onRefresh();
    super.onReady();
  }
}
