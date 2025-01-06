// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'upgrade_version.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UpgradeVersion _$UpgradeVersionFromJson(Map<String, dynamic> json) =>
    UpgradeVersion(
      id: json['id'] as int?,
      phoneSystem: json['phoneSystem'] as int?,
      versionCode: json['versionCode'] as int?,
      version: json['version'] as String?,
      channel: json['channel'] as String?,
      title: json['title'] as String?,
      content: json['content'] as String?,
      focus: json['focus'] as bool?,
      apkDownLoadUrl: json['apkDownLoadUrl'] as String?,
      iosLinkUrl: json['iosLinkUrl'] as String?,
    );

Map<String, dynamic> _$UpgradeVersionToJson(UpgradeVersion instance) =>
    <String, dynamic>{
      'id': instance.id,
      'phoneSystem': instance.phoneSystem,
      'versionCode': instance.versionCode,
      'version': instance.version,
      'channel': instance.channel,
      'title': instance.title,
      'content': instance.content,
      'focus': instance.focus,
      'apkDownLoadUrl': instance.apkDownLoadUrl,
      'iosLinkUrl': instance.iosLinkUrl,
    };
