import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

class SearchOutsideWidget extends StatelessWidget {
  final String? title;
  final GestureTapCallback? onTap;
  const SearchOutsideWidget({Key? key, this.title, this.onTap}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: onTap ?? () => Get.toNamed(PageRoutes.search),
      child: Container(
        margin: const EdgeInsets.symmetric(vertical: 8, horizontal: 12),
        padding: const EdgeInsets.symmetric(vertical: 8, horizontal: 8),
        decoration: ShapeDecoration(
            color: Colors.grey.shade50,
            shape: const RoundedRectangleBorder(
                borderRadius: BorderRadius.all(Radius.circular(24)))),
        child: Row(
          children: [
            SvgPicture.asset(Utils.getSvgPath('ic_action_search'), width: 20,),
            const SizedBox(
              width: 8,
            ),
            Text(title ?? 'search'.tr)
          ],
        ),
      ),
    );
  }
}
