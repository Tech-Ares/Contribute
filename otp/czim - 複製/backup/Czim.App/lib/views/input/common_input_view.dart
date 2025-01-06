import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:easy_chat/global/i_style.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'common_input_logic.dart';

class CommonInputPage extends GetView<CommonInputLogic> {
  const CommonInputPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      backgroundColor: IColors.uselessColor,
      appBar: BrnAppBar(
        title: controller.getTitle(),
        actions: BrnTextAction(
          'commit'.tr,
          iconPressed: () => controller.commit(),
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(12),
        child: ClipRRect(
          borderRadius: const BorderRadius.all(Radius.circular(4)),
          child: BrnInputText(
            textEditingController: controller.textEditingController,
            maxHeight: 200,
            minHeight: 30,
            minLines: 1,
            maxLength: controller.maxCount,
            textInputAction: TextInputAction.newline,
            maxHintLines: 20,
            hint: controller.getHint(),
            padding: const EdgeInsets.fromLTRB(20, 10, 20, 14),
            onTextChange: (text) {},
            onSubmit: (text) {},
          ),
        ),
      ),
    );
  }
}
