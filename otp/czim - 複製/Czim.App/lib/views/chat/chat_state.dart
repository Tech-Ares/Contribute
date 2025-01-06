import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

class ChatState {
  RxInt selectedFire = 0.obs;
  RxInt fontSize = 14.obs;
  Rx<Decoration> decoration = const BoxDecoration(
    color: Colors.white
  ).obs;
}
