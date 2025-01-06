import 'package:easy_chat/im/im_group_service.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

class IMGroupServiceImpl extends IMGroupService {
  @override
  Future<List<EMGroup>?> fetchGroups() async {
    try {
      List<EMGroup> groupsList =
          await EMClient.getInstance.groupManager.getJoinedGroupsFromServer();
      return groupsList;
    } on EMError catch (e) {
      logger.e('操作失败，原因是: $e');
    }
  }

  @override
  Future<List<EMGroup>?> getGroups() async {
    try {
      List<EMGroup> groupsList =
          await EMClient.getInstance.groupManager.getJoinedGroups();
      return groupsList;
    } on EMError catch (e) {
      logger.e('操作失败，原因是: $e');
    }
  }

  @override
  Future<EMGroup?> createGroup(String groupName,
      {required EMGroupOptions settings,
      String desc = '',
      List<String>? inviteMembers,
      String inviteReason = ''}) async {
    return await EMClient.getInstance.groupManager.createGroup(groupName,
        settings: settings,
        desc: desc,
        inviteMembers: inviteMembers,
        inviteReason: inviteReason);
  }

  @override
  Future<EMGroup> fetchGroupInfo(String id) {
    return EMClient.getInstance.groupManager
        .getGroupSpecificationFromServer(id);
  }
}
