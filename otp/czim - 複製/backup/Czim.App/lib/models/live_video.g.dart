// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'live_video.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

LiveVideo _$LiveVideoFromJson(Map<String, dynamic> json) => LiveVideo(
      json['id'] as String?,
      json['title'] as String?,
      json['crtDate'] as String?,
      json['videoUrl'] as String?,
      json['content'] as String?,
      json['headPicture'] as String?,
      json['liveStatus'] as int?,
      json['giveLikeCount'] as int?,
    );

Map<String, dynamic> _$LiveVideoToJson(LiveVideo instance) => <String, dynamic>{
      'id': instance.id,
      'title': instance.title,
      'crtDate': instance.crtDate,
      'videoUrl': instance.videoUrl,
      'content': instance.content,
      'headPicture': instance.headPicture,
      'liveStatus': instance.liveStatus,
      'giveLikeCount': instance.giveLikeCount,
    };
