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
-- Table structure for table `czim_feed_back`
--

DROP TABLE IF EXISTS `czim_feed_back`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_feed_back` (
  `Id` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '反馈表Id',
  `MemberId` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '会员id',
  `Content` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '反馈内容',
  `IsHandle` bit(1) DEFAULT b'0' COMMENT '是否已处理',
  `HandleUser` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '处理人',
  `IsActive` bit(1) DEFAULT b'1' COMMENT '是否可用',
  `CDate` datetime(3) DEFAULT NULL COMMENT '创建时间',
  `UDate` datetime(3) DEFAULT NULL COMMENT '修改时间',
  `CUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '创建人',
  `UUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_feed_back`
--

LOCK TABLES `czim_feed_back` WRITE;
INSERT INTO `czim_feed_back` VALUES ('15d4599fa01b40dcb27ea9548e93511b','68558c39bd184463a836cc82ac2d726c','123',_binary '\0',NULL,_binary '','2022-01-09 16:48:09.784','2022-01-09 16:48:09.785',NULL,NULL),('5e35603c1aa04c6a8852c8ef877f769c','1505316dc6564efd8b22c5575711de3b','ghhh',_binary '\0',NULL,_binary '','2022-01-15 17:59:02.110','2022-01-15 17:59:02.110',NULL,NULL),('721710ff993a4254a326b51e98ab2b1f','68558c39bd184463a836cc82ac2d726c','666',_binary '\0',NULL,_binary '','2022-01-09 16:49:46.773','2022-01-09 16:49:46.773',NULL,NULL),('9557d292de5b4a45bb4e8e8faf031503','68558c39bd184463a836cc82ac2d726c','估计',_binary '\0',NULL,_binary '','2022-01-09 16:53:17.958','2022-01-09 16:53:17.958',NULL,NULL),('e79777d997554a84a247d686ed9dd552','68558c39bd184463a836cc82ac2d726c','22222',_binary '\0',NULL,_binary '','2022-01-09 16:48:38.263','2022-01-09 16:48:38.263',NULL,NULL);
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
