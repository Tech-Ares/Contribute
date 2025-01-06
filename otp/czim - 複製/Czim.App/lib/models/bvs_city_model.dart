import 'package:json_annotation/json_annotation.dart';

import 'bvs_city.dart';

part 'bvs_city_model.g.dart';

@JsonSerializable()
class BVSCityModel {
  final List<BVSCity> areaCodes;
  final String abbreStr;

  BVSCityModel({required this.areaCodes, required this.abbreStr});

  factory BVSCityModel.fromJson(Map<String, dynamic> json) =>
      _$BVSCityModelFromJson(json);

  Map<String, dynamic> toJson() => _$BVSCityModelToJson(this);
}
