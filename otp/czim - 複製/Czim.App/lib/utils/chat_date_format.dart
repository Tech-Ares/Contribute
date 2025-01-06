import 'package:intl/intl.dart';

String chatDateFormat(
  DateTime dateTime, {
  DateFormat? dateFormat,
  String? dateLocale,
  DateFormat? timeFormat,
}) {
  final formattedDate = dateFormat != null
      ? dateFormat.format(dateTime)
      : DateFormat.MMMd(dateLocale).format(dateTime);
  final formattedTime = timeFormat != null
      ? timeFormat.format(dateTime)
      : DateFormat.Hm(dateLocale).format(dateTime);
  final localDateTime = dateTime.toLocal();
  final now = DateTime.now();

  if (localDateTime.day == now.day &&
      localDateTime.month == now.month &&
      localDateTime.year == now.year) {
    return formattedTime;
  }

  return '$formattedDate, $formattedTime';
}

String fireCountDownFormat(int leftTime) {
  int millsOfOneSecond = 1000;
  int millsOfOneMinute = 60 * millsOfOneSecond;
  int millsOfOneHour = 60 * millsOfOneMinute;
  int millsOfOneDay = 24 * millsOfOneHour;

  int leftDay = leftTime ~/ millsOfOneDay;
  int leftHour = (leftTime - leftDay * millsOfOneDay) ~/ millsOfOneHour;
  int leftMinute =
      (leftTime - leftDay * millsOfOneDay - leftHour * millsOfOneHour) ~/
          millsOfOneMinute;
  int leftSecond = (leftTime -
          leftDay * millsOfOneDay -
          leftHour * millsOfOneHour -
          leftMinute * millsOfOneMinute) ~/
      millsOfOneSecond;
  StringBuffer stringBuffer = StringBuffer();
  if (leftDay > 0) {
    stringBuffer.write('$leftDay天');
  }
  if (leftDay > 0 || leftHour > 0) {
    stringBuffer.write('$leftHour小时');
  }
  if (leftDay > 0 || leftHour > 0 || leftMinute > 0) {
    stringBuffer.write('$leftMinute分');
  }
  return '$stringBuffer$leftSecond秒';
}
