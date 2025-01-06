// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'upgrade_params.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UpgradeParams _$UpgradeParamsFromJson(Map<String, dynamic> json) =>
    UpgradeParams(
      code: json['code'] as int?,
      phoneSystem: json['phoneSystem'] as int?,
      channel: json['channel'] as String?,
      version: json['version'] as String?,
    );

Map<String, dynamic> _$UpgradeParamsToJson(UpgradeParams instance) =>
    <String, dynamic>{
      'code': instance.code,
      'phoneSystem': instance.phoneSystem,
      'channel': instance.channel,
      'version': instance.version,
    };
