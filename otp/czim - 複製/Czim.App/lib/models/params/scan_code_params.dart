import 'package:json_annotation/json_annotation.dart';

part 'scan_code_params.g.dart';

@JsonSerializable()
class ScanCodeParams {
  final String? qrCode;
  final int? czType;

  ScanCodeParams({this.qrCode, this.czType});

  factory ScanCodeParams.fromJson(Map<String, dynamic> json) =>
      _$ScanCodeParamsFromJson(json);

  Map<String, dynamic> toJson() => _$ScanCodeParamsToJson(this);
}
