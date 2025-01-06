import 'package:easy_chat/export.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/friend.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'member_search_state.dart';

class MemberSearchLogic extends BaseController<List<Friend>?> {
  final MemberSearchState _state = MemberSearchState();
  final GlobalController _globalController = Get.find();
  final TextEditingController textController = TextEditingController();
  Worker? searchWork;
  String keyword = '';

  @override
  Future<List<Friend>?> Function() get requestFunc =>
      () => memberRestClient.searchMembers(keyword);

  @override
  void onInit() {
    searchWork = debounce(_state.searchAction, (v) {
      _doMemberSearch();
    }, time: const Duration(seconds: 1));
    super.onInit();
  }

  @override
  void onReady() {
    onRefresh();
    super.onReady();
  }

  @override
  void onClose() {
    searchWork?.dispose();
    super.onClose();
  }

  Future? _doMemberSearch() async {
    String text = textController.text.trim();
    logger.d('_doMemberSearch == $keyword');
    if (text.isNotEmpty) {
      keyword = text;
      reload();
    }
  }

  void inputChange() => _state.searchAction.toggle();

  showTag(int index) {
    var item = indexOfItem<Friend>(index);
    return (item!.isFriend ?? false) ||
        item.targetId == _globalController.emUserInfo?.userId;
  }

  bool isMyself(Friend item) =>
      item.targetId == _globalController.emUserInfo?.userId;
}
