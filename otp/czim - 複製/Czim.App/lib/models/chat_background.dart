import 'package:json_annotation/json_annotation.dart';

part 'chat_background.g.dart';

@JsonSerializable()
class ChatBackground {
  ///类型
  ///0：内置图片 assets目录
  ///1：本地图片 file，文件磁盘
  ///2：网络图片 url
  final int type;

  ///存放的值
  ///assets文件名
  ///file路径
  ///url路径
  final String? value;

  ///会话
  ///为null则表示全局设置的
  ///否则存放聊天目标ID
  final String? sessionId;

  ChatBackground({required this.type, this.value, this.sessionId});

  factory ChatBackground.fromJson(Map<String, dynamic> json) =>
      _$ChatBackgroundFromJson(json);

  factory ChatBackground.asset(String asset, {String? sessionId}) =>
      ChatBackground(type: 0, value: asset, sessionId: sessionId);

  factory ChatBackground.file(String filePath, {String? sessionId}) =>
      ChatBackground(type: 1, value: filePath, sessionId: sessionId);

  factory ChatBackground.network(String url, {String? sessionId}) =>
      ChatBackground(type: 2, value: url, sessionId: sessionId);

  Map<String, dynamic> toJson() => _$ChatBackgroundToJson(this);

  factory ChatBackground.empty() =>
      ChatBackground(type: 0, value: null, sessionId: null);
}
