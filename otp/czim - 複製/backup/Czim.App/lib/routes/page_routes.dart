import 'package:easy_chat/views/about/about_binding.dart';
import 'package:easy_chat/views/about/about_view.dart';
import 'package:easy_chat/views/chat/chat_binding.dart';
import 'package:easy_chat/views/chat/chat_view.dart';
import 'package:easy_chat/views/chat/setup/chatroom/chat_room_setup_binding.dart';
import 'package:easy_chat/views/chat/setup/chatroom/chat_room_setup_view.dart';
import 'package:easy_chat/views/chat/setup/group/group_chat_setup_binding.dart';
import 'package:easy_chat/views/chat/setup/group/group_chat_setup_view.dart';
import 'package:easy_chat/views/chat/setup/single/single_chat_setup_binding.dart';
import 'package:easy_chat/views/chat/setup/single/single_chat_setup_view.dart';
import 'package:easy_chat/views/citypick/city_pick_binding.dart';
import 'package:easy_chat/views/citypick/city_pick_view.dart';
import 'package:easy_chat/views/feeback/feedback_binding.dart';
import 'package:easy_chat/views/feeback/feedback_view.dart';
import 'package:easy_chat/views/group/mylist/my_groups_binding.dart';
import 'package:easy_chat/views/group/mylist/my_groups_view.dart';
import 'package:easy_chat/views/index/index_binding.dart';
import 'package:easy_chat/views/index/index_view.dart';
import 'package:easy_chat/views/input/common_input_binding.dart';
import 'package:easy_chat/views/input/common_input_view.dart';
import 'package:easy_chat/views/login/login_binding.dart';
import 'package:easy_chat/views/login/login_view.dart';
import 'package:easy_chat/views/member/addfriend/add_friend_binding.dart';
import 'package:easy_chat/views/member/addfriend/add_friend_view.dart';
import 'package:easy_chat/views/member/apply/member_apply_binding.dart';
import 'package:easy_chat/views/member/apply/member_apply_view.dart';
import 'package:easy_chat/views/member/home/member_home_binding.dart';
import 'package:easy_chat/views/member/home/member_home_view.dart';
import 'package:easy_chat/views/member/newfriend/new_friend_binding.dart';
import 'package:easy_chat/views/member/newfriend/new_friend_view.dart';
import 'package:easy_chat/views/member/search/member_search_binding.dart';
import 'package:easy_chat/views/member/search/member_search_view.dart';
import 'package:easy_chat/views/member/select/select_member_binding.dart';
import 'package:easy_chat/views/member/select/select_member_view.dart';
import 'package:easy_chat/views/profile/profile_binding.dart';
import 'package:easy_chat/views/profile/profile_view.dart';
import 'package:easy_chat/views/qrcode/q_r_code_binding.dart';
import 'package:easy_chat/views/qrcode/q_r_code_view.dart';
import 'package:easy_chat/views/register/register_binding.dart';
import 'package:easy_chat/views/register/register_view.dart';
import 'package:easy_chat/views/room/chat_room_binding.dart';
import 'package:easy_chat/views/room/chat_room_view.dart';
import 'package:easy_chat/views/scan/scan_binding.dart';
import 'package:easy_chat/views/scan/scan_view.dart';
import 'package:easy_chat/views/search/global_search_binding.dart';
import 'package:easy_chat/views/search/global_search_view.dart';
import 'package:easy_chat/views/setup/chat/background/chat_background_binding.dart';
import 'package:easy_chat/views/setup/chat/background/chat_background_view.dart';
import 'package:easy_chat/views/setup/chat/background/select/chat_background_select_binding.dart';
import 'package:easy_chat/views/setup/chat/background/select/chat_background_select_view.dart';
import 'package:easy_chat/views/setup/generate/blacklist/blacklist_binding.dart';
import 'package:easy_chat/views/setup/generate/blacklist/blacklist_view.dart';
import 'package:easy_chat/views/setup/generate/generate_setup_binding.dart';
import 'package:easy_chat/views/setup/generate/generate_setup_view.dart';
import 'package:easy_chat/views/setup/message/message_setup_binding.dart';
import 'package:easy_chat/views/setup/message/message_setup_view.dart';
import 'package:easy_chat/views/setup/resetpwd/reset_pwd_binding.dart';
import 'package:easy_chat/views/setup/resetpwd/reset_pwd_view.dart';
import 'package:easy_chat/views/setup/setup_binding.dart';
import 'package:easy_chat/views/setup/setup_view.dart';
import 'package:easy_chat/views/splash/splash_view.dart';
import 'package:get/get.dart';

import 'middleware/router_auth.dart';

class PageRoutes {
  static const String index = '/';
  static const String splash = '/splash';
  static const String login = '/login';
  static const String register = '/register';
  static const String forget = '/forget';
  static const String resetPwd = '/setup/reset';
  static const String profile = '/profile';
  static const String setup = '/setup';
  static const String messageSetup = '/setup/message';
  static const String generateSetup = '/setup/generate';
  static const String about = '/about';
  static const String chat = '/chat';
  static const String groupList = '/group/list';
  static const String roomList = '/room/list';
  static const String qrcode = '/qrcode';
  static const String feedback = '/feedback';
  static const String selectMember = '/member/select';
  static const String memberHome = '/member/home';
  static const String memberSearch = '/member/search';
  static const String singChatSetup = '/chat/setup';
  static const String groupSetup = '/group/setup';
  static const String roomSetup = '/room/setup';
  static const String addFriend = '/add/friend';
  static const String applyFriend = '/apply/friend';
  static const String scan = '/scan';
  static const String search = '/search';
  static const String commonInput = '/common/input';
  static const String cityPicker = '/pick/city';
  static const String newFriend = '/new/friend';
  static const String blacklist = '/blacklist';
  static const String backgroundSetup = '/background/setup';
  static const String backgroundSelect = '/background/select';

  static final List<GetPage> getRoutes = <GetPage>[
    GetPage(name: splash, page: () => SplashPage()),
    GetPage(
        name: login, page: () => const LoginPage(), binding: LoginBinding()),
    GetPage(
        name: register,
        page: () => const RegisterPage(),
        binding: RegisterBinding()),
    GetPage(
        name: resetPwd,
        page: () => const ResetPwdPage(),
        binding: ResetPwdBinding()),
    GetPage(
      transition: Transition.fadeIn,
      name: index,
      page: () => IndexPage(),
      binding: IndexBinding(),
    ),
    GetPage(name: chat, page: () => ChatPage(), binding: ChatBinding()),
    GetPage(
        name: profile,
        page: () => const ProfilePage(),
        binding: ProfileBinding()),
    GetPage(
        name: qrcode, page: () => const QRCodePage(), binding: QRCodeBinding()),
    GetPage(
        name: setup, page: () => const SetupPage(), binding: SetupBinding()),
    GetPage(
        name: messageSetup,
        page: () => const MessageSetupPage(),
        binding: MessageSetupBinding()),
    GetPage(
        name: generateSetup,
        page: () => const GenerateSetupPage(),
        binding: GenerateSetupBinding()),
    GetPage(
        name: feedback,
        page: () => const FeedbackPage(),
        binding: FeedbackBinding()),
    GetPage(
        name: about, page: () => const AboutPage(), binding: AboutBinding()),
    GetPage(
        name: groupList,
        page: () => const MyGroupsPage(),
        binding: MyGroupsBinding()),
    GetPage(
        name: roomList,
        page: () => const ChatRoomPage(),
        binding: ChatRoomBinding()),
    GetPage(
        name: selectMember,
        page: () => const SelectMemberPage(),
        binding: SelectMemberBinding()),
    GetPage(
        name: memberHome,
        page: () => const MemberHomePage(),
        binding: MemberHomeBinding()),
    GetPage(
        name: singChatSetup,
        page: () => const SingleChatSetupPage(),
        binding: SingleChatSetupBinding()),
    GetPage(
        name: groupSetup,
        page: () => const GroupChatSetupPage(),
        binding: GroupChatSetupBinding()),
    GetPage(
        name: roomSetup,
        page: () => const ChatRoomSetupPage(),
        binding: ChatRoomSetupBinding()),
    GetPage(
        name: addFriend,
        page: () => const AddFriendPage(),
        binding: AddFriendBinding()),
    GetPage(
        name: applyFriend,
        page: () => const MemberApplyPage(),
        binding: MemberApplyBinding()),
    GetPage(name: scan, page: () => const ScanPage(), binding: ScanBinding()),
    GetPage(
        name: search,
        page: () => const GlobalSearchPage(),
        binding: GlobalSearchBinding()),
    GetPage(
        name: memberSearch,
        page: () => const MemberSearchPage(),
        binding: MemberSearchBinding()),
    GetPage(
        name: commonInput,
        page: () => const CommonInputPage(),
        binding: CommonInputBinding()),
    GetPage(
        name: cityPicker,
        page: () => const CityPickPage(),
        binding: CityPickBinding()),
    GetPage(
        name: newFriend,
        page: () => const NewFriendPage(),
        binding: NewFriendBinding()),
    GetPage(
        name: blacklist,
        page: () => const BlacklistPage(),
        binding: BlacklistBinding()),
    GetPage(
        name: backgroundSetup,
        page: () => const ChatBackgroundPage(),
        binding: ChatBackgroundBinding()),
    GetPage(
        name: backgroundSelect,
        page: () => const ChatBackgroundSelectPage(),
        binding: ChatBackgroundSelectBinding()),
  ];

  static GetMiddleware authMiddleware = RouteAuthMiddleware(priority: 1);
}
