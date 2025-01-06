flutter build apk --target-platform android-arm --split-per-abi
flutter build ios --no-tree-shake-icons
#编译apk并写入渠道号
flutter pub run flutter_walle_plugin setChannel flutter build apk --no-tree-shake-icons --target-platform android-arm64
#编译apk并写入渠道信息
flutter pub run flutter_walle_plugin setInfo flutter build apk --no-tree-shake-icons
#查看指定apk文件渠道信息
flutter pub run flutter_walle_plugin getInfo /Users/glavesoft/workspace/fae_app/channelApks/app-release_vivo.apk
#keystore
keytool -genkey -v -keystore chat.jks -alias chat -storepass 123456 -keypass 123456 -keyalg RSA -keysize 2048 -validity 36500
#jsonSer
flutter pub run build_runner build
#splash
flutter pub run flutter_native_splash:create