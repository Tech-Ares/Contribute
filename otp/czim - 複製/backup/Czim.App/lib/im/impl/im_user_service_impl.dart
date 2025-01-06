import 'package:easy_chat/im/im_user_service.dart';
import 'package:easy_chat/models/contact_model.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

class IMUserServiceImpl extends IMUserService {
  @override
  Future<List<ContactModel>> fetchUserInfo(List<String> list) async {
    List<ContactModel> ret = [];
    Map<String, EMUserInfo> map = await EMClient.getInstance.userInfoManager
        .fetchUserInfoByIdWithExpireTime(list);
    List<String> emIds = map.keys.toList();
    List<String> noInfoIds = list.toList();
    noInfoIds.removeWhere((element) {
      return emIds.contains(element);
    });
    for (var emId in noInfoIds) {
      ret.add(
        ContactModel.fromUserId(emId),
      );
    }
    for (var info in map.values.toList()) {
      ret.add(
        ContactModel.fromUserInfo(info),
      );
    }
    return ret;
  }

  @override
  Future<List<EMUserInfo>> fetchAllUsers(List<String> list) async {
    Map<String, EMUserInfo> map = await EMClient.getInstance.userInfoManager
        .fetchUserInfoByIdWithExpireTime(list);
    return map.values.toList();
  }

  @override
  Future<EMUserInfo?> fetchUMemberInfo(String memberId) async {
    Map<String, EMUserInfo> map = await EMClient.getInstance.userInfoManager
        .fetchUserInfoByIdWithExpireTime([memberId], expireTime: 10);
    return map[memberId];
  }

  @override
  Future<EMUserInfo?> fetchOwnInfo() {
    return EMClient.getInstance.userInfoManager.fetchOwnInfo();
  }

  @override
  String? currentUsername() {
    return EMClient.getInstance.currentUsername;
  }

  @override
  Future<EMUserInfo?> updateOwnUserInfo(EMUserInfoType type, String userInfoValue) async {
    return EMClient.getInstance.userInfoManager.updateOwnUserInfoWithType(type, userInfoValue);
  }
}
