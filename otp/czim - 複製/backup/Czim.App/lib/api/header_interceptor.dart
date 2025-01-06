import 'package:bruno/bruno.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/authorization.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

class HeaderInterceptor extends Interceptor {
  final GlobalController _globalLogic = Get.find<GlobalController>();

  @override
  void onRequest(RequestOptions options, RequestInterceptorHandler handler) {
    options.contentType ??= 'application/json; charset=utf-8';
    options.connectTimeout = 30000;
    final Authorization? authorization = _globalLogic.authorization;
    if (authorization?.tokenType != null && authorization?.token != null) {
      options.headers.addAll(
        <String, dynamic>{'Authorization': '${authorization?.token}'},
      );
    }
    if (options.headers.containsKey('loading')) {
      BrnLoadingDialog.show(Get.context!);
    }
    handler.next(options);
  }
}
