import 'package:easy_chat/global/i_style.dart';
import 'package:flutter/material.dart';
import 'package:getwidget/components/appbar/gf_appbar.dart';

GFAppBar baseAppbar(
    {dynamic title,
    Widget? leading,
    Brightness? brightness,
    List<Widget>? actions,
    Color? color = IColors.primarySwatch,
    double? elevation = 1,
    double? titleSpacing,
      bool? centerTitle = true,
      bool automaticallyImplyLeading = true,
    TextTheme? textTheme,
      IconThemeData? iconTheme,
    PreferredSizeWidget? bottom,
    IconThemeData? iconThemeData}) {
  return GFAppBar(
    brightness: brightness ?? Brightness.light,
    leading: leading,
    backgroundColor: color,
    elevation: elevation,
    centerTitle: centerTitle,
    iconTheme: iconTheme,
    automaticallyImplyLeading: automaticallyImplyLeading,
    titleSpacing: titleSpacing ?? NavigationToolbar.kMiddleSpacing,
    // textTheme: textTheme ?? Get.textTheme,
    // iconTheme: iconThemeData ?? Get.theme.iconTheme,
    title: (title == null)
        ? null
        : ((title is String) ? Text(title) : title as Widget),
    actions: actions,
    bottom: bottom,
  );
}
