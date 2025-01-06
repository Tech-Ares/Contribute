import 'package:json_annotation/json_annotation.dart';

part 'upgrade_version.g.dart';

@JsonSerializable()
class UpgradeVersion {
  final int? id;
  final int? phoneSystem;
  final int? versionCode;
  final String? version;
  final String? channel;
  final String? title;
  final String? content;
  final bool? focus;
  final String? apkDownLoadUrl;
  final String? iosLinkUrl;

  UpgradeVersion(
      {this.id,
      this.phoneSystem,
      this.versionCode,
      this.version,
      this.channel,
      this.title,
      this.content,
      this.focus,
      this.apkDownLoadUrl,
      this.iosLinkUrl});

  factory UpgradeVersion.fromJson(Map<String, dynamic> json) =>
      _$UpgradeVersionFromJson(json);

  Map<String, dynamic> toJson() => _$UpgradeVersionToJson(this);

  bool shouldUpgrade(String version) {
    if (this.version == null) {
      return false;
    }
    final int latestVersion = int.parse(this.version!.replaceAll('.', ''));
    final int currentVersion = int.parse(version.replaceAll('.', ''));
    return latestVersion > currentVersion;
  }

  factory UpgradeVersion.test() {
    return UpgradeVersion(
      version: '1.0.1',
    );
  }
}
