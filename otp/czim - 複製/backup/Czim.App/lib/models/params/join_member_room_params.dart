import 'package:json_annotation/json_annotation.dart';

part 'join_member_room_params.g.dart';

@JsonSerializable()
class JoinMemberRoomParams {
  final List<String>? targetIds;
  final String? chatRoomId;
  final String? remarks;

  JoinMemberRoomParams({this.targetIds, this.chatRoomId, this.remarks});

  factory JoinMemberRoomParams.fromJson(Map<String, dynamic> json) =>
      _$JoinMemberRoomParamsFromJson(json);

  Map<String, dynamic> toJson() => _$JoinMemberRoomParamsToJson(this);
}
