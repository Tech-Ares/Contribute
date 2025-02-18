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
-- Table structure for table `czim_live_info`
--

DROP TABLE IF EXISTS `czim_live_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_live_info` (
  `id` text,
  `Number` int DEFAULT NULL,
  `Title` text,
  `CrtMemberId` text,
  `CrtDate` text,
  `LiveDate` text,
  `EndDate` text,
  `VideoUrl` text,
  `Content` text,
  `HeadPicture` text,
  `PushFlowUrl` text,
  `PlayUrl` text,
  `IsOvert` text,
  `Openness` int DEFAULT NULL,
  `IsExamine` int DEFAULT NULL,
  `ExamineDate` text,
  `LiveStatus` int DEFAULT NULL,
  `GiveLikeCount` int DEFAULT NULL,
  `CommentCount` int DEFAULT NULL,
  `SignUpCount` int DEFAULT NULL,
  `IsCream` text,
  `IsActive` int DEFAULT NULL,
  `CDate` text,
  `UDate` text,
  `CUser` text,
  `UUser` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_live_info`
--

LOCK TABLES `czim_live_info` WRITE;
INSERT INTO `czim_live_info` VALUES ('dfabf34d0e7b48398af233df2237233f',0,'南开商学院','947702d3727d11ec956f0242ac110002','2022-01-17 16:12:35','2021-12-01 00:00:00','2023-01-11 00:00:00','https://www.loulfes.cn','','http://faeapi.oss-cn-hongkong.aliyuncs.com/bsvoss/0024AC20-3698-4F21-BF04-FD95DE4A162B.png','','','0',0,1,'2022-01-17 16:12:35',0,0,0,0,'0',1,'2022-01-17 16:12:34.545','2022-01-17 16:12:34.545','947702d3727d11ec956f0242ac110002','');
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
