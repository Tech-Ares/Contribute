import 'package:easy_chat/models/friend.dart';
import 'package:json_annotation/json_annotation.dart';

part 'data_list.g.dart';

@JsonSerializable()
class DataList {
  final List<Friend> dataList;

  DataList(this.dataList);

  factory DataList.fromJson(Map<String, dynamic> json) =>
      _$DataListFromJson(json);

  Map<String, dynamic> toJson() => _$DataListToJson(this);
}
