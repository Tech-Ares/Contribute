// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'join_member_group_params.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

JoinMemberGroupParams _$JoinMemberGroupParamsFromJson(
        Map<String, dynamic> json) =>
    JoinMemberGroupParams(
      memberIds: (json['memberIds'] as List<dynamic>?)
          ?.map((e) => e as String)
          .toList(),
      chatgroupId: json['chatgroupId'] as String?,
      remarks: json['remarks'] as String?,
    );

Map<String, dynamic> _$JoinMemberGroupParamsToJson(
        JoinMemberGroupParams instance) =>
    <String, dynamic>{
      'memberIds': instance.memberIds,
      'chatgroupId': instance.chatgroupId,
      'remarks': instance.remarks,
    };
