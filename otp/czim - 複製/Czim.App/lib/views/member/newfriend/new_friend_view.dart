import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/base/base_scaffold.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'new_friend_logic.dart';

class NewFriendPage extends GetView<NewFriendLogic> {
  const NewFriendPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      appBar: BrnAppBar(
        title: '新的朋友',
      ),
      body: Center(
        child: BrnAbnormalStateWidget(
          img: Image.asset(
            Utils.getImgPath('ic_placeholder_error'),
            scale: 3.0,
          ),
          content: 'Coming Soon',
        ),
      ),
    );
  }
}
