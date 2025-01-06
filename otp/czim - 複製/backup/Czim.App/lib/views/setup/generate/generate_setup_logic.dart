import 'package:bruno/bruno.dart';
import 'package:easy_chat/component/brn_row_delegate.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:get/get.dart';

import 'generate_setup_state.dart';

class GenerateSetupLogic extends GetxController {
  final GenerateSetupState _state = GenerateSetupState();
  final GlobalController _globalController = Get.find();

  final fontSizeTitleList = <String>[
    '小',
    '标准',
    '大',
    '无敌大',
  ];

  final fontSizeValueList = <int>[12, 16, 20, 28];

  int get selectedFontSize => _state.selectedFontSize.value;

  set selectedFontSize(int value) => _state.selectedFontSize.value = value;

  get isShowHistory => _state.isShowHistory.value;

  onShowHistory(bool value) {
    _state.isShowHistory.toggle();
  }

  setupChatFontSize() => BrnMultiDataPicker(
        context: Get.context!,
        title: '聊天字体大小',
        delegate:
            Brn1RowDelegate(fontSizeTitleList, selectedIndex: selectedFontSize),
        confirmClick: (list) {
          selectedFontSize = fontSizeValueList[list[0]];
          _globalController.generateFontSize = selectedFontSize;
        },
      ).show();

  @override
  void onInit() {
    selectedFontSize =
        fontSizeValueList.indexOf(_globalController.generateFontSize);
    super.onInit();
  }
}
