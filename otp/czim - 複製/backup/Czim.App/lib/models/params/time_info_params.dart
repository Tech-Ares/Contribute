import 'package:json_annotation/json_annotation.dart';

part 'time_info_params.g.dart';

@JsonSerializable()
class TimeInfoParams {
  final String? meId;
  final String? loginModel;
  final String? loginOS;

  TimeInfoParams({this.meId, this.loginModel, this.loginOS});

  factory TimeInfoParams.fromJson(Map<String, dynamic> json) =>
      _$TimeInfoParamsFromJson(json);

  Map<String, dynamic> toJson() => _$TimeInfoParamsToJson(this);
}
