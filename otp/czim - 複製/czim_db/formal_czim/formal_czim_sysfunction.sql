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
-- Table structure for table `sysfunction`
--

DROP TABLE IF EXISTS `sysfunction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sysfunction` (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '方法主键id',
  `Number` int DEFAULT NULL COMMENT '编号',
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '别名',
  `Title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '标题',
  `ByName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '别名',
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '备注',
  `IsActive` bit(1) DEFAULT NULL COMMENT '是否可用',
  `CUser` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `UUser` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `CDate` datetime DEFAULT NULL,
  `UDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sysfunction`
--

LOCK TABLES `sysfunction` WRITE;
INSERT INTO `sysfunction` VALUES ('12f5b70d5811487e85589b6ae95b7b3a',30,'user.edit','修改','Update','Update',_binary '',NULL,NULL,NULL,NULL),('3996e7403bae4f569ebf4c3c5cde0f1a',10,'user.show','显示','Display','Display',_binary '',NULL,NULL,NULL,NULL),('3f7d6327e27e44f3995dc5633dbc6040',50,'user.save','保存','Save','Save',_binary '',NULL,NULL,NULL,NULL),('734c90d533fb4fbf8a8ad2e8ddfc6610',20,'user.add','新增','Insert','Insert',_binary '',NULL,NULL,NULL,NULL),('7bdfd8f68f8e4844bdd44a9cccf582fc',70,'user.export','导出','Export','Export',_binary '',NULL,NULL,NULL,NULL),('863b37531f814ca782710a6315fca062',40,'user.del','删除','Delete','Delete',_binary '',NULL,NULL,NULL,NULL),('8dea07a9615d47c79f174df9ff2abd9e',60,'user.search','检索','Search','Search',_binary '',NULL,NULL,NULL,NULL),('cec2a4b9b55b46cab16243291b7c7f4c',80,'user.print','打印','Print','Print',_binary '',NULL,NULL,NULL,NULL);
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
