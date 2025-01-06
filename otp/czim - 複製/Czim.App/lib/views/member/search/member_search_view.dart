import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/widget_im_avatar.dart';
import 'package:easy_chat/models/friend.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'member_search_logic.dart';

class MemberSearchPage extends GetView<MemberSearchLogic> {
  const MemberSearchPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: _searchBar(),
      body: controller.volatile((state) => ListView.separated(
            itemBuilder: _itemBuilder,
            itemCount: controller.getItemCount,
            separatorBuilder: (BuildContext context, int index) => BrnLine(),
          )),
    );
  }

  _searchBar() => BrnSearchAppbar(
        brightness: Brightness.light,
        showDivider: false,
        hint: 'member_search_hint'.tr,
        //输入框 文本内容变化的监听
        searchBarInputChangeCallback: (input) {
          logger.d('searchBarInputChangeCallback ==> $input');
          controller.inputChange();
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

  Widget _itemBuilder(BuildContext context, int index) {
    Friend item = controller.indexOfItem(index);
    return ListTile(
      leading: imUserAvatar(item.avatar, 36, name: item.nickName),
      title: Text(
        item.nickName ?? item.targetId ?? '-',
        style: Get.textTheme.subtitle1,
      ),
      trailing: Visibility(
        visible: controller.isMyself(item) || (item.isFriend ?? false),
        child: BrnStateTag(
          tagText: controller.isMyself(item) ? '我自己' :'好友',
          tagState: controller.isMyself(item) ? TagState.failed : TagState.succeed,
        ),
      ),
      onTap: () => item.onClick(),
    );
  }
}
