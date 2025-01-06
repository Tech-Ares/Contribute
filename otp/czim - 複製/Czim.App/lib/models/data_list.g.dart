// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'data_list.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

DataList _$DataListFromJson(Map<String, dynamic> json) => DataList(
      (json['dataList'] as List<dynamic>)
          .map((e) => Friend.fromJson(e as Map<String, dynamic>))
          .toList(),
    );

Map<String, dynamic> _$DataListToJson(DataList instance) => <String, dynamic>{
      'dataList': instance.dataList,
    };
