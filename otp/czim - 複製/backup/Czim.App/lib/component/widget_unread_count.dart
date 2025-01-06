import 'package:flutter/material.dart';

class UnreadCountWidget extends StatelessWidget {
  final int unreadCount;

  const UnreadCountWidget({Key? key, required this.unreadCount})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    if (unreadCount == 0) return Container();
    String count;
    if (unreadCount > 99) {
      count = '99+';
    } else {
      count = unreadCount.toString();
    }
    return Container(
      padding: EdgeInsets.only(left: (3), right: (3)),
      decoration: BoxDecoration(
        color: Colors.red,
        borderRadius: BorderRadius.all(
          Radius.circular((15)),
        ),
      ),
      constraints: BoxConstraints(
        minWidth: (18),
        maxWidth: (30),
      ),
      child: Text(
        count,
        maxLines: 1,
        textAlign: TextAlign.center,
        style: TextStyle(
          fontSize: (12),
          color: Colors.white,
        ),
      ),
    );
  }
}
