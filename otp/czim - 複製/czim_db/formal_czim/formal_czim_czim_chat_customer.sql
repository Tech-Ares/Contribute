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
-- Table structure for table `czim_chat_customer`
--

DROP TABLE IF EXISTS `czim_chat_customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_chat_customer` (
  `id` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '客服信息主键',
  `UserId` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '后台帐号id',
  `Avatar` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '客服头像',
  `NickName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '客服昵称',
  `ManTime` int DEFAULT '0' COMMENT '服务人次',
  `IsAgent` bit(1) DEFAULT b'0' COMMENT '是否推广专员',
  `EaseChatCustomerId` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '环信Id',
  `IsActive` bit(1) DEFAULT b'1' COMMENT '是否可用',
  `CDate` datetime(3) DEFAULT NULL COMMENT '创建时间',
  `UDate` datetime(3) DEFAULT NULL COMMENT '修改时间',
  `CUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '创建人',
  `UUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`id`) USING BTREE,
  KEY `UserId` (`UserId`) USING BTREE COMMENT '用户id'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='聊天客服表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_chat_customer`
--

LOCK TABLES `czim_chat_customer` WRITE;
INSERT INTO `czim_chat_customer` VALUES ('147de7b55d16479397b21158376d83db','f974d09ccb784991a7321aa28ab1221f','http://xchat001.oss-cn-hongkong.aliyuncs.com/defaultoss/03102315670906.jpg','小月',4,_binary '\0','48f432e0-908b-11ec-8935-d1184dece33e',_binary '','2022-02-18 15:20:43.834','2022-02-18 15:23:03.212',NULL,'45346f25ee18465fa1efbeca99658c26'),('6190470f3f9d469484df7ee38d5eb738','44d5813a053b4724b3659b024a097e13','http://xchat001.oss-cn-hongkong.aliyuncs.com/defaultoss/客服头像.jpg','巨丰微讯客服',15,_binary '\0','fe8c0040-8fc5-11ec-bd90-ef12de817d94',_binary '','2022-02-17 15:48:28.189','2022-02-17 15:48:28.189',NULL,NULL),('7518a5d903094853ba2e1b3e11575d5a','1669b947ce4c456e923ca79f5a8578dc','http://xchat001.oss-cn-hongkong.aliyuncs.com/defaultoss/客服头像.jpg','客服',0,_binary '','d0bd0540-8ff9-11ec-8412-036f50353160',_binary '','2022-02-17 21:59:24.602','2022-02-17 21:59:24.602',NULL,NULL),('bb0d2c28bc2841d4b43f9353dda11b05','5b96eb571930428f9b1710f971dfd38f','http://xchat001.oss-cn-hongkong.aliyuncs.com/defaultoss/客服头像.jpg','巨丰微讯客服',0,_binary '','45f95ef0-8fc6-11ec-81c0-6fd573955afc',_binary '','2022-02-17 15:50:28.090','2022-02-17 15:50:28.090',NULL,NULL);
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
