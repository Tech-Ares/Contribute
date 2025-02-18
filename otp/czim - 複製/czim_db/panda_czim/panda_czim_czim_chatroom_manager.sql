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
-- Table structure for table `czim_chatroom_manager`
--

DROP TABLE IF EXISTS `czim_chatroom_manager`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_chatroom_manager` (
  `id` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '0' COMMENT '聊天室主键',
  `ChatRoomId` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '聊天室id',
  `ManagerId` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '管理员(sysuser表id)',
  `IsSuper` bit(1) DEFAULT b'0' COMMENT '是否超管',
  `CrtDate` datetime DEFAULT NULL COMMENT '创建时间',
  `IsActive` bit(1) DEFAULT b'1' COMMENT '是否可用',
  `CDate` datetime(3) DEFAULT NULL COMMENT '创建时间',
  `UDate` datetime(3) DEFAULT NULL COMMENT '修改时间',
  `CUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '创建人',
  `UUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE KEY `ChatRoomId_ManagerId` (`ChatRoomId`,`ManagerId`) USING BTREE COMMENT '管理员和聊天室唯一',
  KEY `ChatRoomId` (`ChatRoomId`) USING BTREE COMMENT '聊天室id',
  KEY `ManagerId` (`ManagerId`) USING BTREE COMMENT '管理员id'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='聊天室管理员列表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_chatroom_manager`
--

LOCK TABLES `czim_chatroom_manager` WRITE;
INSERT INTO `czim_chatroom_manager` VALUES ('5adf0057a29245dd97f3b99741fe33de','e0de86165df745a291b2fcec2920a370','a0e7ae3ac6e94dd2b36514f12da6100b',_binary '\0','2022-01-18 21:59:16',_binary '','2022-01-18 21:59:16.471','2022-01-18 21:59:16.471','947702d3727d11ec956f0242ac110002','947702d3727d11ec956f0242ac110002'),('b468a2d924564d23ac5b952ee42323d9','e0de86165df745a291b2fcec2920a370','2c103ba6742644dea3bb02624af6c559',_binary '','2022-01-18 20:02:33',_binary '','2022-01-18 20:02:33.293','2022-01-18 20:02:33.293','947702d3727d11ec956f0242ac110002','947702d3727d11ec956f0242ac110002');
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
