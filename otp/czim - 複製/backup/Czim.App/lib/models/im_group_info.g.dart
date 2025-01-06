// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'im_group_info.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

IMGroupInfo _$IMGroupInfoFromJson(Map<String, dynamic> json) => IMGroupInfo(
      json['count'] as int?,
      json['easeChatgroupId'] as String?,
      json['groupName'] as String?,
      json['groupImg'] as String?,
      json['announcement'] as String?,
      json['description'] as String?,
      json['friend'] == null
          ? null
          : Friend.fromJson(json['friend'] as Map<String, dynamic>),
    )..members = (json['members'] as List<dynamic>?)
        ?.map((e) => Friend.fromJson(e as Map<String, dynamic>))
        .toList();

Map<String, dynamic> _$IMGroupInfoToJson(IMGroupInfo instance) =>
    <String, dynamic>{
      'count': instance.count,
      'easeChatgroupId': instance.easeChatgroupId,
      'groupName': instance.groupName,
      'groupImg': instance.groupImg,
      'announcement': instance.announcement,
      'description': instance.description,
      'friend': instance.friend,
      'members': instance.members,
    };
