import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

class MyMessageStatusListener extends EMMessageStatusListener {
  final EMMessage message;

  final Function(EMMessageStatus status)? onCallback;

  // final Function(EMMessage message)? onStatusChange;

  MyMessageStatusListener(this.message, {this.onCallback});

  @override
  void onSuccess() {
    logger.d('MyMessageStatusListener ==> onSuccess');
    onCallback?.call(EMMessageStatus.SUCCESS);
    super.onSuccess();
  }

  @override
  void onDeliveryAck() {
    logger.d('MyMessageStatusListener ==> onDeliveryAck');
    super.onDeliveryAck();
  }

  @override
  void onError(EMError error) {
    logger.d('MyMessageStatusListener ==> onError');
    onCallback?.call(EMMessageStatus.FAIL);
    super.onError(error);
  }

  @override
  void onProgress(int progress) {
    logger.d('MyMessageStatusListener ==> onProgress');
    onCallback?.call(EMMessageStatus.PROGRESS);
    super.onProgress(progress);
  }

  @override
  void onStatusChanged() {
    logger.d('MyMessageStatusListener ==> onStatusChanged');
    // onStatusChange?.call(message);
    super.onStatusChanged();
  }
}
