// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'chat_background.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ChatBackground _$ChatBackgroundFromJson(Map<String, dynamic> json) =>
    ChatBackground(
      type: json['type'] as int,
      value: json['value'] as String?,
      sessionId: json['sessionId'] as String?,
    );

Map<String, dynamic> _$ChatBackgroundToJson(ChatBackground instance) =>
    <String, dynamic>{
      'type': instance.type,
      'value': instance.value,
      'sessionId': instance.sessionId,
    };
