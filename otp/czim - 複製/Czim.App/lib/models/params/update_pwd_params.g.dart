// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'update_pwd_params.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UpdatePwdParams _$UpdatePwdParamsFromJson(Map<String, dynamic> json) =>
    UpdatePwdParams(
      oldPwd: json['oldPwd'] as String?,
      newPwd: json['newPwd'] as String?,
      newPwd2: json['newPwd2'] as String?,
    );

Map<String, dynamic> _$UpdatePwdParamsToJson(UpdatePwdParams instance) =>
    <String, dynamic>{
      'oldPwd': instance.oldPwd,
      'newPwd': instance.newPwd,
      'newPwd2': instance.newPwd2,
    };
