import 'package:easy_chat/models/data_list.dart';
import 'package:easy_chat/models/friend.dart';
import 'package:json_annotation/json_annotation.dart';

part 'im_group_member_list.g.dart';

@JsonSerializable()
class IMGroupMemberList {
  final int? count;
  final List<Friend> dataList;

  IMGroupMemberList(this.count, this.dataList);

  factory IMGroupMemberList.fromJson(Map<String, dynamic> json) =>
      _$IMGroupMemberListFromJson(json);

  Map<String, dynamic> toJson() => _$IMGroupMemberListToJson(this);
}
