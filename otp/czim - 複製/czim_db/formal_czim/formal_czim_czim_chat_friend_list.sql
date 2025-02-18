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
-- Table structure for table `czim_chat_friend_list`
--

DROP TABLE IF EXISTS `czim_chat_friend_list`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_chat_friend_list` (
  `Id` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '好友id',
  `MemberId` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '会员id(app会员id)',
  `UserId` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '好友id（管理员userId）',
  `CrtDate` datetime NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '成为好友时间',
  `JoinLtr` bit(1) DEFAULT b'0' COMMENT '左删右',
  `JoinRtl` bit(1) DEFAULT b'0' COMMENT '右删左',
  `IsActive` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否有效',
  `CUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '' COMMENT '创建人',
  `CDate` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `UUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '' COMMENT '修改人',
  `UDate` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE,
  KEY `MemberId` (`MemberId`) USING BTREE COMMENT '会员id',
  KEY `UserId` (`UserId`) USING BTREE COMMENT '管理员id'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='好友列表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_chat_friend_list`
--

LOCK TABLES `czim_chat_friend_list` WRITE;
INSERT INTO `czim_chat_friend_list` VALUES ('0a193c85cec944f58eae2af2fa4f4baa','19b4d720e3df4636b0109b73d81a6698','1669b947ce4c456e923ca79f5a8578dc','2022-02-18 15:01:41',_binary '\0',_binary '\0',_binary '',NULL,'2022-02-18 15:01:41',NULL,'2022-02-18 15:01:41'),('2a74a6f189c8477cbb0b116df2b6e502','6bfff9e967b14d95b727bdd077dfdfbd','5b96eb571930428f9b1710f971dfd38f','2022-02-17 18:23:24',_binary '\0',_binary '\0',_binary '',NULL,'2022-02-17 18:23:24',NULL,'2022-02-17 18:23:24'),('308826b9a8b44cfca3cea5ea23371343','287ab00591724e36b55ecd9c3ffddff7','1669b947ce4c456e923ca79f5a8578dc','2022-02-18 11:53:55',_binary '\0',_binary '\0',_binary '',NULL,'2022-02-18 11:53:55',NULL,'2022-02-18 11:53:55'),('3819bdc8eb47436ebe2efaec2e403fc1','a782b031156d4028899a412d6319fc50','5b96eb571930428f9b1710f971dfd38f','2022-02-17 16:30:03',_binary '\0',_binary '\0',_binary '',NULL,'2022-02-17 16:30:03',NULL,'2022-02-17 16:30:03'),('4989309d6e034d099a168d0ad700e03e','1dc00ad0582e4148a1c61e9d36d10bd2','5b96eb571930428f9b1710f971dfd38f','2022-02-17 17:31:14',_binary '\0',_binary '\0',_binary '',NULL,'2022-02-17 17:31:14',NULL,'2022-02-17 17:31:14'),('4d0ef4c083674d65accf386f12c8c7d4','19b4d720e3df4636b0109b73d81a6698','f974d09ccb784991a7321aa28ab1221f','2022-02-18 15:27:25',_binary '\0',_binary '\0',_binary '',NULL,'2022-02-18 15:27:25',NULL,'2022-02-18 15:27:25'),('59531af70bc4431692509e3c4c50a327','19b4d720e3df4636b0109b73d81a6698','5b96eb571930428f9b1710f971dfd38f','2022-02-17 19:26:58',_binary '\0',_binary '\0',_binary '','5b96eb571930428f9b1710f971dfd38f','2022-02-17 19:26:58','5b96eb571930428f9b1710f971dfd38f','2022-02-17 19:26:58'),('82c57880d31743e38842eb31389b782c','ce9c2b2057254ce994f27c9835345274','5b96eb571930428f9b1710f971dfd38f','2022-02-17 17:31:32',_binary '\0',_binary '\0',_binary '',NULL,'2022-02-17 17:31:32',NULL,'2022-02-17 17:31:32'),('8975bbc009554c189876289254d0a998','519651d418c44749b9888fadd2fb35cd','5b96eb571930428f9b1710f971dfd38f','2022-02-20 08:48:31',_binary '\0',_binary '\0',_binary '',NULL,'2022-02-20 08:48:31',NULL,'2022-02-20 08:48:31'),('91d38406f59b460d9e11dd552e879be1','d345460a8a1f4f39ac671a390d79a6da','5b96eb571930428f9b1710f971dfd38f','2022-02-17 17:54:55',_binary '\0',_binary '\0',_binary '',NULL,'2022-02-17 17:54:55',NULL,'2022-02-17 17:54:55'),('9e6aa486c58f49e7987fa0c183422c7c','daf4f87e01e646029b464254d9f1e08f','5b96eb571930428f9b1710f971dfd38f','2022-02-17 16:30:48',_binary '\0',_binary '\0',_binary '',NULL,'2022-02-17 16:30:48',NULL,'2022-02-17 16:30:48'),('b6801f2a507c4b24b140ea58ddb7cf46','a782b031156d4028899a412d6319fc50','1669b947ce4c456e923ca79f5a8578dc','2022-02-18 23:00:01',_binary '\0',_binary '\0',_binary '','1669b947ce4c456e923ca79f5a8578dc','2022-02-18 23:00:01','1669b947ce4c456e923ca79f5a8578dc','2022-02-18 23:00:01'),('f308bd43faea4a28bb4525eda7b52c8c','d2874769315d46d1bea349502ce1d377','5b96eb571930428f9b1710f971dfd38f','2022-02-17 17:42:15',_binary '\0',_binary '\0',_binary '',NULL,'2022-02-17 17:42:15',NULL,'2022-02-17 17:42:15'),('f735fd50f62c46c782d4d32c6c03c052','128c73fde1bf4262aed5f22e55efdf1b','5b96eb571930428f9b1710f971dfd38f','2022-02-17 22:09:05',_binary '\0',_binary '\0',_binary '',NULL,'2022-02-17 22:09:05',NULL,'2022-02-17 22:09:05');
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
