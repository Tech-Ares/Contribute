// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'bvs_city_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

BVSCityModel _$BVSCityModelFromJson(Map<String, dynamic> json) => BVSCityModel(
      areaCodes: (json['areaCodes'] as List<dynamic>)
          .map((e) => BVSCity.fromJson(e as Map<String, dynamic>))
          .toList(),
      abbreStr: json['abbreStr'] as String,
    );

Map<String, dynamic> _$BVSCityModelToJson(BVSCityModel instance) =>
    <String, dynamic>{
      'areaCodes': instance.areaCodes,
      'abbreStr': instance.abbreStr,
    };
