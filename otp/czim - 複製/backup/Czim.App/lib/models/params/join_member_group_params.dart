import 'package:json_annotation/json_annotation.dart';

part 'join_member_group_params.g.dart';

@JsonSerializable()
class JoinMemberGroupParams {
  final List<String>? memberIds;
  final String? chatgroupId;
  final String? remarks;

  JoinMemberGroupParams({this.memberIds, this.chatgroupId, this.remarks});

  factory JoinMemberGroupParams.fromJson(Map<String, dynamic> json) =>
      _$JoinMemberGroupParamsFromJson(json);

  Map<String, dynamic> toJson() => _$JoinMemberGroupParamsToJson(this);
}
