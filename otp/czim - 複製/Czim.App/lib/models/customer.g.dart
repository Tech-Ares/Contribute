// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'customer.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Customer _$CustomerFromJson(Map<String, dynamic> json) => Customer(
      json['targetId'] as String?,
      json['avatar'] as String?,
      json['nickName'] as String?,
    );

Map<String, dynamic> _$CustomerToJson(Customer instance) => <String, dynamic>{
      'targetId': instance.targetId,
      'avatar': instance.avatar,
      'nickName': instance.nickName,
    };
