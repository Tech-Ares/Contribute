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
-- Table structure for table `czim_complaint_info`
--

DROP TABLE IF EXISTS `czim_complaint_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_complaint_info` (
  `Id` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '投诉id',
  `MId` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '投诉用户id',
  `ComplaintDesc` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '投诉说明',
  `ImgOne` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '投诉图片',
  `ImgTwo` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '投诉图片2',
  `ImgThree` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '投诉图片3',
  `CrtDate` datetime DEFAULT NULL COMMENT '投诉时间',
  `Status` int DEFAULT NULL COMMENT '投诉状态\r\n0：发起投诉\r\n1：已受理\r\n2：已经完结',
  `AcceptDate` datetime DEFAULT NULL COMMENT '受理时间',
  `AcceptUser` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '受理人',
  `FinishedDate` datetime DEFAULT NULL COMMENT '完结时间',
  `FinishedUser` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '完结人',
  `IsActive` tinyint(1) NOT NULL DEFAULT '1' COMMENT '是否可用',
  `CDate` datetime(3) DEFAULT NULL COMMENT '创建时间',
  `UDate` datetime(3) DEFAULT NULL COMMENT '修改时间',
  `CUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '创建人',
  `UUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='会员投诉表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_complaint_info`
--

LOCK TABLES `czim_complaint_info` WRITE;
INSERT INTO `czim_complaint_info` VALUES ('247e67f8692543f88fb8e9e74717e565','287ab00591724e36b55ecd9c3ffddff7','456','http://xchat001.oss-cn-hongkong.aliyuncs.com/xchatoss/Screenshot_2022-02-16-23-07-34-578_巨丰微讯.png',NULL,NULL,'2022-02-17 17:54:57',0,'0001-01-01 00:00:00',NULL,'0001-01-01 00:00:00',NULL,1,'2022-02-17 17:54:57.274','2022-02-17 17:54:57.274','287ab00591724e36b55ecd9c3ffddff7','287ab00591724e36b55ecd9c3ffddff7'),('340b087c14b5484893f00289371aee72','5b96eb571930428f9b1710f971dfd38f','阿哈哈哈哈哈','http://xchat001.oss-cn-hongkong.aliyuncs.com/xchatoss/IMG_0164.PNG',NULL,NULL,'2022-02-20 00:54:58',0,'0001-01-01 00:00:00',NULL,'0001-01-01 00:00:00',NULL,1,'2022-02-20 00:54:58.498','2022-02-20 00:54:58.498','5b96eb571930428f9b1710f971dfd38f','5b96eb571930428f9b1710f971dfd38f'),('683c3fa3b1e54774893ba372a898dbe1','287ab00591724e36b55ecd9c3ffddff7','789','http://xchat001.oss-cn-hongkong.aliyuncs.com/xchatoss/我的名片 (13).jpg',NULL,NULL,'2022-02-17 17:55:39',0,'0001-01-01 00:00:00',NULL,'0001-01-01 00:00:00',NULL,1,'2022-02-17 17:55:38.579','2022-02-17 17:55:38.579','287ab00591724e36b55ecd9c3ffddff7','287ab00591724e36b55ecd9c3ffddff7'),('70ca0b198d9549a3876dfb9e4609488c','1669b947ce4c456e923ca79f5a8578dc','123','http://xchat001.oss-cn-hongkong.aliyuncs.com/xchatoss/我的名片 (13).jpg',NULL,NULL,'2022-02-19 23:53:13',0,'0001-01-01 00:00:00',NULL,'0001-01-01 00:00:00',NULL,1,'2022-02-19 23:53:12.647','2022-02-19 23:53:12.647','1669b947ce4c456e923ca79f5a8578dc','1669b947ce4c456e923ca79f5a8578dc'),('b66e9d0c03a346e0a0a34046d97d4b95','287ab00591724e36b55ecd9c3ffddff7','123',NULL,NULL,NULL,'2022-02-17 17:53:58',0,'0001-01-01 00:00:00',NULL,'0001-01-01 00:00:00',NULL,1,'2022-02-17 17:53:57.853','2022-02-17 17:53:57.853','287ab00591724e36b55ecd9c3ffddff7','287ab00591724e36b55ecd9c3ffddff7');
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
