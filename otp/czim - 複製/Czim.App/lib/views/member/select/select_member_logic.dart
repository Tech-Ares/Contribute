import 'package:azlistview/azlistview.dart';
import 'package:bruno/bruno.dart' show BrnLoadingDialog;
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/contact_model.dart';
import 'package:easy_chat/models/params/im_group_info_params.dart';
import 'package:easy_chat/models/params/join_member_group_params.dart';
import 'package:easy_chat/models/params/join_member_room_params.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/base_controller.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';

import '../../../export.dart';
import 'select_member_state.dart';

class SelectMemberLogic extends BaseController<List<ContactModel>> {
  final GlobalController _globalController = Get.find();
  final SelectMemberState _state = SelectMemberState();
  final SelectType? selectType = Get.arguments?['type'];
  final String? id = Get.arguments?['id'];
  List<String>? selected = Get.arguments['selected'];

  @override
  Future<List<ContactModel>> Function() get requestFunc => () async {
        final List<ContactModel> members = await _fetchContacts();
        _handleSortContactMembers(members);
        // _state.selectContacts.addAll(members);
        return members;
      };

  get selectedItemCount =>
      _state.selectContacts.length;

  String getTitle() {
    switch (selectType) {
      case SelectType.createGroup:
        return '创建群组';
      case SelectType.addRoomMember:
      case SelectType.addGroupMember:
        return '添加好友到群组';
      case SelectType.removeRoomMember:
      case SelectType.removeGroupMember:
        return '删除群人员';
      case SelectType.roomMembers:
      case SelectType.groupMembers:
        return '群成员';
      case SelectType.singleRoomChoose:
      case SelectType.singleGroupChoose:
        return '选择你想要@的人';
      case SelectType.contacts:
      default:
        return '通讯录';
    }
  }

  bool get isSelectMode =>
      (selectType == SelectType.createGroup) ||
      (selectType == SelectType.removeRoomMember) ||
      (selectType == SelectType.addGroupMember) ||
      (selectType == SelectType.removeGroupMember) ||
      (selectType == SelectType.addRoomMember);

  void _handleSortContactMembers(List<ContactModel> list) {
    if (list.isEmpty) return;
    // A-Z sort.
    SuspensionUtil.sortListBySuspensionTag(list);
    // show sus tag.
    SuspensionUtil.setShowSuspensionStatus(list);
  }

  @override
  void onReady() {
    onRefresh();
    logger.d('当前群成员：$selected');
    super.onReady();
  }

  @override
  void onClose() {
    super.onClose();
  }

  _onCheckChange(int index) {
    var model = state![index];
    model.isSelect = !model.isSelect;
    refresh();
    if(model.isSelect) {
      _state.selectContacts.add(model);
    } else {
      _state.selectContacts.remove(model);
    }

    logger.d('已选择的：${ _state.selectContacts.map((element) => element.showName).toList()}');
    // _state.selectContacts.refresh();
  }

  bool isSelected(int index) {
    var current = state![index];
    return current.isSelect ||
        ((selected?.contains(current.contactId) ?? false));
  }

  Future? _startChat() async {
    ToastUtil.error('暂未开放');
    return;
    try {
      BrnLoadingDialog.show(Get.context!);
      var inviteMembers = _state.selectContacts
          .where((element) => element.isSelect)
          .map((e) => e.username)
          .toList();
      EMGroup? emGroup = await imManager.imGroupService.createGroup('群聊',
          inviteMembers: inviteMembers,
          settings: EMGroupOptions(
              style: EMGroupStyle.PrivateMemberCanInvite,
              inviteNeedConfirm: false));
      if (emGroup == null) {
        ToastUtil.error('创建群组失败');
        BrnLoadingDialog.dismiss(Get.context!);
        return;
      } else {
        EMConversation? conv = await EMClient.getInstance.chatManager
            .getConversation(emGroup.groupId!, EMConversationType.GroupChat);
        conv?.name = emGroup.name ?? '';
        logger.d('EMConversation ==> ${conv?.toJson()}');
        BrnLoadingDialog.dismiss(Get.context!);
        Get.offAndToNamed(PageRoutes.chat, arguments: conv);
      }
    } catch (e) {
      BrnLoadingDialog.dismiss(Get.context!);
      ToastUtil.show('$e');
      logger.e('创建群聊失败: $e');
    }
  }

  String? memberAvatar(int index) => _state.selectContacts[index].avatar;

  String? memberName(int index) => _state.selectContacts[index].showName;

  Future<List<ContactModel>> _fetchContacts() async {
    switch (selectType ?? SelectType.contacts) {
      case SelectType.createGroup:
      // final memberIds =
      //     await imManager.imContactService.getAllContactFromServer();
      // final imMembers = await imManager.imUserService
      //     .fetchUserInfo(memberIds.map((e) => e ?? '').toList());
      // return imMembers;
      case SelectType.addRoomMember:
      case SelectType.addGroupMember:
      case SelectType.contacts:
        final friends = await memberRestClient.friends();
        if (friends?.isEmpty ?? true) {
          return [];
        }
        return friends!.map((f) => ContactModel.fromFriend(f)).toList();
      case SelectType.removeGroupMember:
      case SelectType.groupMembers:
      case SelectType.singleGroupChoose:
      if (id == null) {
        logger.e('获取群组人员请传groupId');
        return [];
      } else {
        // EMGroup group = await imManager.imGroupService.fetchGroupInfo(id!);
        // List<String> allMembers = [];
        // allMembers.add(group.owner!);
        // //创建人和自己不能踢出
        // if (selectType == SelectType.removeRoomMember) {
        //   selected = [
        //     group.owner ?? '',
        //     _globalController.emUserInfo?.userId ?? ''
        //   ];
        // }
        // group.adminList?.forEach((element) {
        //   if (element is String) {
        //     allMembers.add(element);
        //   }
        // });
        // group.memberList?.forEach((element) {
        //   if (element is String) {
        //     allMembers.add(element);
        //   }
        // });
        // var members = await imManager.imUserService.fetchAllUsers(allMembers);
        // await _globalController.fetchUserExt(members);
        var members = await memberRestClient.imGroupMembers(IMGroupInfoParams(targetId: id));
        return members.dataList.map((e) => ContactModel.fromFriend(e)).toList();
      }
      case SelectType.roomMembers:
      case SelectType.removeRoomMember:
      case SelectType.singleRoomChoose:
        if (id == null) {
          logger.e('获取聊天室人员请传roomId');
          return [];
        } else {
          EMChatRoom chatRoom =
              await imManager.imRoomService.getChatRoomInfo(id!);
          List<String> allMembers = [];
          allMembers.add(chatRoom.owner!);
          //创建人和自己不能踢出
          if (selectType == SelectType.removeRoomMember) {
            selected = [
              chatRoom.owner,
              _globalController.emUserInfo?.userId ?? ''
            ];
          }
          chatRoom.adminList?.forEach((element) {
            if (element is String) {
              allMembers.add(element);
            }
          });
          chatRoom.memberList?.forEach((element) {
            if (element is String) {
              allMembers.add(element);
            }
          });
          var members = await imManager.imUserService.fetchAllUsers(allMembers);
          await _globalController.fetchUserExt(members);
          return members.map((e) => ContactModel.fromUserInfo(e)).toList();
        }
    }
  }

  GestureTapCallback? dispatchCheckTap(int index) {
    if(selectType == SelectType.singleGroupChoose || selectType == SelectType.singleRoomChoose) {
      return () => Get.back(result: state![index]);
    }
    // else if(selectType == SelectType.groupMembers) {
    //   //管理员可以与成员沟通
    //   if (_globalController.userInfo?.isAgent ?? false) {
    //     return () => Get.toNamed(PageRoutes.memberHome, arguments: state![index].friend?.targetId);
    //   }
    // }
    else {
      return (selected?.contains(state?[index].contactId) ?? false)
          ? null
          : () => _onCheckChange(index);
    }
  }

  dispatchCheckChange(int index) =>
      (selected?.contains(state?[index].contactId) ?? false)
          ? null
          : (value) => _onCheckChange(index);

  bool get isAddMode => selectType == SelectType.addRoomMember;

  dispatchAction() {
    switch(selectType ?? SelectType.contacts) {
      case SelectType.contacts:
      // nothing
        break;
      case SelectType.createGroup:
        _startChat();
        break;
      case SelectType.addRoomMember:
        _doAddMemberToRoom();
        break;
      case SelectType.removeRoomMember:
        _doRemoveMemberFromRoom();
        break;
      case SelectType.groupMembers:
      case SelectType.roomMembers:
        // nothing
        break;
      case SelectType.addGroupMember:
        _doAddMemberToRoom(isGroup: true);
        break;
      case SelectType.removeGroupMember:
        _doRemoveMemberFromRoom(isGroup: true);
        break;
      case SelectType.singleGroupChoose:
      case SelectType.singleRoomChoose:
        break;
    }
  }

  Future? _doAddMemberToRoom({bool isGroup = false}) async {
    var inviteMembers = _state.selectContacts
        .map((e) => e.contactId!)
        .toList();
    if(isGroup) {
      await memberRestClient.joinMemberToGroup(JoinMemberGroupParams(
        memberIds: inviteMembers,
        chatgroupId: id,
      ));
    } else {
      await memberRestClient.joinMemberToRoom(JoinMemberRoomParams(
        targetIds: inviteMembers,
        chatRoomId: id,
      ));
    }
    Get.back(result: true);
  }

  void _doRemoveMemberFromRoom({bool isGroup = false}) async {
    var inviteMembers = _state.selectContacts
        .map((e) => e.contactId!)
        .toList();
    if(isGroup) {
      await memberRestClient.removeMemberFromGroup(JoinMemberGroupParams(
        memberIds: inviteMembers,
        chatgroupId: id,
      ));
    } else {
      ToastUtil.error('暂未开放');
    }
    Get.back(result: true);
  }
}

enum SelectType {
  contacts, //我的好友
  createGroup, //创建群聊
  addRoomMember, //添加好友到聊天室
  addGroupMember, //添加好友到群组
  removeRoomMember, //聊天室踢人
  removeGroupMember, //群组踢人
  roomMembers, //聊天室成员
  groupMembers, //群组成员
  singleRoomChoose, //单选择聊天室人员
  singleGroupChoose, //单选择群组人员
}
