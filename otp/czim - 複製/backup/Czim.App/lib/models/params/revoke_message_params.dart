import 'package:json_annotation/json_annotation.dart';

part 'revoke_message_params.g.dart';

@JsonSerializable()
class RevokeMessageParams {
  final String? receiveId;
  final String? msgId;

  RevokeMessageParams({this.receiveId, this.msgId});

  factory RevokeMessageParams.fromJson(Map<String, dynamic> json) =>
      _$RevokeMessageParamsFromJson(json);

  Map<String, dynamic> toJson() => _$RevokeMessageParamsToJson(this);
}
