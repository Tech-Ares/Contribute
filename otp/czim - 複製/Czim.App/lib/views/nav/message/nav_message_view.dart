import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_pull_refresh.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/base/base_slide_action.dart';
import 'package:easy_chat/component/conversation/conversation_item.dart';
import 'package:easy_chat/component/widget_search_outside.dart';
import 'package:easy_chat/global/i_style.dart';
import 'package:flutter/material.dart';
import 'package:flutter_slidable/flutter_slidable.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'nav_message_logic.dart';

class NavMessagePage extends GetView<NavMessageLogic> {
  const NavMessagePage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: _messageAppbar(context),
      body: controller.volatile((state) => Obx(() => BasePullToRefresh(
            onRefresh: () => controller.onRefresh(),
            enableLoadMore: false,
            refreshController: controller.refreshController,
            child: ListView.separated(
              itemBuilder: _itemBuilder,
              itemCount: controller.getItemCount,
              separatorBuilder: (BuildContext context, int index) => BrnLine(),
            ),
          ))),
    );
  }

  Widget _itemBuilder(BuildContext context, int index) {
    return Slidable(
      enabled: false,
      child: ConversationItem(
        conv: controller.indexOfConv(index),
        onTap: () => controller.onConversationItem(index),
      ),
      endActionPane: ActionPane(
        motion: const ScrollMotion(),
        children: [
          // BaseSlideAction(
          //   label: '置顶',
          //   iconData: Icons.delete,
          //   color: IColors.primarySwatch,
          //   onTap: (BuildContext context) {},
          // ),
          BaseSlideAction(
            label: '全部已读',
            iconData: Icons.delete,
            color: IColors.primarySwatch,
            onTap: (BuildContext context) => controller.readAll(index),
          ),
          BaseSlideAction(
            label: '删除',
            iconData: Icons.delete,
            color: Colors.red,
            onTap: (BuildContext context) => controller.deleteConv(index),
          ),
        ],
      ),
    );
  }

  _messageAppbar(BuildContext context) => BrnAppBar(
        title: Text(
          'app_name'.tr,
          style: Get.textTheme.headline3
              ?.copyWith(fontFamily: 'Horizon', color: IColors.primarySwatch),
        ),
        // Obx(() => Text(
        //   '${'app_name'.tr}${controller.connectStr()}',
        //   style: Get.textTheme.subtitle1,
        // )),
        showDefaultBottom: false,
        //左侧多icon
        leading: Container(),
        bottom: const PreferredSize(
          preferredSize: Size.fromHeight(kToolbarHeight),
          child: SearchOutsideWidget(),
        ),
        //文本action
        actions: BrnIconAction(
          child: SvgPicture.asset(
            Utils.getSvgPath('ic_action_add'),
          ),
          themeData: BrnAppBarConfig.dark()
            ..backgroundColor = Colors.blueAccent,
          key: controller.actionKey,
          //自定义action的点击
          iconPressed: () {
            //show pop
            controller.popMoreAction(context);
          },
        ),
      );
}
