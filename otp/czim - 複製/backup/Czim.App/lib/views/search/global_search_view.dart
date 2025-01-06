import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'global_search_logic.dart';

class GlobalSearchPage extends GetView<GlobalSearchLogic> {
  const GlobalSearchPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: _searchBar(),
    );
  }

  _searchBar() => BrnSearchAppbar(
    brightness: Brightness.light,
    showDivider: false,
    //输入框 文本内容变化的监听
    searchBarInputChangeCallback: (input) {
      logger.d('searchBarInputChangeCallback ==> $input');
    },
    //输入框 键盘确定的监听
    searchBarInputSubmitCallback: (input) {
      logger.d('searchBarInputSubmitCallback ==> $input');
    },
    //为输入框添加 文本控制器，如果不传则使用默认的
    controller: controller.textController,
    //为输入框添加 焦点控制器，如果不传则使用默认的
    //右侧取消action的点击回调
    //参数同leadClickCallback 一样
    dismissClickCallback: (controller, update) {
      Get.back();
    },
  );
}
