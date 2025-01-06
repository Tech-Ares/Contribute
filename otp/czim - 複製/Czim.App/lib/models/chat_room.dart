import 'package:json_annotation/json_annotation.dart';

part 'chat_room.g.dart';

@JsonSerializable()
class ChatRoom {

  final String? targetId;
  final String? chatRoomNotice;
  final String? avatar;
  final String? nickName;
  final String? chatRoomId;

  ChatRoom(this.targetId, this.chatRoomNotice, this.avatar, this.nickName, this.chatRoomId);
  
  factory ChatRoom.fromJson(Map<String, dynamic> json) =>
      _$ChatRoomFromJson(json);

  Map<String, dynamic> toJson() => _$ChatRoomToJson(this);
}