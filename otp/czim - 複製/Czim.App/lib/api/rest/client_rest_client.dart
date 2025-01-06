import 'dart:io';

import 'package:dio/dio.dart' hide Headers;
import 'package:easy_chat/models/upload_image.dart';
import 'package:retrofit/http.dart';

part 'client_rest_client.g.dart';

@RestApi()
abstract class ClientRestClient {
  factory ClientRestClient(Dio dio, {String? baseUrl}) = _ClientRestClient;

  ///上传图片
  @POST('/files/upload/bsvinfo')
  @Headers(<String, dynamic>{
    'Content-Type': 'multipart/form-data',
    'loading': 'true'
  })
  @MultiPart()
  Future<List<UploadImage>> uploadFiles(@Part() List<File> files);

  ///上传图片
  @POST('/files/upload/bsvinfo')
  @Headers(<String, dynamic>{
    'Content-Type': 'multipart/form-data',
  })
  @MultiPart()
  Future<List<UploadImage>> chatUpload(@Part() List<File> files);

}
