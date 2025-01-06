import 'package:easy_chat/models/friend.dart';
import 'package:json_annotation/json_annotation.dart';

part 'im_group_info.g.dart';

@JsonSerializable()
class IMGroupInfo {
  final int? count;
  final String? easeChatgroupId;
  final String? groupName;
  final String? groupImg;
  final String? announcement;
  final String? description;
  final Friend? friend;
  List<Friend>? members;

  IMGroupInfo(this.count, this.easeChatgroupId, this.groupName, this.groupImg,
      this.announcement, this.description, this.friend);

  factory IMGroupInfo.fromJson(Map<String, dynamic> json) =>
      _$IMGroupInfoFromJson(json);

  Map<String, dynamic> toJson() => _$IMGroupInfoToJson(this);
}
