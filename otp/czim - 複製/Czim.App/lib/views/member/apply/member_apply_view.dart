import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'member_apply_logic.dart';

class MemberApplyPage extends GetView<MemberApplyLogic> {
  const MemberApplyPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: 'member_apply'.tr,
      ),
    );
  }
}
