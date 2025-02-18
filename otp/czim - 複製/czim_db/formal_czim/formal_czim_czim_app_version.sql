-- MySQL dump 10.13  Distrib 8.0.29, for Win64 (x86_64)
--
-- Host: rm-3nspf671ij4im81m4zo.mysql.rds.aliyuncs.com    Database: formal_czim
-- ------------------------------------------------------
-- Server version	8.0.25

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `czim_app_version`
--

DROP TABLE IF EXISTS `czim_app_version`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_app_version` (
  `id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
  `VersionCode` int DEFAULT NULL COMMENT '版本编号',
  `PhoneSystem` int DEFAULT '1' COMMENT '手机系统1：安卓2：苹果',
  `Channel` char(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '0' COMMENT '渠道',
  `Version` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '版本号',
  `Title` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '更新提示',
  `Content` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '更新内容',
  `Focus` bit(1) DEFAULT NULL COMMENT '是否强制更新',
  `ApkDownLoadUrl` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '安卓安装包地址',
  `IosLinkUrl` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT 'ios外链地址',
  `UpdateDate` datetime DEFAULT NULL COMMENT '更新时间',
  `IsActive` bit(1) DEFAULT b'1' COMMENT '是否可用',
  `CDate` datetime(3) DEFAULT NULL COMMENT '创建时间',
  `UDate` datetime(3) DEFAULT NULL COMMENT '修改时间',
  `CUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '创建人',
  `UUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='app版本';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_app_version`
--

LOCK TABLES `czim_app_version` WRITE;
INSERT INTO `czim_app_version` VALUES (4,4,1,'czimapp','0.0.4','Android新版本','功能完善',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(5,5,1,'czimapp','0.0.5','Android新版本','调整默认背景图\r\n增加提示音\r\n增加免打扰',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(6,6,1,'czimapp','0.0.6','Android新版本','阅后即焚\r\n其他一些优化',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(7,7,1,'czimapp','0.0.7','Android新版本','群聊历史记录\r\n背景色和字体颜色',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(8,8,1,'czimapp','0.0.8','Android新版本','增加群聊功能\r\n阅后即焚优化',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(9,9,1,'czimapp','0.0.9','Android新版本','阅后即焚，接收端处理消息撤回\r\n群@功能实现',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(10,10,1,'czimapp','0.1.0','Android新版本','普通成员发送群消息\r\nflutter降版本解决阅后消息接收问题',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(11,11,1,'czimapp','0.1.1','Android新版本','修复一些bug\r\n扫码加好友\r\n扫码加群\r\n一些奇怪的问题',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(12,12,1,'czimapp','0.1.2','Android新版本','群聊查看会员页面\r\n修复ios语音bug\r\n群聊可以点击查看会员信息\r\n消息推送处理',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(13,13,1,'czimapp','0.1.3','Android新版本','环信推送\r\n通知栏显示',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(15,15,1,'czimapp','0.1.5','Android新版本','换key\r\n推送测试',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(16,16,1,'czimapp','0.1.6','Android新版本','换key\r\n推送测试',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(17,17,1,'czimapp','0.1.7','Android新版本','问题修复',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(18,18,1,'czimapp','0.1.8','Android新版本','问题修复\r\n离线推送声音配置\r\n离线通知栏中英文修改\r\n苹果白屏的问题',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(20,20,1,'czimapp','0.2.0','Android新版本','问题修复\r\n离线推送声音配置\r\n离线通知栏中英文修改\r\n苹果白屏的问题',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(21,21,1,'czimapp','0.2.1','Android新版本','更新环信token无法获取有效问题\r\n增加App超时自动登出',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(24,24,1,'czimapp','0.2.4','Android新版本','巨丰微讯上架',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(25,25,1,'czimapp','0.2.5','Android新版本','群共享文件',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(26,26,1,'czimapp','0.2.6','Android新版本','视频文件，大文件上传测试',_binary '','http://xchat001.oss-cn-hongkong.aliyuncs.com/jfwx_packages/jfwx_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(27,27,1,'czimapp','0.2.7','Android新版本','更新视频拍摄卡住问题',_binary '','http://faeapi.oss-cn-hongkong.aliyuncs.com/mt5package/xchat_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','',''),(28,28,1,'czimapp','0.2.8','Android新版本','巨丰微讯更换服务器',_binary '','https://jfwxoss.oss-cn-hongkong.aliyuncs.com/a_jfwxpackage/jfwx_install.apk','','2021-05-09 22:50:37',_binary '','2021-05-09 22:50:37.187','2021-05-09 22:50:37.187','','');
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
