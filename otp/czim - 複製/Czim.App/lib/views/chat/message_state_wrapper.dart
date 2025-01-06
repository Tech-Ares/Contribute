import 'package:flutter/cupertino.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

class MessageStateWrapper extends StatefulWidget {
  final Widget child;
  final EMMessage? message;

  const MessageStateWrapper(
      {Key? key, required this.child, required this.message})
      : super(key: key);

  @override
  State<StatefulWidget> createState() => MessageStateWrapperState();
}

class MessageStateWrapperState extends State<MessageStateWrapper>
    implements EMMessageStatusListener {
  EMMessage? msg;

  @override
  void initState() {
    super.initState();
    msg = widget.message;
    logger.d('MessageStateWrapperState: message: ${msg?.toJson()}');
    // 添加监听
    msg?.setMessageListener(this);
  }

  // 消息进度
  @override
  void onProgress(int progress) {
    logger.d('onProgress: $progress');
  }

  // 消息发送失败
  @override
  void onError(EMError error) {
    logger.d('onError: $error');
  }

  // 消息发送成功
  @override
  void onSuccess() {
    logger.d('onSuccess');
  }

  // 消息已读
  @override
  void onReadAck() {
    logger.d('onReadAck');
  }

  // 消息已送达
  @override
  void onDeliveryAck() {
    logger.d('onDeliveryAck');
  }

  // 消息状态发生改变
  @override
  void onStatusChanged() {
    logger.d('onStatusChanged');
  }

  @override
  dispose() {
    msg?.setMessageListener(null);
    super.dispose();
  }

  @override
  Widget build(BuildContext context) => widget.child;
}
