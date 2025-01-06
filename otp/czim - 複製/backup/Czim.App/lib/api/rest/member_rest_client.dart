import 'package:dio/dio.dart' hide Headers;
import 'package:easy_chat/models/authorization.dart';
import 'package:easy_chat/models/chat_room.dart';
import 'package:easy_chat/models/customer.dart';
import 'package:easy_chat/models/friend.dart';
import 'package:easy_chat/models/im_group.dart';
import 'package:easy_chat/models/im_group_info.dart';
import 'package:easy_chat/models/im_group_member_list.dart';
import 'package:easy_chat/models/params/add_friend_params.dart';
import 'package:easy_chat/models/params/feedback_params.dart';
import 'package:easy_chat/models/params/im_group_info_params.dart';
import 'package:easy_chat/models/params/invite_code_params.dart';
import 'package:easy_chat/models/params/join_member_group_params.dart';
import 'package:easy_chat/models/params/join_member_room_params.dart';
import 'package:easy_chat/models/params/login_params.dart';
import 'package:easy_chat/models/params/profile_params.dart';
import 'package:easy_chat/models/params/register_params.dart';
import 'package:easy_chat/models/params/revoke_message_params.dart';
import 'package:easy_chat/models/params/scan_code_params.dart';
import 'package:easy_chat/models/params/time_info_params.dart';
import 'package:easy_chat/models/params/update_pwd_params.dart';
import 'package:easy_chat/models/user_info.dart';
import 'package:retrofit/http.dart';

part 'member_rest_client.g.dart';

@RestApi()
abstract class MemberRestClient {
  factory MemberRestClient(Dio dio, {String? baseUrl}) = _MemberRestClient;

  @POST('/account/register')
  @Headers(<String, dynamic>{'loading': 'true'})
  Future register(@Body() RegisterParams param);

  @POST('/account/login')
  @Headers(<String, dynamic>{'loading': 'true'})
  Future<Authorization> login(@Body() LoginParams param);

  //
  // @POST('/bsvlogin/timeinfo')
  // Future timeInfo(@Body() TimeParams params);

  @POST('/member/updpwd')
  @Headers(<String, dynamic>{'loading': 'true'})
  Future? restPassword(@Body() UpdatePwdParams param);

  @GET('/member/baseinfo')
  Future<UserInfo> userinfo();

  @POST('/member/updinfo')
  Future update(@Body() ProfileParams params);

  @POST('/account/timeinfo')
  Future timeInfo(@Body() TimeInfoParams params);

  @POST('/member/getexclusive')
  Future<List<Customer>?> customer();

  @POST('/member/getchatroom')
  Future<List<ChatRoom>?> chatRooms();

  @POST('/member/getcontacts')
  Future<List<Friend>?> friends();

  @POST('/member/getchatgroup')
  Future<List<IMGroup>?> groups();

  @POST('/appchat/getuserext')
  Future<List<Friend>?> fetchUsers(@Field() List<String?> targetIds);

  @POST('/appchat/getchatroomext')
  Future<List<Friend>?> fetchChatRooms(@Field() List<String> targetIds);

  @POST('/appchat/getchatgroupext')
  Future<List<Friend>?> fetchChatGroups(@Field() List<String> targetIds);

  @POST('/appchat/msgrecall')
  Future? revokeMessage(@Body() RevokeMessageParams params);

  @POST('/appchat/recallchan')
  Future? revokeRoomMessage(@Body() RevokeMessageParams params);

  @POST('/member/searchcontacts')
  Future<List<Friend>?> searchMembers(@Field() String vagueInfo);

  @POST('/member/addchatroom')
  Future? joinMemberToRoom(@Body() JoinMemberRoomParams params);

  @POST('/member/pullgroup')
  Future? joinMemberToGroup(@Body() JoinMemberGroupParams params);

  @POST('/member/deletgroupuser')
  Future? removeMemberFromGroup(@Body() JoinMemberGroupParams params);

  @POST('/member/addFriend')
  @Headers(<String, dynamic>{'loading': 'true'})
  Future? addFriend(@Body() AddFriendParams params);

  @POST('/member/getbyid')
  Future<Friend?> getMemberInfo(@Field() String memberId);

  @POST('/member/getbyid')
  Future<Friend?> getGroupMember(@Field() String targetId);

  @POST('/member/usereferral')
  Future? fillInviteCode(@Body() InviteCodeParams params);

  @POST('/member/feedback')
  @Headers(<String, dynamic>{'loading': 'true'})
  Future? feedback(@Body() FeedbackParams params);

  @POST('/appchat/getchatgroupinfo')
  Future<IMGroupInfo> imGroupInfo(@Body() IMGroupInfoParams params);

  @POST('/appchat/getchatgroupmember')
  Future<IMGroupMemberList> imGroupMembers(@Body() IMGroupInfoParams params);

  @POST('/member/scanqrcode')
  @Headers(<String, dynamic>{'loading': 'true'})
  Future? scanCode(@Body() ScanCodeParams params);
}
