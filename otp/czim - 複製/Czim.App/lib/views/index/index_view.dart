import 'package:badges/badges.dart';
import 'package:bruno/bruno.dart';
import 'package:easy_chat/global/i_style.dart';
import 'package:easy_chat/views/nav/contact/nav_contact_view.dart';
import 'package:easy_chat/views/nav/live/nav_live_view.dart';
import 'package:easy_chat/views/nav/message/nav_message_view.dart';
import 'package:easy_chat/views/nav/mine/nav_mine_view.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:persistent_bottom_nav_bar/persistent-tab-view.dart';

import 'index_logic.dart';

class IndexPage extends GetView<IndexLogic> {
  IndexPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) => _indexView();

  List<Widget> _buildScreens() {
    return <Widget>[
      keepAliveWrapper(const NavMessagePage()),
      keepAliveWrapper(const NavContactPage()),
      keepAliveWrapper(const NavLivePage()),
      keepAliveWrapper(const NavMinePage()),
    ];
  }

  final GlobalKey _key = GlobalKey();

  Widget _indexView() => PersistentTabView(
        Get.context!,
        controller: controller.tabController,
        screens: _buildScreens(),
        items: _navBarsItems(),
        confineInSafeArea: true,
        backgroundColor: IColors.backgroundColor,
        handleAndroidBackButtonPress: true,
        resizeToAvoidBottomInset: true,
        stateManagement: true,
        navBarHeight: 40.h,
        hideNavigationBarWhenKeyboardShows: false,
        margin: const EdgeInsets.all(0.0),
        popActionScreens: PopActionScreensType.all,
        // bottomScreenMargin: 48.h,
        onWillPop: (context) => controller.back(),
        selectedTabScreenContext: (context) {},
        hideNavigationBar: false,
        // decoration: NavBarDecoration(
        //     colorBehindNavBar: Colors.indigo,
        //     borderRadius: BorderRadius.circular(0.0)),
        // bottomScreenMargin: 0,
        popAllScreensOnTapOfSelectedTab: true,
        navBarStyle:
            NavBarStyle.style1, // Choose the nav bar style with this property
      );

  List<PersistentBottomNavBarItem> _navBarsItems() {
    return [
      PersistentBottomNavBarItem(
        inactiveIcon: _badgeWrapper(_unselectedIcon('ic_nav_message')),
        icon: _badgeWrapper(_selectedIcon('ic_nav_message')),
        activeColorPrimary: Get.theme.bottomAppBarColor,
        inactiveColorPrimary: Get.theme.unselectedWidgetColor,
        title: 'nav_home'.tr,
      ),
      PersistentBottomNavBarItem(
        inactiveIcon: _unselectedIcon('ic_nav_contact'),
        icon: _selectedIcon('ic_nav_contact'),
        activeColorPrimary: Get.theme.bottomAppBarColor,
        inactiveColorPrimary: Get.theme.unselectedWidgetColor,
        title: 'nav_contact'.tr,
      ),
      PersistentBottomNavBarItem(
        inactiveIcon: _unselectedIcon('ic_nav_live'),
        icon: _selectedIcon('ic_nav_live'),
        activeColorPrimary: Get.theme.bottomAppBarColor,
        inactiveColorPrimary: Get.theme.unselectedWidgetColor,
        title: 'nav_live'.tr,
      ),
      PersistentBottomNavBarItem(
        inactiveIcon: _unselectedIcon('ic_nav_mine'),
        icon: _selectedIcon('ic_nav_mine'),
        activeColorPrimary: Get.theme.bottomAppBarColor,
        inactiveColorPrimary: Get.theme.unselectedWidgetColor,
        title: 'nav_mine'.tr,
      ),
    ];
  }

  // Widget _homeView() => Column(
  //       children: [
  //         Expanded(child: Container()),
  //         bottomTabBar()
  //       ],
  //     );
  //
  // Widget bottomTabBar() => BrnBottomTabBar(
  //       // currentIndex: _selectedIndex,
  //       // onTap: _onItemSelected,
  //       badgeColor: Colors.red,
  //       items: <BrnBottomTabBarItem>[
  //         BrnBottomTabBarItem(
  //             icon: _unselectedIcon('ic_nav_message'),
  //             activeIcon: _selectedIcon('ic_nav_contact'),
  //             title: Text('nav_contact'.tr)),
  //         BrnBottomTabBarItem(
  //             icon: _unselectedIcon('ic_nav_contact'),
  //             activeIcon: _selectedIcon('ic_nav_contact'),
  //             title: Text('nav_contact'.tr)),
  //         BrnBottomTabBarItem(
  //             icon: _unselectedIcon('ic_nav_live'),
  //             activeIcon: _selectedIcon('ic_nav_live'),
  //             title: Text('nav_live'.tr)),
  //         BrnBottomTabBarItem(
  //             icon: _unselectedIcon('ic_nav_mine'),
  //             activeIcon: _selectedIcon('ic_nav_mine'),
  //             title: Text(
  //               'nav_mine'.tr,
  //             )),
  //       ],
  //     );

  Widget _badgeWrapper(Widget child) =>Obx(() => Badge(
    showBadge: controller.showUnreadCount,
    animationType: BadgeAnimationType.scale,
    badgeContent: Text(
      controller.unreadMessageCount,
      style: Get.textTheme.bodyText2?.copyWith(color: Colors.white),
    ),
    shape: BadgeShape.circle,
    badgeColor: IColors.primarySwatch,
    child: child,
  ));

  Widget _selectedIcon(String name) => SvgPicture.asset(Utils.getSvgPath(name));

  Widget _unselectedIcon(String name) => SvgPicture.asset(
        Utils.getSvgPath(name),
        color: Get.theme.unselectedWidgetColor,
      );
}
