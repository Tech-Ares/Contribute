import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/global/i_style.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'feedback_logic.dart';

class FeedbackPage extends GetView<FeedbackLogic> {
  const FeedbackPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      backgroundColor: IColors.uselessColor,
      appBar: BrnAppBar(
        title: 'feedback'.tr,
        actions: BrnTextAction(
          'commit'.tr,
          iconPressed: () => controller.commit(),
        ),
      ),
      body: Padding(
        padding: EdgeInsets.all(12),
        child: ClipRRect(
          borderRadius: BorderRadius.all(Radius.circular(4)),
          child: BrnInputText(
            textEditingController: controller.controller,
            maxHeight: 200,
            minHeight: 30,
            minLines: 8,
            maxLength: 200,
            textInputAction: TextInputAction.newline,
            maxHintLines: 20,
            hint: '输入您的意见',
              // cursorColor: Colors.blue,
            padding: EdgeInsets.fromLTRB(20, 10, 20, 14),
            onTextChange: (text) {
            },
            onSubmit: (text) {
            },
          ),
        ),
      ),
    );
  }
}
