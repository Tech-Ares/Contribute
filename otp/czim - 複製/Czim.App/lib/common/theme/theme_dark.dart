import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'theme_style.dart';

const int _themeIndex = 1;

final ThemeData themeDark = ThemeData.dark().copyWith(
  splashColor: Colors.transparent,
  brightness: Brightness.dark,
  scaffoldBackgroundColor: ThemeColors.themePrimaryColor[_themeIndex],
  highlightColor: ThemeColors.themeHoverColor[_themeIndex],
  primaryColor: ThemeColors.themePrimaryColor[_themeIndex],
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
  accentIconTheme: PandaConstant.primaryIconTheme[_themeIndex],
  hoverColor: ThemeColors.themeHoverColor[_themeIndex],
  textSelectionTheme: Get.theme.textSelectionTheme.copyWith(
    cursorColor: ThemeColors.iconAccentColor[_themeIndex],
  ),
  hintColor: ThemeColors.hintColor[_themeIndex],
  textButtonTheme: TextButtonThemeData(
    style: ButtonStyle(
      foregroundColor: MaterialStateProperty.all(Colors.white),
    ),
  ),
);
