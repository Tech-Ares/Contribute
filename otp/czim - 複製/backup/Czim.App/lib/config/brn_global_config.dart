import 'package:bruno/bruno.dart';
import 'package:easy_chat/global/i_style.dart';

class BrnGlobalConfig {
  static BrnAllThemeConfig defaultAllConfig = BrnAllThemeConfig(
      commonConfig: defaultCommonConfig);

  /// 全局配置
  static BrnCommonConfig defaultCommonConfig = BrnCommonConfig(
    ///品牌色
    brandPrimary: IColors.primarySwatch,
  );
}
