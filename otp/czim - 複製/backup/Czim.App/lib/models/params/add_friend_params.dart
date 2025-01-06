import 'package:json_annotation/json_annotation.dart';

part 'add_friend_params.g.dart';

@JsonSerializable()
class AddFriendParams {
  final String? targetId;
  final String? remarks;

  AddFriendParams({this.targetId, this.remarks});

  factory AddFriendParams.fromJson(Map<String, dynamic> json) =>
      _$AddFriendParamsFromJson(json);

  Map<String, dynamic> toJson() => _$AddFriendParamsToJson(this);
}
