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
-- Table structure for table `czim_chatgroup_info`
--

DROP TABLE IF EXISTS `czim_chatgroup_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_chatgroup_info` (
  `id` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '0' COMMENT '群主键',
  `EaseChatgroupId` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '环信id',
  `GroupName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '群名称',
  `GroupImg` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '群图标',
  `Announcement` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '群公告',
  `Description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '群组描述',
  `MemberSonly` bit(1) DEFAULT b'0' COMMENT '加入群组是否需要群主或者群管理员审批。true：是，false：否。',
  `AllowInvites` bit(1) DEFAULT b'0' COMMENT '是否允许群成员邀请别人加入此群。 true：允许群成员邀请人加入此群，false：只有群主才可以往群里加人。由于只有私有群才允许群成员邀请人加入此群，所以当群组为私有群时，该参数设置为true才有效。默认为false',
  `Maxusers` int DEFAULT NULL COMMENT '群成员上限，创建群组的时候设置需要设置，',
  `OwnId` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '主管理员id',
  `UserNum` int DEFAULT '1' COMMENT '用户数量',
  `IsPublic` bit(1) NOT NULL DEFAULT b'1' COMMENT '群组类型：true：公开群，false：私有群。是否',
  `IsProhibit` bit(1) DEFAULT b'0' COMMENT '全员禁言',
  `IsDefault` bit(1) DEFAULT b'0' COMMENT '是否默认',
  `CrtDate` datetime DEFAULT NULL COMMENT '创建时间',
  `IsActive` bit(1) DEFAULT b'1' COMMENT '是否可用',
  `CDate` datetime(3) DEFAULT NULL COMMENT '创建时间',
  `UDate` datetime(3) DEFAULT NULL COMMENT '修改时间',
  `CUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '创建人',
  `UUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE KEY `EaseChatgroupId` (`EaseChatgroupId`) USING BTREE COMMENT '环信群id'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='群组管理';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_chatgroup_info`
--

LOCK TABLES `czim_chatgroup_info` WRITE;
INSERT INTO `czim_chatgroup_info` VALUES ('95c971bf77204800a9ca8336320a77e6','173735077281793','巨丰微讯','http://xchat001.oss-cn-hongkong.aliyuncs.com/defaultoss/ed2d824735e0079996d5629a0b773d5.png','','欢迎来到巨丰微讯',_binary '\0',_binary '',3000,'44d5813a053b4724b3659b024a097e13',0,_binary '',_binary '\0',_binary '\0','2022-02-17 16:04:41',_binary '','2022-02-17 16:04:41.007','2022-02-17 16:04:41.218','947702d3727d11ec956f0242ac110002',NULL);
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
