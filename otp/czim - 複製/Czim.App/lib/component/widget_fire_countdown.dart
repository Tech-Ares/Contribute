import 'dart:async';

import 'package:easy_chat/utils/chat_date_format.dart';
import 'package:flutter/cupertino.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

class FireCountDownWidget extends StatefulWidget {
  final int fireTimeMills;

  const FireCountDownWidget({Key? key, this.fireTimeMills = 0})
      : super(key: key);

  @override
  State<StatefulWidget> createState() => _FireCountDownWidgetState();
}

class _FireCountDownWidgetState extends State<FireCountDownWidget> {
  Timer? _timer;
  int leftTime = 0;

  @override
  void initState() {
    leftTime = widget.fireTimeMills;
    _timer = Timer.periodic(const Duration(milliseconds: 200), (timer) {
      setState(() {
        leftTime -= 200;
        if (leftTime <= 0) {
          _timer?.cancel();
          Get.back();
        }
      });
    });
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Text(fireCountDownFormat(leftTime), style: Get.textTheme.bodyText2,);
  }

  @override
  void dispose() {
    _timer?.cancel();
    super.dispose();
  }
}
