import 'package:json_annotation/json_annotation.dart';

part 'feedback_params.g.dart';

@JsonSerializable()
class FeedbackParams {
  final String? content;

  FeedbackParams({this.content});

  factory FeedbackParams.fromJson(Map<String, dynamic> json) =>
      _$FeedbackParamsFromJson(json);

  Map<String, dynamic> toJson() => _$FeedbackParamsToJson(this);
}
