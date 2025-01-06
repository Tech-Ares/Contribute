// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'im_group_member_list.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

IMGroupMemberList _$IMGroupMemberListFromJson(Map<String, dynamic> json) =>
    IMGroupMemberList(
      json['count'] as int?,
      (json['dataList'] as List<dynamic>)
          .map((e) => Friend.fromJson(e as Map<String, dynamic>))
          .toList(),
    );

Map<String, dynamic> _$IMGroupMemberListToJson(IMGroupMemberList instance) =>
    <String, dynamic>{
      'count': instance.count,
      'dataList': instance.dataList,
    };
