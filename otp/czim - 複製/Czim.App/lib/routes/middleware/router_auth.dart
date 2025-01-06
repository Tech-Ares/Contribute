import 'package:easy_chat/global/global_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import '../page_routes.dart';

class RouteAuthMiddleware extends GetMiddleware {
  @override
  int? priority = 0;

  RouteAuthMiddleware({@required this.priority});

  @override
  RouteSettings? redirect(String? route) {
    if (!Get.find<GlobalController>().isLogin) {
      return const RouteSettings(name: PageRoutes.login);
    }
    return null;
  }
}
