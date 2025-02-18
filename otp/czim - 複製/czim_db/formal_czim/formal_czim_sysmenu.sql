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
-- Table structure for table `sysmenu`
--

DROP TABLE IF EXISTS `sysmenu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sysmenu` (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '菜单主键id',
  `Number` int DEFAULT '0' COMMENT '排序',
  `ComponentName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '组件名',
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '名称',
  `Icon` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '图标',
  `Path` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '路由',
  `Title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '菜单标题',
  `MenuType` char(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '菜单类型（menu\\button\\link\\ifream）',
  `Affix` bit(1) DEFAULT b'0' COMMENT '是否可以关闭',
  `ParentId` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '父节点',
  `Router` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '路由',
  `JumpUrl` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '跳转url',
  `Url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT 'url',
  `Close` int DEFAULT NULL COMMENT '是否关闭',
  `Show` int DEFAULT NULL COMMENT '是否显示',
  `IsActive` bit(1) DEFAULT NULL COMMENT '是否可用',
  `CUser` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `UUser` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `CDate` datetime DEFAULT NULL,
  `UDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sysmenu`
--

LOCK TABLES `sysmenu` WRITE;
INSERT INTO `sysmenu` VALUES ('0acb6f7faa75443b8b7df73620ad5f0a',30,'','member','el-icon-user','/member','会员管理','menu',_binary '\0','',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2021-12-28 11:02:33','2021-12-28 11:02:33'),('2b088e72d3194be59553bfacaee92b7d',71,'baseSetting/base','base','el-icon-operation','/baseSetting/base','配置','menu',_binary '\0','a71dff08082c4c06a94f21e7a69ba3fd',NULL,NULL,NULL,1,1,_binary '','3c4bba14649711ec956f0242ac110002','3c4bba14649711ec956f0242ac110002','2022-01-10 16:22:32','2022-01-10 16:22:32'),('329f1f638f7e45faa82aa2c699bc77b4',50,'','customerService','el-icon-service','/customerService','客服管理','menu',_binary '\0','',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2021-12-29 09:53:35','2021-12-29 09:53:35'),('37f75d7d59cf46aeba4358597c11673f',61,'liveStream/liveList','liveList','el-icon-video-play','/liveStream/liveList','直播列表','menu',_binary '\0','ecc0c27967ab4e7c8d58990246b4cb5e',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2022-01-10 11:39:41','2022-01-10 11:39:41'),('44f1f7ee23df44f385717ab8390c9837',25,'chat','chat','el-icon-promotion','/chat','聊天','menu',_binary '\0','',NULL,NULL,NULL,1,1,_binary '',NULL,'947702d3727d11ec956f0242ac110002','2022-01-14 21:26:55','2022-01-14 21:26:55'),('45718a84c9d14010ae3822f6e1704b7f',20,'setting/role','role','el-icon-document-copy','/setting/role','角色管理','menu',_binary '\0','79f151791f0042aabc592f537021d091',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2021-12-28 14:37:55','2021-12-28 14:37:55'),('4ae0cdbd47674f32bfdd3cbaae727f65',25,'setting/user','user','el-icon-avatar','/setting/user','用户管理','menu',_binary '\0','79f151791f0042aabc592f537021d091',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2021-12-28 14:38:34','2021-12-28 14:38:34'),('5cd75265eb8b4574bfee26b497b50e4b',31,'member/list','list','el-icon-document','/member/list','会员列表','menu',_binary '\0','0acb6f7faa75443b8b7df73620ad5f0a',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2021-12-29 09:38:15','2021-12-29 09:38:15'),('79c8ff2f4b6d4128a52b138d2723085c',45,'chatRoom/room','room','el-icon-comment','/chatroom/room','聊天室配置','menu',_binary '\0','8d4da11de4fe4bfba7f031177b7f3bee',NULL,NULL,NULL,1,1,_binary '',NULL,'947702d3727d11ec956f0242ac110002','2022-01-14 18:44:28','2022-01-14 18:44:28'),('79f151791f0042aabc592f537021d089',10,'home','dashboard','el-icon-menu','/dashboard','控制台','menu',_binary '','79f151791f0042aabc592f537021d093',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2021-12-28 17:53:33','2021-12-28 17:53:33'),('79f151791f0042aabc592f537021d090',20,'userCenter','userCenter','el-icon-user','/usercenter','个人信息','menu',_binary '\0','79f151791f0042aabc592f537021d093',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2021-12-28 17:53:27','2021-12-28 17:53:27'),('79f151791f0042aabc592f537021d091',1000,NULL,'setting','el-icon-setting','/setting','系统设置','menu',_binary '\0',NULL,NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2022-01-10 16:10:54','2022-01-10 16:10:54'),('79f151791f0042aabc592f537021d092',21,'setting/menu','settingMenu','el-icon-fold','/setting/menu','菜单管理','menu',_binary '\0','79f151791f0042aabc592f537021d091',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2021-12-31 22:06:11','2021-12-31 22:06:11'),('79f151791f0042aabc592f537021d093',10,'','home','el-icon-eleme-filled','/home','首页','menu',_binary '\0',NULL,'/home',NULL,'',1,1,_binary '',NULL,NULL,NULL,NULL),('8d4da11de4fe4bfba7f031177b7f3bee',40,'','chatRoom','el-icon-chat-dot-round','/chatroom','聊天管理','menu',_binary '\0','',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2022-01-10 09:39:26','2022-01-10 09:39:26'),('a71dff08082c4c06a94f21e7a69ba3fd',70,'baseSetting','baseSetting','el-icon-set-up','/baseSetting','基础配置','menu',_binary '\0','',NULL,NULL,NULL,1,1,_binary '','3c4bba14649711ec956f0242ac110002','3c4bba14649711ec956f0242ac110002','2022-01-10 16:13:54','2022-01-10 16:13:54'),('ad5c102106b247529f45ef479054b8bc',42,'chatRoom/groupChat','groupChat','el-icon-chat-dot-square','/chatRoom/groupChat','群聊管理','menu',_binary '\0','8d4da11de4fe4bfba7f031177b7f3bee',NULL,NULL,NULL,1,1,_binary '','cc236d2dc65a4f13be91690fe90e8fbd','cc236d2dc65a4f13be91690fe90e8fbd','2022-01-13 09:29:19','2022-01-13 09:29:19'),('cfc667c784de4770b61fb7363474b97c',51,'customerService/backAccount','backAccount','el-icon-coordinate','/customerservice/backaccount','客服账户','menu',_binary '\0','329f1f638f7e45faa82aa2c699bc77b4',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2022-01-10 09:39:41','2022-01-10 09:39:41'),('dce7dd8d0b554ccc98caf91e2bcecab5',22,'setting/userMenu','userMenu','el-icon-list','/setting/usermenu','角色菜单','menu',_binary '\0','79f151791f0042aabc592f537021d091',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2021-12-28 14:39:33','2021-12-28 14:39:33'),('ecc0c27967ab4e7c8d58990246b4cb5e',60,'liveStream','liveStream','el-icon-video-camera','/liveStream','直播管理','menu',_binary '\0','',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2022-01-10 09:47:54','2022-01-10 09:47:54'),('ffbf47e4bd3547059bdc8e9e7ef65f55',52,'customerService/preset','preset','el-icon-finished','/customerService/preset','预设消息','menu',_binary '\0','329f1f638f7e45faa82aa2c699bc77b4',NULL,NULL,NULL,1,1,_binary '',NULL,'3c4bba14649711ec956f0242ac110002','2022-01-10 09:39:49','2022-01-10 09:39:49');
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
