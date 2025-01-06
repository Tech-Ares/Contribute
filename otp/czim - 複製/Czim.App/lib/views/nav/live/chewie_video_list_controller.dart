import 'package:chewie/chewie.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:gogoboom_flutter_getx_start/widget/base_loading.dart';
import 'package:video_player/video_player.dart';

///管理视频列表的播放控制器
class ChewieVideoListController {
  final Map<String, ChewieController> _chewieControllers = {};

  ChewieController getChewieController(String url) {
    logger.d('getChewieController ==> $url');
    var chewieController = _chewieControllers[url];
    if (chewieController == null) {
      var _videoPlayerController = VideoPlayerController.network(url,
          videoPlayerOptions: VideoPlayerOptions(mixWithOthers: true));
      chewieController = ChewieController(
          aspectRatio: 16 / 9,
          videoPlayerController: _videoPlayerController,
          deviceOrientationsAfterFullScreen: [
            DeviceOrientation.portraitUp,
            DeviceOrientation.portraitDown
          ],
          deviceOrientationsOnEnterFullScreen: [
            DeviceOrientation.landscapeLeft
          ],
          // isLive: true,
          playbackSpeeds: [],
          allowPlaybackSpeedChanging: false,
          allowedScreenSleep: false,
          autoPlay: false,
          looping: false,
          autoInitialize: true,
          showControlsOnInitialize: false,
          placeholder: loading(),
          showOptions: false);
      _chewieControllers[url] = chewieController;
    }
    return chewieController;
  }

  void play(String url) {
    var chewieController = _chewieControllers[url];
    if (chewieController != null) {
      chewieController.play();
    }
    pauseOther(url);
  }

  Future<void> pause(String url) async {
    var chewieController = _chewieControllers[url];
    if (chewieController != null) {
      await chewieController.pause();
    }
  }

  Future<void> pauseAll() async {
    _chewieControllers.forEach((key, value) {
      if(value.isPlaying) {
        value.pause();
      }
    });
  }

  void stop() {
    _chewieControllers.forEach((key, c) {
      if (c.isPlaying) {
        c.pause();
      }
    });
  }

  void dispose(String url) {
    var chewieController = _chewieControllers[url];
    if (chewieController != null) {
      logger.w('dispose ==> $url');
      chewieController.videoPlayerController.dispose();
      chewieController.dispose();
      _chewieControllers.remove(url);
    }
  }

  Future<void> pauseOther(String url) async {
    // logger.d('pauseOther ==> $_chewieControllers');
    _chewieControllers.forEach((key, c) async {
      if (key != url) {
        if (c.isPlaying) {
          c.videoPlayerController.pause();
        }
      }
    });
  }
}
