import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:pull_to_refresh/pull_to_refresh.dart';

///dart template stateful
class BasePullToRefresh extends StatelessWidget {
  final Widget child;
  final VoidCallback? onLoading;
  final VoidCallback? onRefresh;
  final RefreshController refreshController;
  final bool? enableLoadMore;
  final bool? enableRefresh;

  const BasePullToRefresh({
    Key? key,
    required this.child,
    required this.refreshController,
    required this.onRefresh,
    this.enableRefresh,
    this.onLoading,
    this.enableLoadMore,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return RefreshConfiguration(
      headerBuilder: () => _newHeader(context),
      child: Container(child: _refreshWidget()),
    );
  }

  Widget _newHeader(BuildContext context) {
    // int theme = state.theme.value;
    return const ClassicHeader();
  }

  Widget _refreshWidget() {
    return SmartRefresher(
      onRefresh: onRefresh,
      onLoading: onLoading,
      enablePullUp: enableLoadMore ?? true,
      enablePullDown: enableRefresh ?? true,
      controller: refreshController,
      footer: const ClassicFooter(),
      child: child,
    );
  }
}
