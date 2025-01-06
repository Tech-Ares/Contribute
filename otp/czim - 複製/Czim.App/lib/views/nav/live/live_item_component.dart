import 'package:chewie/chewie.dart';
import 'package:easy_chat/models/live_video.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/services.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:video_player/video_player.dart';

import 'chewie_video_list_controller.dart';

class LiveItemComponent extends StatefulWidget {
  final LiveVideo item;
  final ChewieVideoListController chewieVideoListController;
  final bool isPlay;

  const LiveItemComponent(
      {Key? key,
      required this.item,
      required this.chewieVideoListController,
      required this.isPlay})
      : super(key: key);

  @override
  State<StatefulWidget> createState() => _LiveItemComponentState();
}

class _LiveItemComponentState extends State<LiveItemComponent> {
  late VideoPlayerController _videoPlayerController;
  late ChewieController _chewieController;
  late String url;

  @override
  void initState() {
    logger.d('initState ==> ${widget.item.title}');
    url = widget.item.videoUrl!;
    _initVideo();
    super.initState();
  }

  _initVideo() {
    _chewieController =
        widget.chewieVideoListController.getChewieController(url);
    _videoPlayerController = _chewieController.videoPlayerController;
    _videoPlayerController.addListener(_listener);
    _chewieController.addListener(() {
      //判断是否全屏
      if (!_chewieController.isFullScreen) {
        SystemChrome.setPreferredOrientations([
          // 强制竖屏
          DeviceOrientation.portraitUp,
          DeviceOrientation.portraitDown
        ]);
      } else {}
    });
  }

  void _listener() {
    WidgetsBinding.instance?.addPostFrameCallback((__) {
      if (mounted) {
        setState(() {});
        if (_videoPlayerController.value.isPlaying) {
          widget.chewieVideoListController.pauseOther(url);
        }
      }
    });
  }

  @override
  void didUpdateWidget(LiveItemComponent oldWidget) {
    if (oldWidget.isPlay != widget.isPlay) {
      if (widget.isPlay) {
        logger.d('didUpdateWidget play ==> $url');
        _chewieController.play();
      } else {
        logger.d('didUpdateWidget pause ==> $url');
        if (_chewieController.isPlaying) {
          _chewieController.pause();
        }
      }
    }
    super.didUpdateWidget(oldWidget);
  }

  @override
  void dispose() {
    logger.d('dispose ==> ${widget.item.title}');
    _videoPlayerController.removeListener(_listener);
    widget.chewieVideoListController.dispose(url);
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AspectRatio(
      aspectRatio: 16 / 9,
      child: Chewie(
        controller: _chewieController,
      ),
    );
  }
}
