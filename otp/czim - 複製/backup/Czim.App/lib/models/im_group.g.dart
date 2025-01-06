// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'im_group.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

IMGroup _$IMGroupFromJson(Map<String, dynamic> json) => IMGroup(
      json['targetId'] as String?,
      json['announcement'] as String?,
      json['avatar'] as String?,
      json['groupName'] as String?,
      json['chatgroupId'] as String?,
    );

Map<String, dynamic> _$IMGroupToJson(IMGroup instance) => <String, dynamic>{
      'targetId': instance.targetId,
      'announcement': instance.announcement,
      'avatar': instance.avatar,
      'groupName': instance.groupName,
      'chatgroupId': instance.chatgroupId,
    };
