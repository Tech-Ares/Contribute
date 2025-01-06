import 'dart:async';
import 'dart:developer';

import 'package:bot_toast/bot_toast.dart';
import 'package:bruno/bruno.dart';
import 'package:easy_chat/common/theme/theme_white.dart';
import 'package:easy_chat/config/brn_global_config.dart';
import 'package:easy_chat/config/chat_core_config.dart';
import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';
import 'common/theme/theme_dark.dart';
import 'config/chat_common_config.dart';
import 'global/global_controller.dart';
import 'l10n/messages.dart';
import 'routes/page_routes.dart';

void main() async => globalExceptionCatchAndRun();

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    SystemUtil.simpleSystemUIOverlayStyle(Brightness.light);
    BrnInitializer.register(allThemeConfig: BrnGlobalConfig.defaultAllConfig);
    commonConfig = ChatCommonConfig();
    coreConfig = ChatCoreConfig();
    Get.config(
        enableLog: true,
        defaultPopGesture: true,
        defaultTransition: Transition.cupertino);
    Get.put(GlobalController());
    return FutureBuilder<Widget>(
        builder: (c, _) => ScreenUtilInit(builder: () {
              log('app builder');
              return GetMaterialApp(
                enableLog: true,
                defaultTransition: Transition.cupertino,
                theme: themeWhite,
                darkTheme: themeDark,
                themeMode: ThemeMode.light,
                opaqueRoute: Get.isOpaqueRouteDefault,
                popGesture: Get.isPopGestureEnable,
                getPages: PageRoutes.getRoutes,
                initialRoute: PageRoutes.splash,
                builder: BotToastInit(),
                navigatorObservers: [BotToastNavigatorObserver()],
                translations: Messages(),
                localizationsDelegates: const <LocalizationsDelegate<dynamic>>[
                  RefreshLocalizations.delegate,
                  GlobalMaterialLocalizations.delegate,
                  // uses `flutter_localizations`
                  GlobalWidgetsLocalizations.delegate,
                  GlobalCupertinoLocalizations.delegate,
                ],
                // 你的翻译
                locale: const Locale('zh', 'CN'),
                supportedLocales: const <Locale>[
                  Locale('en', 'US'),
                  Locale('zh', 'CN'),
                ],
                // 将会按照此处指定的语言翻译
                fallbackLocale: const Locale('en', 'US'),
                // 添加一个回调语言选项，以备上面指定的语言翻译不存在
                localeListResolutionCallback:
                    (List<Locale>? locales, Iterable<Locale> supportedLocales) {
                  log('当前系统语言环境$locales');
                  return;
                },
              );
            }));
  }
}

void globalExceptionCatchAndRun() {
  void reportErrorAndLog(FlutterErrorDetails details) {
    ExceptionHandler.handleException(details);
  }

  FlutterErrorDetails makeDetails(Object error, StackTrace stackTrace) {
    // 构建错误信息
    return FlutterErrorDetails(stack: stackTrace, exception: error);
  }

  FlutterError.onError = (FlutterErrorDetails details) {
    //获取 widget build 过程中出现的异常错误
    reportErrorAndLog(details);
  };
  runZonedGuarded(
    () {
      runApp(const MyApp());
    },
    (Object error, StackTrace stackTrace) {
      //没被我们catch的异常
      reportErrorAndLog(makeDetails(error, stackTrace));
    },
  );
}
