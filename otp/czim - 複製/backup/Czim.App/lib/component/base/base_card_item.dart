
import 'package:easy_chat/config/global_config.dart';
import 'package:easy_chat/global/i_style.dart';
import 'package:flutter/material.dart';

/// Card Widget
class BaseCardView extends StatelessWidget {
  final Widget child;
  final Color color;
  final EdgeInsets? margin;
  final RoundedRectangleBorder? shape;
  final double elevation;
  final bool? highlight;
  final EdgeInsetsGeometry padding;
  final double corner;
  final BorderSide side;

  const BaseCardView({
    Key? key,
    required this.child,
    this.margin,
    this.shape,
    this.elevation = GlobalConfig.cardElevation,
    this.highlight,
    this.padding = EdgeInsets.zero,
    this.corner = 12,
    this.color = IColors.primarySwatch,
    this.side = BorderSide.none,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Card(
      color: color,
      margin: margin,
      elevation: elevation,
      shape: RoundedRectangleBorder(
        side: side,
        borderRadius: BorderRadius.all(
          Radius.circular(corner),
        ),
      ),
      child: child,
    );
  }
}
