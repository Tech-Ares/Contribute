import 'package:easy_chat/export.dart';
import 'package:easy_chat/models/params/feedback_params.dart';
import 'package:flutter/cupertino.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'feedback_state.dart';

class FeedbackLogic extends GetxController {
  final FeedbackState state = FeedbackState();
  final TextEditingController controller = TextEditingController();

  commit() async {
    var content = controller.text.trim();
    if(content.isEmpty) {
      ToastUtil.info('内容不能为空');
      return;
    }
    await memberRestClient.feedback(FeedbackParams(
      content: content
    ));
    Get.back();
  }
}
