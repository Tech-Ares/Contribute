import 'package:json_annotation/json_annotation.dart';

part 'update_pwd_params.g.dart';

@JsonSerializable()
class UpdatePwdParams {
  final String? oldPwd;
  final String? newPwd;
  final String? newPwd2;

  UpdatePwdParams({this.oldPwd, this.newPwd, this.newPwd2});

  factory UpdatePwdParams.fromJson(Map<String, dynamic> json) =>
      _$UpdatePwdParamsFromJson(json);

  Map<String, dynamic> toJson() => _$UpdatePwdParamsToJson(this);
}
