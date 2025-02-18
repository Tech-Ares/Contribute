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
-- Table structure for table `czim_chat_preset`
--

DROP TABLE IF EXISTS `czim_chat_preset`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czim_chat_preset` (
  `id` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '客服问答id',
  `Context` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '消息内容',
  `Abbre` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '缩略',
  `Vague` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '模糊查询',
  `IsActive` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否可用',
  `CUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '' COMMENT '创建人',
  `CDate` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `UUser` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '' COMMENT '修改人',
  `UDate` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='预设聊天消息';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czim_chat_preset`
--

LOCK TABLES `czim_chat_preset` WRITE;
INSERT INTO `czim_chat_preset` VALUES ('006f9d2340dd40f982a70072e88c872e','您好，在的哟， 目前什么问题呀','在','在',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 17:00:17','3c4bba14649711ec956f0242ac110002','2022-01-05 17:00:17'),('043c3ed58d3c42d8bdc59158aa37e28e','气温变化较大，要注意身体哦~如您对我的服务满意，还请您对我做出评价呢~衷心的祝愿您生活愉快，一切顺利！','评价','评价',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 17:04:33','3c4bba14649711ec956f0242ac110002','2022-01-05 17:04:33'),('107ab683eedd45fba3d5122b04653726','亲，您好，在的，很高兴为您服务，请问有什么能帮您的吗?','亲','在的',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 10:19:56','3c4bba14649711ec956f0242ac110002','2022-01-05 10:19:56'),('18da7d529ed94a23a2a04d13fc91053c','因您长时间没有回应，这边先退下了，有问题咨询请随时联系我们哦，我们会第一时间回复您的消息哒～～祝您生活愉快，天天开心！','咨询','下',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 17:03:05','3c4bba14649711ec956f0242ac110002','2022-01-05 17:03:05'),('3ba41b4a5f884e70b271cd3eb64790ea','HI，在的噢，请问有啥能帮你的么?','在','Hi',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 17:00:43','3c4bba14649711ec956f0242ac110002','2022-01-05 17:00:43'),('46cd5fa144bd480fb6a896f9fb120d77','亲，发货时按照下单的顺序发货的，如果您急要的话，可以先下单付款，我这边给您备注今天发出，让仓库优先给您安排发货的呢。','发','11',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 13:14:17','3c4bba14649711ec956f0242ac110002','2022-01-05 13:14:17'),('5e437f9dd65a45509b6744e47199274f','亲，您这边是还有哪里不明白的吗? 您这边对产品还满意的话，可以拍下的哦，快递现在刚好在我们仓库收件呢，您现在下单付款的话，还能赶上最后一班快递发货的，不然就要等到明天才能发货的呢。','发','发货',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 13:15:41','3c4bba14649711ec956f0242ac110002','2022-01-05 13:15:41'),('7e7c2994731c4172b8d2fd4d58d82596','尊敬的客户，您真有眼光，这款产品是我们畅销款，您看一下我们的销量就知道了哦。','销量','销量',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 17:04:09','3c4bba14649711ec956f0242ac110002','2022-01-05 17:04:09'),('8ffd45495743411eae67262e81c5172a','我们宝贝上显示的都是实际库存，您能看到的都是有货的!','有库存','库存',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 11:04:06','3c4bba14649711ec956f0242ac110002','2022-01-05 11:04:06'),('a1c1218f50c743d69d253061c98dd49a','～请您稍等一下，让我迅速浏览一下聊天记录了解一下您的问题，帮您更好的解答问题哈～','记录','记录',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 17:03:38','3c4bba14649711ec956f0242ac110002','2022-01-05 17:03:38'),('a678bf0496f74f3e875d48762d311f31','感谢您对小客服的支持。小客服先不打扰您了，祝您生活愉快','谢','谢',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 17:02:10','3c4bba14649711ec956f0242ac110002','2022-01-05 17:02:10'),('a6e4a150086249fd8c808e9353c758cf','在的，随时为您待命，请指示，有啥可以为您效劳的呢','在','在',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 17:01:02','3c4bba14649711ec956f0242ac110002','2022-01-05 17:01:02'),('ce6081f4c9394c0281674816d9d2805f','东西再好也会有人喜欢有人不喜欢的，所谓众口难调，具体好不好用只有自己使用了才知道的，这款还支持7天无理由退换货的哦，这个对您来说也是一个保障','退换','7',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 17:05:31','3c4bba14649711ec956f0242ac110002','2022-01-05 17:05:31'),('e1918a3bc10147cba7c11b9c1948b9e2','感谢您的咨询，可以点击收藏下店铺或者喜欢的宝贝哦，期待您的光临，祝您生活愉快~','感谢','感谢',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 17:01:54','3c4bba14649711ec956f0242ac110002','2022-01-05 17:01:54'),('e2f657501ae44a15b3b27bec2598d659','33','测试','22',_binary '\0','','2022-01-05 12:29:12','3c4bba14649711ec956f0242ac110002','2022-01-05 12:29:12'),('f198fab82f18473d867d299fee8f0e34','欢迎光临本店铺，请问您有什么问题需要咨询的呢？不要忘了关注本店铺哦','欢迎','欢迎',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 17:01:36','3c4bba14649711ec956f0242ac110002','2022-01-05 17:01:36'),('febcc851d71f4195868d2edf0d6ab554','您可以关注下我们店铺或者将中意的型号加购物车，方便您随时购买哦~有任何问题客服小妹都愿意为您效劳哦~','关注','关注',_binary '','3c4bba14649711ec956f0242ac110002','2022-01-05 17:02:37','3c4bba14649711ec956f0242ac110002','2022-01-05 17:02:37');
UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
