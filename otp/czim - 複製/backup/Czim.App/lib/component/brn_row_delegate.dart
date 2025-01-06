import 'package:bruno/bruno.dart';

class Brn1RowDelegate implements BrnMultiDataPickerDelegate {
  int selectedIndex = 0;
  final List<String> list;

  Brn1RowDelegate(this.list,
      {this.selectedIndex = 0});

  @override
  int numberOfComponent() {
    return 1;
  }

  @override
  int numberOfRowsInComponent(int component) {
    return list.length;
  }

  @override
  String titleForRowInComponent(int component, int index) {
    return list[index];
  }

  @override
  double rowHeightForComponent(int component) {
    return PICKER_ITEM_HEIGHT;
  }

  @override
  selectRowInComponent(int component, int row) {
    selectedIndex = row;
  }

  @override
  int initSelectedRowForComponent(int component) {
    return selectedIndex;
  }
}
