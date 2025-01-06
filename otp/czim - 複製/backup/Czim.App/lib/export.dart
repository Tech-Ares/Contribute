import 'package:easy_chat/api/rest/member_rest_client.dart';
import 'package:easy_chat/im/im_manager.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'api/rest/client_rest_client.dart';
import 'api/rest/common_rest_client.dart';

final IMManager imManager = IMManager();

final ClientRestClient clientRestClient =
    ClientRestClient(dio, baseUrl: 'http://47.57.142.43:5108');

final MemberRestClient memberRestClient =
    MemberRestClient(dio, baseUrl: coreConfig.devHost);

final CommonRestClient commonRestClient =
    CommonRestClient(dio, baseUrl: coreConfig.devHost);
