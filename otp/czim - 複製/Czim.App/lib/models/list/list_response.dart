class ListResponse<T> {
  int? count;
  List<T>? dataList;

  ListResponse.from(Map<String, dynamic> parsedJson)
      : count = parsedJson['count'] as int?;

  factory ListResponse.fromJson(Map<String, dynamic> json) {
    switch (T) {
      // case BVSVideoInfo:
      //   return HomeVideosResponse.fromJson(json) as ListResponse<T>;
    }
    throw UnimplementedError();
  }
}
