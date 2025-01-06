
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

abstract class IMContactService {

  Future<List<String?>> getAllContactFromServer({bool db = false});

  void addContactListener(EMContactEventListener contactListener);

  void removeContactListener(EMContactEventListener contactListener);
}