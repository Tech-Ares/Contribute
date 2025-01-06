import 'package:easy_chat/models/bvs_city_model.dart';
import 'package:easy_chat/models/list/list_response.dart';
import 'package:easy_chat/models/live_video.dart';
import 'package:easy_chat/models/params/live_video_params.dart';
import 'package:easy_chat/models/upgrade_params.dart';
import 'package:easy_chat/models/upgrade_version.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

part 'common_rest_client.g.dart';

@RestApi()
abstract class CommonRestClient {
  factory CommonRestClient(Dio dio, {String? baseUrl}) = _CommonRestClient;

  ///地区列表
  @POST('/appsys/checkarea')
  Future<ListResponse<BVSCityModel>> cities();

  ///版本更新
  @POST('/appsys/versioncheck')
  Future<UpgradeVersion?> upgrade(@Body() UpgradeParams params);

  ///直播列表
  @POST('/applive/alllive')
  Future<List<LiveVideo>?> lives(@Body() LiveVideoParams params);
  //
  // ///上传文件
  // @POST('/common/uploads')
  // @Headers(<String, dynamic>{
  //   'Content-Type': 'multipart/form-data',
  // })
  // @MultiPart()
  // Future<List<UploadFile>> uploadFiles(
  //     @Queries() UploadFileParams params, @Part() List<File> files);
}
