import 'package:easy_chat/models/chat_background.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

class ChatBackgroundSelectState {
  Rx<ChatBackground> chatBackground = ChatBackground.empty().obs;
  RxInt selectedIndex = (-1).obs;
}
