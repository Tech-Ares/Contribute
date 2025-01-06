import 'package:easy_chat/models/contact_model.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class NavContactState {

  ///联系人
  final RxList<ContactModel> contactMembers = RxList.empty();
  ///头部
  final List<ContactModel> contactHeaders = [
    // ContactModel.custom('新的朋友', ContactType.friends, 'ic_contact_new_friend', Colors.orange),
    ContactModel.custom('群聊', ContactType.groups, 'ic_group_avatar_default', Colors.green),
    // ContactModel.custom('聊天室', ContactType.rooms, 'ic_contact_room', Colors.blue),
    ContactModel.custom('我的客服', ContactType.customer, 'ic_service_avatar', Colors.blue),
    // ContactModel.custom('公众号', ContactType.tags, Icons.person, Colors.blueAccent),
    // ContactInfo(
    //     name: '新的朋友',
    //     tagIndex: '↑',
    //     bgColor: Colors.orange,
    //     iconData: Icons.person_add),
    // ContactInfo(
    //     name: '群聊',
    //     tagIndex: '↑',
    //     bgColor: Colors.green,
    //     iconData: Icons.people),
    // ContactInfo(
    //     name: '标签',
    //     tagIndex: '↑',
    //     bgColor: Colors.blue,
    //     iconData: Icons.local_offer),
    // ContactInfo(
    //     name: '公众号',
    //     tagIndex: '↑',
    //     bgColor: Colors.blueAccent,
    //     iconData: Icons.person)
  ];

}
