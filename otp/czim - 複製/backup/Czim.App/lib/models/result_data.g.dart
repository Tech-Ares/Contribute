// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'result_data.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ResultData _$ResultDataFromJson(Map<String, dynamic> json) => ResultData(
      code: json['code'] as int?,
      message: json['message'] as String?,
      data: json['data'],
    );

Map<String, dynamic> _$ResultDataToJson(ResultData instance) =>
    <String, dynamic>{
      'code': instance.code,
      'message': instance.message,
      'data': instance.data,
    };
