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
-- Table structure for table `czim_chatgroup_file`
--

DROP TABLE IF EXISTS `czim_chatgroup_file`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_chatgroup_file` (
  `id` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '群成员表id',
  `ChatgroupId` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '群id',
  `MemberId` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '上传会员id',
  `UploadDate` datetime DEFAULT NULL COMMENT '上传时间',
  `FileType` smallint DEFAULT NULL COMMENT '文件类型\r\n1：doc\r\n2：docx\r\n3：txt\r\n4：zip\r\n5：pdf\r\n6：video\r\n9：other',
  `FileName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '文件名称',
  `FileUrl` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '文件路径',
  `FileSize` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '文件大小（带单位）',
  `IsActive` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否可用',
  `CDate` datetime(3) DEFAULT NULL COMMENT '创建时间',
  `UDate` datetime(3) DEFAULT NULL COMMENT '修改时间',
  `CUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '创建人',
  `UUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_chatgroup_file`
--

LOCK TABLES `czim_chatgroup_file` WRITE;
INSERT INTO `czim_chatgroup_file` VALUES ('81c1cc33461148af980f81ebf70b5bee','95c971bf77204800a9ca8336320a77e6','5b96eb571930428f9b1710f971dfd38f','2022-02-20 08:33:08',2,'日内收评.docx','http://xchat001.oss-cn-hongkong.aliyuncs.com/xchatoss/groupfiles/173735077281793/日内收评.docx','12.56 kB',_binary '','2022-02-20 08:33:08.183','2022-02-20 08:33:08.183','5b96eb571930428f9b1710f971dfd38f',NULL),('9a87f1a867bf409ea9709810e8536ebe','95c971bf77204800a9ca8336320a77e6','1669b947ce4c456e923ca79f5a8578dc','2022-02-19 22:59:13',9,'Screenrecord-2022-02-07-00-22-25-577.mp4','http://xchat001.oss-cn-hongkong.aliyuncs.com/xchatoss/groupfiles/173735077281793/Screenrecord-2022-02-07-00-22-25-577.mp4','22.57 MB',_binary '','2022-02-19 22:59:13.163','2022-02-19 22:59:13.163','1669b947ce4c456e923ca79f5a8578dc',NULL),('d7309c01c52346d48dc305c60936fb63','95c971bf77204800a9ca8336320a77e6','1669b947ce4c456e923ca79f5a8578dc','2022-02-19 23:24:12',9,'share_bb8a48c188b197e12288738d4b39d31e.mp4','http://xchat001.oss-cn-hongkong.aliyuncs.com/xchatoss/groupfiles/173735077281793/share_bb8a48c188b197e12288738d4b39d31e.mp4','2.97 MB',_binary '','2022-02-19 23:24:12.378','2022-02-19 23:24:12.378','1669b947ce4c456e923ca79f5a8578dc',NULL),('e9fd8c776e7e4d1ea7257b17e6e6e8d6','95c971bf77204800a9ca8336320a77e6','1669b947ce4c456e923ca79f5a8578dc','2022-02-19 22:57:37',9,'xchat_v0.2.4.apk.1','http://xchat001.oss-cn-hongkong.aliyuncs.com/xchatoss/groupfiles/173735077281793/xchat_v0.2.4.apk.1','43.00 MB',_binary '','2022-02-19 22:57:36.876','2022-02-19 22:57:36.876','1669b947ce4c456e923ca79f5a8578dc',NULL);
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
