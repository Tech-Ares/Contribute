// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'common_rest_client.dart';

// **************************************************************************
// RetrofitGenerator
// **************************************************************************

class _CommonRestClient implements CommonRestClient {
  _CommonRestClient(this._dio, {this.baseUrl});

  final Dio _dio;

  String? baseUrl;

  @override
  Future<ListResponse<BVSCityModel>> cities() async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{};
    final _headers = <String, dynamic>{};
    final _data = <String, dynamic>{};
    final _result = await _dio.fetch<Map<String, dynamic>>(
        _setStreamType<ListResponse<BVSCityModel>>(
            Options(method: 'POST', headers: _headers, extra: _extra)
                .compose(_dio.options, '/appsys/checkarea',
                    queryParameters: queryParameters, data: _data)
                .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value = ListResponse<BVSCityModel>.fromJson(_result.data!);
    return value;
  }

  @override
  Future<UpgradeVersion?> upgrade(params) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{};
    final _headers = <String, dynamic>{};
    final _data = <String, dynamic>{};
    _data.addAll(params.toJson());
    final _result = await _dio.fetch<Map<String, dynamic>?>(
        _setStreamType<UpgradeVersion>(
            Options(method: 'POST', headers: _headers, extra: _extra)
                .compose(_dio.options, '/appsys/versioncheck',
                    queryParameters: queryParameters, data: _data)
                .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    final value =
        _result.data == null ? null : UpgradeVersion.fromJson(_result.data!);
    return value;
  }

  @override
  Future<List<LiveVideo>?> lives(params) async {
    const _extra = <String, dynamic>{};
    final queryParameters = <String, dynamic>{};
    final _headers = <String, dynamic>{};
    final _data = <String, dynamic>{};
    _data.addAll(params.toJson());
    final _result = await _dio.fetch<List<dynamic>>(
        _setStreamType<List<LiveVideo>>(
            Options(method: 'POST', headers: _headers, extra: _extra)
                .compose(_dio.options, '/applive/alllive',
                    queryParameters: queryParameters, data: _data)
                .copyWith(baseUrl: baseUrl ?? _dio.options.baseUrl)));
    var value = _result.data
        ?.map((dynamic i) => LiveVideo.fromJson(i as Map<String, dynamic>))
        .toList();
    return value;
  }

  RequestOptions _setStreamType<T>(RequestOptions requestOptions) {
    if (T != dynamic &&
        !(requestOptions.responseType == ResponseType.bytes ||
            requestOptions.responseType == ResponseType.stream)) {
      if (T == String) {
        requestOptions.responseType = ResponseType.plain;
      } else {
        requestOptions.responseType = ResponseType.json;
      }
    }
    return requestOptions;
  }
}
