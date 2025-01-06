// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'scan_code_params.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ScanCodeParams _$ScanCodeParamsFromJson(Map<String, dynamic> json) =>
    ScanCodeParams(
      qrCode: json['qrCode'] as String?,
      czType: json['czType'] as int?,
    );

Map<String, dynamic> _$ScanCodeParamsToJson(ScanCodeParams instance) =>
    <String, dynamic>{
      'qrCode': instance.qrCode,
      'czType': instance.czType,
    };
