import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'theme_style.dart';

const int _themeIndex = 0;

final ThemeData themeWhite = ThemeData.light().copyWith(
  splashColor: Colors.transparent,
  brightness: Brightness.light,
  scaffoldBackgroundColor: ThemeColors.themePrimaryBackColor[_themeIndex],
  primaryColor: ThemeColors.themePrimaryColor[_themeIndex],
  highlightColor: ThemeColors.themeHoverColor[_themeIndex],
  accentColor: ThemeColors.themeAccentColor[_themeIndex],
  secondaryHeaderColor: ThemeColors.themeSecondaryHeaderColor[_themeIndex],
  backgroundColor: ThemeColors.themeBackgroundColor[_themeIndex],
  cardColor: ThemeColors.themeCardColor[_themeIndex],
  cardTheme: ThemeColors.cardTheme[_themeIndex],
  iconTheme: PandaConstant.iconTheme[_themeIndex],
  textTheme: PandaConstant.textTheme[_themeIndex],
  primaryTextTheme: PandaConstant.textTheme[_themeIndex],
  accentTextTheme: PandaConstant.textTheme[_themeIndex],
  shadowColor: ThemeColors.themeShadowColor[_themeIndex],
  appBarTheme: PandaConstant.appbarTheme[_themeIndex],
  tabBarTheme: PandaConstant.tabBarTheme[_themeIndex],
  indicatorColor: ThemeColors.themeAccentColor[_themeIndex],
  primaryIconTheme: PandaConstant.primaryIconTheme[_themeIndex],
  accentIconTheme: PandaConstant.accentIconTheme[_themeIndex],
  hoverColor: ThemeColors.themeHoverColor[_themeIndex],
  textSelectionTheme: Get.theme.textSelectionTheme.copyWith(
    cursorColor: ThemeColors.iconAccentColor[_themeIndex],
  ),
  unselectedWidgetColor: ThemeColors.themeUnselected[_themeIndex],
  hintColor: ThemeColors.hintColor[_themeIndex],
  bottomAppBarColor: ThemeColors.themeSecondColor[_themeIndex],
  textButtonTheme: TextButtonThemeData(
    style: ButtonStyle(
      foregroundColor: MaterialStateProperty.all(Colors.grey),
    ),
  ),
);
