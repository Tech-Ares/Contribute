// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'profile_params.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ProfileParams _$ProfileParamsFromJson(Map<String, dynamic> json) =>
    ProfileParams(
      nickName: json['nickName'] as String?,
      mailbox: json['mailbox'] as String?,
      avatar: json['avatar'] as String?,
      introduce: json['introduce'] as String?,
      gender: json['gender'] as int?,
    );

Map<String, dynamic> _$ProfileParamsToJson(ProfileParams instance) =>
    <String, dynamic>{
      'nickName': instance.nickName,
      'mailbox': instance.mailbox,
      'avatar': instance.avatar,
      'introduce': instance.introduce,
      'gender': instance.gender,
    };
