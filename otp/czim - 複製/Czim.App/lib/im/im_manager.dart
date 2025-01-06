import 'package:easy_chat/im/im_chat_service.dart';
import 'package:easy_chat/im/im_contact_service.dart';
import 'package:easy_chat/im/im_group_service.dart';
import 'package:easy_chat/im/im_room_service.dart';
import 'package:easy_chat/im/im_user_service.dart';
import 'package:easy_chat/im/impl/im_chat_service_impl.dart';
import 'package:easy_chat/im/impl/im_contact_service_impl.dart';
import 'package:easy_chat/im/impl/im_group_service_impl.dart';
import 'package:easy_chat/im/impl/im_room_service_impl.dart';
import 'package:easy_chat/im/impl/im_user_service_impl.dart';

import 'im_client_service.dart';
import 'impl/im_client_service_impl.dart';

class IMManager {
  final IMClientService imClientService = IMClientServiceImpl();
  final IMContactService imContactService = IMContactServiceImpl();
  final IMUserService imUserService = IMUserServiceImpl();
  final IMChatService imChatService = IMChatServiceImpl();
  final IMGroupService imGroupService = IMGroupServiceImpl();
  final IMRoomService imRoomService = IMRoomServiceImpl();
}
