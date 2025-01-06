import 'package:json_annotation/json_annotation.dart';

part 'upload_image.g.dart';

@JsonSerializable()
class UploadImage {
  String? name;
  String? imageUrl;

  UploadImage(this.name, this.imageUrl);

  factory UploadImage.fromJson(Map<String, dynamic> json) =>
      _$UploadImageFromJson(json);

  bool get login => false;

  Map<String, dynamic> toJson() => _$UploadImageToJson(this);
}
