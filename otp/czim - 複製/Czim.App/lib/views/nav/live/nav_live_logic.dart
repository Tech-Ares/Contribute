import 'package:easy_chat/export.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/live_video.dart';
import 'package:easy_chat/models/params/live_video_params.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

import 'chewie_video_list_controller.dart';
import 'nav_live_state.dart';

class NavLiveLogic extends BaseController<List<LiveVideo>?> {
  final NavLiveState _state = NavLiveState();
  final ChewieVideoListController chewieVideoListController =
      ChewieVideoListController();
  final GlobalController _globalController = Get.find();

  @override
  Future<List<LiveVideo>?> Function() get requestFunc => () async {
        var lives = await commonRestClient.lives(LiveVideoParams.empty());
        lives?[0].videoUrl =
            'http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4';
        lives?[1].videoUrl = 'http://vjs.zencdn.net/v/oceans.mp4';
        // lives?[2].videoUrl = 'https://media.w3.org/2010/05/sintel/trailer.mp4';
        // lives?[3].videoUrl =
        //     'http://mirror.aarnet.edu.au/pub/TED-talks/911Mothers_2010W-480p.mp4';
        // lives?[4].videoUrl =
        //     'https://file-examples-com.github.io/uploads/2017/04/file_example_MP4_480_1_5MG.mp4';
        // lives?[5].videoUrl =
        //     'https://file-examples-com.github.io/uploads/2017/04/file_example_MP4_640_3MG.mp4';
        return lives;
      };

  @override
  void onInit() {
    _globalController.pageIndex.listen((page) {
      if(page != 2) {
        chewieVideoListController.pauseAll();
      }
    });
    super.onInit();
  }

  @override
  void onReady() {
    onRefresh();
    super.onReady();
  }

  @override
  void onPaused() {
    chewieVideoListController.pauseAll();
    super.onPaused();
  }
}
