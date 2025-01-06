import 'dart:convert';
import 'dart:io';

import 'package:bruno/bruno.dart';
import 'package:easy_chat/common/image_processor.dart';
import 'package:easy_chat/export.dart';
import 'package:easy_chat/global/global_controller.dart';
import 'package:easy_chat/models/em_user_ext.dart';
import 'package:easy_chat/models/params/profile_params.dart';
import 'package:easy_chat/models/user_info.dart';
import 'package:easy_chat/routes/page_routes.dart';
import 'package:easy_chat/views/input/common_input_logic.dart';
import 'package:flutter/material.dart';
import 'package:gogoboom_flutter_getx_start/gogoboom_flutter_getx_start.dart';
import 'package:im_flutter_sdk/im_flutter_sdk.dart';
import 'package:wechat_assets_picker/wechat_assets_picker.dart';

import 'profile_state.dart';

class ProfileLogic extends GetxController {
  final ProfileState state = ProfileState();
  final GlobalController _globalController = Get.find();

  EMUserInfo? get emUserInfo => _globalController.emUserInfo;
  UserInfo? get userInfo => _globalController.userInfo;

  String? get nickname => userInfo?.nickName;

  get genderStr => userInfo?.gender == 2 ? '女' : '男';
  get gender => userInfo?.gender;

  get birth => emUserInfo?.birth;

  get signature => userInfo?.introduce;

  get city => _globalController.userExt()?.city;

  String? get avatar => userInfo?.avatar;

  get showName => userInfo!.showName;

  selectAvatar() {
    List<BrnCommonActionSheetItem> actions = [];
    actions.add(BrnCommonActionSheetItem(
      '从相册中选取',
      actionStyle: BrnCommonActionSheetItemStyle.normal,
    ));
    actions.add(BrnCommonActionSheetItem(
      '拍摄',
      actionStyle: BrnCommonActionSheetItemStyle.normal,
    ));
    // 展示actionSheet
    showModalBottomSheet(
        context: Get.context!,
        backgroundColor: Colors.transparent,
        builder: (BuildContext context) {
          return BrnCommonActionSheet(
            title: '更换头像',
            actions: actions,
            cancelTitle: '取消',
            clickCallBack: (int index, BrnCommonActionSheetItem actionEle) =>
                _selectImage(index),
          );
        });
  }

  void _selectImage(int type) async {
    List<AssetEntity>? assetEntities = await selectPhoto(type, size: 1);
    if (assetEntities == null || assetEntities.isEmpty) {
      logger.w('所选内容为空！');
      return;
    }
    var originFile = await assetEntities[0].originFile;
    if (!(originFile?.existsSync() ?? false)) {
      logger.w('图片不存在！');
      return;
    }
    final File file = await compressAndGetFile(originFile);
    // 上传图片
    final res = await clientRestClient.uploadFiles([file]);
    if (res.isNotEmpty) {
      await memberRestClient.update(ProfileParams(
        gender: gender,
        nickName: nickname,
        avatar: res[0].imageUrl,
        introduce: signature,
      ));
      _globalController.refreshUserInfo();

      EMUserInfo? newUser = await imManager.imUserService
          .updateOwnUserInfo(EMUserInfoType.AvatarURL, res[0].imageUrl!);
      _globalController.emUserInfo = newUser;
    } else {
      ToastUtil.show('图片不存在');
    }
  }

  editNickname() => Get.toNamed(PageRoutes.commonInput, arguments: {
        'type': CommonInputType.nickname,
        'value': userInfo?.nickName
      });

  onGender() {
    List<BrnCommonActionSheetItem> actions = [];
    actions.add(BrnCommonActionSheetItem(
      '男',
      actionStyle: BrnCommonActionSheetItemStyle.normal,
    ));
    actions.add(BrnCommonActionSheetItem(
      '女',
      actionStyle: BrnCommonActionSheetItemStyle.normal,
    ));

    // 展示actionSheet
    showModalBottomSheet(
        context: Get.context!,
        backgroundColor: Colors.transparent,
        builder: (BuildContext context) {
          return BrnCommonActionSheet(
            actions: actions,
            title: '选择性别',
            clickCallBack: (
              int index,
              BrnCommonActionSheetItem actionEle,
            ) async {
              await memberRestClient.update(ProfileParams(
                gender: index + 1,
                nickName: nickname,
                avatar: avatar,
                introduce: signature,
              ));
              _globalController.refreshUserInfo();
              // _globalController.emUserUpdate(await imManager.imUserService
              //     .updateOwnUserInfo(EMUserInfoType.Gender, '$index'));
            },
          );
        });
  }

  onBirth() => BrnDatePicker.showDatePicker(
        Get.context!,
        maxDateTime: DateTime.now(),
        initialDateTime: DateTime.parse('2020-01-01'),
        // 支持DateTimePickerMode.date、DateTimePickerMode.datetime、DateTimePickerMode.time
        pickerMode: BrnDateTimePickerMode.date,
        pickerTitleConfig: BrnPickerTitleConfig.Default,
        onConfirm: (dateTime, list) async {
          _globalController.emUserUpdate(await imManager.imUserService
              .updateOwnUserInfo(
                  EMUserInfoType.Birth,
                  DateTimeFormatter.formatDate(
                      dateTime, 'yyyy-MM-dd', DateTimePickerLocale.zh_cn)));
        },
      );

  onSignature() => Get.toNamed(PageRoutes.commonInput,
      arguments: {'type': CommonInputType.signature, 'value': userInfo?.introduce});

  onCity() => Get.toNamed(PageRoutes.cityPicker)?.then((city) async {
    logger.d('选择的城市: $city');
        if (city != null) {
          String cityName = (city as BrnSelectCityModel).name;
          EmUserExt? userExt = _globalController.userExt();
          userExt?.city = cityName;
          userExt ??= EmUserExt(cityName);
          logger.d('userExt: ${userExt.toJson()}');
          _globalController.emUserUpdate(await imManager.imUserService
              .updateOwnUserInfo(EMUserInfoType.Ext, jsonEncode(userExt)));
        }
      });
}
