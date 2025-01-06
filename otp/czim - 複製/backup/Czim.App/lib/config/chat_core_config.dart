import 'package:easy_chat/api/header_interceptor.dart';
import 'package:easy_chat/api/response_interceptor.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

class ChatCoreConfig extends CoreConfig {

  @override
  String get devHost => 'http://124.71.186.27:5600/baseapp/';

  @override
  Profiles get profiles => Profiles.dev;

  @override
  Interceptors get interceptors => _handleInterceptors();

  Interceptors _handleInterceptors() {
    Interceptors interceptors = Interceptors();
    interceptors.add(HeaderInterceptor());
    interceptors.add(ResponseInterceptor());
    return interceptors;
  }
}
