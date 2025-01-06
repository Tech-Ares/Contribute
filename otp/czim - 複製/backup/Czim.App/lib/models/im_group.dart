import 'package:json_annotation/json_annotation.dart';

part 'im_group.g.dart';

@JsonSerializable()
class IMGroup {

  final String? targetId;
  final String? announcement;
  final String? avatar;
  final String? groupName;
  final String? chatgroupId;

  IMGroup(this.targetId, this.announcement, this.avatar, this.groupName, this.chatgroupId);
  
  factory IMGroup.fromJson(Map<String, dynamic> json) =>
      _$IMGroupFromJson(json);

  Map<String, dynamic> toJson() => _$IMGroupToJson(this);
}