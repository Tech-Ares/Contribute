import 'dart:convert';

import 'package:bruno/bruno.dart';
import 'package:easy_chat/models/result_data.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

class ResponseInterceptor extends Interceptor {
  @override
  void onResponse(
      Response<dynamic> response, ResponseInterceptorHandler handler) {
    final bool showLoading =
        response.requestOptions.headers.containsKey('loading');
    if (showLoading) {
      BrnLoadingDialog.dismiss(Get.context!);
      cancelLoading();
    }
    if (response.statusCode == 200) {
      final dynamic data = response.data;
      if (data == null) {
        throw ApiError(statusMessage: response.statusMessage);
      }
      var result = data;
      if (data is String) {
        result = json.decode(data);
      }
      final ResultData resultData =
          ResultData.fromJson(result as Map<String, dynamic>);
      logger.w(
        '接口访问成功：「code = ${resultData.code}, message  = ${resultData.message}」',
      );
      if (resultData.isOk) {
        response.data = resultData.data;
        if (showLoading) {
          ToastUtil.show(resultData.message);
        }
        super.onResponse(response, handler);
      } else {
        if (resultData.isLogout) {
          // _globalLogic.logout();
          // ToastUtil.show('请先登录！');
          Get.toNamed(PageRoutes.login);
        }
        throw ApiError(statusMessage: resultData.message);
        // if(showLoading) {
        //   throw ApiError(statusMessage: resultData.message);
        // } else {
        //   logger.e(resultData.message);
        // }
      }
    } else {
      throw ApiError(statusMessage: response.statusMessage);
    }
  }

  // @override
  // void onError(DioError err, ErrorInterceptorHandler handler) {
  //   super.onError(err, handler);
  //   // logger.e('$err');
  //   cancelLoading();
  //   // throw ApiError(
  //   //   statusCode: -1,
  //   //   statusMessage: '${err.error}',
  //   // );
  //   if (err is ApiError) {
  //     ToastUtil.error((err.error as ApiError).statusMessage ?? '请求出错');
  //     return;
  //   }
  //   if (err.error is ApiError) {
  //     ToastUtil.error((err.error as ApiError).statusMessage ?? '请求出错');
  //     return;
  //   }
  //   // throw ApiError(
  //   //   statusCode: -1,
  //   //   statusMessage: err.error as String?,
  //   // );
  // }
}
