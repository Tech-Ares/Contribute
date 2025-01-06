import 'package:json_annotation/json_annotation.dart';

part 'profile_params.g.dart';

@JsonSerializable()
class ProfileParams {
  final String? nickName;
  final String? mailbox;
  final String? avatar;
  final String? introduce;
  final int? gender;

  ProfileParams(
  {this.nickName, this.mailbox, this.avatar, this.introduce, this.gender});

  factory ProfileParams.fromJson(Map<String, dynamic> json) =>
      _$ProfileParamsFromJson(json);

  Map<String, dynamic> toJson() => _$ProfileParamsToJson(this);
}
