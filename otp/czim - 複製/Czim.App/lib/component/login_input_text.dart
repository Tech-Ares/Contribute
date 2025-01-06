import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

class LoginInputText extends StatelessWidget {
  final String title;
  final String hint;
  final int? maxLength;
  final Widget? suffix;
  final TextEditingController textEditingController;
  final TextInputType? keyboardType;
  final bool? obscureText;

  const LoginInputText(
      {Key? key,
      required this.title,
      required this.hint,
      this.maxLength,
      required this.textEditingController, this.suffix, this.keyboardType, this.obscureText})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          title,
          textAlign: TextAlign.center,
          style: Get.textTheme.headline4,
        ),
        const SizedBox(
          width: 12,
        ),
        TextFormField(
          controller: textEditingController,
          decoration: InputDecoration(
            border: InputBorder.none,
            hintText: hint,
          ),
          keyboardType: keyboardType,
          obscureText: obscureText ?? false,
        ),
        suffix ?? Container()
      ],
    );
  }
}
