import 'package:easy_chat/models/contact_model.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

abstract class IMUserService {
  ///获取用户信息
  Future<List<ContactModel>> fetchUserInfo(List<String> list);

  Future<List<EMUserInfo>> fetchAllUsers(List<String> list);

  Future<EMUserInfo?> fetchUMemberInfo(String memberId);

  ///获取当前登录的用户信息
  Future<EMUserInfo?> fetchOwnInfo();

  ///获取当前登录的用户名
  String? currentUsername();

  ///更新用户信息
  Future<EMUserInfo?> updateOwnUserInfo(
      EMUserInfoType type, String userInfoValue);
}
