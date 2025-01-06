import 'package:json_annotation/json_annotation.dart';

part 'live_video.g.dart';

@JsonSerializable()
class LiveVideo {
  final String? id;
  final String? title;
  final String? crtDate;
  String? videoUrl;
  final String? content;
  final String? headPicture;
  final int? liveStatus;
  final int? giveLikeCount;

  LiveVideo(this.id, this.title, this.crtDate, this.videoUrl, this.content,
      this.headPicture, this.liveStatus, this.giveLikeCount);

  factory LiveVideo.fromJson(Map<String, dynamic> json) =>
      _$LiveVideoFromJson(json);

  Map<String, dynamic> toJson() => _$LiveVideoToJson(this);

  onClick() {}
}
