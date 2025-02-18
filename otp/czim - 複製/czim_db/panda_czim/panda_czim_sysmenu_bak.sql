-- MySQL dump 10.13  Distrib 8.0.29, for Win64 (x86_64)
--
-- Host: rm-3nspf671ij4im81m4zo.mysql.rds.aliyuncs.com    Database: panda_czim
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
-- Table structure for table `sysmenu_bak`
--

DROP TABLE IF EXISTS `sysmenu_bak`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sysmenu_bak` (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Number` int DEFAULT NULL,
  `Name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Url` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Router` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ComponentName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Icon` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ParentId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Show` int DEFAULT NULL,
  `Close` int DEFAULT NULL,
  `JumpUrl` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  `CUser` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `UUser` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `CDate` datetime DEFAULT NULL,
  `UDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sysmenu_bak`
--

LOCK TABLES `sysmenu_bak` WRITE;
INSERT INTO `sysmenu_bak` VALUES ('0abfd53b-6bde-42c6-9f99-e32775bc31dd',180,'组织机构','views/system/organization/index','/system/organization','system_organization','ClusterOutlined','9591f249-1471-44f7-86b5-6fdae8b93888',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('10E7B3CF-D846-4B1B-7662-08D95814698B',200,'操作日志','views/system/sys_operation_log/index','/system/sys_operation_log','sys_operation_log','ContainerOutlined','9591F249-1471-44F7-86B5-6FDAE8B93888',1,1,'',NULL,NULL,NULL,NULL,NULL),('15455a8d-262d-4266-85b3-08d8b9f578ef',40,'图表 AntV G2',NULL,NULL,NULL,'PieChartOutlined','6f48b583-9c8f-490f-a3e0-81fc5f2e71b4',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('1ec76c4c-b9b3-4517-9854-f08cd11d653d',90,'基础信息',NULL,NULL,NULL,'GoldOutlined',NULL,1,1,NULL,NULL,NULL,NULL,NULL,NULL),('35dc130e-4034-11eb-a2cb-4cedfb69a3e8',110,'标准表格','views/list/index','/list','list',NULL,'9674d439-864e-4bf8-9dd8-08d7d70ad6bb',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('383053F9-DA70-4A0D-B9F0-08D94F7318CD',190,'数据字典','views/system/dictionary/index','/system/dictionary','system_dictionary','FileDoneOutlined','9591F249-1471-44F7-86B5-6FDAE8B93888',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('38d864ff-f6e7-43af-8c5c-8bbcf9fa586d',100,'账户管理','views/system/user/index','/system/user','system_user','UserOutlined','9591f249-1471-44f7-86b5-6fdae8b93888',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('38fd48a9-4035-11eb-a2cb-4cedfb69a3e8',30,'按钮','views/button','/button','button','AppstoreOutlined','6f48b583-9c8f-490f-a3e0-81fc5f2e71b4',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('60ae9382-31ab-4276-a379-d3876e9bb783',110,'角色管理','views/system/role/index','/system/role','system_role','TeamOutlined','9591f249-1471-44f7-86b5-6fdae8b93888',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('63a6d076-3e22-4d26-85b4-08d8b9f578ef',10,'基础图表','views/chart/base','/chart/base','chart_base',NULL,'15455a8d-262d-4266-85b3-08d8b9f578ef',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('6f48b583-9c8f-490f-a3e0-81fc5f2e71b4',10,'更多示例',NULL,NULL,NULL,NULL,NULL,1,1,NULL,NULL,NULL,NULL,NULL,NULL),('7c34c2fd-98ed-4655-aa04-bb00b915a751',10,'会员管理','views/base/member','/base/member','base_member','UsergroupAddOutlined','1ec76c4c-b9b3-4517-9854-f08cd11d653d',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('90E7F189-9160-4E46-E2D5-08D958181601',70,'跳转外部地址',NULL,NULL,NULL,'RadarChartOutlined','6F48B583-9C8F-490F-A3E0-81FC5F2E71B4',1,1,'https://antv.vision/zh',NULL,NULL,NULL,NULL,NULL),('9591f249-1471-44f7-86b5-6fdae8b93888',100,'系统管理',NULL,NULL,NULL,'DesktopOutlined',NULL,1,1,NULL,NULL,NULL,NULL,NULL,NULL),('9674d439-864e-4bf8-9dd8-08d7d70ad6bb',50,'表格管理',NULL,NULL,NULL,'TableOutlined','6f48b583-9c8f-490f-a3e0-81fc5f2e71b4',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('9a27dfc2-4a66-11eb-87b1-4cedfb69a3e8',160,'接口文档','views/system/swagger','/system/swagger','system_swagger','FileSearchOutlined','9591f249-1471-44f7-86b5-6fdae8b93888',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('9b01f9f3-5621-4bc2-85b5-08d8b9f578ef',20,'更多示例','views/chart/more','/chart/more','chart_more',NULL,'15455a8d-262d-4266-85b3-08d8b9f578ef',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('bd024f3a-99a7-4407-861c-a016f243f222',140,'角色功能','views/system/rolefunction/index','/system/role/function','system_role_function','UserSwitchOutlined','9591f249-1471-44f7-86b5-6fdae8b93888',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('cc99f568-a831-4ea8-4c7a-08d8bba503bf',60,'富文本编辑器','views/editor','/editor','editor','PicRightOutlined','6f48b583-9c8f-490f-a3e0-81fc5f2e71b4',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('d29fde94-0d6a-4a64-8446-55ee63df5885',170,'岗位管理','views/system/post/index','/system/post','system_post','IdcardOutlined','9591f249-1471-44f7-86b5-6fdae8b93888',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('d721fc55-2174-40eb-bb37-5c54a158525a',120,'功能管理','views/system/function/index','/system/function','system_function','ControlOutlined','9591f249-1471-44f7-86b5-6fdae8b93888',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('d788896b-4033-11eb-a2cb-4cedfb69a3e8',100,'基础表格','views/baseList','/baseList','base_list',NULL,'9674d439-864e-4bf8-9dd8-08d7d70ad6bb',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('e5d4da6b-aab0-4aaa-982f-43673e8152c0',130,'菜单功能','views/system/menu/index','/system/menu','system_menu','MenuOutlined','9591f249-1471-44f7-86b5-6fdae8b93888',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('f35d64ae-ecb7-4d06-acfb-d91595966d9e',150,'修改密码','views/system/changePassword/index','/system/change/password','system_change_password','FormOutlined','9591f249-1471-44f7-86b5-6fdae8b93888',1,1,NULL,NULL,NULL,NULL,NULL,NULL),('f55a8858-ede4-4380-85b2-08d8b9f578ef',10,'Antd Vue3.0组件库','views/antd_vue_components','/antd/vue/components','antd_vue_components','LayoutOutlined','6f48b583-9c8f-490f-a3e0-81fc5f2e71b4',1,1,NULL,NULL,NULL,NULL,NULL,NULL);
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
