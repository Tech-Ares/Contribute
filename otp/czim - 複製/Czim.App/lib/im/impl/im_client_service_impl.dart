import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/im/im_client_service.dart';
import 'package:flutter_oppo_push_init/flutter_oppo_push_init.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

class IMClientServiceImpl extends IMClientService {

  final GlobalController _globalController = Get.find();

  @override
  Future? init() async {
    EMOptions options = EMOptions(appKey: '1107220118095349#xchat');
    // EMOptions options = EMOptions(appKey: '1129211213083526#xchat');
    EMPushConfig config = EMPushConfig();
    // 配置推送信息
    config
      ..enableAPNs('Dev')
      ..enableHWPush()
      // ..enableFCM('')
      ..enableMeiZuPush('146559', '6b2ee134c9a646928c1e91caae57ab19')
      ..enableVivoPush()
      ..enableOppPush('7e1c66d380bc4fc19f74eb7cc0a73ac4', '652238e9a005476f8abbec2d8cf535d9')
      ..enableMiPush('2882303761520130536', '5872013067536');
    options.pushConfig = config;
    options.debugModel = true;
    await EMClient.getInstance.init(options);
    await FlutterOppoPushInit.initPush;
  }

  @override
  Future? register(String username, String password) async {
    return await EMClient.getInstance.createAccount(username, password);
  }

  @override
  Future? login(String username, String password) async {
    return await EMClient.getInstance.login(username, password);
  }

  @override
  String? currentUsername() {
    return EMClient.getInstance.currentUsername;
  }

  @override
  Future? logout() async {
    try {
      // true: 是否解除deviceToken绑定。
      _globalController.imToken = null;
      return await EMClient.getInstance.logout(true);
    } on EMError catch (e) {
      logger.e('操作失败，原因是: $e');
    }
    return null;
  }
}
