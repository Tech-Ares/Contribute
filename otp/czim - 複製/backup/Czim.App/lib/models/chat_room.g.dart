// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'chat_room.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ChatRoom _$ChatRoomFromJson(Map<String, dynamic> json) => ChatRoom(
      json['targetId'] as String?,
      json['chatRoomNotice'] as String?,
      json['avatar'] as String?,
      json['nickName'] as String?,
      json['chatRoomId'] as String?,
    );

Map<String, dynamic> _$ChatRoomToJson(ChatRoom instance) => <String, dynamic>{
      'targetId': instance.targetId,
      'chatRoomNotice': instance.chatRoomNotice,
      'avatar': instance.avatar,
      'nickName': instance.nickName,
      'chatRoomId': instance.chatRoomId,
    };
