
import 'package:easy_chat/models/contact_model.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

abstract class IMGroupService {

  ///从服务器获取自己的群组
  Future<List<EMGroup>?> fetchGroups();

  ///从缓存中获取自己的群组
  Future<List<EMGroup>?> getGroups();

  ///创建群组
  Future<EMGroup?> createGroup(String groupName,
      {required EMGroupOptions settings,
        String desc = '',
        List<String>? inviteMembers,
        String inviteReason = ''});

  Future<EMGroup> fetchGroupInfo(String id);
}