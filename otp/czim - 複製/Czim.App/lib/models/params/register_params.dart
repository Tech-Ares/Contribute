import 'package:json_annotation/json_annotation.dart';

part 'register_params.g.dart';

@JsonSerializable()
class RegisterParams {
  final String loginName;
  final String nickName;
  final String meId;
  final String passWord;
  final String passwordTwo;
  final String vCode;
  final String referralCode;

  RegisterParams(
      {required this.loginName,
      required this.nickName,
      required this.meId,
      required this.passWord,
      required this.passwordTwo,
      required this.referralCode,
      required this.vCode});

  factory RegisterParams.fromJson(Map<String, dynamic> json) =>
      _$RegisterParamsFromJson(json);

  Map<String, dynamic> toJson() => _$RegisterParamsToJson(this);
}
