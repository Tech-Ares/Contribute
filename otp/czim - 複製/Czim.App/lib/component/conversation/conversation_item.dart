import 'package:easy_chat/component/widget_im_avatar.dart';
import 'package:easy_chat/component/widget_unread_count.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/utils/chat_date_format.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import '../widget_expression.dart';

class ConversationItem extends StatefulWidget {
  @override
  const ConversationItem({
    required EMConversation conv,
    required VoidCallback onTap,
  })  : _conv = conv,
        _onTap = onTap;
  final EMConversation _conv;
  final VoidCallback _onTap;

  _ConversationItemState createState() => _ConversationItemState();
}

class _ConversationItemState extends State<ConversationItem> {
  final GlobalController _globalController = Get.find();
  late bool isMute;

  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    isMute = _globalController.isConvNotify(widget._conv.id);
    bool hasUnreadAt = false;
    if (widget._conv.type == EMConversationType.GroupChat) {
      hasUnreadAt = _globalController.conversationHasAt(widget._conv.id) &&
          ((widget._conv.unreadCount ?? 0) > 0);
    }
    return ListTile(
      leading: convAvatar(widget._conv),
      title: Row(
        children: [
          Expanded(
              child: Text(
            _showName(),
            style: Get.textTheme.subtitle1,
            maxLines: 1,
            overflow: TextOverflow.ellipsis,
          )),
          Text(
            _latestMessageTime(),
            maxLines: 1,
            style: Get.textTheme.bodyText2
                ?.copyWith(color: Colors.grey, fontSize: 12),
          )
        ],
      ),
      onTap: widget._onTap,
      subtitle: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          if (hasUnreadAt)
            Text(
              '[有人@你] ',
              style: Get.textTheme.bodyText2?.copyWith(color: Colors.red),
            )
          else
            Visibility(
                visible: _unreadCount() > 1 && isMute,
                child: Text(
                  '[${_unreadCount()}条未读] ',
                  style: Get.textTheme.bodyText2?.copyWith(color: Colors.grey),
                )),
          Expanded(
              child: ExpressionText(
            _showInfo(),
            Get.textTheme.bodyText2!.copyWith(color: Colors.grey),
            maxLine: 1,
          )),
          isMute
              ? const Icon(
                  Icons.notifications_off_outlined,
                  color: Colors.grey,
                  size: 16,
                )
              : UnreadCountWidget(
                  unreadCount: _unreadCount(),
                )
        ],
      ),
    );
  }

  /// 消息详情
  String _showInfo() {
    String showInfo = '';
    EMMessage? _latestMesage = this.widget._conv.latestMessage;
    logger.d('_showInfo _latestMesage: ${_latestMesage?.toJson()}');
    if (_latestMesage == null) {
      return showInfo;
    }

    try {
      if (_latestMesage.attributes['fireTime'] > 0) {
        return '[阅后即焚]';
      }
    } catch (e) {
      // logger.e(e);
    }

    switch (_latestMesage.body?.type) {
      case EMMessageBodyType.TXT:
        var body = _latestMesage.body as EMTextMessageBody;
        showInfo = body.content ?? '';
        break;
      case EMMessageBodyType.IMAGE:
        showInfo = '[图片]';
        break;
      case EMMessageBodyType.VIDEO:
        showInfo = '[视频]';
        break;
      case EMMessageBodyType.FILE:
        showInfo = '[文件]';
        break;
      case EMMessageBodyType.VOICE:
        showInfo = '[语音]';
        break;
      case EMMessageBodyType.LOCATION:
        showInfo = '[位置]';
        break;
      case EMMessageBodyType.CUSTOM:
        var params = (_latestMesage.body as EMCustomMessageBody).params;
        if (params?['action'] == 'shake') {
          showInfo =
              '抖了${_latestMesage.direction == EMMessageDirection.SEND ? 'Ta' : '你'}一下';
        }
        break;
      default:
        showInfo = '';
    }
    return showInfo;
  }

  /// 显示的名称
  String _showName() {
    return '${widget._conv.name}';
  }

  /// 未读数
  int _unreadCount() {
    return this.widget._conv.unreadCount ?? 0;
  }

  /// 消息时间
  String _latestMessageTime() {
    if (this.widget._conv.latestMessage == null) {
      return '';
    }
    String text = chatDateFormat(
        DateTime.fromMillisecondsSinceEpoch(
            widget._conv.latestMessage?.serverTime ?? 0),
        dateLocale: Get.locale?.languageCode);
    return text;
  }
}
