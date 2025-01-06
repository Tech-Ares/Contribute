import 'dart:async';
import 'dart:convert';
import 'dart:ffi';
import 'dart:typed_data';
import 'dart:ui';

import 'package:awesome_notifications/awesome_notifications.dart';
import 'package:easy_chat/export.dart';
import 'package:easy_chat/models/authorization.dart';
import 'package:easy_chat/models/chat_background.dart';
import 'package:easy_chat/models/em_user_ext.dart';
import 'package:easy_chat/models/friend.dart';
import 'package:easy_chat/models/params/time_info_params.dart';
import 'package:easy_chat/models/user_info.dart';
import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:get_storage/get_storage.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'global_pref.dart';
import 'global_state.dart';

class GlobalController extends GetxController implements EMConnectionListener {
  final GlobalState _state = GlobalState();
  final GlobalPref _globalPref = Get.put<GlobalPref>(GlobalPref());
  final List<Function(UserInfo userInfo)> _userChangedListeners =
      <Function(UserInfo)>[];
  final Rx<EMUserInfo?> _emUserInfo = Rx(null);
  final RxInt unreadMessageCount = 0.obs;

  ///用来缓存用户ext信息
  final Map<String, Friend> userExtCache = {};

  ///首页下标
  final RxInt pageIndex = 0.obs;

  UserInfo? get userInfo => _state.userInfo.value;

  EMUserInfo? get emUserInfo => _emUserInfo.value;

  RxBool get refreshContact => _state.refreshContact;

  RxBool get refreshConvs => _state.refreshConvs;

  bool get isFirstLogin => _globalPref.firstLogin(userId).val;

  int get generateFontSize => _globalPref.generateFontSize(userId).val;

  String? get imToken => _globalPref.imToken.val;

  set imToken(String? token) => _globalPref.imToken.val = token;

  set generateFontSize(int value) =>
      _globalPref.generateFontSize(userId).val = value;

  set isFirstLogin(value) => _globalPref.firstLogin(userId).val;

  set emUserInfo(user) => _emUserInfo.value = user;

  get username => emUserInfo?.nickName ?? emUserInfo?.userId;

  get userId => emUserInfo?.userId;

  set userInfo(UserInfo? userinfo) => _state.userInfo.value = userinfo;

  int get imConnectState => _state.imConnectState.value;

  set imConnectState(s) => _state.imConnectState.value = s;

  bool get isAppPause => _state.appPause.value;

  set isAppPause(bool pause) => _state.appPause.value = pause;

  EmUserExt? userExt() {
    if (emUserInfo?.ext == null) {
      return null;
    }
    try {
      return EmUserExt.fromJson(jsonDecode(emUserInfo!.ext!));
    } catch (e) {
      logger.e('emUserExt解析失败: $e');
    }
    return null;
  }

  Authorization? get authorization => _state.authorization.value;

  set authorization(Authorization? authorization) =>
      _state.authorization.value = authorization;

  bool get isLogin => _state.userInfo.value != null;

  void addUserChangedListener(Function(UserInfo) listener) =>
      _userChangedListeners.add(listener);

  Future<Void?> loginSuccess(
      Authorization? authorization, UserInfo userInfo) async {
    this.userInfo = userInfo;
    this.authorization = authorization ?? this.authorization;
    if (authorization != null) {
      _globalPref.authorization.val = authorization.toJson();
      logger.d('loginSuccess Authorization: ${authorization.toJson()}');
    }
    _globalPref.userinfo.val = userInfo.toJson();
    EMClient.getInstance.pushManager
        .updatePushNickname(userInfo.nickName ?? userInfo.id ?? '');
    logger.d('loginSuccess UserInfo: ${userInfo.toJson()}');
  }

  Future<Void?> updateUserInfo(UserInfo userInfo) async {
    this.userInfo = userInfo;
    _globalPref.userinfo.val = userInfo.toJson();
    logger.d('updateUserInfo UserInfo: ${userInfo.toJson()}');
  }

  @override
  Future<Void?> onInit() async {
    await GetStorage.init();
    loadUserFromLocal();

    imConnectState = EMClient.getInstance.connected ? 1 : 2;
    logger
        .d('EMClient.getInstance.connected: ${EMClient.getInstance.connected}');
    // 添加连接监听
    EMClient.getInstance.addConnectionListener(this);
    _initNotifications();
    super.onInit();
  }

  void emUserUpdate(EMUserInfo? emUserInfo) {
    this.emUserInfo = emUserInfo;
  }

  ///本地读取登录的用户数据
  void loadUserFromLocal() {
    try {
      userInfo = UserInfo.fromJson(_globalPref.userinfo.val);
      authorization = Authorization.fromJson(_globalPref.authorization.val);
      logger.d('读取本地用户信息成功: ${userInfo?.toJson()} -${authorization?.toJson()}');
    } catch (e) {
      logger.e(e);
      logger.w('本地用户信息解析失败，已置为未登录状态');
    }
  }

  void refreshContactAction() => refreshContact.toggle();

  bool hasSetupPerformance() => _globalPref.performance.val;

  void setupPerformance() => _globalPref.performance.val = true;

  Future<String?> getMachineCode() => DeviceUtil.getMachineCode();

  Future<TimeInfoParams> getTimeParams() async {
    final DeviceInfoPlugin deviceInfo = DeviceInfoPlugin();
    String machineCode = uuid.v4();
    late String model;
    late String os;
    if (GetPlatform.isAndroid) {
      final AndroidDeviceInfo androidInfo = await deviceInfo.androidInfo;
      machineCode = androidInfo.androidId ?? 'Unknown';
      model = androidInfo.model ?? 'Unknown';
      os = androidInfo.brand ?? 'Unknown';
    } else if (GetPlatform.isIOS) {
      final IosDeviceInfo iosInfo = await deviceInfo.iosInfo;
      machineCode = iosInfo.identifierForVendor ?? 'Unknown';
      model = iosInfo.model ?? 'Unknown';
      os = iosInfo.utsname.sysname ?? 'Unknown';
    }
    return TimeInfoParams(meId: machineCode, loginModel: model, loginOS: os);
  }

  Future? refreshUserInfo() async {
    UserInfo userInfo = await memberRestClient.userinfo();
    updateUserInfo(userInfo);
  }

  Future<Uint8List?> capturePng(GlobalKey paintKey) async {
    try {
      final RenderRepaintBoundary? boundary =
          paintKey.currentContext!.findRenderObject() as RenderRepaintBoundary?;
      final image = await boundary!.toImage(pixelRatio: 3.0);
      final ByteData? byteData =
          await image.toByteData(format: ImageByteFormat.png);
      return byteData!.buffer.asUint8List();
    } catch (e) {
      logger.e(e);
    }
    return null;
  }

  Future? fetchUserExt(List<EMUserInfo> members) async {
    if (members.isNotEmpty) {
      final unGetUserExts = <String>[];
      for (var m in members) {
        Friend? ext = userExtCache[m.userId];
        if (ext == null) {
          //说明缓存没有
          unGetUserExts.add(m.userId);
        } else {
          m.nickName = ext.nickName;
          m.avatarUrl = ext.avatar;
        }
      }
      List<Friend>? list;
      if (unGetUserExts.isNotEmpty) {
        list = await memberRestClient.fetchUsers(unGetUserExts);
      } else {
        list = <Friend>[];
      }
      logger.d('unGetUserExts: $unGetUserExts');
      final newValue = <String, Friend>{};
      list?.forEach((element) {
        newValue[element.targetId!] = element;
        for (var m in members) {
          if (m.userId == element.targetId) {
            m.nickName = element.nickName;
            m.avatarUrl = element.avatar;
          }
        }
      });
      userExtCache.addAll(newValue);
      logger.d('当前缓存的数据：${userExtCache}');
    }
  }

  Future? joinAllRooms() async {
    var rooms = await memberRestClient.chatRooms();
    rooms?.forEach((element) async {
      await EMClient.getInstance.chatRoomManager
          .joinChatRoom(element.targetId!);
      var conversation = await EMClient.getInstance.chatManager
          .getConversation(element.targetId!, EMConversationType.ChatRoom);
      await imManager.imChatService.loadMessages(conversation!);
      logger.d('加入聊天室${element.targetId}成功');
      refreshConvs.toggle();
    });
  }

  Future? leaveAllRooms() async {
    var rooms = await memberRestClient.chatRooms();
    rooms?.forEach((element) async {
      await EMClient.getInstance.chatRoomManager
          .leaveChatRoom(element.targetId!);
      logger.d('退出聊天室${element.targetId}成功');
    });
  }

  @override
  void onClose() {
    // 移除连接监听
    EMClient.getInstance.removeConnectionListener(this);
    super.onClose();
    super.onClose();
  }

  @override
  void onConnected() {
    imConnectState = 1;
    logger.d('IM Connected----');
  }

  @override
  void onDisconnected(int? errorCode) {
    imConnectState = 2;
    logger.d('IM Disconnected----errorCode: $errorCode');
  }

  bool checkMessageNotify(List<EMMessage> messages,
      {bool ignoreGlobal = true}) {
    //检查消息是全局否免打扰
    if (ignoreGlobal) {
      if (!isGlobalNotify) return false;
    }
    for (var element in messages) {
      String id = element.conversationId ?? '';
      if (isConvNotify(id) || _state.currentConv.value == id) {
        return false;
      }
    }
    return true;
  }

  bool checkMessageReceive() => isGlobalReceive;

  bool checkConvNotify(EMConversation conv) => isConvNotify(conv.id);

  ///是否全局声音
  bool get isGlobalNotify {
    logger.d('isGlobalNotify?.get ==> ${userInfo?.toJson()}');
    return _globalPref.globalNotify(userInfo?.memberId).val;
  }

  ///是否全局收消息
  bool get isGlobalReceive {
    return _globalPref.globalReceive(userInfo?.memberId).val;
  }

  ///是否全局收消息
  set isGlobalReceive(bool value) {
    _globalPref.globalReceive(userInfo?.memberId).val = value;
  }

  ///设置全局收消息开关
  set isGlobalNotify(bool value) {
    logger.d('isGlobalNotify?.set ==> ${userInfo?.toJson()}');
    _globalPref.globalNotify(userInfo?.memberId).val = value;
  }

  ///是否全局振动
  bool get isGlobalVibrate => _globalPref.globalVibrate(userInfo?.memberId).val;

  ///设置全局振动
  set isGlobalVibrate(bool value) =>
      _globalPref.globalVibrate(userInfo?.memberId).val = value;

  ///免到扰列表
  List<String> get notifies => _globalPref.notifies(userInfo?.memberId).val;

  ///是否免打扰
  bool isConvNotify(String id) {
    var muteList = <String>[];
    try {
      muteList = _globalPref.notifies(userInfo?.memberId).val;
    } catch (e) {
      // logger.e(e);
    }
    logger.d('muteList ==> $muteList');
    return muteList.contains(id);
  }

  void changeNotify(String id, bool value) {
    logger.d('changeNotify ==> ${id}_$value');
    value ? _addConvNotify(id) : _removeConvNotify(id);
  }

  ///设置为免打扰
  void _addConvNotify(String id) {
    var list = <String>[];
    try {
      list = _globalPref.notifies(userInfo?.memberId).val;
    } catch (e) {
      logger.e(e);
    }
    list.add(id);
    _globalPref.notifies(userInfo?.memberId).val = list;
  }

  ///取消免打扰
  void _removeConvNotify(String id) {
    var list = _globalPref.notifies(userInfo?.memberId).val;
    list.remove(id);
    _globalPref.notifies(userInfo?.memberId).val = list;
  }

  openConv(String id) => _state.currentConv.value = id;

  closeConv(String id) => _state.currentConv.value = '';

  void saveFire(String id, int index) {
    _globalPref.fire(userInfo?.id, id).val = index;
  }

  int getFire(String id) {
    return _globalPref.fire(userInfo?.id, id).val;
  }

  ///获取本地保存的聊天背景数据
  ChatBackground loadChatBackground({String? sessionId}) {
    List<dynamic> list = _globalPref.chatBackground(userId).val;
    if (list.isEmpty) {
      return ChatBackground.empty();
    }
    try {
      return list
          .map((e) => ChatBackground.fromJson(e))
          .firstWhere((element) => element.sessionId == sessionId);
    } catch (e) {
      logger.e(e);
      if (sessionId != null) {
        return loadChatBackground();
      }
      return ChatBackground.empty();
    }
  }

  ///保存聊天背景数据
  void saveChatBackground({required ChatBackground chatBackground}) {
    List<dynamic> list = _globalPref.chatBackground(userId).val;
    logger.d('chatBackground: ${chatBackground.toJson()}');
    logger.d('list before: $list');
    list.removeWhere(
        (element) => element['sessionId'] == chatBackground.sessionId);
    // list.map((e) => ChatBackground.fromJson(e)).toList().removeWhere(
    //     (element) =>
    //         element.sessionId == sessionId ||
    //         (sessionId == null && element.sessionId == null));
    list.add(chatBackground.toJson());
    logger.d('list after: $list');
    _globalPref.chatBackground(userId).val = list;
  }

  ///将当前聊天背景应用到所有场景
  void setupAllChatBackground() {
    List<dynamic> list = _globalPref.chatBackground(userId).val;
    //先找到之前设置的
    var background = list.map((e) => ChatBackground.fromJson(e)).firstWhere(
        (element) => element.sessionId == null,
        orElse: ChatBackground.empty);
    list.clear();
    list.add(background.toJson());
    logger.d('list after: $list');
    _globalPref.chatBackground(userId).val = list;
  }

  void saveAtConversation(String convId, bool hasAt) {
    try {
      Map<String, bool> map = Map<String, bool>.from(
          _globalPref.atConversation(convId, userId).val);
      map[convId] = hasAt;
      _globalPref.atConversation(convId, userId).val = map;
    } catch (e) {
      logger.e(e);
      _globalPref.atConversation(convId, userId).val = <String, bool>{
        convId: hasAt
      };
    }
  }

  bool conversationHasAt(String convId) {
    try {
      return _globalPref.atConversation(convId, userId).val[convId] ?? false;
    } catch (e) {
      logger.e(e);
      return false;
    }
  }

  void _initNotifications() {
    AwesomeNotifications().initialize(
        // set the icon to null if you want to use the default app icon
        null,
        [
          NotificationChannel(
              channelGroupKey: 'xchat_channel_group',
              channelKey: 'xchat_channel',
              channelName: 'XChat',
              channelDescription: 'XChat channel for receive and send message',
              defaultColor: Color(0xFF9D50DD),
              playSound: false,
              enableVibration: false,
              ledColor: Colors.white)
        ],
        debug: false);

    AwesomeNotifications().isNotificationAllowed().then((isAllowed) {
      if (!isAllowed) {
        // This is just a basic example. For real apps, you must show some
        // friendly dialog box before call the request method.
        // This is very important to not harm the user experience
        AwesomeNotifications().requestPermissionToSendNotifications();
      }
    });
  }
}
