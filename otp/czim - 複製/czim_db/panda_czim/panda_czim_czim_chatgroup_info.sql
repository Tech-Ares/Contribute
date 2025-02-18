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
-- Table structure for table `czim_chatgroup_info`
--

DROP TABLE IF EXISTS `czim_chatgroup_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_chatgroup_info` (
  `id` text,
  `EaseChatgroupId` text,
  `GroupName` text,
  `GroupImg` text,
  `Announcement` text,
  `Description` text,
  `MemberSonly` text,
  `AllowInvites` text,
  `Maxusers` int DEFAULT NULL,
  `OwnId` text,
  `UserNum` int DEFAULT NULL,
  `IsPublic` text,
  `IsProhibit` text,
  `IsDefault` text,
  `CrtDate` text,
  `IsActive` text,
  `CDate` text,
  `UDate` text,
  `CUser` text,
  `UUser` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_chatgroup_info`
--

LOCK TABLES `czim_chatgroup_info` WRITE;
INSERT INTO `czim_chatgroup_info` VALUES ('69f98a3d4afa4734ab6483cf2f8571a6','173197757579265','DD群聊测试','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/远方.jpg','烦烦烦方法 ','132552222','0','1',1000,'b48fea66e89e431eb7cdbff0eef01e54',0,'0','0','0','2022-02-11 17:44:13','1','2022-02-11 17:44:13.410','2022-02-11 17:44:13.663','947702d3727d11ec956f0242ac110002',''),('73dcd92cf64b4568872cc25914088f41','173653305131009','五黑','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/QQ图片20220120120417.gif','','可见课件','0','0',100,'3c2bbe80935647eeb3b2985a9ee8f3dc',0,'1','0','0','2022-02-16 18:24:58','1','2022-02-16 18:24:57.582','2022-02-16 18:24:57.870','947702d3727d11ec956f0242ac110002',''),('7b3217af9af74c25b9f1e30889b7bdcf','173200060252161','丁丁测试','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/信念.jpg','丁丁测试','丁丁测试','0','1',500,'5bbb7660eace4c48bddb8ddcca86c7f2',0,'1','0','0','2022-02-11 18:20:49','1','2022-02-11 18:20:49.239','2022-02-11 18:20:49.545','947702d3727d11ec956f0242ac110002',''),('860941b7c9a149908fd6c3a989e488bf','171100321415169','汤姆和龙虾','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/460001992221.jpg','环信能不能给力点','这是一个客户测试群','0','0',200,'a0e7ae3ac6e94dd2b36514f12da6100b',0,'1','0','1','2022-01-19 14:06:22','1','2022-01-19 14:06:22.032','2022-01-19 14:06:22.412','947702d3727d11ec956f0242ac110002',''),('b978629d8cc44089b6abb91ab4386fc1','171467576770561','测试3','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/111.jpg','1','1','0','1',200,'d5078a8ab3784593b8e1f3e3793c772d',0,'1','0','0','2022-01-23 15:23:44','1','2022-01-23 15:23:44.118','2022-01-23 15:23:44.491','d5078a8ab3784593b8e1f3e3793c772d',''),('c58ad682d4b348f1aae51863e659dac7','173485021265921','PC端客服管理测试群','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/乐观.jpg','.............................................................................大家随便聊','对于客户测试PC或是APP端测试群聊功能，消息推送，离线推送。等。。。。功能','0','1',1000,'0001614dd45e473a8808662b1499ebe1',0,'1','0','0','2022-02-14 21:50:08','1','2022-02-14 21:50:08.273','2022-02-14 22:00:38.294','0001614dd45e473a8808662b1499ebe1','0001614dd45e473a8808662b1499ebe1'),('c646ed65365045a0a4f189562953ac24','171032101060609','测试','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/cef44a9d-67dc-4c89-8daa-f58ce81d9e99.jpg','测试','测试','1','1',100,'2c103ba6742644dea3bb02624af6c559',0,'0','0','0','2022-01-18 20:02:01','1','2022-01-18 20:02:01.248','2022-01-18 20:02:02.504','947702d3727d11ec956f0242ac110002',''),('cbcba59f2da145d883bc46483f4954fa','171033595281409','聊了聊','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/头像 (4).jpeg','234','来来来','0','0',333,'b783168ebeba4de0a2c6eccc07cebac2',0,'1','0','0','2022-01-18 20:25:47','1','2022-01-18 20:25:47.180','2022-01-18 20:25:47.533','947702d3727d11ec956f0242ac110002',''),('d53ba4667cab431eafa82340f1f26557','171184100540417','测试1','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/QQ图片20220120120417.gif','财务','测','0','0',200,'aa0f90e0356e40f1a305e171e06021fa',0,'0','0','0','2022-01-20 12:18:00','1','2022-01-20 12:17:59.939','2022-01-20 12:18:00.288','947702d3727d11ec956f0242ac110002',''),('e7eb0c2df7a04b0aadb3c99c65b420d5','171036669706241','group act not open','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/60d9bba39e857f5b0a72863cb357fe0.png','','group act not open','0','0',500,'a0e7ae3ac6e94dd2b36514f12da6100b',0,'1','0','0','2022-01-18 21:14:39','1','2022-01-18 21:14:38.902','2022-01-18 21:14:39.230','947702d3727d11ec956f0242ac110002',''),('fd106c5c9b4d49649e03276d469e94bb','171459425140737','大白测试','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/111.jpg','','1','0','1',200,'d5078a8ab3784593b8e1f3e3793c772d',0,'1','0','0','2022-01-23 13:14:10','1','2022-01-23 13:14:10.172','2022-01-23 13:14:10.600','45346f25ee18465fa1efbeca99658c26','');
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
