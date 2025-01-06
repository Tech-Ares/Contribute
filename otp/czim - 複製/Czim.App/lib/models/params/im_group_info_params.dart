import 'package:json_annotation/json_annotation.dart';

part 'im_group_info_params.g.dart';

@JsonSerializable()
class IMGroupInfoParams {
  final String? targetId;

  IMGroupInfoParams({this.targetId});

  factory IMGroupInfoParams.fromJson(Map<String, dynamic> json) =>
      _$IMGroupInfoParamsFromJson(json);

  Map<String, dynamic> toJson() => _$IMGroupInfoParamsToJson(this);
}
