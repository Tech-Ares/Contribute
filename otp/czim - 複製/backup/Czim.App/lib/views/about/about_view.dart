import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'about_logic.dart';

class AboutPage extends GetView<AboutLogic> {
  const AboutPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: 'about'.tr,
      ),
      body: Column(
        children: [
          const SizedBox(height: 40,),
          // SvgPicture.asset(Utils.getSvgPath('ic_nav_message'), width: 100,),
          Image.asset(Utils.getImgPath('ic_logo'), width: 160,),
          const SizedBox(height: 12,),
          Obx(() => Text('版本：${controller.foundation}', style: Get.textTheme.headline4,)),
          const SizedBox(height: 40,),
          _itemView(title: 'feedback'.tr, onTap: () => Get.toNamed(PageRoutes.feedback)),
          BrnLine(),
          _itemView(title: 'agreement'.tr, onTap: () => {}),
          BrnLine(),
          _itemView(title: 'check_version'.tr, onTap: () => controller.checkVersion()),
          Expanded(child: Container()),
          Text('copyright'.tr, style: Get.textTheme.bodyText2,),
          const SizedBox(height: 16,),
        ],
      ),
    );
  }

  Widget _itemView(
      {required String title, GestureTapCallback? onTap, String? tip}) {
    return ListTile(
      onTap: onTap,
      title: Text(title),
      trailing: Row(
        mainAxisSize: MainAxisSize.min,
        children: [
          if(tip != null) Text(tip),
          const Icon(Icons.chevron_right)
        ],
      ),
    );
  }
}
