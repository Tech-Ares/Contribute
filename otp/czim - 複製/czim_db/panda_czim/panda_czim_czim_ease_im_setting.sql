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
-- Table structure for table `czim_ease_im_setting`
--

DROP TABLE IF EXISTS `czim_ease_im_setting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_ease_im_setting` (
  `Id` text,
  `Orgname` text,
  `AppName` text,
  `AppKey` text,
  `ClientId` text,
  `ClientSecret` text,
  `AccessToken` text,
  `ExpiresTime` text,
  `ApplicationId` text,
  `IsActive` text,
  `CDate` text,
  `UDate` text,
  `CUser` text,
  `UUser` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_ease_im_setting`
--

LOCK TABLES `czim_ease_im_setting` WRITE;
INSERT INTO `czim_ease_im_setting` VALUES ('0384c702688611ec956f0242ac110002','1107220118095349','xchat','1107220118095349#xchat','YXA6clpiHVluTLS5WcphPQGeaQ','YXA6TUqaY1wr4ZJLghnYEJPXNAKkjDA','YWMteyF5jIiMEeyPS0UGxnTfALbjJboTUDW8jge27S9wFNJyWmIdWW5MtLlZymE9AZ5pAgMAAAF-105txwWP1AClBljmLfAhdxoLcizbKT0ODJLFbHfKz2nVFYu38AU1cQ','2025-01-23 11:09:08','725a621d-596e-4cb4-b959-ca613d019e69','1','0001-01-01 00:00:00.000','0001-01-01 00:00:00.000','','');
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
