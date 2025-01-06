import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_card_item.dart';
import 'package:easy_chat/component/base/base_pull_refresh.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:inview_notifier_list/inview_notifier_list.dart';

import 'live_cover_component.dart';
import 'nav_live_logic.dart';

class NavLivePage extends GetView<NavLiveLogic> {
  const NavLivePage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: 'nav_live'.tr,
        //左侧多icon
        leading: Container(),
      ),
      body: controller.volatile((state) => InViewNotifierCustomScrollView(
            slivers: [
              SliverFillRemaining(
                child: BasePullToRefresh(
                  refreshController: controller.refreshController,
                  enableLoadMore: false,
                  onRefresh: () => controller.onRefresh(),
                  onLoading: () => controller.onLoadMore(),
                  child: ListView.builder(
                    itemBuilder: (BuildContext context, int index) =>
                        LayoutBuilder(builder:
                            (BuildContext context, BoxConstraints constraints) {
                      return InViewNotifierWidget(
                        id: '$index',
                        builder: (BuildContext context, bool isInView,
                                Widget? child) =>
                            _itemBuilder(context, isInView, index),
                      );
                    }),
                    itemCount: controller.getItemCount,
                    // separatorBuilder: (BuildContext context, int index) =>
                    //     BrnLine(),
                  ),
                ),
              )
            ],
            isInViewPortCondition: (double deltaTop, double deltaBottom,
                double viewPortDimension) {
              return deltaTop < (0.5 * viewPortDimension) &&
                  deltaBottom > (0.5 * viewPortDimension);
            },
          )),
      // body: controller.volatile((state) => Obx(() => BasePullToRefresh(
      //       onRefresh: () => controller.onRefresh(),
      //       enableLoadMore: false,
      //       refreshController: controller.refreshController,
      //       child: ListView.separated(
      //         itemBuilder: _itemBuilder,
      //         itemCount: controller.getItemCount,
      //         separatorBuilder: (BuildContext context, int index) => BrnLine(),
      //       ),
      //     ))),
    );
  }

  _itemBuilder(BuildContext context, bool isInView, int index) {
    return BaseCardView(
      corner: 0,
      color: Colors.black,
      padding: const EdgeInsets.symmetric(vertical: 4, horizontal: 8),
      margin: EdgeInsets.zero,
      child: keepAliveWrapper(LiveCoverComponent(
        liveVideo: controller.indexOfItem(index),
        isInView: isInView,
      )),
    );
  }
}
