import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'dart:typed_data';

import 'package:bruno/bruno.dart';
import 'package:easy_chat/common/image_processor.dart';
import 'package:easy_chat/component/brn_row_delegate.dart';
import 'package:easy_chat/component/widget_fire_countdown.dart';
import 'package:easy_chat/export.dart';
import 'package:easy_chat/ext/im_message_ext.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/chat_background.dart';
import 'package:easy_chat/models/contact_model.dart';
import 'package:easy_chat/models/params/revoke_message_params.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:easy_chat/views/chat/message_state_wrapper.dart';
import 'package:easy_chat/views/member/select/select_member_logic.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_chat_types/flutter_chat_types.dart' as types;
import 'package:flutter_chat_ui/flutter_chat_ui.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';
import 'package:shake_animation_widget/shake_animation_widget.dart';

import 'chat_state.dart';
import 'my_message_status_listener.dart';

class ChatLogic extends BaseController<List<types.Message>>
    implements EMChatManagerListener {
  final ChatState _state = ChatState();
  final EMConversation _conversation = Get.arguments;
  final GlobalController _globalController = Get.find();
  final RxBool _loading = false.obs;
  late types.User user;
  String _messageId = '';
  final List<ChatAttachment> attachments = [];
  final ShakeAnimationController shakeAnimationController =
      ShakeAnimationController();
  final FlutterSoundPlayer mPlayer = FlutterSoundPlayer(logLevel: Level.error);
  late FocusNode focusNode;
  final TextEditingController textEditingController = TextEditingController();
  final List<ContactModel> atMembers = [];
  String? latestInput; //上一次的输入

  bool _mPlayerIsInited = false;

  String? get title => _conversation.name;

  final RxList<types.Message> _messages = <types.Message>[].obs;

  RxBool updateObx = false.obs;

  List<types.Message> get conversationMessages => _messages.value;

  late Worker _worker;

  Timer? _timer;

  @override
  bool get supportPagination => true;

  @override
  Future<List<types.Message>> Function() get requestFunc => () => imManager
      .imChatService
      .loadMessages(_conversation, messageId: _messageId)
      .then((messages) => messages == null
          ? []
          : messages.map((e) => e!.typeMessage()).toList().reversed.toList());

  get showName =>
      _conversation.type == EMConversationType.ChatRoom ||
      _conversation.type == EMConversationType.GroupChat;

  get fireNow =>
      _state.selectedFire.value == 0 ? null : list[_state.selectedFire.value];

  bool get isFire => _state.selectedFire.value > 0;

  get fontSize => _state.fontSize.value.toDouble();

  Decoration get decoration => _state.decoration.value;

  get imToken => _globalController.imToken;

  set decoration(value) => _state.decoration.value = value;

  @override
  void onDataLoaded() {
    if (pageParams?.page == 1) {
      _messages.clear();
    }
    _messages.addAll(state!);
    if (state!.isNotEmpty) {
      _messageId = state!.last.id;
    }
    _conversation.markAllMessagesAsRead();
    updateObx.toggle();
    super.onDataLoaded();
  }

  @override
  void onInit() async {
    focusNode =
        FocusNode(onKey: _focusNodeOnKey, onKeyEvent: _focusNodeOnKeyEvent);
    logger.d('conversation: ${_conversation.toJson()}');
    _initAttachments();
    user = _globalController.userInfo!.typeUser();
    // if (_conversation.type == EMConversationType.ChatRoom) {
    //   await joinChatRoom();
    // }
    await _initIMJob();
    _worker = interval(_loading, (b) async {
      logger.d('loadMore');
      await onLoadMore(append: false);
    }, time: const Duration(milliseconds: 500));

    mPlayer.openAudioSession().then((value) {
      _mPlayerIsInited = true;
    });
    _globalController.openConv(_conversation.id);
    _state.selectedFire.value = _globalController.getFire(_conversation.id);
    _timer = Timer.periodic(const Duration(milliseconds: 500), (timer) {
      _checkFireMessage();
    });
    _state.fontSize.value = _globalController.generateFontSize;
    logger.d('_state.fontSize.value: ${_state.fontSize.value}');
    initBackgroundDecoration();
    textEditingController.addListener(_handleTextControllerChange);
    focusNode.addListener(_handleFocusNodeChange);
    super.onInit();
  }

  Future? _addMessage(EMMessage emMessage) async {
    // final emMessage = message.emMessage();
    _chatType() {
      EMMessageChatType type = EMMessageChatType.Chat;
      switch (_conversation.type) {
        case EMConversationType.Chat:
          type = EMMessageChatType.Chat;
          break;
        case EMConversationType.ChatRoom:
          type = EMMessageChatType.ChatRoom;
          break;
        case EMConversationType.GroupChat:
          type = EMMessageChatType.GroupChat;
          break;
        default:
      }
      return type;
    }

    emMessage.chatType = _chatType();
    var at = _getAtMembers();
    emMessage.attributes = {
      'nickName': _globalController.userInfo?.showName,
      'avatar': _globalController.userInfo?.avatar,
      'fireTime': fireTime[_state.selectedFire.value],
      'at': at,
    };
    // emMessage.needGroupAck = true;
    logger.d('sendMsg ==> ${emMessage.toJson()}');
    // await _conversation.insertMessage(emMessage);
    // message = message.copyWith(
    //   status: types.Status.sent,
    // );
    await imManager.imChatService.sendMessage(emMessage);
    logger.d('发送消息 emMessage:${emMessage.toJson()}');
    // logger.d('发送消息:${emMessage.typeMessage().toJson()}');
    var typeMessage = emMessage.typeMessage();
    _messages.insert(0, typeMessage);
    var listener = MyMessageStatusListener(emMessage,
        onCallback: (EMMessageStatus status) {
      var message = typeMessage.copyWith(status: emMessage.typeStatus(), metadata: typeMessage.metadata);
      var indexWhere = _messages.indexWhere((element) => element.id == typeMessage.id);
      _messages[indexWhere] = message;
      updateObx.toggle();
      // _messages.firstWhereOrNull((element) => element.id == e.msgId)?.status = newStatusMsg;
    });
    emMessage.setMessageListener(listener);
    atMembers.clear();
    updateObx.toggle();
  }

  void handleMessageTap(types.Message message) async {
    if (message is types.FileMessage) {
      await OpenFile.open(message.uri);
    }
  }

  void handlePreviewDataFetched(
    types.TextMessage message,
    types.PreviewData previewData,
  ) {
    final index = _messages.indexWhere((element) => element.id == message.id);
    final updatedMessage = _messages[index].copyWith(previewData: previewData);

    logger.d('previewData.title: ${previewData.title}');
    logger.d('previewData.description: ${previewData.description}');
    WidgetsBinding.instance?.addPostFrameCallback((_) {
      _messages[index] = updatedMessage;
      updateObx.toggle();
    });
  }

  ///语音消息
  void handleAudioSend(File file, int sec) {
    logger.d('录音完成，时长：$sec');
    var emMessage = EMMessage.createVoiceSendMessage(
        username: _conversation.id, filePath: file.path, duration: sec);
    // final message = types.FileMessage(
    //   author: user,
    //   remoteId: _conversation.id,
    //   createdAt: emMessage.serverTime,
    //   mimeType: lookupMimeType(file.path),
    //   id: emMessage.msgId ?? uuid.v4(),
    //   name: file.path,
    //   size: sec,
    //   uri: file.path,
    // );
    _addMessage(emMessage);
  }

  ///发送按钮，发送文本消息
  void handleSendPressed(types.PartialText message) {
    var emMessage =
        EMMessage.createTxtSendMessage(_conversation.id, message.text);
    // final textMessage = types.TextMessage(
    //   author: user,
    //   createdAt: emMessage.serverTime,
    //   id: emMessage.msgId ?? uuid.v4(),
    //   remoteId: _conversation.id,
    //   text: message.text,
    // );
    _addMessage(emMessage);
  }

  ///发送视频消息
  void _sendFileMessage(File file) {
    var emMessage = EMMessage.createVideoSendMessage(
        username: _conversation.id, filePath: file.path);
    // final message = types.FileMessage(
    //   author: user,
    //   createdAt: emMessage.serverTime,
    //   id: emMessage.msgId ?? uuid.v4(),
    //   mimeType: lookupMimeType(file.path),
    //   name: file.path,
    //   size: file.lengthSync(),
    //   uri: file.path,
    //   remoteId: _conversation.id,
    // );
    _addMessage(emMessage);
  }

  ///发送图片消息
  void _sendImageMessage(File file) {
    var emMessage = EMMessage.createImageSendMessage(
        username: _conversation.id, filePath: file.path);
    // final message = types.ImageMessage(
    //   author: user,
    //   createdAt: DateTime.now().millisecondsSinceEpoch,
    //   id: emMessage.msgId ?? uuid.v4(),
    //   name: file.path,
    //   size: file.lengthSync(),
    //   uri: file.path,
    //   remoteId: _conversation.id,
    // );
    _addMessage(emMessage);
  }

  Future<void> handleEndReached() async {
    _loading.toggle();
    return Future.value();
  }

  Future? _initIMJob() async {
    await imManager.imChatService.readAllMessage(_conversation);
    // imManager.imChatService.sendConversationReadAck(_conversation.id);
    imManager.imChatService.addListener(this);
  }

  void selectGallery() => _selectImageOrVideo(0);

  void selectCamera() => _selectImageOrVideo(1);

  void _selectImageOrVideo(int type) async {
    List<AssetEntity>? assetEntities = await selectPhoto(type, size: 9);
    if (assetEntities == null || assetEntities.isEmpty) {
      logger.w('所选内容为空！');
      return;
    }
    for (var asset in assetEntities) {
      var originFile = await asset.originFile;
      if (!(originFile?.existsSync() ?? false)) {
        continue;
      }
      if (asset.type == AssetType.image) {
        //图片压缩
        var file = await compressAndGetFile(originFile);
        if (file.existsSync()) {
          _sendImageMessage(file);
        }
      } else {
        //视频压缩
        MediaInfo? mediaInfo = await VideoCompress.compressVideo(
          originFile!.path,
          quality: VideoQuality.DefaultQuality,
          deleteOrigin: false, // It's false by default
        );
        var file = mediaInfo!.file!;
        if (file.existsSync()) {
          _sendFileMessage(file);
        }
      }
    }
  }

  @override
  void onClose() {
    _worker.dispose();
    imManager.imChatService.removeListener(this);
    _globalController.closeConv(_conversation.id);
    _timer?.cancel();
    // textEditingController.removeListener(_handleTextControllerChange);
    // focusNode.removeListener(_handleFocusNodeChange);
    super.onClose();
  }

  /// 收到消息[messages]
  @override
  Future? onMessagesReceived(List<EMMessage> messages) async {
    logger.e('Chat ==> 收到新消息');
    var thisConvMessages = messages
        .where((element) => element.conversationId == _conversation.id)
        .toList();
    for (var e in thisConvMessages) {
      await imManager.imChatService.sendMessageReadAck(e);
      e.attributes['readTime'] = DateTime.now().millisecondsSinceEpoch;
      await _conversation.updateMessage(e);
      await _conversation.markMessageAsRead(e.msgId ?? '');
    }
    await imManager.imChatService.fetchMessageExt(thisConvMessages);
    final receiveMessages = thisConvMessages.map((e) {
      if (e.body is EMCustomMessageBody) {
        var params = (e.body as EMCustomMessageBody).params;
        if (params?['action'] == 'shake') {
          _doShake();
        }
      }
      return e.typeMessage();
    });
    _messages.insertAll(0, receiveMessages);
    updateObx.toggle();
    await imManager.imChatService.readAllMessage(_conversation);
  }

  /// 收到cmd消息[messages]
  @override
  void onCmdMessagesReceived(List<EMMessage> messages) {
    // TODO: implement onCmdMessagesReceived
  }

  /// 会话已读`from`是已读的发送方, `to`是已读的接收方
  @override
  void onConversationRead(String? from, String? to) {
    // TODO: implement onConversationRead
  }

  /// 会话列表变化
  @override
  void onConversationsUpdate() {
    // TODO: implement onConversationsUpdate
  }

  /// 收到[groupMessageAcks]群消息已读回调
  @override
  void onGroupMessageRead(List<EMGroupMessageAck> groupMessageAcks) {
    // TODO: implement onGroupMessageRead
  }

  /// 收到[messages]消息已送达
  @override
  void onMessagesDelivered(List<EMMessage> messages) {
    // TODO: implement onMessagesDelivered
  }

  /// 收到[messages]消息已读
  @override
  void onMessagesRead(List<EMMessage> messages) {
    // TODO: implement onMessagesRead
  }

  /// 收到[messages]消息被撤回
  @override
  void onMessagesRecalled(List<EMMessage> messages) {
    if (messages.isNotEmpty) {
      _messages.removeWhere((m) => m.id == messages[0].msgId);
    }
    // updateObx.toggle();
  }

  void _initAttachments() {
    attachments.add(ChatAttachment(
        const Icon(Icons.photo),
        Text('相册',
            style: Get.textTheme.bodyText2?.copyWith(color: Colors.black87)),
        () => selectGallery()));
    attachments.add(ChatAttachment(
        const Icon(Icons.camera_alt_outlined),
        Text('拍照',
            style: Get.textTheme.bodyText2?.copyWith(color: Colors.black87)),
        () => selectCamera()));
    if (_conversation.type == EMConversationType.Chat) {
      attachments.add(ChatAttachment(
          SvgPicture.asset(
            Utils.getSvgPath('ic_action_shake'),
            width: 24,
          ),
          Text('抖一下',
              style: Get.textTheme.bodyText2?.copyWith(color: Colors.black87)),
          () => _shake()));
    }
  }

  void handleMessageLongPress(types.Message message, GlobalKey globalKey) {
    logger.d('handleMessageLongPress: ${message.toJson()}');
    if (message.author.id == user.id) {
      // FocusScope.of(globalKey.currentContext!).unfocus();
      RenderBox renderBox =
          globalKey.currentContext?.findRenderObject() as RenderBox;
      logger.d('renderBox.size: ${renderBox.size}');
      var offset = renderBox.size.width / 2;
      BrnPopupWindow.showPopWindow(globalKey.currentContext, '撤回', globalKey,
          spaceMargin: offset - 32,
          turnOverFromBottom: 300,
          // borderRadius: 4,
          offset: 6,
          paddingInsets: EdgeInsets.zero,
          widget: Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              SizedBox(
                height: 40,
                width: 64,
                child: TextButton(
                    onPressed: () => _revokeMessage(message),
                    child: Text(
                      '撤回',
                      style: Get.textTheme.bodyText2
                          ?.copyWith(color: Colors.white),
                    )),
              ),
            ],
          ));
      // BrnPopupListWindow.showPopListWindow(Get.context, globalKey,
      //     offset: offset,
      //     data: [
      //       // '删除',
      //       '撤回',
      //     ], onItemClick: (index, item) async {
      //   if (index == 0) {
      //     try {
      //       bool result = await EMClient.getInstance.chatManager
      //           .recallMessage(message.id);
      //       logger.d('撤回消息: ${message.id}, result: $result}');
      //       if (result) {
      //         _messages.removeWhere((element) => element.id == message.id);
      //         updateObx.toggle();
      //         ToastUtil.success('消息已撤回');
      //       } else {
      //         ToastUtil.error('撤回失败');
      //       }
      //     } catch (e) {
      //       ToastUtil.error('$e');
      //     }
      //   }
      //   // BrnToast.show(item, Get.context);
      // });
    }
  }

  _revokeMessage(types.Message message) async {
    if (_conversation.type == EMConversationType.Chat) {
      await memberRestClient.revokeMessage(
          RevokeMessageParams(msgId: message.id, receiveId: message.remoteId));
    } else if (_conversation.type == EMConversationType.ChatRoom) {
      await memberRestClient.revokeRoomMessage(
          RevokeMessageParams(msgId: message.id, receiveId: message.remoteId));
    } else {
      ToastUtil.error('该消息不支持撤回');
      return;
    }
    try {
      await _conversation.deleteMessage(message.id);
    } catch (e) {
      logger.e(e);
    }
    _messages.removeWhere((element) => element.id == message.id);
    ToastUtil.success('消息已撤回');
    Get.back();
    // try {
    //   Get.back();
    //   bool result =
    //       await EMClient.getInstance.chatManager.recallMessage(message.id);
    //   logger.d('撤回消息: ${message.id}, result: $result}');
    //   if (result) {
    //     _messages.removeWhere((element) => element.id == message.id);
    //     updateObx.toggle();
    //     ToastUtil.success('消息已撤回');
    //   } else {
    //     // ToastUtil.error('撤回失败');
    //     await memberRestClient.revokeMessage(RevokeMessageParams(
    //         msgId: message.id, receiveId: message.remoteId));
    //   }
    // } catch (e) {
    //   ToastUtil.error('$e');
    // }
  }

  onChatMore() {
    switch (_conversation.type!) {
      case EMConversationType.Chat:
        Get.toNamed(PageRoutes.singChatSetup,
            arguments: {'id': _conversation.id, 'name': _conversation.name});
        break;
      case EMConversationType.GroupChat:
        Get.toNamed(PageRoutes.groupSetup, arguments: _conversation.id);
        break;
      case EMConversationType.ChatRoom:
        logger.d('_conversation.id: ${_conversation.id}');
        Get.toNamed(PageRoutes.roomSetup, arguments: _conversation.id);
        break;
    }
  }

  Future? joinChatRoom() async {
    try {
      // await EMClient.getInstance.chatRoomManager.joinChatRoom(_conversation.id);
      var chatRoom = await EMClient.getInstance.chatRoomManager
          .fetchChatRoomInfoFromServer(_conversation.id);
      logger.d('chatRoom: ${chatRoom.toJson()}');
      if (chatRoom.isAllMemberMuted) {
        ToastUtil.success('全员禁言');
      } else {
        // ToastUtil.success('加入聊天室成功');
      }
    } on EMError catch (e) {
      logger.d('加入房间失败 -- ' + e.toString());
      // ToastUtil.error('加入聊天室失败');
    }
  }

  _shake() async {
    logger.d('抖一下');
    // String message = '抖了你一下';
    var emMessage = EMMessage.createCustomSendMessage(
        username: _conversation.id,
        event: 'shake',
        params: {'action': 'shake'});
    // final textMessage = types.TextMessage(
    //     author: user,
    //     createdAt: DateTime.now().millisecondsSinceEpoch,
    //     id: emMessage.msgId ?? uuid.v4(),
    //     remoteId: _conversation.id,
    //     text: '抖了Ta一下',
    //     status: types.Status.sending,
    //     metadata: const {'action': 'shake'});
    await _addMessage(emMessage);
    _doShake();
  }

  _doShake() async {
    if (_mPlayerIsInited) {
      ByteData audioByteData = await rootBundle.load('assets/sounds/shake.mp3');
      Uint8List audioUint8List = audioByteData.buffer.asUint8List(
          audioByteData.offsetInBytes, audioByteData.lengthInBytes);
      mPlayer.startPlayer(
        fromDataBuffer: audioUint8List,
        codec: Codec.mp3,
      );
    }
    shakeAnimationController.start(shakeCount: 1);
  }

  List<String> list = [
    '关闭',
    '5秒',
    '30秒',
    '90秒',
    '10分',
    '30分',
    '1小时',
    '5小时',
    '1天',
    '7天',
    '30天',
    '90天',
    '半年',
    '1年',
  ];

  List<int?> fireTime = [
    null,
    5 * 1000,
    30 * 1000,
    90 * 1000,
    10 * 60 * 1000,
    30 * 60 * 1000,
    60 * 60 * 1000,
    5 * 60 * 60 * 1000,
    24 * 60 * 60 * 1000,
    7 * 24 * 60 * 60 * 1000,
    30 * 24 * 60 * 60 * 1000,
    90 * 24 * 60 * 60 * 1000,
    187 * 24 * 60 * 60 * 1000,
    365 * 24 * 60 * 60 * 1000,
  ];

  showFireDialog() {
    BrnMultiDataPicker(
      context: Get.context!,
      title: '设置信息自毁时间',
      delegate: Brn1RowDelegate(list, selectedIndex: _state.selectedFire.value),
      confirmClick: (list) {
        _state.selectedFire.value = list[0];
        _globalController.saveFire(_conversation.id, _state.selectedFire.value);
        logger.d(list);
      },
    ).show();
  }

  Future? showFirePop(types.Message message, GlobalKey globalKey) async {
    // BrnPopupWindow.showPopWindow(
    //     Get.context, '5:30', globalKey,
    //     dismissCallback: () {},);
    int? leftMills = message.getMessageFireLeftTime(user.id);
    logger.d('leftMills ==> $leftMills');
    if (leftMills != null && leftMills > 0) {
      BrnPopupListWindow.showPopCustomWindow(
        Get.context,
        globalKey,
        offset: 6,
        child: FireCountDownWidget(
          fireTimeMills: leftMills,
        ),
      );
    } else {
      await _fireMessage(message);
    }
  }

  Future? _checkFireMessage() async {
    final list = <types.Message>[];
    for (var element in _messages) {
      int? leftMills = element.getMessageFireLeftTime(user.id);
      if (leftMills != null && leftMills <= 0) {
        list.add(element);
      }
    }
    for (var element in list) {
      logger.d(
          '_checkFireMessage ==> ${element.getMessageFireLeftTime(user.id)}');
      await _fireMessage(element);
    }
  }

  Future? _fireMessage(types.Message message) async {
    //TODO 先将他变为「消息已焚」延时一秒后删除
    logger.d('_fireMessage ==> ${message.toJson()}');
    message.copyWith(metadata: {'firing': true});
    try {
      var firingMessage =
          _messages.firstWhere((element) => element.id == message.id);
      logger.d('$firingMessage即将焚毁');
      message.metadata?['firing'] = true;
      message.metadata?['fireTip'] = '消息已焚';
      _messages.refresh();
      updateObx.toggle();
    } catch (e) {
      logger.e(e);
    }
    await Future.delayed(const Duration(seconds: 1), () async {
      _messages.remove(message);
      await _conversation.deleteMessage(message.id);
      if (_conversation.type == EMConversationType.Chat) {
        await memberRestClient.revokeMessage(RevokeMessageParams(
            receiveId: _conversation.id, msgId: message.id));
      } else {
        await memberRestClient.revokeRoomMessage(RevokeMessageParams(
            receiveId: _conversation.id, msgId: message.id));
      }
    });
  }

  void initBackgroundDecoration() {
    ChatBackground chatBackground =
        _globalController.loadChatBackground(sessionId: _conversation.id);
    int type = chatBackground.type;
    DecorationImage? decorationImage;
    if (chatBackground.value != null) {
      switch (type) {
        case 0:
          decorationImage = DecorationImage(
              image: AssetImage(
                Utils.getImgPath(chatBackground.value!, format: 'jpg'),
              ),
              fit: BoxFit.cover);
          break;
        case 1:
          decorationImage = DecorationImage(
              image: FileImage(File(chatBackground.value!)), fit: BoxFit.cover);
          break;
        case 2:
          decorationImage = DecorationImage(
              image: NetworkImage(chatBackground.value!), fit: BoxFit.cover);
          break;
      }
    }
    decoration = BoxDecoration(
      image: decorationImage,
    );
  }

  void reloadBackground() => initBackgroundDecoration();

  void handleAvatarLongPressed(types.User u) {
    _doHandleAtMember(u.firstName ?? '', at: true);
    atMembers.add(ContactModel.fromAt(u.id, u.firstName ?? u.id));
    logger.d('当前@的人${atMembers.map((e) => e.showName).toList()}');
  }

  _doHandleAtMember(String name, {bool at = false}) {
    focusNode.requestFocus();
    textEditingController
      ..text += '${at ? '@' : ''}$name '
      ..selection = TextSelection.fromPosition(
          TextPosition(offset: textEditingController.text.length));
  }

  void _handleTextControllerChange() {
    if (_conversation.type == EMConversationType.GroupChat ||
        _conversation.type == EMConversationType.ChatRoom) {
      String thisInput = textEditingController.text;
      logger.d('${textEditingController.selection}');
      int offset = textEditingController.selection.baseOffset - 1;
      if (thisInput.length > (latestInput?.length ?? 0) &&
          thisInput[offset] == '@') {
        logger.d('输入了@符号');
        Get.toNamed(PageRoutes.selectMember, arguments: {
          'type': _conversation.type == EMConversationType.GroupChat
              ? SelectType.singleGroupChoose
              : SelectType.singleRoomChoose,
          'id': _conversation.id
        })?.then((value) => _atMember(value));
      }
      latestInput = thisInput;
    }
  }

  void _handleFocusNodeChange() {
    logger.d('_handleFocusNodeChange');
  }

  KeyEventResult _focusNodeOnKey(FocusNode node, RawKeyEvent event) {
    // logger.d('_focusNodeOnKey ==> ${event}');
    return KeyEventResult.ignored;
  }

  KeyEventResult _focusNodeOnKeyEvent(FocusNode node, KeyEvent event) {
    // logger.d('_focusNodeOnKeyEvent ==> ${event}');
    if (event is KeyDownEvent && event.logicalKey.keyLabel == 'Backspace') {
      logger.d('按了返回键');
      String text = textEditingController.text;
      //TODO 这里是检查下回退的时候前面是不是@的人，如果是的话就把整个人名给删除掉
      int offset = textEditingController.selection.baseOffset - 1;
      if (offset <= 0) {
        return KeyEventResult.ignored;
      }
      logger.d('光标的前一个字符：${text[offset]}');
      //检查下光标的前一个字符是不是空格
      if (text[offset] != ' ') {
        return KeyEventResult.ignored;
      }
      String candidateMemberName = text.substring(0, offset + 1);
      logger.d('待定的人员字符串：$candidateMemberName, 光标位置：$offset');
      logger.d('最后一个字符：${candidateMemberName[offset]}');
      if (candidateMemberName.endsWith(' ')) {
        //有可能是@XX
        var lastAtIndex = candidateMemberName.lastIndexOf('@');
        logger.d('光标前第一个找到的@符号位置:$lastAtIndex');
        if (lastAtIndex >= 0) {
          String deleteMemberName =
              candidateMemberName.substring(lastAtIndex, offset + 1);
          logger.d('准备删除的字符串:$deleteMemberName');
          textEditingController
            ..text = text.replaceRange(lastAtIndex, offset + 1, '')
            ..selection =
                TextSelection.fromPosition(TextPosition(offset: lastAtIndex));
          atMembers.removeWhere(
              (element) => deleteMemberName.contains(element.showName));
          logger.d('当前@的人${atMembers.map((e) => e.showName).toList()}');
          return KeyEventResult.handled;
        }
      }
    }
    return KeyEventResult.ignored;
  }

  ///发消息之前再检查一下@的对象
  String _getAtMembers() {
    String text = textEditingController.text;
    List<String> memberIds = [];
    //如果已经删除调了，则需要从atMembers中剔除
    for (var m in atMembers) {
      m.isSelect = text.contains(m.showName);
      if (m.isSelect) {
        memberIds.add(m.contactId!);
      }
    }
    return jsonEncode(memberIds);
  }

  _atMember(ContactModel? member) {
    if (member != null) {
      _doHandleAtMember(member.showName);
      logger.d('_atMember ==> ${member.showName}, ${member.contactId}');
      // atMembers.add(member);
      atMembers.add(ContactModel.fromAt(member.contactId!, member.showName));
      logger.d('当前@的人${atMembers.map((e) => e.showName).toList()}');
    }
  }

  void handleAvatarTap(types.User user) {
    //管理员可以与成员沟通
    if (_globalController.userInfo?.isAgent ?? false) {
      Get.toNamed(PageRoutes.memberHome, arguments: user.id);
    }
  }

  Widget wrapMessageState(types.Message message, Widget child) =>
      MessageStateWrapper(
          message: message.metadata?['emMessage'], child: child);

  void handleMessageStatus(types.Message message) async {
    if (message.status == types.Status.error) {
      BrnDialogManager.showConfirmDialog(Get.context!,
          title: '是否重新发送该消息',
          cancel: '取消',
          confirm: '确定',
          onCancel: () => Get.back(),
          onConfirm: () async {
            Get.back();
            await _doResendMessage(message);
          });
    }
  }

  Future? _doResendMessage(types.Message typeMessage) async {
    try {
      EMMessage emMessage = typeMessage.metadata?['emMessage'];
      EMMessage resendMessage  = await EMClient.getInstance.chatManager.resendMessage(emMessage);
      var listener = MyMessageStatusListener(resendMessage,
          onCallback: (EMMessageStatus status) {
            var message = typeMessage.copyWith(status: resendMessage.typeStatus());
            int indexWhere = _messages.indexWhere((element) => element.id == typeMessage.id);
            _messages[indexWhere] = message;
            updateObx.toggle();
          });
      resendMessage.setMessageListener(listener);
      var message = typeMessage.copyWith(status: types.Status.sending);
      _messages[_messages.indexOf(typeMessage)] = message;
      updateObx.toggle();
    } on EMError catch (e) {
      logger.e('操作失败，原因是: $e');
    }
  }
}
