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
-- Table structure for table `czim_chat_customer`
--

DROP TABLE IF EXISTS `czim_chat_customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_chat_customer` (
  `id` text,
  `UserId` text,
  `Avatar` text,
  `NickName` text,
  `ManTime` int DEFAULT NULL,
  `IsAgent` text,
  `EaseChatCustomerId` text,
  `IsActive` text,
  `CDate` text,
  `UDate` text,
  `CUser` text,
  `UUser` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_chat_customer`
--

LOCK TABLES `czim_chat_customer` WRITE;
INSERT INTO `czim_chat_customer` VALUES ('22cd17eb9aaf4573a53b731025a0dec5','fb084db9e2d444458d2f7d18bc0bf1b1','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/d0c8a786c9177f3e15a5182e85d0bfc19f3d5622.jpeg','另一个客服',22,'0','5ffa63e0-7873-11ec-9636-45fe38cb51db','1','2022-01-18 23:29:06.689','2022-01-18 23:29:06.689','',''),('26d14dc878b5445f8c32773152addfce','09c60955fc814d6995aae2709d20f1e5','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/信念.jpg','测试专属客服APP',0,'1','60ab5050-8d9b-11ec-8afa-3db6462aea5b','1','2022-02-14 21:38:22.056','2022-02-14 21:38:22.056','',''),('2c07a765494b405abf4349554a52b62e','5f2aa8a244e649d89e9d16ad9ea0572a','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/远方.jpg','测试专用02',0,'1','a20eb5b0-88f9-11ec-b111-87d508502450','1','2022-02-09 00:10:27.734','2022-02-09 00:10:27.734','',''),('2de28bccc3a84d45a394ca1f85689691','5bbb7660eace4c48bddb8ddcca86c7f2','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/屏幕截图 2022-01-12 145800.jpg','环信测试昵称',0,'1','de2cb1b0-8b23-11ec-ba4f-9f8c4662b7ea','1','2022-02-11 18:17:49.819','2022-02-11 18:17:49.819','',''),('312b9e4a98864c30be16abb0bae12528','d5078a8ab3784593b8e1f3e3793c772d','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/信念.jpg','测试专用',0,'1','22006ed0-7ad1-11ec-a75d-27968e7f1322','1','2022-01-21 23:45:17.586','2022-01-21 23:45:17.586','',''),('58c422d82427498ebdbf0ffb2d530f1c','3c2bbe80935647eeb3b2985a9ee8f3dc','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/QQ图片20220120120827.jpg','大帅1',0,'1','ed4fd1d0-8f11-11ec-b20d-5183928d01fe','1','2022-02-16 18:19:29.577','2022-02-16 18:19:29.577','',''),('5cb600d671984cab9b104c36542ddc77','9bdf9d60aa9e4eba903b96651c83f49f','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/yang.JPEG','官方客服--1',21,'0','a0ee5fe0-7856-11ec-b699-c95c88535c3f','1','2022-01-18 20:03:19.600','2022-01-18 20:03:19.600','',''),('5e7013272a21429cb8b0a46ba6c749e2','8159df0beebf4c9aaab5f886c85002cf','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/乐观.jpg','恭喜发财',0,'1','2ad3c2c0-8b23-11ec-9a30-358069ba9f3f','1','2022-02-11 18:12:49.733','2022-02-11 18:12:49.733','',''),('6e2261fbbff44d23b626c18ef5da0015','0001614dd45e473a8808662b1499ebe1','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/好运来.jpg','测试专属客服苹果',3,'0','33940a30-8d9b-11ec-adb7-8f81ae2ef5ef','1','2022-02-14 21:37:06.361','2022-02-14 21:37:06.361','',''),('ab50104b59e24fce9dc519e5df0a9dbc','a0e7ae3ac6e94dd2b36514f12da6100b','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/comment-author2.jpg','官网客服',21,'0','bb665850-7856-11ec-9972-2b3eb62c6d47','1','2022-01-18 20:04:04.729','2022-01-18 20:04:04.729','',''),('afa9b1f550724cb68d95400ef8563548','b48fea66e89e431eb7cdbff0eef01e54','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/HOLmes.jpg','DD测试',0,'1','bd5d97b0-8b1e-11ec-8491-1bc726985d83','1','2022-02-11 17:41:08.174','2022-02-11 17:41:08.174','',''),('b1c4a399935b4b319abf8e95b61e551f','828389f485a9457b8657d6762113d9c2','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/头像 (1).jpeg','推广小美',0,'1','c093e040-7856-11ec-8a11-5dcdc7b79f4c','1','2022-01-18 20:04:13.418','2022-01-18 20:04:13.418','',''),('bae7dd83d1164aec93694e89c302685a','2c103ba6742644dea3bb02624af6c559','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/cef44a9d-67dc-4c89-8daa-f58ce81d9e99.jpg','wang',0,'1','4fe82fe0-7856-11ec-9be2-a52049cb0349','1','2022-01-18 20:01:04.151','2022-01-18 20:01:04.151','',''),('bd7e885dd8594bc6913e26a2ed0c1792','b783168ebeba4de0a2c6eccc07cebac2','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/头像 (4).jpeg','推广员11',0,'1','6bdaf690-7858-11ec-86b8-0390f586f624','1','2022-01-18 20:16:09.190','2022-01-18 20:16:09.190','',''),('d35373477caf4094a86d8f8fc66e232c','0e1af8a574644290aed56c1805216732','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/好运来.jpg','螃蛤888',0,'1','d12137b0-8719-11ec-be95-93799937f8e5','1','2022-02-06 14:55:47.378','2022-02-06 14:55:47.378','',''),('d44aa5c4694b4b19b3091fc9bb200120','aa0f90e0356e40f1a305e171e06021fa','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/QQ图片20220120120827.jpg','测试2',0,'1','dca20360-79a6-11ec-b811-a78bedb4e5a3','1','2022-01-20 12:10:10.234','2022-01-20 12:10:10.234','',''),('e75f51ccee5747faa1133833dd631075','e9c0c5fe9be44dd889d9f433566d6fe9','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/5.jpg','非官方客服8',21,'0','d44eb010-7856-11ec-9521-7b8f3de5932e','1','2022-01-18 20:04:46.518','2022-01-18 20:04:46.518','',''),('f3e12c667e7444bda3dde17aa2fd5f57','e26d90852c3b43eab3a88a1f65a49c3d','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/好运来.jpg','午夜测试',0,'1','e1b8c600-8b20-11ec-b876-513c1e49e509','1','2022-02-11 17:56:28.170','2022-02-11 17:56:28.170','','');
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
