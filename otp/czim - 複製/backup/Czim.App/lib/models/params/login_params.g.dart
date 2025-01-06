// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'login_params.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

LoginParams _$LoginParamsFromJson(Map<String, dynamic> json) => LoginParams(
      loginName: json['loginName'] as String,
      meId: json['meId'] as String,
      passWord: json['passWord'] as String,
      areaCode: json['areaCode'] as String,
    );

Map<String, dynamic> _$LoginParamsToJson(LoginParams instance) =>
    <String, dynamic>{
      'loginName': instance.loginName,
      'meId': instance.meId,
      'passWord': instance.passWord,
      'areaCode': instance.areaCode,
    };
