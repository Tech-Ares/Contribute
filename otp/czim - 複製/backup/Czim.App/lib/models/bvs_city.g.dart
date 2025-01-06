// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'bvs_city.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

BVSCity _$BVSCityFromJson(Map<String, dynamic> json) => BVSCity(
      areaCode: json['areaCode'] as String,
      areaName: json['areaName'] as String,
      parentCode: json['parentCode'] as String,
      abbreCode: json['abbreCode'] as String,
      abbreStr: json['abbreStr'] as String,
      areaType: json['areaType'] as int,
    );

Map<String, dynamic> _$BVSCityToJson(BVSCity instance) => <String, dynamic>{
      'areaCode': instance.areaCode,
      'areaName': instance.areaName,
      'parentCode': instance.parentCode,
      'abbreCode': instance.abbreCode,
      'abbreStr': instance.abbreStr,
      'areaType': instance.areaType,
    };
