import 'package:json_annotation/json_annotation.dart';

part 'upgrade_params.g.dart';

@JsonSerializable()
class UpgradeParams {
  final int? code;
  final int? phoneSystem;
  final String? channel;
  final String? version;

  UpgradeParams({this.code, this.phoneSystem, this.channel, this.version});

  factory UpgradeParams.fromJson(Map<String, dynamic> json) =>
      _$UpgradeParamsFromJson(json);

  bool get login => false;

  Map<String, dynamic> toJson() => _$UpgradeParamsToJson(this);
}
