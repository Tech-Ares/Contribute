import 'package:json_annotation/json_annotation.dart';

part 'result_data.g.dart';

@JsonSerializable()
class ResultData {
  int? code;
  String? message;
  dynamic data;

  ResultData({this.code, this.message, this.data});

  factory ResultData.fromJson(Map<String, dynamic> json) => _$ResultDataFromJson(json);
  Map<String, dynamic> toJson() => _$ResultDataToJson(this);

  bool get isOk => code == 0;

  bool get isLogout => code == 403;

  bool get needLogin => code == -1;
}