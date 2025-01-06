// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_info.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserInfo _$UserInfoFromJson(Map<String, dynamic> json) => UserInfo(
      id: json['id'] as String?,
      mailbox: json['mailbox'] as String?,
      introduce: json['introduce'] as String?,
      loginName: json['loginName'] as String?,
      bsvNumber: json['bsvNumber'] as String?,
      nickName: json['nickName'] as String?,
      avatar: json['avatar'] as String?,
      gender: json['gender'] as int?,
      rNumber: json['rNumber'] as String?,
      regDate: json['regDate'] as String?,
      lastLoginDate: json['lastLoginDate'] as String?,
      frozenTime: json['frozenTime'] as String?,
      capital: (json['capital'] as num?)?.toDouble(),
      capitalLocking: (json['capitalLocking'] as num?)?.toDouble(),
      freeTimesPerDay: json['freeTimesPerDay'] as int?,
      allFreeTimesPerDay: json['allFreeTimesPerDay'] as int?,
      loginLat: (json['loginLat'] as num?)?.toDouble(),
      loginLong: (json['loginLong'] as num?)?.toDouble(),
      easePwd: json['easePwd'] as String?,
      isAgent: json['isAgent'] as bool?,
    );

Map<String, dynamic> _$UserInfoToJson(UserInfo instance) => <String, dynamic>{
      'id': instance.id,
      'loginName': instance.loginName,
      'bsvNumber': instance.bsvNumber,
      'nickName': instance.nickName,
      'avatar': instance.avatar,
      'mailbox': instance.mailbox,
      'introduce': instance.introduce,
      'gender': instance.gender,
      'rNumber': instance.rNumber,
      'regDate': instance.regDate,
      'lastLoginDate': instance.lastLoginDate,
      'frozenTime': instance.frozenTime,
      'capital': instance.capital,
      'capitalLocking': instance.capitalLocking,
      'freeTimesPerDay': instance.freeTimesPerDay,
      'loginLat': instance.loginLat,
      'loginLong': instance.loginLong,
      'allFreeTimesPerDay': instance.allFreeTimesPerDay,
      'easePwd': instance.easePwd,
      'isAgent': instance.isAgent,
    };
