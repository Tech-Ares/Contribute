import 'package:easy_chat/im/im_room_service.dart';
import 'package:easy_chat/models/params/page_params.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

class IMRoomServiceImpl extends IMRoomService {
  @override
  Future<List<EMChatRoom>?> fetchChatRooms({PageParams? pageParams}) async {
    EMPageResult<EMChatRoom> result = await EMClient.getInstance.chatRoomManager
        .fetchPublicChatRoomsFromServer();
    return result.data;
  }

  @override
  Future<List<EMChatRoom>?> getChatRooms() async {
    return await EMClient.getInstance.chatRoomManager.getAllChatRooms();
  }

  @override
  Future<EMChatRoom> getChatRoomInfo(String roomId) {
    return EMClient.getInstance.chatRoomManager.fetchChatRoomInfoFromServer(roomId);
  }
}
