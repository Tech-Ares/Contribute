-- MySQL dump 10.13  Distrib 5.7.11, for osx10.9 (x86_64)
--
-- Host: localhost    Database: ygg
-- ------------------------------------------------------
-- Server version	5.7.11

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `merchants`
--

DROP TABLE IF EXISTS `merchants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `merchants` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `topOrg` varchar(16) DEFAULT NULL,
  `org` varchar(16) DEFAULT NULL COMMENT 'YGG平台方设定的子账号',
  `key` varchar(128) DEFAULT NULL,
  `createTime` datetime DEFAULT NULL,
  `updateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `merchants_topOrg_org_uindex` (`topOrg`,`org`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `transferlog`
--

DROP TABLE IF EXISTS `transferlog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `transferlog` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `loginname` varchar(32) DEFAULT NULL,
  `topOrg` varchar(16) DEFAULT NULL,
  `org` varchar(16) DEFAULT NULL,
  `currency` varchar(8) DEFAULT NULL,
  `amount` double(10,2) DEFAULT NULL,
  `type` varchar(16) DEFAULT NULL,
  `transferTime` datetime DEFAULT NULL,
  `billno` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `billno` (`billno`),
  KEY `transferTime` (`transferTime`),
  KEY `type` (`type`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ygg_player`
--

DROP TABLE IF EXISTS `ygg_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ygg_player` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `loginname` varchar(32) NOT NULL,
  `topOrg` varchar(16) DEFAULT NULL,
  `org` varchar(16) DEFAULT NULL,
  `currency` varchar(16) NOT NULL,
  `balance` double(10,2) DEFAULT '0.00',
  `sessionKey` varchar(32) DEFAULT NULL,
  `countryCode` varchar(2) DEFAULT NULL,
  `updateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `sessionKey` (`sessionKey`),
  UNIQUE KEY `ygg_player_loginname_topOrg_org_currency_uindex` (`loginname`,`topOrg`,`org`,`currency`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ygglog`
--

DROP TABLE IF EXISTS `ygglog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ygglog` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `loginname` varchar(32) DEFAULT NULL,
  `topOrg` varchar(16) DEFAULT NULL,
  `org` varchar(16) DEFAULT NULL,
  `currency` varchar(16) DEFAULT NULL,
  `type` varchar(32) DEFAULT NULL,
  `amount` double(10,2) DEFAULT '0.00',
  `beforeAmount` double(10,2) DEFAULT '0.00',
  `afterAmount` double(10,2) DEFAULT '0.00',
  `gameName` varchar(128) DEFAULT NULL,
  `reference` varchar(128) DEFAULT NULL,
  `subReference` varchar(128) DEFAULT NULL,
  `createTime` datetime DEFAULT NULL,
  `params` text,
  PRIMARY KEY (`id`),
  UNIQUE KEY `unique_request` (`type`,`reference`,`subReference`),
  KEY `createTime` (`createTime`),
  KEY `loginname` (`loginname`),
  KEY `reference_type` (`type`,`reference`),
  KEY `ygglog_org_index` (`org`),
  KEY `ygglog_topOrg_index` (`topOrg`),
  KEY `ygglog_currency_index` (`currency`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-05-13 19:51:06
