import 'package:json_annotation/json_annotation.dart';

part 'live_video_params.g.dart';

@JsonSerializable()
class LiveVideoParams {
  final String? liveContent;

  LiveVideoParams({this.liveContent});

  factory LiveVideoParams.fromJson(Map<String, dynamic> json) =>
      _$LiveVideoParamsFromJson(json);

  factory LiveVideoParams.empty() => LiveVideoParams(liveContent: '');

  Map<String, dynamic> toJson() => _$LiveVideoParamsToJson(this);
}
