import 'package:easy_chat/config/global_config.dart';
import 'package:easy_chat/models/authorization.dart';
import 'package:easy_chat/models/user_info.dart';
import 'package:get_storage/get_storage.dart';

class GlobalPref {
  static final GetStorage Function() _globalBox = () => GetStorage();

  final ReadWriteValue authorization = ReadWriteValue(
    GlobalConfig.storageAuthorization,
    Authorization.empty().toJson(),
    _globalBox,
  );

  final ReadWriteValue userinfo = ReadWriteValue(
    GlobalConfig.storageUserinfo,
    UserInfo.empty().toJson(),
    _globalBox,
  );

  final ReadWriteValue<bool> performance =
      ReadWriteValue<bool>(GlobalConfig.performance, false, _globalBox);

  final ReadWriteValue<List<dynamic>> searchHistory =
      ReadWriteValue<List<dynamic>>(GlobalConfig.searchHistory, [], _globalBox);

  ReadWriteValue<List<String>> notifies(String? user) =>
      ReadWriteValue<List<String>>(
          '${GlobalConfig.notifies}', <String>[], _globalBox);

  ReadWriteValue<bool> globalReceive(String? user) =>
      ReadWriteValue<bool>('${GlobalConfig.globalReceive}', true, _globalBox);

  ReadWriteValue<bool> globalNotify(String? user) =>
      ReadWriteValue<bool>('${GlobalConfig.globalNotify}', true, _globalBox);

  ReadWriteValue<bool> globalVibrate(String? user) =>
      ReadWriteValue<bool>('${GlobalConfig.globalVibrate}', true, _globalBox);

  ReadWriteValue<bool> firstLogin(String? user) =>
      ReadWriteValue<bool>('${GlobalConfig.first}_$user', true, _globalBox);

  ReadWriteValue<int> fire(String? user, String? id) =>
      ReadWriteValue<int>('${GlobalConfig.fire}_${user}_$id', 0, _globalBox);

  ReadWriteValue<int> generateFontSize(String? user) => ReadWriteValue<int>(
      '${GlobalConfig.generateFontSize}_$user', 14, _globalBox);

  ReadWriteValue<List<dynamic>> chatBackground(String? user) =>
      ReadWriteValue<List<dynamic>>(
          '${GlobalConfig.chatBackground}_$user', [], _globalBox);

  ReadWriteValue<Map<String, bool>> atConversation(
          String? convId, String userId) =>
      ReadWriteValue<Map<String, bool>>(
          '${GlobalConfig.atConversation}_$userId', {'$convId': false}, _globalBox);

  ReadWriteValue<String?> imToken =
      ReadWriteValue<String?>(
          GlobalConfig.imToken, null, _globalBox);

  void clearSearch() => _globalBox().remove(GlobalConfig.searchHistory);

  Future<void> clear() => _globalBox().erase();
}
