// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'authorization.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Authorization _$AuthorizationFromJson(Map<String, dynamic> json) =>
    Authorization(
      tokenType: json['tokenType'] as String,
      token: json['token'] as String,
    );

Map<String, dynamic> _$AuthorizationToJson(Authorization instance) =>
    <String, dynamic>{
      'tokenType': instance.tokenType,
      'token': instance.token,
    };
