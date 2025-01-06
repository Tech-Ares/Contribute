import 'package:json_annotation/json_annotation.dart';

part 'em_user_ext.g.dart';

@JsonSerializable()
class EmUserExt {
  String? city;

  EmUserExt(
    this.city,
  );

  factory EmUserExt.fromJson(Map<String, dynamic> json) =>
      _$EmUserExtFromJson(json);

  bool get login => false;

  Map<String, dynamic> toJson() => _$EmUserExtToJson(this);
}
