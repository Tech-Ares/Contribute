import 'package:easy_chat/export.dart';
import 'package:easy_chat/models/chat_room.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import 'chat_room_state.dart';

class ChatRoomLogic extends BaseController<List<ChatRoom>?> {
  final ChatRoomState _state = ChatRoomState();

  @override
  Future<List<ChatRoom>?> Function() get requestFunc =>
      () => memberRestClient.chatRooms();

  @override
  void onReady() {
    onRefresh();
    super.onReady();
  }

  openConv(int index) async {
    ChatRoom room = indexOfItem(index);
    EMConversation? conv = await EMClient.getInstance.chatManager
        .getConversation(room.targetId!, EMConversationType.ChatRoom);
    logger.d('room conv: ${conv?.toJson()}');
    conv?.name = room.nickName ?? '';
    Get.toNamed(PageRoutes.chat, arguments: conv);
  }
}
