import 'package:azlistview/azlistview.dart';
import 'package:bruno/bruno.dart' hide AzListView;
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/component/widget_im_avatar.dart';
import 'package:easy_chat/global/i_style.dart';
import 'package:easy_chat/models/contact_model.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'select_member_logic.dart';

class SelectMemberPage extends GetView<SelectMemberLogic> {
  const SelectMemberPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: controller.getTitle(),
      ),
      body: Column(
        children: [
          Expanded(
              child: controller.volatile((state) => AzListView(
                    data: controller.state!,
                    itemCount: controller.getItemCount,
                    itemBuilder: (BuildContext context, int index) =>
                        _memberItem(controller.indexOfItem(index), index),
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
                      downTextStyle: Get.textTheme.bodyText2
                          ?.copyWith(color: Colors.white),
                      downItemDecoration: const BoxDecoration(
                          shape: BoxShape.circle, color: IColors.primarySwatch),
                      indexHintWidth: 120 / 2,
                      indexHintHeight: 100 / 2,
                      indexHintDecoration: BoxDecoration(
                        image: DecorationImage(
                          image: AssetImage(
                              Utils.getImgPath('ic_index_bar_bubble_gray')),
                          fit: BoxFit.contain,
                        ),
                      ),
                      indexHintAlignment: Alignment.centerRight,
                      indexHintChildAlignment: const Alignment(-0.25, 0.0),
                      indexHintOffset: const Offset(-20, 0),
                    ),
                  ))),
          if(controller.isSelectMode)
          _bottomSelection()
        ],
      ),
    );
  }

  Widget _memberItem(ContactModel item, int index,
          {Color defHeaderBgColor = IColors.tabBorderColor}) =>
      ListTile(
        leading: Row(
          mainAxisSize: MainAxisSize.min,
          children: [
            if(controller.isSelectMode)
            Checkbox(
                shape: const CircleBorder(),
                value: controller.isSelected(index),
                activeColor: IColors.primarySwatch,
                onChanged: controller.dispatchCheckChange(index)),
            imUserAvatar(item.avatar, 36, name: item.showName)
          ],
        ),
        title: Text(
          item.showName,
          style: Get.textTheme.subtitle1,
        ),
        onTap: controller.dispatchCheckTap(index),
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

  _bottomSelection() => SafeArea(
          child: Container(
        padding: const EdgeInsets.symmetric(vertical: 8, horizontal: 12),
        child: Row(
          children: [
            const Text('已选择：'),
            Expanded(
                child: SizedBox(
              height: 32,
              child: _selectedMembers(),
            )),
            Obx(() => BrnSmallMainButton(
                  title: '确认',
                  isEnable: controller.selectedItemCount > 0,
                  onTap: () => controller.dispatchAction(),
                ))
          ],
        ),
      ));

  _selectedMembers() => Obx(() => ListView.separated(
        scrollDirection: Axis.horizontal,
        itemBuilder: (c, index) => Container(
          child: imUserAvatar(controller.memberAvatar(index), 32, name: controller.memberName(index)),
        ),
        itemCount: controller.selectedItemCount,
        separatorBuilder: (BuildContext context, int index) => const SizedBox(
          width: 4,
        ),
      ));
}
