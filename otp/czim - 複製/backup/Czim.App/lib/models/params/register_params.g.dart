// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'register_params.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

RegisterParams _$RegisterParamsFromJson(Map<String, dynamic> json) =>
    RegisterParams(
      loginName: json['loginName'] as String,
      nickName: json['nickName'] as String,
      meId: json['meId'] as String,
      passWord: json['passWord'] as String,
      passwordTwo: json['passwordTwo'] as String,
      referralCode: json['referralCode'] as String,
      vCode: json['vCode'] as String,
    );

Map<String, dynamic> _$RegisterParamsToJson(RegisterParams instance) =>
    <String, dynamic>{
      'loginName': instance.loginName,
      'nickName': instance.nickName,
      'meId': instance.meId,
      'passWord': instance.passWord,
      'passwordTwo': instance.passwordTwo,
      'vCode': instance.vCode,
      'referralCode': instance.referralCode,
    };
