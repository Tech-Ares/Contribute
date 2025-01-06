import 'package:easy_chat/models/live_video.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'live_item_component.dart';
import 'nav_live_logic.dart';

class LiveCoverComponent extends GetView<NavLiveLogic> {
  final LiveVideo liveVideo;
  final bool isInView;

  const LiveCoverComponent(
      {Key? key, required this.liveVideo, required this.isInView})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    var contentWidget = Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      mainAxisAlignment: MainAxisAlignment.start,
      children: [
        const SizedBox(
          height: 8,
        ),
        Text(
          '${liveVideo.title}',
          style: Get.textTheme.subtitle1!.copyWith(fontWeight: FontWeight.bold),
          maxLines: 2,
          overflow: TextOverflow.ellipsis,
        ),
        // Expanded(child: Container()),
        Text(
          liveVideo.content ?? '',
          maxLines: 1,
          style: Get.textTheme.bodyText2,
          overflow: TextOverflow.ellipsis,
        ),
        const SizedBox(
          height: 4,
        ),
        const SizedBox(
          height: 8,
        ),
      ],
    );
    var composeWidget = Column(
      mainAxisSize: MainAxisSize.min,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Container(
          padding: const EdgeInsets.only(left: 8, top: 12, right: 8),
          child: contentWidget,
        ),
        LiveItemComponent(
          isPlay: isInView,
          chewieVideoListController: controller.chewieVideoListController,
          item: liveVideo,
        )
      ],
    );
    var root = GestureDetector(
      onTap: () => liveVideo.onClick(),
      child: Container(
        color: Colors.white,
        child: composeWidget,
      ),
    );
    return root;
  }
}
