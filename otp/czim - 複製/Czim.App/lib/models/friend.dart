import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:json_annotation/json_annotation.dart';

part 'friend.g.dart';

@JsonSerializable()
class Friend {
  final String? targetId;
  final String? avatar;
  final String? nickName;
  final String? introduce;
  final int? gender;
  final int? czType;
  bool? isFriend;

  Friend(this.targetId, this.avatar, this.nickName, this.introduce, this.gender, this.isFriend, this.czType);

  factory Friend.fromJson(Map<String, dynamic> json) => _$FriendFromJson(json);

  Map<String, dynamic> toJson() => _$FriendToJson(this);

  Future? onClick() {
    GlobalController globalController = Get.find();
    if(globalController.emUserInfo?.userId == targetId) {
      Get.toNamed(PageRoutes.profile);
    } else {
      Get.toNamed(PageRoutes.memberHome, arguments: targetId);
    }
  }
}
