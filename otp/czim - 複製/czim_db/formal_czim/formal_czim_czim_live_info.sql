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
-- Table structure for table `czim_live_info`
--

DROP TABLE IF EXISTS `czim_live_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_live_info` (
  `id` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创作id',
  `Number` bigint DEFAULT NULL COMMENT '视频编号（使用种子表id）',
  `Title` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '标题',
  `CrtMemberId` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创作人id',
  `CrtDate` datetime DEFAULT NULL COMMENT '创建时间',
  `LiveDate` datetime DEFAULT NULL COMMENT '直播时间（开始时间）',
  `EndDate` datetime DEFAULT NULL COMMENT '直播结束时间',
  `VideoUrl` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '录播地址',
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci COMMENT '内容',
  `HeadPicture` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT '' COMMENT '视频贴图',
  `PushFlowUrl` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '推流地址',
  `PlayUrl` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '播放地址',
  `IsOvert` bit(1) DEFAULT b'1' COMMENT '是否公开',
  `Openness` smallint DEFAULT NULL COMMENT '可见度1：私人，2：团队，3：部分',
  `IsExamine` smallint NOT NULL DEFAULT '0' COMMENT '审核状态 0：待 -1：未通过 1：通过',
  `ExamineDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '审核时间',
  `LiveStatus` smallint DEFAULT '0' COMMENT '直播状态0：未开始1：直播中2：已结束',
  `GiveLikeCount` int DEFAULT '0' COMMENT '点赞次数',
  `CommentCount` int DEFAULT '0' COMMENT '评论次数',
  `SignUpCount` int DEFAULT '0' COMMENT '报名人数',
  `IsCream` bit(1) DEFAULT b'0' COMMENT '是否精华',
  `IsActive` tinyint(1) DEFAULT '1' COMMENT '是否可用',
  `CDate` datetime(3) DEFAULT NULL COMMENT '创建时间',
  `UDate` datetime(3) DEFAULT NULL COMMENT '修改时间',
  `CUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '创建人',
  `UUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='视频直播地址';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_live_info`
--

LOCK TABLES `czim_live_info` WRITE;
INSERT INTO `czim_live_info` VALUES ('dfabf34d0e7b48398af233df2237233f',0,'一帘幽梦','947702d3727d11ec956f0242ac110002','2022-01-17 16:12:35','2021-12-01 00:00:00','2023-01-11 00:00:00','https://www.loulfes.cn',NULL,'http://xchat001.oss-cn-hongkong.aliyuncs.com/xchatoss/ylym001.png',NULL,NULL,_binary '\0',0,1,'2022-01-17 16:12:35',0,0,0,0,_binary '\0',1,'2022-01-17 16:12:34.545','2022-02-17 21:20:18.004','947702d3727d11ec956f0242ac110002','45346f25ee18465fa1efbeca99658c26');
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
