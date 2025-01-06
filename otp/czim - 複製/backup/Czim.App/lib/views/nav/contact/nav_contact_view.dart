import 'package:azlistview/azlistview.dart';
import 'package:bruno/bruno.dart' hide AzListView;
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/widget_im_avatar.dart';
import 'package:easy_chat/component/widget_search_outside.dart';
import 'package:easy_chat/global/i_style.dart';
import 'package:easy_chat/models/contact_model.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'nav_contact_logic.dart';

class NavContactPage extends GetView<NavContactLogic> {
  const NavContactPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: 'nav_contact'.tr,
        //左侧多icon
        leading: Container(),
        elevation: 0.2,
        bottom: const PreferredSize(
          preferredSize: Size.fromHeight(kToolbarHeight),
          child: SearchOutsideWidget(),
        ),
        showDefaultBottom: false,
        actions: BrnIconAction(
          child: SvgPicture.asset(Utils.getSvgPath('ic_action_add')),
          themeData: BrnAppBarConfig.dark()
            ..backgroundColor = Colors.blueAccent,
          key: controller.actionKey,
          //自定义action的点击
          iconPressed: () {
            //show pop
            controller.popMoreAction(context);
          },
        ),
      ),
      body: controller.volatile((state) => AzListView(
            data: controller.state!,
            itemCount: controller.getItemCount,
            itemBuilder: (BuildContext context, int index) =>
                _memberItem(controller.indexOfItem(index), context: context),
            physics: const BouncingScrollPhysics(),
            susItemBuilder: (BuildContext context, int index) {
              ContactModel model = controller.indexOfItem(index);
              if ('↑' == model.getSuspensionTag()) {
                return Container();
              }
              return _susItem(model.getSuspensionTag());
            },
            indexBarData: const ['↑', '☆', ...kIndexBarData],
            indexBarOptions: IndexBarOptions(
              needRebuild: true,
              ignoreDragCancel: true,
              downTextStyle:
                  Get.textTheme.bodyText2?.copyWith(color: Colors.white),
              downItemDecoration: const BoxDecoration(
                  shape: BoxShape.circle, color: IColors.primarySwatch),
              indexHintWidth: 120 / 2,
              indexHintHeight: 100 / 2,
              indexHintTextStyle:
                  Get.textTheme.headline3!.copyWith(color: Colors.white),
              indexHintDecoration: BoxDecoration(
                image: DecorationImage(
                  image:
                      AssetImage(Utils.getImgPath('ic_index_bar_bubble_gray')),
                  fit: BoxFit.contain,
                ),
              ),
              indexHintAlignment: Alignment.centerRight,
              indexHintChildAlignment: const Alignment(-0.25, 0.0),
              indexHintOffset: const Offset(-20, 0),
            ),
          )),
    );
  }

  Widget _memberItem(ContactModel item,
          {Color defHeaderBgColor = IColors.tabBorderColor,
          required BuildContext context}) =>
      Column(
        children: [
          ListTile(
            leading: item.isCustom
                ? SvgPicture.asset(
                    Utils.getSvgPath(item.svgPath!),
                    width: 36,
                    height: 36,
                  )
                : imUserAvatar(item.avatar, 36, name: item.showName),
            title: Text(
              item.showName,
              style: Get.textTheme.subtitle1,
            ),
            onTap: () => item.onClick(context),
          ),
          Visibility(
              visible: item.isCustom,
              child: Padding(
                padding: EdgeInsets.only(left: 64),
                child: BrnLine(),
              ))
        ],
      );

  Widget _susItem(String tag, {double susHeight = 40}) => Container(
        height: susHeight,
        width: MediaQuery.of(Get.context!).size.width,
        padding: const EdgeInsets.only(left: 16.0),
        color: const Color(0xFFF3F4F5),
        alignment: Alignment.centerLeft,
        child: Text(
          tag,
          softWrap: false,
          style: Get.textTheme.subtitle1,
        ),
      );
}
