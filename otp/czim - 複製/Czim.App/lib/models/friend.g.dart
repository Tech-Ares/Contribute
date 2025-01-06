// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'friend.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Friend _$FriendFromJson(Map<String, dynamic> json) => Friend(
      json['targetId'] as String?,
      json['avatar'] as String?,
      json['nickName'] as String?,
      json['introduce'] as String?,
      json['gender'] as int?,
      json['isFriend'] as bool?,
      json['czType'] as int?,
    );

Map<String, dynamic> _$FriendToJson(Friend instance) => <String, dynamic>{
      'targetId': instance.targetId,
      'avatar': instance.avatar,
      'nickName': instance.nickName,
      'introduce': instance.introduce,
      'gender': instance.gender,
      'czType': instance.czType,
      'isFriend': instance.isFriend,
    };
