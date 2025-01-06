
import 'package:easy_chat/models/contact_model.dart';
import 'package:easy_chat/models/params/page_params.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

abstract class IMRoomService {

  ///从服务器获取聊天室列表
  Future<List<EMChatRoom>?> fetchChatRooms({PageParams? pageParams});

  ///从缓存中获取聊天室列表
  Future<List<EMChatRoom>?> getChatRooms();

  ///获取聊天室详情
  Future<EMChatRoom> getChatRoomInfo(String roomId);
}