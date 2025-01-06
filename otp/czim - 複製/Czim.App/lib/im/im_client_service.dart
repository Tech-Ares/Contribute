
abstract class IMClientService {

  ///初始化环信IM
  Future? init();

  ///注册
  Future? register(String username, String password);

  ///登录
  Future? login(String username, String password);

  ///获取当前登录的用户名
  String? currentUsername();

  ///退出登录
  Future? logout();
}