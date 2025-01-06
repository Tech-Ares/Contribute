import 'package:easy_chat/models/contact_model.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';

class SelectMemberState {

  RxList<ContactModel> selectContacts = RxList.empty();
  RxList<ContactModel> unselectContacts = RxList.empty();
}
