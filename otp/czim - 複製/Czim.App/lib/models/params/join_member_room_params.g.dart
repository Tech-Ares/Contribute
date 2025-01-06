// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'join_member_room_params.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

JoinMemberRoomParams _$JoinMemberRoomParamsFromJson(
        Map<String, dynamic> json) =>
    JoinMemberRoomParams(
      targetIds: (json['targetIds'] as List<dynamic>?)
          ?.map((e) => e as String)
          .toList(),
      chatRoomId: json['chatRoomId'] as String?,
      remarks: json['remarks'] as String?,
    );

Map<String, dynamic> _$JoinMemberRoomParamsToJson(
        JoinMemberRoomParams instance) =>
    <String, dynamic>{
      'targetIds': instance.targetIds,
      'chatRoomId': instance.chatRoomId,
      'remarks': instance.remarks,
    };
