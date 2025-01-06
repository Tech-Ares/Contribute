import 'package:json_annotation/json_annotation.dart';

part 'login_params.g.dart';

@JsonSerializable()
class LoginParams {
  final String loginName;
  final String meId;
  final String passWord;
  final String areaCode;

  LoginParams(
      {required this.loginName,
      required this.meId,
      required this.passWord,
      required this.areaCode});

  factory LoginParams.fromJson(Map<String, dynamic> json) =>
      _$LoginParamsFromJson(json);

  Map<String, dynamic> toJson() => _$LoginParamsToJson(this);
}
