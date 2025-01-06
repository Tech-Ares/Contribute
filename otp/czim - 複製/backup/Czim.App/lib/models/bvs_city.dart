import 'package:json_annotation/json_annotation.dart';

part 'bvs_city.g.dart';

@JsonSerializable()
class BVSCity {
  final String areaCode;
  final String areaName;
  final String parentCode;
  final String abbreCode;
  final String abbreStr;
  final int areaType;

  BVSCity(
      {required this.areaCode,
      required this.areaName,
      required this.parentCode,
      required this.abbreCode,
      required this.abbreStr,
      required this.areaType});

  factory BVSCity.fromJson(Map<String, dynamic> json) =>
      _$BVSCityFromJson(json);

  Map<String, dynamic> toJson() => _$BVSCityToJson(this);
}
