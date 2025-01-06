import 'package:json_annotation/json_annotation.dart';

part 'customer.g.dart';

@JsonSerializable()
class Customer {
  final String? targetId;
  final String? avatar;
  final String? nickName;

  Customer(this.targetId, this.avatar, this.nickName);

  factory Customer.fromJson(Map<String, dynamic> json) =>
      _$CustomerFromJson(json);

  bool get login => false;

  Map<String, dynamic> toJson() => _$CustomerToJson(this);
}
