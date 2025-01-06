import 'package:json_annotation/json_annotation.dart';

part 'invite_code_params.g.dart';

@JsonSerializable()
class InviteCodeParams {
  final String? referralCode;

  InviteCodeParams({this.referralCode});

  factory InviteCodeParams.fromJson(Map<String, dynamic> json) =>
      _$InviteCodeParamsFromJson(json);

  Map<String, dynamic> toJson() => _$InviteCodeParamsToJson(this);
}
