import 'package:easy_chat/global/i_style.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:google_fonts/google_fonts.dart';

///颜色
class ThemeColors {
  /// 主色调
  static const List<Color> themePrimaryColor = <Color>[
    Color(0xffffffff),
    Color(0xFF000000),
  ];

  /// 主背景色
  static const List<Color> themePrimaryBackColor = <Color>[
    Color(0xffffffff),
    Color(0xFF000000),
  ];

  /// 次色调
  static const List<Color> themeSecondColor = <Color>[
    Color(0xfffc7a7f),
    Color(0xff191919),
  ];

  static const List<Color> themeUnselected = <Color>[
    Color(0xffcccccc),
    Color(0xff191919),
  ];

  static const List<Color> themeAccentColor = <Color>[
    Color(0xff2E4DD7),
    Color(0xffffffff),
  ];

  static const List<Color> themeSecondaryHeaderColor = <Color>[
    Color(0xffCCCCCC),
    Color(0x861d1d1d),
  ];

  static const List<Color> themeBackgroundColor = themePrimaryColor;

  static const List<Color> themeCardColor = <Color>[
    Color(0xFFFFFFFF),
    Color(0xff191919),
  ];

  static const List<CardTheme> cardTheme = <CardTheme>[
    CardTheme(
      color: Color(0xFFFFFFFF),
      shadowColor: Color(0xffFFFFFF),
    ),
    CardTheme(
      color: Color(0xff191919),
      shadowColor: Color(0xff191919),
    ),
  ];

  static const List<Color> themeShadowColor = <Color>[
    Color(0xffF5F5F5),
    Color(0xff1e2534),
  ];

  static const List<Color> primaryDarkColor = <Color>[
    Color(0xff2E4DD7),
    Color(0xffffffff),
  ];

  static const List<Color> themeHoverColor = <Color>[
    Color(0x1a099f7f),
    Color(0xff363636),
  ];

  static const List<Color> hintColor = <Color>[
    Color(0xffbababa),
    Color(0xffbababa),
  ];

  static const List<Color> iconAccentColor = <Color>[
    IColors.primarySwatch,
    Colors.white,
  ];
}

///文本样式
class PandaConstant {
  static const double lagerTextSize = 18.0;
  static const double bigTextSize = 16.0;
  static const double normalTextSize = 15.0;
  static const double middleTextSize = 14.0;
  static const double smallTextSize = 12.0;
  static const double minTextSize = 10.0;
  static const double leastTextSize = 8.0;

  static const List<IconThemeData> iconTheme = <IconThemeData>[
    IconThemeData(color: Color(0xFF333333)),
    IconThemeData(color: Color(0xffffffff))
  ];

  static const List<IconThemeData> primaryIconTheme = <IconThemeData>[
    IconThemeData(color: Color(0xff666666)),
    IconThemeData(color: Color(0xffffffff))
  ];

  static const List<IconThemeData> accentIconTheme = <IconThemeData>[
    IconThemeData(color: Color(0xFF333333)),
    IconThemeData(color: Color(0xffffffff))
  ];

  static const List<Diagnosticable> buttonTheme = <Diagnosticable>[
    ButtonThemeData(),
    IconThemeData(color: Color(0xffffffff))
  ];

  static List<TextTheme> primaryTextTheme = <TextTheme>[
    TextTheme(
      headline3: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(lagerTextSize),
        color: const Color(0xFF333333),
        fontWeight: FontWeight.bold,
      ),
      headline4: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(bigTextSize),
        color: const Color(0xFF333333),
      ),
      headline5: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(normalTextSize),
        color: const Color(0xFF333333),
        fontWeight: FontWeight.bold,
      ),
      headline6: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(normalTextSize),
        color: const Color(0xFF333333),
      ),
      subtitle1: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(middleTextSize),
        color: const Color(0xFF333333),
      ),
      subtitle2: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(middleTextSize),
        color: const Color(0xFF666666),
      ),
      bodyText1: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(middleTextSize),
        color: const Color(0xFF999999),
      ),
      bodyText2: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(smallTextSize),
        color: const Color(0xFF666666),
      ),
    ),
    TextTheme(
      headline3: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(lagerTextSize),
        color: Colors.white,
        fontWeight: FontWeight.bold,
      ),
      headline4: GoogleFonts.lato(fontSize: bigTextSize, color: Colors.white),
      headline5: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(normalTextSize),
        color: Colors.white,
        fontWeight: FontWeight.bold,
      ),
      headline6: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(normalTextSize),
        color: Colors.white,
      ),
      subtitle1: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(middleTextSize),
        color: Colors.white,
      ),
      subtitle2: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(middleTextSize),
        color: Colors.white,
      ),
      bodyText1: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(middleTextSize),
        color: Colors.white,
      ),
      bodyText2: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(smallTextSize),
        color: Colors.white,
      ),
    ),
  ];

  static List<TextTheme> textTheme = <TextTheme>[
    TextTheme(
      headline3: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(lagerTextSize),
        color: const Color(0xFF333333),
        fontWeight: FontWeight.bold,
      ),
      headline4: GoogleFonts.lato(
        fontSize: bigTextSize,
        color: const Color(0xFF333333),
      ),
      headline5: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(normalTextSize),
        color: const Color(0xFF333333),
        fontWeight: FontWeight.bold,
      ),
      headline6: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(normalTextSize),
        color: const Color(0xFF333333),
      ),
      subtitle1: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(middleTextSize),
        color: const Color(0xFF333333),
      ),
      subtitle2: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(middleTextSize),
        color: const Color(0xFF666666),
      ),
      bodyText1: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(middleTextSize),
        color: const Color(0xFF999999),
      ),
      bodyText2: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(smallTextSize),
        color: const Color(0xFF666666),
      ),
    ),
    TextTheme(
      headline3: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(lagerTextSize),
        color: Colors.white,
        fontWeight: FontWeight.bold,
      ),
      headline4: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(bigTextSize),
        color: Colors.white,
      ),
      headline5: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(normalTextSize),
        color: Colors.white,
        fontWeight: FontWeight.bold,
      ),
      headline6: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(normalTextSize),
        color: Colors.white,
      ),
      subtitle1: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(middleTextSize),
        color: Colors.white,
      ),
      subtitle2: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(middleTextSize),
        color: Colors.white,
      ),
      bodyText1: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(middleTextSize),
        color: Colors.white,
      ),
      bodyText2: GoogleFonts.lato(
        fontSize: ScreenUtil().setSp(smallTextSize),
        color: Colors.white,
      ),
    ),
  ];

  static List<AppBarTheme> appbarTheme = <AppBarTheme>[
    AppBarTheme(
      textTheme: TextTheme(
        headline3: GoogleFonts.lato(
          fontSize: lagerTextSize,
          color: IColors.mainTextColor,
          fontWeight: FontWeight.bold,
        ),
        headline4: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(bigTextSize),
          color: IColors.mainTextColor,
        ),
        headline5: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(normalTextSize),
          color: IColors.mainTextColor,
          fontWeight: FontWeight.bold,
        ),
        headline6: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(normalTextSize),
          color: IColors.mainTextColor,
        ),
        subtitle1: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(middleTextSize),
          color: IColors.mainTextColor,
        ),
        subtitle2: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(middleTextSize),
          color: IColors.mainTextColor,
        ),
        bodyText1: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(middleTextSize),
          color: IColors.mainTextColor,
        ),
        bodyText2: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(smallTextSize),
          color: IColors.mainTextColor,
        ),
      ),
      shadowColor: const Color(0xffF5F5F5),
      actionsIconTheme: const IconThemeData(color: IColors.mainTextColor,),
      backgroundColor: Colors.white,
      iconTheme: const IconThemeData(color: IColors.mainTextColor,),
    ),
    AppBarTheme(
      textTheme: TextTheme(
        headline3: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(lagerTextSize),
          color: Colors.white,
          fontWeight: FontWeight.bold,
        ),
        headline4: GoogleFonts.lato(fontSize: bigTextSize, color: Colors.white),
        headline5: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(normalTextSize),
          color: Colors.white,
          fontWeight: FontWeight.bold,
        ),
        headline6: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(normalTextSize),
          color: Colors.white,
        ),
        subtitle1: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(middleTextSize),
          color: Colors.white,
        ),
        subtitle2: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(middleTextSize),
          color: Colors.white,
        ),
        bodyText1: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(middleTextSize),
          color: Colors.white,
        ),
        bodyText2: GoogleFonts.lato(
          fontSize: ScreenUtil().setSp(smallTextSize),
          color: Colors.white,
        ),
      ),
      shadowColor: const Color(0xff191919),
      actionsIconTheme: const IconThemeData(color: Colors.white),
      backgroundColor: Colors.black,
      iconTheme: const IconThemeData(color: Color(0xffffffff)),
    )
  ];

  static List<TabBarTheme> tabBarTheme = <TabBarTheme>[
    TabBarTheme(
      unselectedLabelStyle: GoogleFonts.lato(
        color: const Color(0xff333333),
        fontSize: ScreenUtil().setSp(normalTextSize),
      ),
      labelStyle: GoogleFonts.lato(
        color: Colors.black87,
        fontSize: bigTextSize,
        fontWeight: FontWeight.w800,
      ),
      indicator: const UnderlineTabIndicator(
        insets: EdgeInsets.all(8),
        borderSide: BorderSide(width: 2, color: Color(0xff2E4DD7)),
      ),
    ),
    TabBarTheme(
      unselectedLabelStyle: GoogleFonts.lato(
        color: Colors.white,
        fontSize: ScreenUtil().setSp(normalTextSize),
      ),
      labelStyle: GoogleFonts.lato(
        color: Colors.white,
        fontSize: ScreenUtil().setSp(bigTextSize),
        fontWeight: FontWeight.w800,
      ),
      indicator: const UnderlineTabIndicator(
        insets: EdgeInsets.all(8),
        borderSide: BorderSide(width: 2, color: Color(0xff2E4DD7)),
      ),
    ),
  ];
}
