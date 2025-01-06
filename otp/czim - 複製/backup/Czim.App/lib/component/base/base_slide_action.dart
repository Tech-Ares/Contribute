import 'package:easy_chat/global/i_style.dart';
import 'package:flutter/material.dart';
import 'package:flutter_slidable/flutter_slidable.dart';

class BaseSlideAction extends StatelessWidget {
  final SlidableActionCallback onTap;
  final Color color;
  final IconData? iconData;
  final String label;

  const BaseSlideAction(
      {Key? key,
      required this.onTap,
      this.color = IColors.accentColor,
      this.iconData,
      required this.label})
      : super(key: key);

  @override
  Widget build(BuildContext context) => SlidableAction(
        flex: 1,
        onPressed: onTap,
        backgroundColor: color,
        foregroundColor: Colors.white,
        icon: iconData,
        label: label,
      );
}
