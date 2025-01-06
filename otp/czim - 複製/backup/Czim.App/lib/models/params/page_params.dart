import 'package:json_annotation/json_annotation.dart';

part 'page_params.g.dart';

@JsonSerializable()
class PageParams {
  int page;

  int size;


  PageParams(
      {required this.page,
      required this.size,});

  factory PageParams.fromJson(Map<String, dynamic> json) =>
      _$PageParamsFromJson(json);

  Map<String, dynamic> toJson() => _$PageParamsToJson(this);

  factory PageParams.init() => PageParams(page: 1, size: 10);

  void next() {
    page = page + 1;
  }

  void reset() {
    page = 1;
  }
}
