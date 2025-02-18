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
-- Table structure for table `sysorganization`
--

DROP TABLE IF EXISTS `sysorganization`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sysorganization` (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '组织id',
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '名称',
  `OrderNumber` int DEFAULT NULL COMMENT '编号',
  `Leader` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '领导',
  `Phone` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '电话',
  `Email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '邮箱',
  `State` int DEFAULT NULL COMMENT '状态',
  `ParentId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '父节点',
  `IsActive` bit(1) DEFAULT NULL COMMENT '是否可用',
  `CUser` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '创建人',
  `UUser` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '' COMMENT '修改人',
  `CDate` datetime DEFAULT NULL,
  `UDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sysorganization`
--

LOCK TABLES `sysorganization` WRITE;
INSERT INTO `sysorganization` VALUES ('05e55b19-261a-4df8-aea0-83a56f1aeb7a','市场部门',2,'市场部门','123213','123@qq.com',1,'47c547ea-e07a-4475-a4f7-ae09e3d8fafc',NULL,NULL,NULL,NULL,NULL),('1b001a4d-bce5-4e09-bc96-4e7da7686c48','北京分部',3,'北京分部','132123','13131',1,'f23777dd-2c03-449f-953b-df91c19dee5b',NULL,NULL,NULL,NULL,NULL),('47c547ea-e07a-4475-a4f7-ae09e3d8fafc','成都分部',2,'成都分部','123123123','123@qq.com',1,'f23777dd-2c03-449f-953b-df91c19dee5b',NULL,NULL,NULL,NULL,NULL),('6e36f7eb-3d03-417d-8815-c0e0f19ce6e6','市场部门',1,'市场部门','234124234','234234@qq.com',1,'1b001a4d-bce5-4e09-bc96-4e7da7686c48',NULL,NULL,NULL,NULL,NULL),('9cf32211-f255-4c2e-9c53-4258755e43c5','测试部门',3,'测试部门','12313','123123@qq.com',1,'47c547ea-e07a-4475-a4f7-ae09e3d8fafc',NULL,NULL,NULL,NULL,NULL),('a5e87436-53d5-4fff-8f2e-0af511db9ba4','研发部门',1,'研发部门','1234323423','12312@qq.com',1,'47c547ea-e07a-4475-a4f7-ae09e3d8fafc',NULL,NULL,NULL,NULL,NULL),('aea25949-b0a0-49a3-9fbc-e80224b75fc1','财务部门',2,'财务部门','435543535','123@qq.com',1,'1b001a4d-bce5-4e09-bc96-4e7da7686c48',NULL,NULL,NULL,NULL,NULL),('e898e8ad-2de7-46e0-b6f3-08f5fb9662ed','财务部门',4,'财务部门','12323452345','12312@qq.com',1,'47c547ea-e07a-4475-a4f7-ae09e3d8fafc',NULL,NULL,NULL,NULL,NULL),('eb913607-932f-433b-8321-dfe35258bb88','运维部门',5,'运维部门','1232133452','12341@qq.com',1,'47c547ea-e07a-4475-a4f7-ae09e3d8fafc',NULL,NULL,NULL,NULL,NULL),('f23777dd-2c03-449f-953b-df91c19dee5b','阿里巴巴集团',1,'hzy','18410912184','18410912184@qq.com',1,NULL,NULL,NULL,NULL,NULL,NULL);
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
