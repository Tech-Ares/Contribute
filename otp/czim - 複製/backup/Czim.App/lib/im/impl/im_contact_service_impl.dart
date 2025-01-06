import 'package:easy_chat/im/im_contact_service.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';


class IMContactServiceImpl extends IMContactService {
  @override
  Future<List<String?>> getAllContactFromServer({bool db = false}) async {
    try {
      if (db) {
        return await EMClient.getInstance.contactManager.getAllContactsFromDB();
      }
      return await EMClient.getInstance.contactManager
          .getAllContactsFromServer();
    } on EMError catch (e) {
      logger.e('操作失败，原因是: $e');
    }
    return [];
  }

  @override
  void addContactListener(EMContactEventListener contactListener) {
    EMClient.getInstance.contactManager.addContactListener(contactListener);
  }

  @override
  void removeContactListener(EMContactEventListener contactListener) {
    EMClient.getInstance.contactManager.removeContactListener(contactListener);
  }
}
