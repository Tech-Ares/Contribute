use ph_gateway;

// Capital
db.Capital.createIndex({"ObjectId":1}, {"unique":true,"name":"ObjectId_1","background":true});
db.Capital.createIndex({"_id":1}, {"name":"_id_"});

// CapitalHistory
db.CapitalHistory.createIndex({"Key":1}, {"name":"Key_1","background":true});
db.CapitalHistory.createIndex({"_id":1}, {"name":"_id_"});

// CppAutoIncr
db.CppAutoIncr.createIndex({"_id":1}, {"name":"_id_"});

// TGTEsunnyPlus
db.TGTEsunnyPlus.createIndex({"_id":1}, {"name":"_id_"});
db.TGTEsunnyPlus.createIndex({"client_order_id":1,"esunny_orderid":1}, {"name":"client_order_id_1_esunny_orderid_1","background":true});

// qb_QuoteHistory_1440_2021
db.qb_QuoteHistory_1440_2021.createIndex({"GatewayId":1,"Market":1,"Commodity":1,"Contract":1}, {"name":"GatewayId_1_Market_1_Commodity_1_Contract_1","background":true});
db.qb_QuoteHistory_1440_2021.createIndex({"GatewayId":1,"SymbolId":1,"StartStamp":1}, {"unique":true,"name":"GatewayId_1_SymbolId_1_StartStamp_1","background":true});
db.qb_QuoteHistory_1440_2021.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteHistory_15_2021
db.qb_QuoteHistory_15_2021.createIndex({"GatewayId":1,"Market":1,"Commodity":1,"Contract":1}, {"name":"GatewayId_1_Market_1_Commodity_1_Contract_1","background":true});
db.qb_QuoteHistory_15_2021.createIndex({"GatewayId":1,"SymbolId":1,"StartStamp":1}, {"unique":true,"name":"GatewayId_1_SymbolId_1_StartStamp_1","background":true});
db.qb_QuoteHistory_15_2021.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteHistory_1_2021
db.qb_QuoteHistory_1_2021.createIndex({"GatewayId":1,"Market":1,"Commodity":1,"Contract":1}, {"name":"GatewayId_1_Market_1_Commodity_1_Contract_1","background":true});
db.qb_QuoteHistory_1_2021.createIndex({"GatewayId":1,"SymbolId":1,"StartStamp":1}, {"unique":true,"name":"GatewayId_1_SymbolId_1_StartStamp_1","background":true});
db.qb_QuoteHistory_1_2021.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteHistory_30_2021
db.qb_QuoteHistory_30_2021.createIndex({"GatewayId":1,"Market":1,"Commodity":1,"Contract":1}, {"name":"GatewayId_1_Market_1_Commodity_1_Contract_1","background":true});
db.qb_QuoteHistory_30_2021.createIndex({"GatewayId":1,"SymbolId":1,"StartStamp":1}, {"unique":true,"name":"GatewayId_1_SymbolId_1_StartStamp_1","background":true});
db.qb_QuoteHistory_30_2021.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteHistory_5_2021
db.qb_QuoteHistory_5_2021.createIndex({"GatewayId":1,"Market":1,"Commodity":1,"Contract":1}, {"name":"GatewayId_1_Market_1_Commodity_1_Contract_1","background":true});
db.qb_QuoteHistory_5_2021.createIndex({"GatewayId":1,"SymbolId":1,"StartStamp":1}, {"unique":true,"name":"GatewayId_1_SymbolId_1_StartStamp_1","background":true});
db.qb_QuoteHistory_5_2021.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteHistory_60_2021
db.qb_QuoteHistory_60_2021.createIndex({"GatewayId":1,"Market":1,"Commodity":1,"Contract":1}, {"name":"GatewayId_1_Market_1_Commodity_1_Contract_1","background":true});
db.qb_QuoteHistory_60_2021.createIndex({"GatewayId":1,"SymbolId":1,"StartStamp":1}, {"unique":true,"name":"GatewayId_1_SymbolId_1_StartStamp_1","background":true});
db.qb_QuoteHistory_60_2021.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuotePeriodPart
db.qb_QuotePeriodPart.createIndex({"GatewayId":1,"Market":1,"Commodity":1,"Contract":1}, {"name":"GatewayId_1_Market_1_Commodity_1_Contract_1","background":true});
db.qb_QuotePeriodPart.createIndex({"GatewayId":1,"SymbolId":1,"PeriodType":1,"StartStamp":1}, {"unique":true,"name":"GatewayId_1_SymbolId_1_PeriodType_1_StartStamp_1","background":true});
db.qb_QuotePeriodPart.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteReal
db.qb_QuoteReal.createIndex({"GatewayId":1,"Market":1,"Commodity":1,"Contract":1}, {"name":"GatewayId_1_Market_1_Commodity_1_Contract_1","background":true});
db.qb_QuoteReal.createIndex({"GatewayId":1,"SymbolId":1}, {"unique":true,"name":"GatewayId_1_SymbolId_1","background":true});
db.qb_QuoteReal.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteTradeDetail
db.qb_QuoteTradeDetail.createIndex({"GatewayId":1,"SymbolId":1,"Index":1}, {"unique":true,"name":"GatewayId_1_SymbolId_1_Index_1","background":true});
db.qb_QuoteTradeDetail.createIndex({"GatewayId":1,"SymbolId":1,"Serial":1}, {"unique":true,"name":"GatewayId_1_SymbolId_1_Serial_1","background":true});
db.qb_QuoteTradeDetail.createIndex({"_id":1}, {"name":"_id_"});

// qg_GatewayPara
db.qg_GatewayPara.createIndex({"_id":1}, {"name":"_id_"});

// qg_Subscribe
db.qg_Subscribe.createIndex({"GatewayId":1,"UpAccountId":1,"SymbolId":1}, {"name":"GatewayId_1_UpAccountId_1_SymbolId_1","background":true});
db.qg_Subscribe.createIndex({"_id":1}, {"name":"_id_"});

// qg_SymbolCommodityMap
db.qg_SymbolCommodityMap.createIndex({"GatewayId":1,"UpAccountId":1,"Market":1,"Commodity":1}, {"name":"GatewayId_1_UpAccountId_1_Market_1_Commodity_1","background":true});
db.qg_SymbolCommodityMap.createIndex({"_id":1}, {"name":"_id_"});

// qg_SymbolContractMapRule
db.qg_SymbolContractMapRule.createIndex({"GatewayId":1,"UpAccountId":1,"Market":1}, {"name":"GatewayId_1_UpAccountId_1_Market_1","background":true});
db.qg_SymbolContractMapRule.createIndex({"_id":1}, {"name":"_id_"});

// qg_SymbolMarketMap
db.qg_SymbolMarketMap.createIndex({"GatewayId":1,"UpAccountId":1,"Market":1}, {"name":"GatewayId_1_UpAccountId_1_Market_1","background":true});
db.qg_SymbolMarketMap.createIndex({"_id":1}, {"name":"_id_"});

// qg_UpAccount
db.qg_UpAccount.createIndex({"_id":1}, {"name":"_id_"});

// sunx_gateway
db.sunx_gateway.createIndex({"_id":1}, {"name":"_id_"});

// tb_Alarm
db.tb_Alarm.createIndex({"_id":1}, {"name":"_id_"});

// tb_Commodity
db.tb_Commodity.createIndex({"GatewayId":1,"Market":1,"Commodity":1}, {"name":"GatewayId_1_Market_1_Commodity_1","background":true});
db.tb_Commodity.createIndex({"_id":1}, {"name":"_id_"});

// tb_CommodityTradeTime
db.tb_CommodityTradeTime.createIndex({"GatewayId":1,"CommodityId":1}, {"name":"GatewayId_1_CommodityId_1","background":true});
db.tb_CommodityTradeTime.createIndex({"_id":1}, {"name":"_id_"});

// tb_ContractCodeRule
db.tb_ContractCodeRule.createIndex({"GatewayId":1,"Market":1}, {"name":"GatewayId_1_Market_1","background":true});
db.tb_ContractCodeRule.createIndex({"_id":1}, {"name":"_id_"});

// tb_Gateway
db.tb_Gateway.createIndex({"_id":1}, {"name":"_id_"});

// tb_Market
db.tb_Market.createIndex({"GatewayId":1,"Market":1}, {"name":"GatewayId_1_Market_1","background":true});
db.tb_Market.createIndex({"_id":1}, {"name":"_id_"});

// tb_Symbol
db.tb_Symbol.createIndex({"GatewayId":1,"Market":1,"Commodity":1,"Contract":1}, {"name":"GatewayId_1_Market_1_Commodity_1_Contract_1","background":true});
db.tb_Symbol.createIndex({"GatewayId":1,"Market":1,"Year":1,"Month":1}, {"name":"GatewayId_1_Market_1_Year_1_Month_1","background":true});
db.tb_Symbol.createIndex({"_id":1}, {"name":"_id_"});

// tg_App
db.tg_App.createIndex({"_id":1}, {"name":"_id_"});

// tg_ClientUserOrgUpAccount
db.tg_ClientUserOrgUpAccount.createIndex({"_id":1}, {"name":"_id_"});

// tg_GatewayPara
db.tg_GatewayPara.createIndex({"_id":1}, {"name":"_id_"});

// tg_Order
db.tg_Order.createIndex({"ClientOrderId":1}, {"name":"ClientOrderId_1","background":true});
db.tg_Order.createIndex({"OrderId":1}, {"unique":true,"name":"OrderId_1","background":true});
db.tg_Order.createIndex({"OrderTime":1}, {"name":"OrderTime_1","background":true});
db.tg_Order.createIndex({"_id":1}, {"name":"_id_"});

// tg_OtcQuotePara
db.tg_OtcQuotePara.createIndex({"GatewayId":1,"SymbolId":1}, {"name":"GatewayId_1_SymbolId_1","background":true});
db.tg_OtcQuotePara.createIndex({"_id":1}, {"name":"_id_"});

// tg_Position
db.tg_Position.createIndex({"PositionId":1}, {"unique":true,"name":"PositionId_1","background":true});
db.tg_Position.createIndex({"_id":1}, {"name":"_id_"});

// tg_SymbolCommodityMap
db.tg_SymbolCommodityMap.createIndex({"GatewayId":1,"UpAccountId":1,"Market":1,"Commodity":1}, {"name":"GatewayId_1_UpAccountId_1_Market_1_Commodity_1","background":true});
db.tg_SymbolCommodityMap.createIndex({"_id":1}, {"name":"_id_"});

// tg_SymbolContractMapRule
db.tg_SymbolContractMapRule.createIndex({"GatewayId":1,"UpAccountId":1,"Market":1}, {"name":"GatewayId_1_UpAccountId_1_Market_1","background":true});
db.tg_SymbolContractMapRule.createIndex({"_id":1}, {"name":"_id_"});

// tg_SymbolMarketMap
db.tg_SymbolMarketMap.createIndex({"GatewayId":1,"UpAccountId":1,"Market":1}, {"name":"GatewayId_1_UpAccountId_1_Market_1","background":true});
db.tg_SymbolMarketMap.createIndex({"_id":1}, {"name":"_id_"});

// tg_Tag50All
db.tg_Tag50All.createIndex({"_id":1}, {"name":"_id_"});

// tg_Trader
db.tg_Trader.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpAccount
db.tg_UpAccount.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpFillSyncTime
db.tg_UpFillSyncTime.createIndex({"UpAccountId":1}, {"unique":true,"name":"UpAccountId_1","background":true});
db.tg_UpFillSyncTime.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpOrder
db.tg_UpOrder.createIndex({"ClientOrderId":1}, {"name":"ClientOrderId_1","background":true});
db.tg_UpOrder.createIndex({"OrderId":1}, {"name":"OrderId_1","background":true});
db.tg_UpOrder.createIndex({"OrderTime":1}, {"name":"OrderTime_1","background":true});
db.tg_UpOrder.createIndex({"UpClientOrderId":1}, {"unique":true,"name":"UpClientOrderId_1","background":true});
db.tg_UpOrder.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpOrderSyncTime
db.tg_UpOrderSyncTime.createIndex({"UpAccountId":1}, {"unique":true,"name":"UpAccountId_1","background":true});
db.tg_UpOrderSyncTime.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpPosition
db.tg_UpPosition.createIndex({"PositionId":1}, {"unique":true,"name":"PositionId_1","background":true});
db.tg_UpPosition.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpPositionAdjust
db.tg_UpPositionAdjust.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpTag50PositionAdjust
db.tg_UpTag50PositionAdjust.createIndex({"_id":1}, {"name":"_id_"});



use ph_java_db_riskServer;

// ph_java_db_riskServer
db.ph_java_db_riskServer.createIndex({"_id":1}, {"name":"_id_"});

// prod01_java_db_riskServer
db.prod01_java_db_riskServer.createIndex({"_id":1}, {"name":"_id_"});

// prod_java_db_riskServer
db.prod_java_db_riskServer.createIndex({"_id":1}, {"name":"_id_"});

// riskAccount
db.riskAccount.createIndex({"_id":1}, {"name":"_id_"});
db.riskAccount.createIndex({"currency":1}, {"name":"currency","background":true});
db.riskAccount.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.riskAccount.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.riskAccount.createIndex({"userId":1}, {"name":"userId","background":true});

// riskAccountGroup
db.riskAccountGroup.createIndex({"_id":1}, {"name":"_id_"});
db.riskAccountGroup.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.riskAccountGroup.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.riskAccountGroup.createIndex({"userId":1}, {"name":"userId","background":true});

// riskPosition
db.riskPosition.createIndex({"_id":1}, {"name":"_id_"});
db.riskPosition.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.riskPosition.createIndex({"commodityCurrency":1}, {"name":"commodityCurrency","background":true});
db.riskPosition.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.riskPosition.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.riskPosition.createIndex({"contractId":1}, {"name":"contractId","background":true});
db.riskPosition.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.riskPosition.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.riskPosition.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.riskPosition.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.riskPosition.createIndex({"orgName":1}, {"name":"orgName","background":true});
db.riskPosition.createIndex({"productId":1}, {"name":"productId","background":true});
db.riskPosition.createIndex({"productOrgCode":1}, {"name":"productOrgCode","background":true});
db.riskPosition.createIndex({"productOrgId":1}, {"name":"productOrgId","background":true});
db.riskPosition.createIndex({"userId":1}, {"name":"userId","background":true});

// riskPositionMain
db.riskPositionMain.createIndex({"_id":1}, {"name":"_id_"});
db.riskPositionMain.createIndex({"sub.commodityCode":1}, {"name":"sub.commodityCode","background":true});
db.riskPositionMain.createIndex({"sub.commodityCurrency":1}, {"name":"sub.commodityCurrency","background":true});
db.riskPositionMain.createIndex({"sub.commodityId":1}, {"name":"sub.commodityId","background":true});
db.riskPositionMain.createIndex({"sub.contractCode":1}, {"name":"sub.contractCode","background":true});
db.riskPositionMain.createIndex({"sub.contractId":1}, {"name":"sub.contractId","background":true});
db.riskPositionMain.createIndex({"sub.marketCode":1}, {"name":"sub.marketCode","background":true});
db.riskPositionMain.createIndex({"sub.marketId":1}, {"name":"sub.marketId","background":true});
db.riskPositionMain.createIndex({"sub.orgCode":1}, {"name":"sub.orgCode","background":true});
db.riskPositionMain.createIndex({"sub.orgId":1}, {"name":"sub.orgId","background":true});
db.riskPositionMain.createIndex({"sub.orgName":1}, {"name":"sub.orgName","background":true});
db.riskPositionMain.createIndex({"sub.productId":1}, {"name":"sub.productId","background":true});
db.riskPositionMain.createIndex({"sub.productOrgCode":1}, {"name":"sub.productOrgCode","background":true});
db.riskPositionMain.createIndex({"sub.productOrgId":1}, {"name":"sub.productOrgId","background":true});
db.riskPositionMain.createIndex({"sub.userId":1}, {"name":"sub.userId","background":true});

// riskPositionTotal
db.riskPositionTotal.createIndex({"_id":1}, {"name":"_id_"});
db.riskPositionTotal.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.riskPositionTotal.createIndex({"commodityCurrency":1}, {"name":"commodityCurrency","background":true});
db.riskPositionTotal.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.riskPositionTotal.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.riskPositionTotal.createIndex({"contractId":1}, {"name":"contractId","background":true});
db.riskPositionTotal.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.riskPositionTotal.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.riskPositionTotal.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.riskPositionTotal.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.riskPositionTotal.createIndex({"orgName":1}, {"name":"orgName","background":true});
db.riskPositionTotal.createIndex({"productId":1}, {"name":"productId","background":true});
db.riskPositionTotal.createIndex({"productOrgCode":1}, {"name":"productOrgCode","background":true});
db.riskPositionTotal.createIndex({"productOrgId":1}, {"name":"productOrgId","background":true});
db.riskPositionTotal.createIndex({"userId":1}, {"name":"userId","background":true});

// riskPositionTotalMain
db.riskPositionTotalMain.createIndex({"_id":1}, {"name":"_id_"});
db.riskPositionTotalMain.createIndex({"sub.commodityCode":1}, {"name":"sub.commodityCode","background":true});
db.riskPositionTotalMain.createIndex({"sub.commodityCurrency":1}, {"name":"sub.commodityCurrency","background":true});
db.riskPositionTotalMain.createIndex({"sub.commodityId":1}, {"name":"sub.commodityId","background":true});
db.riskPositionTotalMain.createIndex({"sub.contractCode":1}, {"name":"sub.contractCode","background":true});
db.riskPositionTotalMain.createIndex({"sub.contractId":1}, {"name":"sub.contractId","background":true});
db.riskPositionTotalMain.createIndex({"sub.marketCode":1}, {"name":"sub.marketCode","background":true});
db.riskPositionTotalMain.createIndex({"sub.marketId":1}, {"name":"sub.marketId","background":true});
db.riskPositionTotalMain.createIndex({"sub.orgCode":1}, {"name":"sub.orgCode","background":true});
db.riskPositionTotalMain.createIndex({"sub.orgId":1}, {"name":"sub.orgId","background":true});
db.riskPositionTotalMain.createIndex({"sub.orgName":1}, {"name":"sub.orgName","background":true});
db.riskPositionTotalMain.createIndex({"sub.productId":1}, {"name":"sub.productId","background":true});
db.riskPositionTotalMain.createIndex({"sub.productOrgCode":1}, {"name":"sub.productOrgCode","background":true});
db.riskPositionTotalMain.createIndex({"sub.productOrgId":1}, {"name":"sub.productOrgId","background":true});
db.riskPositionTotalMain.createIndex({"sub.userId":1}, {"name":"sub.userId","background":true});



use ph_gateway_sl;

// Capital
db.Capital.createIndex({"ObjectId":1}, {"unique":true,"name":"ObjectId_1","background":true});
db.Capital.createIndex({"_id":1}, {"name":"_id_"});

// CapitalHistory
db.CapitalHistory.createIndex({"_id":1}, {"name":"_id_"});

// CppAutoIncr
db.CppAutoIncr.createIndex({"_id":1}, {"name":"_id_"});

// TGTEsunnyPlus
db.TGTEsunnyPlus.createIndex({"_id":1}, {"name":"_id_"});
db.TGTEsunnyPlus.createIndex({"client_order_id":1,"esunny_orderid":1}, {"name":"client_order_id_1_esunny_orderid_1","background":true});

// qb_QuoteHistory_1440_2021
db.qb_QuoteHistory_1440_2021.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteHistory_15_2021
db.qb_QuoteHistory_15_2021.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteHistory_1_2021
db.qb_QuoteHistory_1_2021.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteHistory_30_2021
db.qb_QuoteHistory_30_2021.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteHistory_5_2021
db.qb_QuoteHistory_5_2021.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteHistory_60_2021
db.qb_QuoteHistory_60_2021.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuotePeriodPart
db.qb_QuotePeriodPart.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteReal
db.qb_QuoteReal.createIndex({"_id":1}, {"name":"_id_"});

// qb_QuoteTradeDetail
db.qb_QuoteTradeDetail.createIndex({"_id":1}, {"name":"_id_"});

// qg_GatewayPara
db.qg_GatewayPara.createIndex({"_id":1}, {"name":"_id_"});

// qg_Subscribe
db.qg_Subscribe.createIndex({"GatewayId":1,"UpAccountId":1,"SymbolId":1}, {"name":"GatewayId_1_UpAccountId_1_SymbolId_1","background":true});
db.qg_Subscribe.createIndex({"_id":1}, {"name":"_id_"});

// qg_SymbolCommodityMap
db.qg_SymbolCommodityMap.createIndex({"GatewayId":1,"UpAccountId":1,"Market":1,"Commodity":1}, {"name":"GatewayId_1_UpAccountId_1_Market_1_Commodity_1","background":true});
db.qg_SymbolCommodityMap.createIndex({"_id":1}, {"name":"_id_"});

// qg_SymbolContractMapRule
db.qg_SymbolContractMapRule.createIndex({"GatewayId":1,"UpAccountId":1,"Market":1}, {"name":"GatewayId_1_UpAccountId_1_Market_1","background":true});
db.qg_SymbolContractMapRule.createIndex({"_id":1}, {"name":"_id_"});

// qg_SymbolMarketMap
db.qg_SymbolMarketMap.createIndex({"GatewayId":1,"UpAccountId":1,"Market":1}, {"name":"GatewayId_1_UpAccountId_1_Market_1","background":true});
db.qg_SymbolMarketMap.createIndex({"_id":1}, {"name":"_id_"});

// qg_UpAccount
db.qg_UpAccount.createIndex({"_id":1}, {"name":"_id_"});

// sunx_gateway_sl
db.sunx_gateway_sl.createIndex({"_id":1}, {"name":"_id_"});

// tb_Alarm
db.tb_Alarm.createIndex({"_id":1}, {"name":"_id_"});

// tb_Commodity
db.tb_Commodity.createIndex({"GatewayId":1,"Market":1,"Commodity":1}, {"name":"GatewayId_1_Market_1_Commodity_1","background":true});
db.tb_Commodity.createIndex({"_id":1}, {"name":"_id_"});

// tb_CommodityTradeTime
db.tb_CommodityTradeTime.createIndex({"GatewayId":1,"CommodityId":1}, {"name":"GatewayId_1_CommodityId_1","background":true});
db.tb_CommodityTradeTime.createIndex({"_id":1}, {"name":"_id_"});

// tb_ContractCodeRule
db.tb_ContractCodeRule.createIndex({"GatewayId":1,"Market":1}, {"name":"GatewayId_1_Market_1","background":true});
db.tb_ContractCodeRule.createIndex({"_id":1}, {"name":"_id_"});

// tb_Gateway
db.tb_Gateway.createIndex({"_id":1}, {"name":"_id_"});

// tb_Market
db.tb_Market.createIndex({"GatewayId":1,"Market":1}, {"name":"GatewayId_1_Market_1","background":true});
db.tb_Market.createIndex({"_id":1}, {"name":"_id_"});

// tb_Symbol
db.tb_Symbol.createIndex({"GatewayId":1,"Market":1,"Commodity":1,"Contract":1}, {"name":"GatewayId_1_Market_1_Commodity_1_Contract_1","background":true});
db.tb_Symbol.createIndex({"GatewayId":1,"Market":1,"Year":1,"Month":1}, {"name":"GatewayId_1_Market_1_Year_1_Month_1","background":true});
db.tb_Symbol.createIndex({"_id":1}, {"name":"_id_"});

// tg_App
db.tg_App.createIndex({"_id":1}, {"name":"_id_"});

// tg_ClientUserOrgUpAccount
db.tg_ClientUserOrgUpAccount.createIndex({"_id":1}, {"name":"_id_"});

// tg_GatewayPara
db.tg_GatewayPara.createIndex({"_id":1}, {"name":"_id_"});

// tg_Order
db.tg_Order.createIndex({"ClientOrderId":1}, {"name":"ClientOrderId_1","background":true});
db.tg_Order.createIndex({"OrderId":1}, {"unique":true,"name":"OrderId_1","background":true});
db.tg_Order.createIndex({"OrderTime":1}, {"name":"OrderTime_1","background":true});
db.tg_Order.createIndex({"_id":1}, {"name":"_id_"});

// tg_OtcQuotePara
db.tg_OtcQuotePara.createIndex({"GatewayId":1,"SymbolId":1}, {"name":"GatewayId_1_SymbolId_1","background":true});
db.tg_OtcQuotePara.createIndex({"_id":1}, {"name":"_id_"});

// tg_Position
db.tg_Position.createIndex({"PositionId":1}, {"unique":true,"name":"PositionId_1","background":true});
db.tg_Position.createIndex({"_id":1}, {"name":"_id_"});

// tg_SymbolCommodityMap
db.tg_SymbolCommodityMap.createIndex({"GatewayId":1,"UpAccountId":1,"Market":1,"Commodity":1}, {"name":"GatewayId_1_UpAccountId_1_Market_1_Commodity_1","background":true});
db.tg_SymbolCommodityMap.createIndex({"_id":1}, {"name":"_id_"});

// tg_SymbolContractMapRule
db.tg_SymbolContractMapRule.createIndex({"GatewayId":1,"UpAccountId":1,"Market":1}, {"name":"GatewayId_1_UpAccountId_1_Market_1","background":true});
db.tg_SymbolContractMapRule.createIndex({"_id":1}, {"name":"_id_"});

// tg_SymbolMarketMap
db.tg_SymbolMarketMap.createIndex({"GatewayId":1,"UpAccountId":1,"Market":1}, {"name":"GatewayId_1_UpAccountId_1_Market_1","background":true});
db.tg_SymbolMarketMap.createIndex({"_id":1}, {"name":"_id_"});

// tg_Tag50All
db.tg_Tag50All.createIndex({"_id":1}, {"name":"_id_"});

// tg_Trader
db.tg_Trader.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpAccount
db.tg_UpAccount.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpFillSyncTime
db.tg_UpFillSyncTime.createIndex({"UpAccountId":1}, {"unique":true,"name":"UpAccountId_1","background":true});
db.tg_UpFillSyncTime.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpOrder
db.tg_UpOrder.createIndex({"ClientOrderId":1}, {"name":"ClientOrderId_1","background":true});
db.tg_UpOrder.createIndex({"OrderId":1}, {"name":"OrderId_1","background":true});
db.tg_UpOrder.createIndex({"OrderTime":1}, {"name":"OrderTime_1","background":true});
db.tg_UpOrder.createIndex({"UpClientOrderId":1}, {"unique":true,"name":"UpClientOrderId_1","background":true});
db.tg_UpOrder.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpOrderSyncTime
db.tg_UpOrderSyncTime.createIndex({"UpAccountId":1}, {"unique":true,"name":"UpAccountId_1","background":true});
db.tg_UpOrderSyncTime.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpPosition
db.tg_UpPosition.createIndex({"PositionId":1}, {"unique":true,"name":"PositionId_1","background":true});
db.tg_UpPosition.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpPositionAdjust
db.tg_UpPositionAdjust.createIndex({"_id":1}, {"name":"_id_"});

// tg_UpTag50PositionAdjust
db.tg_UpTag50PositionAdjust.createIndex({"_id":1}, {"name":"_id_"});




use ph_java_db_fsr;

// Capital
db.Capital.createIndex({"ObjectId":1}, {"unique":true,"name":"ObjectId_1","background":true});
db.Capital.createIndex({"_id":1}, {"name":"_id_"});

// CapitalHistory
db.CapitalHistory.createIndex({"Key":1}, {"name":"Key_1","background":true});
db.CapitalHistory.createIndex({"_id":1}, {"name":"_id_"});

// TradeInterOrder
db.TradeInterOrder.createIndex({"OrderId":1}, {"unique":true,"name":"OrderId_1","background":true});
db.TradeInterOrder.createIndex({"OrderStamp":1}, {"name":"OrderStamp_1","background":true});
db.TradeInterOrder.createIndex({"_id":1}, {"name":"_id_"});

// TradeNtyMatch
db.TradeNtyMatch.createIndex({"OrderId":1}, {"name":"OrderId_1","background":true});
db.TradeNtyMatch.createIndex({"_id":1}, {"name":"_id_"});

// TradeNtyState
db.TradeNtyState.createIndex({"OrderId":1}, {"name":"OrderId_1","background":true});
db.TradeNtyState.createIndex({"_id":1}, {"name":"_id_"});

// TradeRspInsert
db.TradeRspInsert.createIndex({"OrderId":1}, {"name":"OrderId_1","background":true});
db.TradeRspInsert.createIndex({"_id":1}, {"name":"_id_"});

// TradeUpOrder
db.TradeUpOrder.createIndex({"LocalNo":1}, {"name":"LocalNo_1","background":true});
db.TradeUpOrder.createIndex({"OrderStamp":1}, {"name":"OrderStamp_1","background":true});
db.TradeUpOrder.createIndex({"SystemNo":1}, {"name":"SystemNo_1","background":true});
db.TradeUpOrder.createIndex({"UpReqSerialNo":1}, {"name":"UpReqSerialNo_1","background":true});
db.TradeUpOrder.createIndex({"_id":1}, {"name":"_id_"});

// camAccountIo
db.camAccountIo.createIndex({"_id":1}, {"name":"_id_"});
db.camAccountIo.createIndex({"createTime":1}, {"name":"createTime","background":true});
db.camAccountIo.createIndex({"createTimeYyyy":1}, {"name":"createTimeYyyy","background":true});
db.camAccountIo.createIndex({"createTimeYyyyMM":1}, {"name":"createTimeYyyyMM","background":true});
db.camAccountIo.createIndex({"createTimeYyyyMMdd":1}, {"name":"createTimeYyyyMMdd","background":true});
db.camAccountIo.createIndex({"createTimeYyyyMMddHH":1}, {"name":"createTimeYyyyMMddHH","background":true});
db.camAccountIo.createIndex({"currency":1}, {"name":"currency","background":true});
db.camAccountIo.createIndex({"ioType":1}, {"name":"ioType","background":true});
db.camAccountIo.createIndex({"orgCode":1,"accountType":1,"tradeType":1}, {"name":"index_query_key1","background":true});
db.camAccountIo.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.camAccountIo.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.camAccountIo.createIndex({"state":1}, {"name":"state","background":true});
db.camAccountIo.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});
db.camAccountIo.createIndex({"tradeDayYyyy":1}, {"name":"tradeDayYyyy","background":true});
db.camAccountIo.createIndex({"tradeDayYyyyMM":1}, {"name":"tradeDayYyyyMM","background":true});
db.camAccountIo.createIndex({"tradeDayYyyyMMdd":1}, {"name":"tradeDayYyyyMMdd","background":true});
db.camAccountIo.createIndex({"tradeType":1}, {"name":"tradeType","background":true});
db.camAccountIo.createIndex({"updateTime":1}, {"name":"updateTime","background":true});
db.camAccountIo.createIndex({"updateTimeYyyy":1}, {"name":"updateTimeYyyy","background":true});
db.camAccountIo.createIndex({"updateTimeYyyyMM":1}, {"name":"updateTimeYyyyMM","background":true});
db.camAccountIo.createIndex({"updateTimeYyyyMMdd":1}, {"name":"updateTimeYyyyMMdd","background":true});
db.camAccountIo.createIndex({"updateTimeYyyyMMddHH":1}, {"name":"updateTimeYyyyMMddHH","background":true});
db.camAccountIo.createIndex({"userId":1}, {"name":"userId","background":true});

// camAttAccountIo
db.camAttAccountIo.createIndex({"_id":1}, {"name":"_id_"});
db.camAttAccountIo.createIndex({"createTime":1}, {"name":"createTime","background":true});
db.camAttAccountIo.createIndex({"createTimeYyyy":1}, {"name":"createTimeYyyy","background":true});
db.camAttAccountIo.createIndex({"createTimeYyyyMM":1}, {"name":"createTimeYyyyMM","background":true});
db.camAttAccountIo.createIndex({"createTimeYyyyMMdd":1}, {"name":"createTimeYyyyMMdd","background":true});
db.camAttAccountIo.createIndex({"createTimeYyyyMMddHH":1}, {"name":"createTimeYyyyMMddHH","background":true});
db.camAttAccountIo.createIndex({"currency":1}, {"name":"currency","background":true});
db.camAttAccountIo.createIndex({"ioType":1}, {"name":"ioType","background":true});
db.camAttAccountIo.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.camAttAccountIo.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.camAttAccountIo.createIndex({"state":1}, {"name":"state","background":true});
db.camAttAccountIo.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});
db.camAttAccountIo.createIndex({"tradeDayYyyy":1}, {"name":"tradeDayYyyy","background":true});
db.camAttAccountIo.createIndex({"tradeDayYyyyMM":1}, {"name":"tradeDayYyyyMM","background":true});
db.camAttAccountIo.createIndex({"tradeDayYyyyMMdd":1}, {"name":"tradeDayYyyyMMdd","background":true});
db.camAttAccountIo.createIndex({"tradeType":1}, {"name":"tradeType","background":true});
db.camAttAccountIo.createIndex({"updateTime":1}, {"name":"updateTime","background":true});
db.camAttAccountIo.createIndex({"updateTimeYyyy":1}, {"name":"updateTimeYyyy","background":true});
db.camAttAccountIo.createIndex({"updateTimeYyyyMM":1}, {"name":"updateTimeYyyyMM","background":true});
db.camAttAccountIo.createIndex({"updateTimeYyyyMMdd":1}, {"name":"updateTimeYyyyMMdd","background":true});
db.camAttAccountIo.createIndex({"updateTimeYyyyMMddHH":1}, {"name":"updateTimeYyyyMMddHH","background":true});
db.camAttAccountIo.createIndex({"userId":1}, {"name":"userId","background":true});

// fs.chunks
db.fs.chunks.createIndex({"_id":1}, {"name":"_id_"});
db.fs.chunks.createIndex({"files_id":1,"n":1}, {"unique":true,"name":"files_id_1_n_1","background":true});

// fs.files
db.fs.files.createIndex({"_id":1}, {"name":"_id_"});
db.fs.files.createIndex({"filename":1,"uploadDate":1}, {"name":"filename_1_uploadDate_1","background":true});

// fsrDailySettlement
db.fsrDailySettlement.createIndex({"_id":1}, {"name":"_id_"});
db.fsrDailySettlement.createIndex({"settlementOrgCode":1}, {"name":"settlementOrgCode","background":true});
db.fsrDailySettlement.createIndex({"settlementOrgId":1}, {"name":"settlementOrgId","background":true});
db.fsrDailySettlement.createIndex({"settlementUserId":1}, {"name":"settlementUserId","background":true});
db.fsrDailySettlement.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});

// fsrDailySettlementEmail
db.fsrDailySettlementEmail.createIndex({"_id":1}, {"name":"_id_"});
db.fsrDailySettlementEmail.createIndex({"settlementDay":1}, {"name":"settlementDay","background":true});
db.fsrDailySettlementEmail.createIndex({"settlementOrgCode":1}, {"name":"settlementOrgCode","background":true});
db.fsrDailySettlementEmail.createIndex({"settlementOrgId":1}, {"name":"settlementOrgId","background":true});
db.fsrDailySettlementEmail.createIndex({"settlementUserId":1}, {"name":"settlementUserId","background":true});

// fsrDailySettlementPrice
db.fsrDailySettlementPrice.createIndex({"_id":1}, {"name":"_id_"});
db.fsrDailySettlementPrice.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.fsrDailySettlementPrice.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.fsrDailySettlementPrice.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});

// fsrExportFile
db.fsrExportFile.createIndex({"_id":1}, {"name":"_id_"});

// hisMarket
db.hisMarket.createIndex({"_id":1}, {"name":"_id_"});

// riskAccount
db.riskAccount.createIndex({"_id":1}, {"name":"_id_"});
db.riskAccount.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.riskAccount.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.riskAccount.createIndex({"userId":1}, {"name":"userId","background":true});

// riskAccountGroup
db.riskAccountGroup.createIndex({"_id":1}, {"name":"_id_"});
db.riskAccountGroup.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.riskAccountGroup.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.riskAccountGroup.createIndex({"userId":1}, {"name":"userId","background":true});

// riskBinaryOrderDetail
db.riskBinaryOrderDetail.createIndex({"_id":1}, {"name":"_id_"});
db.riskBinaryOrderDetail.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.riskBinaryOrderDetail.createIndex({"commodityType":1}, {"name":"commodityType","background":true});
db.riskBinaryOrderDetail.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.riskBinaryOrderDetail.createIndex({"counterOrgId":1,"roundTemplateId":1,"marketCode":1,"commodityCode":1,"contractCode":1,"commodityType":1,"timeStamp":-1}, {"name":"index_query_match_detail_TWO","background":true});
db.riskBinaryOrderDetail.createIndex({"counterOrgId":1,"roundTemplateId":1,"marketCode":1,"commodityCode":1,"contractCode":1,"commodityType":1}, {"name":"index_query_match_detail","background":true});
db.riskBinaryOrderDetail.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.riskBinaryOrderDetail.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.riskBinaryOrderDetail.createIndex({"roundEndTime":1}, {"name":"roundEndTime","background":true});
db.riskBinaryOrderDetail.createIndex({"roundStartTime":1}, {"name":"roundStartTime","background":true});
db.riskBinaryOrderDetail.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});

// riskPosition
db.riskPosition.createIndex({"_id":1}, {"name":"_id_"});
db.riskPosition.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.riskPosition.createIndex({"commodityCurrency":1}, {"name":"commodityCurrency","background":true});
db.riskPosition.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.riskPosition.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.riskPosition.createIndex({"contractId":1}, {"name":"contractId","background":true});
db.riskPosition.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.riskPosition.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.riskPosition.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.riskPosition.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.riskPosition.createIndex({"orgName":1}, {"name":"orgName","background":true});
db.riskPosition.createIndex({"productId":1}, {"name":"productId","background":true});
db.riskPosition.createIndex({"productOrgCode":1}, {"name":"productOrgCode","background":true});
db.riskPosition.createIndex({"productOrgId":1}, {"name":"productOrgId","background":true});
db.riskPosition.createIndex({"userId":1}, {"name":"userId","background":true});

// riskPositionTotal
db.riskPositionTotal.createIndex({"_id":1}, {"name":"_id_"});
db.riskPositionTotal.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.riskPositionTotal.createIndex({"commodityCurrency":1}, {"name":"commodityCurrency","background":true});
db.riskPositionTotal.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.riskPositionTotal.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.riskPositionTotal.createIndex({"contractId":1}, {"name":"contractId","background":true});
db.riskPositionTotal.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.riskPositionTotal.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.riskPositionTotal.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.riskPositionTotal.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.riskPositionTotal.createIndex({"orgName":1}, {"name":"orgName","background":true});
db.riskPositionTotal.createIndex({"productId":1}, {"name":"productId","background":true});
db.riskPositionTotal.createIndex({"productOrgCode":1}, {"name":"productOrgCode","background":true});
db.riskPositionTotal.createIndex({"productOrgId":1}, {"name":"productOrgId","background":true});
db.riskPositionTotal.createIndex({"userId":1}, {"name":"userId","background":true});

// riskRoundData
db.riskRoundData.createIndex({"_id":1}, {"name":"_id_"});
db.riskRoundData.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.riskRoundData.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.riskRoundData.createIndex({"counterOrgCode":1}, {"name":"counterOrgCode","background":true});
db.riskRoundData.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.riskRoundData.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.riskRoundData.createIndex({"roundEndTime":-1}, {"name":"roundEndTime","background":true});
db.riskRoundData.createIndex({"roundStartTime":-1}, {"name":"roundStartTime","background":true});
db.riskRoundData.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});

// riskRoundDataCount
db.riskRoundDataCount.createIndex({"_id":1}, {"name":"_id_"});
db.riskRoundDataCount.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.riskRoundDataCount.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.riskRoundDataCount.createIndex({"counterOrgCode":1}, {"name":"counterOrgCode","background":true});
db.riskRoundDataCount.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.riskRoundDataCount.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.riskRoundDataCount.createIndex({"roundEndTime":1}, {"name":"roundEndTime","background":true});
db.riskRoundDataCount.createIndex({"roundStartTime":1}, {"name":"roundStartTime","background":true});
db.riskRoundDataCount.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});

// sunx_java_db_fsr
db.sunx_java_db_fsr.createIndex({"_id":1}, {"name":"_id_"});

// tradeMatch
db.tradeMatch.createIndex({"_id":1}, {"name":"_id_"});
db.tradeMatch.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.tradeMatch.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.tradeMatch.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.tradeMatch.createIndex({"counterOrgCode":1,"orgCode":1,"tradeDayYyyyMMdd":1}, {"name":"index_query_key1","background":true});
db.tradeMatch.createIndex({"counterOrgCode":1}, {"name":"counterOrgCode","background":true});
db.tradeMatch.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.tradeMatch.createIndex({"createTime":-1}, {"name":"createTime","background":true});
db.tradeMatch.createIndex({"localMatchNo":1}, {"name":"localMatchNo","background":true});
db.tradeMatch.createIndex({"localOrderNo":1}, {"name":"localOrderNo","background":true});
db.tradeMatch.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.tradeMatch.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.tradeMatch.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.tradeMatch.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.tradeMatch.createIndex({"productId":1}, {"name":"productId","background":true});
db.tradeMatch.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});
db.tradeMatch.createIndex({"tradeDayYyyy":1}, {"name":"tradeDayYyyy","background":true});
db.tradeMatch.createIndex({"tradeDayYyyyMM":1}, {"name":"tradeDayYyyyMM","background":true});
db.tradeMatch.createIndex({"tradeDayYyyyMMdd":1}, {"name":"tradeDayYyyyMMdd","background":true});
db.tradeMatch.createIndex({"tradeMode":1,"orderState":1,"reason":1,"createTime":-1}, {"name":"index_query_key2","background":true});
db.tradeMatch.createIndex({"tradeMode":1}, {"name":"tradeMode","background":true});
db.tradeMatch.createIndex({"updateTime":-1}, {"name":"updateTime","background":true});
db.tradeMatch.createIndex({"userId":1}, {"name":"userId","background":true});

// tradeOrder
db.tradeOrder.createIndex({"_id":1}, {"name":"_id_"});
db.tradeOrder.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.tradeOrder.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.tradeOrder.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.tradeOrder.createIndex({"counterOrgCode":1,"orgCode":1,"tradeDayYyyyMMdd":1}, {"name":"index_query_key1","background":true});
db.tradeOrder.createIndex({"counterOrgCode":1}, {"name":"counterOrgCode","background":true});
db.tradeOrder.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.tradeOrder.createIndex({"createTime":-1}, {"name":"createTime","background":true});
db.tradeOrder.createIndex({"localOrderNo":1}, {"name":"localOrderNo","background":true});
db.tradeOrder.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.tradeOrder.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.tradeOrder.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.tradeOrder.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.tradeOrder.createIndex({"productId":1}, {"name":"productId","background":true});
db.tradeOrder.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});
db.tradeOrder.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});
db.tradeOrder.createIndex({"tradeDayYyyy":1}, {"name":"tradeDayYyyy","background":true});
db.tradeOrder.createIndex({"tradeDayYyyyMM":1}, {"name":"tradeDayYyyyMM","background":true});
db.tradeOrder.createIndex({"tradeDayYyyyMMdd":1}, {"name":"tradeDayYyyyMMdd","background":true});
db.tradeOrder.createIndex({"tradeMode":1,"orderState":1,"reason":1,"createTime":-1}, {"name":"index_query_key2","background":true});
db.tradeOrder.createIndex({"tradeMode":1}, {"name":"tradeMode","background":true});
db.tradeOrder.createIndex({"updateTime":-1}, {"name":"updateTime","background":true});
db.tradeOrder.createIndex({"userId":1}, {"name":"userId","background":true});

// tradePosition
db.tradePosition.createIndex({"_id":1}, {"name":"_id_"});
db.tradePosition.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.tradePosition.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.tradePosition.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.tradePosition.createIndex({"counterOrgCode":1}, {"name":"counterOrgCode","background":true});
db.tradePosition.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.tradePosition.createIndex({"createTime":-1}, {"name":"createTime","background":true});
db.tradePosition.createIndex({"localMatchNo":1}, {"name":"localMatchNo","background":true});
db.tradePosition.createIndex({"localOrderNo":1}, {"name":"localOrderNo","background":true});
db.tradePosition.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.tradePosition.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.tradePosition.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.tradePosition.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.tradePosition.createIndex({"positionNo":1}, {"name":"positionNo","background":true});
db.tradePosition.createIndex({"productId":1}, {"name":"productId","background":true});
db.tradePosition.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});
db.tradePosition.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});
db.tradePosition.createIndex({"tradeDayYyyy":1}, {"name":"tradeDayYyyy","background":true});
db.tradePosition.createIndex({"tradeDayYyyyMM":1}, {"name":"tradeDayYyyyMM","background":true});
db.tradePosition.createIndex({"tradeDayYyyyMMdd":1}, {"name":"tradeDayYyyyMMdd","background":true});
db.tradePosition.createIndex({"tradeMode":1,"counterOrgId":1,"commodityType":1,"marketCode":1,"commodityCode":1,"contractCode":1,"roundTemplateId":1,"roundEndTime":1,"roundStartTime":1}, {"name":"index_query_key1","background":true});
db.tradePosition.createIndex({"tradeMode":1,"counterOrgId":1,"commodityType":1,"marketId":1,"commodityId":1,"productId":1,"roundTemplateId":1,"roundEndTime":1,"roundStartTime":1}, {"name":"index_query_key2","background":true});
db.tradePosition.createIndex({"updateTime":-1}, {"name":"updateTime","background":true});
db.tradePosition.createIndex({"userId":1}, {"name":"userId","background":true});

// tradePositionSettleStream
db.tradePositionSettleStream.createIndex({"_id":1}, {"name":"_id_"});
db.tradePositionSettleStream.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.tradePositionSettleStream.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.tradePositionSettleStream.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.tradePositionSettleStream.createIndex({"counterOrgCode":1}, {"name":"counterOrgCode","background":true});
db.tradePositionSettleStream.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.tradePositionSettleStream.createIndex({"createTime":-1}, {"name":"createTime","background":true});
db.tradePositionSettleStream.createIndex({"localMatchNo":1}, {"name":"localMatchNo","background":true});
db.tradePositionSettleStream.createIndex({"localOrderNo":1}, {"name":"localOrderNo","background":true});
db.tradePositionSettleStream.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.tradePositionSettleStream.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.tradePositionSettleStream.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.tradePositionSettleStream.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.tradePositionSettleStream.createIndex({"positionNo":1}, {"name":"positionNo","background":true});
db.tradePositionSettleStream.createIndex({"productId":1}, {"name":"productId","background":true});
db.tradePositionSettleStream.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});
db.tradePositionSettleStream.createIndex({"settleDayYyyy":1}, {"name":"settleDayYyyy","background":true});
db.tradePositionSettleStream.createIndex({"settleDayYyyyMM":1}, {"name":"settleDayYyyyMM","background":true});
db.tradePositionSettleStream.createIndex({"settleDayYyyyMMdd":1}, {"name":"settleDayYyyyMMdd","background":true});
db.tradePositionSettleStream.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});
db.tradePositionSettleStream.createIndex({"tradeDayYyyy":1}, {"name":"tradeDayYyyy","background":true});
db.tradePositionSettleStream.createIndex({"tradeDayYyyyMM":1}, {"name":"tradeDayYyyyMM","background":true});
db.tradePositionSettleStream.createIndex({"tradeDayYyyyMMdd":1}, {"name":"tradeDayYyyyMMdd","background":true});
db.tradePositionSettleStream.createIndex({"updateTime":-1}, {"name":"updateTime","background":true});
db.tradePositionSettleStream.createIndex({"userId":1}, {"name":"userId","background":true});



use ph_java_db_fsr_sl;

// Capital
db.Capital.createIndex({"_id":1}, {"name":"_id_"});

// CapitalHistory
db.CapitalHistory.createIndex({"_id":1}, {"name":"_id_"});

// TradeInterOrder
db.TradeInterOrder.createIndex({"_id":1}, {"name":"_id_"});

// TradeNtyMatch
db.TradeNtyMatch.createIndex({"_id":1}, {"name":"_id_"});

// TradeNtyState
db.TradeNtyState.createIndex({"_id":1}, {"name":"_id_"});

// TradeRspInsert
db.TradeRspInsert.createIndex({"_id":1}, {"name":"_id_"});

// TradeUpOrder
db.TradeUpOrder.createIndex({"_id":1}, {"name":"_id_"});

// camAccountIo
db.camAccountIo.createIndex({"_id":1}, {"name":"_id_"});
db.camAccountIo.createIndex({"createTime":1}, {"name":"createTime","background":true});
db.camAccountIo.createIndex({"createTimeYyyy":1}, {"name":"createTimeYyyy","background":true});
db.camAccountIo.createIndex({"createTimeYyyyMM":1}, {"name":"createTimeYyyyMM","background":true});
db.camAccountIo.createIndex({"createTimeYyyyMMdd":1}, {"name":"createTimeYyyyMMdd","background":true});
db.camAccountIo.createIndex({"createTimeYyyyMMddHH":1}, {"name":"createTimeYyyyMMddHH","background":true});
db.camAccountIo.createIndex({"currency":1}, {"name":"currency","background":true});
db.camAccountIo.createIndex({"ioType":1}, {"name":"ioType","background":true});
db.camAccountIo.createIndex({"orgCode":1,"accountType":1,"tradeType":1}, {"name":"index_query_key1","background":true});
db.camAccountIo.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.camAccountIo.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.camAccountIo.createIndex({"state":1}, {"name":"state","background":true});
db.camAccountIo.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});
db.camAccountIo.createIndex({"tradeDayYyyy":1}, {"name":"tradeDayYyyy","background":true});
db.camAccountIo.createIndex({"tradeDayYyyyMM":1}, {"name":"tradeDayYyyyMM","background":true});
db.camAccountIo.createIndex({"tradeDayYyyyMMdd":1}, {"name":"tradeDayYyyyMMdd","background":true});
db.camAccountIo.createIndex({"tradeType":1}, {"name":"tradeType","background":true});
db.camAccountIo.createIndex({"updateTime":1}, {"name":"updateTime","background":true});
db.camAccountIo.createIndex({"updateTimeYyyy":1}, {"name":"updateTimeYyyy","background":true});
db.camAccountIo.createIndex({"updateTimeYyyyMM":1}, {"name":"updateTimeYyyyMM","background":true});
db.camAccountIo.createIndex({"updateTimeYyyyMMdd":1}, {"name":"updateTimeYyyyMMdd","background":true});
db.camAccountIo.createIndex({"updateTimeYyyyMMddHH":1}, {"name":"updateTimeYyyyMMddHH","background":true});
db.camAccountIo.createIndex({"userId":1}, {"name":"userId","background":true});

// camAttAccountIo
db.camAttAccountIo.createIndex({"_id":1}, {"name":"_id_"});
db.camAttAccountIo.createIndex({"createTime":1}, {"name":"createTime","background":true});
db.camAttAccountIo.createIndex({"createTimeYyyy":1}, {"name":"createTimeYyyy","background":true});
db.camAttAccountIo.createIndex({"createTimeYyyyMM":1}, {"name":"createTimeYyyyMM","background":true});
db.camAttAccountIo.createIndex({"createTimeYyyyMMdd":1}, {"name":"createTimeYyyyMMdd","background":true});
db.camAttAccountIo.createIndex({"createTimeYyyyMMddHH":1}, {"name":"createTimeYyyyMMddHH","background":true});
db.camAttAccountIo.createIndex({"currency":1}, {"name":"currency","background":true});
db.camAttAccountIo.createIndex({"ioType":1}, {"name":"ioType","background":true});
db.camAttAccountIo.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.camAttAccountIo.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.camAttAccountIo.createIndex({"state":1}, {"name":"state","background":true});
db.camAttAccountIo.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});
db.camAttAccountIo.createIndex({"tradeDayYyyy":1}, {"name":"tradeDayYyyy","background":true});
db.camAttAccountIo.createIndex({"tradeDayYyyyMM":1}, {"name":"tradeDayYyyyMM","background":true});
db.camAttAccountIo.createIndex({"tradeDayYyyyMMdd":1}, {"name":"tradeDayYyyyMMdd","background":true});
db.camAttAccountIo.createIndex({"tradeType":1}, {"name":"tradeType","background":true});
db.camAttAccountIo.createIndex({"updateTime":1}, {"name":"updateTime","background":true});
db.camAttAccountIo.createIndex({"updateTimeYyyy":1}, {"name":"updateTimeYyyy","background":true});
db.camAttAccountIo.createIndex({"updateTimeYyyyMM":1}, {"name":"updateTimeYyyyMM","background":true});
db.camAttAccountIo.createIndex({"updateTimeYyyyMMdd":1}, {"name":"updateTimeYyyyMMdd","background":true});
db.camAttAccountIo.createIndex({"updateTimeYyyyMMddHH":1}, {"name":"updateTimeYyyyMMddHH","background":true});
db.camAttAccountIo.createIndex({"userId":1}, {"name":"userId","background":true});

// fs.chunks
db.fs.chunks.createIndex({"_id":1}, {"name":"_id_"});

// fs.files
db.fs.files.createIndex({"_id":1}, {"name":"_id_"});

// fsrDailySettlement
db.fsrDailySettlement.createIndex({"_id":1}, {"name":"_id_"});
db.fsrDailySettlement.createIndex({"settlementOrgCode":1}, {"name":"settlementOrgCode","background":true});
db.fsrDailySettlement.createIndex({"settlementOrgId":1}, {"name":"settlementOrgId","background":true});
db.fsrDailySettlement.createIndex({"settlementUserId":1}, {"name":"settlementUserId","background":true});
db.fsrDailySettlement.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});

// fsrDailySettlementEmail
db.fsrDailySettlementEmail.createIndex({"_id":1}, {"name":"_id_"});
db.fsrDailySettlementEmail.createIndex({"settlementDay":1}, {"name":"settlementDay","background":true});
db.fsrDailySettlementEmail.createIndex({"settlementOrgCode":1}, {"name":"settlementOrgCode","background":true});
db.fsrDailySettlementEmail.createIndex({"settlementOrgId":1}, {"name":"settlementOrgId","background":true});
db.fsrDailySettlementEmail.createIndex({"settlementUserId":1}, {"name":"settlementUserId","background":true});

// fsrDailySettlementPrice
db.fsrDailySettlementPrice.createIndex({"_id":1}, {"name":"_id_"});
db.fsrDailySettlementPrice.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.fsrDailySettlementPrice.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.fsrDailySettlementPrice.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});

// fsrExportFile
db.fsrExportFile.createIndex({"_id":1}, {"name":"_id_"});

// hisMarket
db.hisMarket.createIndex({"_id":1}, {"name":"_id_"});

// riskAccount
db.riskAccount.createIndex({"_id":1}, {"name":"_id_"});
db.riskAccount.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.riskAccount.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.riskAccount.createIndex({"userId":1}, {"name":"userId","background":true});

// riskAccountGroup
db.riskAccountGroup.createIndex({"_id":1}, {"name":"_id_"});
db.riskAccountGroup.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.riskAccountGroup.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.riskAccountGroup.createIndex({"userId":1}, {"name":"userId","background":true});

// riskBinaryOrderDetail
db.riskBinaryOrderDetail.createIndex({"_id":1}, {"name":"_id_"});
db.riskBinaryOrderDetail.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.riskBinaryOrderDetail.createIndex({"commodityType":1}, {"name":"commodityType","background":true});
db.riskBinaryOrderDetail.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.riskBinaryOrderDetail.createIndex({"counterOrgId":1,"roundTemplateId":1,"marketCode":1,"commodityCode":1,"contractCode":1,"commodityType":1,"timeStamp":-1}, {"name":"index_query_match_detail_TWO","background":true});
db.riskBinaryOrderDetail.createIndex({"counterOrgId":1,"roundTemplateId":1,"marketCode":1,"commodityCode":1,"contractCode":1,"commodityType":1}, {"name":"index_query_match_detail","background":true});
db.riskBinaryOrderDetail.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.riskBinaryOrderDetail.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.riskBinaryOrderDetail.createIndex({"roundEndTime":1}, {"name":"roundEndTime","background":true});
db.riskBinaryOrderDetail.createIndex({"roundStartTime":1}, {"name":"roundStartTime","background":true});
db.riskBinaryOrderDetail.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});

// riskPosition
db.riskPosition.createIndex({"_id":1}, {"name":"_id_"});
db.riskPosition.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.riskPosition.createIndex({"commodityCurrency":1}, {"name":"commodityCurrency","background":true});
db.riskPosition.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.riskPosition.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.riskPosition.createIndex({"contractId":1}, {"name":"contractId","background":true});
db.riskPosition.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.riskPosition.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.riskPosition.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.riskPosition.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.riskPosition.createIndex({"orgName":1}, {"name":"orgName","background":true});
db.riskPosition.createIndex({"productId":1}, {"name":"productId","background":true});
db.riskPosition.createIndex({"productOrgCode":1}, {"name":"productOrgCode","background":true});
db.riskPosition.createIndex({"productOrgId":1}, {"name":"productOrgId","background":true});
db.riskPosition.createIndex({"userId":1}, {"name":"userId","background":true});

// riskPositionTotal
db.riskPositionTotal.createIndex({"_id":1}, {"name":"_id_"});
db.riskPositionTotal.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.riskPositionTotal.createIndex({"commodityCurrency":1}, {"name":"commodityCurrency","background":true});
db.riskPositionTotal.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.riskPositionTotal.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.riskPositionTotal.createIndex({"contractId":1}, {"name":"contractId","background":true});
db.riskPositionTotal.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.riskPositionTotal.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.riskPositionTotal.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.riskPositionTotal.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.riskPositionTotal.createIndex({"orgName":1}, {"name":"orgName","background":true});
db.riskPositionTotal.createIndex({"productId":1}, {"name":"productId","background":true});
db.riskPositionTotal.createIndex({"productOrgCode":1}, {"name":"productOrgCode","background":true});
db.riskPositionTotal.createIndex({"productOrgId":1}, {"name":"productOrgId","background":true});
db.riskPositionTotal.createIndex({"userId":1}, {"name":"userId","background":true});

// riskRoundData
db.riskRoundData.createIndex({"_id":1}, {"name":"_id_"});
db.riskRoundData.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.riskRoundData.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.riskRoundData.createIndex({"counterOrgCode":1}, {"name":"counterOrgCode","background":true});
db.riskRoundData.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.riskRoundData.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.riskRoundData.createIndex({"roundEndTime":-1}, {"name":"roundEndTime","background":true});
db.riskRoundData.createIndex({"roundStartTime":-1}, {"name":"roundStartTime","background":true});
db.riskRoundData.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});

// riskRoundDataCount
db.riskRoundDataCount.createIndex({"_id":1}, {"name":"_id_"});
db.riskRoundDataCount.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.riskRoundDataCount.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.riskRoundDataCount.createIndex({"counterOrgCode":1}, {"name":"counterOrgCode","background":true});
db.riskRoundDataCount.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.riskRoundDataCount.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.riskRoundDataCount.createIndex({"roundEndTime":1}, {"name":"roundEndTime","background":true});
db.riskRoundDataCount.createIndex({"roundStartTime":1}, {"name":"roundStartTime","background":true});
db.riskRoundDataCount.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});

// tradeMatch
db.tradeMatch.createIndex({"_id":1}, {"name":"_id_"});
db.tradeMatch.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.tradeMatch.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.tradeMatch.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.tradeMatch.createIndex({"counterOrgCode":1,"orgCode":1,"tradeDayYyyyMMdd":1}, {"name":"index_query_key1","background":true});
db.tradeMatch.createIndex({"counterOrgCode":1}, {"name":"counterOrgCode","background":true});
db.tradeMatch.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.tradeMatch.createIndex({"createTime":-1}, {"name":"createTime","background":true});
db.tradeMatch.createIndex({"localMatchNo":1}, {"name":"localMatchNo","background":true});
db.tradeMatch.createIndex({"localOrderNo":1}, {"name":"localOrderNo","background":true});
db.tradeMatch.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.tradeMatch.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.tradeMatch.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.tradeMatch.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.tradeMatch.createIndex({"productId":1}, {"name":"productId","background":true});
db.tradeMatch.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});
db.tradeMatch.createIndex({"tradeDayYyyy":1}, {"name":"tradeDayYyyy","background":true});
db.tradeMatch.createIndex({"tradeDayYyyyMM":1}, {"name":"tradeDayYyyyMM","background":true});
db.tradeMatch.createIndex({"tradeDayYyyyMMdd":1}, {"name":"tradeDayYyyyMMdd","background":true});
db.tradeMatch.createIndex({"tradeMode":1,"orderState":1,"reason":1,"createTime":-1}, {"name":"index_query_key2","background":true});
db.tradeMatch.createIndex({"tradeMode":1}, {"name":"tradeMode","background":true});
db.tradeMatch.createIndex({"updateTime":-1}, {"name":"updateTime","background":true});
db.tradeMatch.createIndex({"userId":1}, {"name":"userId","background":true});

// tradeOrder
db.tradeOrder.createIndex({"_id":1}, {"name":"_id_"});
db.tradeOrder.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.tradeOrder.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.tradeOrder.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.tradeOrder.createIndex({"counterOrgCode":1,"orgCode":1,"tradeDayYyyyMMdd":1}, {"name":"index_query_key1","background":true});
db.tradeOrder.createIndex({"counterOrgCode":1}, {"name":"counterOrgCode","background":true});
db.tradeOrder.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.tradeOrder.createIndex({"createTime":-1}, {"name":"createTime","background":true});
db.tradeOrder.createIndex({"localOrderNo":1}, {"name":"localOrderNo","background":true});
db.tradeOrder.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.tradeOrder.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.tradeOrder.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.tradeOrder.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.tradeOrder.createIndex({"productId":1}, {"name":"productId","background":true});
db.tradeOrder.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});
db.tradeOrder.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});
db.tradeOrder.createIndex({"tradeDayYyyy":1}, {"name":"tradeDayYyyy","background":true});
db.tradeOrder.createIndex({"tradeDayYyyyMM":1}, {"name":"tradeDayYyyyMM","background":true});
db.tradeOrder.createIndex({"tradeDayYyyyMMdd":1}, {"name":"tradeDayYyyyMMdd","background":true});
db.tradeOrder.createIndex({"tradeMode":1,"orderState":1,"reason":1,"createTime":-1}, {"name":"index_query_key2","background":true});
db.tradeOrder.createIndex({"tradeMode":1}, {"name":"tradeMode","background":true});
db.tradeOrder.createIndex({"updateTime":-1}, {"name":"updateTime","background":true});
db.tradeOrder.createIndex({"userId":1}, {"name":"userId","background":true});

// tradePosition
db.tradePosition.createIndex({"_id":1}, {"name":"_id_"});
db.tradePosition.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.tradePosition.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.tradePosition.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.tradePosition.createIndex({"counterOrgCode":1}, {"name":"counterOrgCode","background":true});
db.tradePosition.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.tradePosition.createIndex({"createTime":-1}, {"name":"createTime","background":true});
db.tradePosition.createIndex({"localMatchNo":1}, {"name":"localMatchNo","background":true});
db.tradePosition.createIndex({"localOrderNo":1}, {"name":"localOrderNo","background":true});
db.tradePosition.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.tradePosition.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.tradePosition.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.tradePosition.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.tradePosition.createIndex({"positionNo":1}, {"name":"positionNo","background":true});
db.tradePosition.createIndex({"productId":1}, {"name":"productId","background":true});
db.tradePosition.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});
db.tradePosition.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});
db.tradePosition.createIndex({"tradeDayYyyy":1}, {"name":"tradeDayYyyy","background":true});
db.tradePosition.createIndex({"tradeDayYyyyMM":1}, {"name":"tradeDayYyyyMM","background":true});
db.tradePosition.createIndex({"tradeDayYyyyMMdd":1}, {"name":"tradeDayYyyyMMdd","background":true});
db.tradePosition.createIndex({"tradeMode":1,"counterOrgId":1,"commodityType":1,"marketCode":1,"commodityCode":1,"contractCode":1,"roundTemplateId":1,"roundEndTime":1,"roundStartTime":1}, {"name":"index_query_key1","background":true});
db.tradePosition.createIndex({"tradeMode":1,"counterOrgId":1,"commodityType":1,"marketId":1,"commodityId":1,"productId":1,"roundTemplateId":1,"roundEndTime":1,"roundStartTime":1}, {"name":"index_query_key2","background":true});
db.tradePosition.createIndex({"updateTime":-1}, {"name":"updateTime","background":true});
db.tradePosition.createIndex({"userId":1}, {"name":"userId","background":true});

// tradePositionSettleStream
db.tradePositionSettleStream.createIndex({"_id":1}, {"name":"_id_"});
db.tradePositionSettleStream.createIndex({"commodityCode":1}, {"name":"commodityCode","background":true});
db.tradePositionSettleStream.createIndex({"commodityId":1}, {"name":"commodityId","background":true});
db.tradePositionSettleStream.createIndex({"contractCode":1}, {"name":"contractCode","background":true});
db.tradePositionSettleStream.createIndex({"counterOrgCode":1}, {"name":"counterOrgCode","background":true});
db.tradePositionSettleStream.createIndex({"counterOrgId":1}, {"name":"counterOrgId","background":true});
db.tradePositionSettleStream.createIndex({"createTime":-1}, {"name":"createTime","background":true});
db.tradePositionSettleStream.createIndex({"localMatchNo":1}, {"name":"localMatchNo","background":true});
db.tradePositionSettleStream.createIndex({"localOrderNo":1}, {"name":"localOrderNo","background":true});
db.tradePositionSettleStream.createIndex({"marketCode":1}, {"name":"marketCode","background":true});
db.tradePositionSettleStream.createIndex({"marketId":1}, {"name":"marketId","background":true});
db.tradePositionSettleStream.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.tradePositionSettleStream.createIndex({"orgId":1}, {"name":"orgId","background":true});
db.tradePositionSettleStream.createIndex({"positionNo":1}, {"name":"positionNo","background":true});
db.tradePositionSettleStream.createIndex({"productId":1}, {"name":"productId","background":true});
db.tradePositionSettleStream.createIndex({"roundTemplateId":1}, {"name":"roundTemplateId","background":true});
db.tradePositionSettleStream.createIndex({"settleDayYyyy":1}, {"name":"settleDayYyyy","background":true});
db.tradePositionSettleStream.createIndex({"settleDayYyyyMM":1}, {"name":"settleDayYyyyMM","background":true});
db.tradePositionSettleStream.createIndex({"settleDayYyyyMMdd":1}, {"name":"settleDayYyyyMMdd","background":true});
db.tradePositionSettleStream.createIndex({"tradeDay":1}, {"name":"tradeDay","background":true});
db.tradePositionSettleStream.createIndex({"tradeDayYyyy":1}, {"name":"tradeDayYyyy","background":true});
db.tradePositionSettleStream.createIndex({"tradeDayYyyyMM":1}, {"name":"tradeDayYyyyMM","background":true});
db.tradePositionSettleStream.createIndex({"tradeDayYyyyMMdd":1}, {"name":"tradeDayYyyyMMdd","background":true});
db.tradePositionSettleStream.createIndex({"updateTime":-1}, {"name":"updateTime","background":true});
db.tradePositionSettleStream.createIndex({"userId":1}, {"name":"userId","background":true});



use ph_java_db_gateway_support;

// mcMessageSend
db.mcMessageSend.createIndex({"_id":1}, {"name":"_id_"});
db.mcMessageSend.createIndex({"countryCode":1}, {"name":"countryCode","background":true});
db.mcMessageSend.createIndex({"errorId":1}, {"name":"errorId","background":true});
db.mcMessageSend.createIndex({"messageType":1}, {"name":"messageType","background":true});
db.mcMessageSend.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.mcMessageSend.createIndex({"reqTime":1}, {"name":"reqTime","background":true});
db.mcMessageSend.createIndex({"sendTo":1}, {"name":"sendTo","background":true});
db.mcMessageSend.createIndex({"sendToType":1}, {"name":"sendToType","background":true});
db.mcMessageSend.createIndex({"templateType":1}, {"name":"templateType","background":true});
db.mcMessageSend.createIndex({"userId":1}, {"name":"userId","background":true});
db.mcMessageSend.createIndex({"userOrgCode":1}, {"name":"userOrgCode","background":true});
db.mcMessageSend.createIndex({"userOrgId":1}, {"name":"userOrgId","background":true});

// otpLog
db.otpLog.createIndex({"_id":1}, {"name":"_id_"});





use ph_java_db_support;

// mcMessageSend
db.mcMessageSend.createIndex({"_id":1}, {"name":"_id_"});
db.mcMessageSend.createIndex({"countryCode":1}, {"name":"countryCode","background":true});
db.mcMessageSend.createIndex({"errorId":1}, {"name":"errorId","background":true});
db.mcMessageSend.createIndex({"messageType":1}, {"name":"messageType","background":true});
db.mcMessageSend.createIndex({"orgCode":1}, {"name":"orgCode","background":true});
db.mcMessageSend.createIndex({"reqTime":1}, {"name":"reqTime","background":true});
db.mcMessageSend.createIndex({"sendTo":1}, {"name":"sendTo","background":true});
db.mcMessageSend.createIndex({"sendToType":1}, {"name":"sendToType","background":true});
db.mcMessageSend.createIndex({"templateType":1}, {"name":"templateType","background":true});
db.mcMessageSend.createIndex({"userId":1}, {"name":"userId","background":true});
db.mcMessageSend.createIndex({"userOrgCode":1}, {"name":"userOrgCode","background":true});
db.mcMessageSend.createIndex({"userOrgId":1}, {"name":"userOrgId","background":true});

// otpLog
db.otpLog.createIndex({"_id":1}, {"name":"_id_"});


use ph_routine;


// CppAutoIncr
db.CppAutoIncr.createIndex({"_id":1}, {"name":"_id_"});

// GatewaySymbol
db.GatewaySymbol.createIndex({"_id":1}, {"name":"_id_"});

// QuoteInterSignalPrice
db.QuoteInterSignalPrice.createIndex({"ProductId":1,"SignType":1}, {"unique":true,"name":"ProductId_1_SignType_1","background":true});
db.QuoteInterSignalPrice.createIndex({"_id":1}, {"name":"_id_"});

// QuotePriceCur
db.QuotePriceCur.createIndex({"ProductArray":1}, {"unique":true,"name":"ProductArray_1","background":true});
db.QuotePriceCur.createIndex({"_id":1}, {"name":"_id_"});

// QuotePriceHistorySignal_1440_2022
db.QuotePriceHistorySignal_1440_2022.createIndex({"ProductArray":1,"SignType":1,"Stamp":1}, {"unique":true,"name":"ProductArray_1_SignType_1_Stamp_1","background":true});
db.QuotePriceHistorySignal_1440_2022.createIndex({"_id":1}, {"name":"_id_"});

// QuotePriceHistorySignal_15_2022
db.QuotePriceHistorySignal_15_2022.createIndex({"ProductArray":1,"SignType":1,"Stamp":1}, {"unique":true,"name":"ProductArray_1_SignType_1_Stamp_1","background":true});
db.QuotePriceHistorySignal_15_2022.createIndex({"_id":1}, {"name":"_id_"});

// QuotePriceHistorySignal_1_2022
db.QuotePriceHistorySignal_1_2022.createIndex({"ProductArray":1,"SignType":1,"Stamp":1}, {"unique":true,"name":"ProductArray_1_SignType_1_Stamp_1","background":true});
db.QuotePriceHistorySignal_1_2022.createIndex({"_id":1}, {"name":"_id_"});

// QuotePriceHistorySignal_240_2022
db.QuotePriceHistorySignal_240_2022.createIndex({"ProductArray":1,"SignType":1,"Stamp":1}, {"unique":true,"name":"ProductArray_1_SignType_1_Stamp_1","background":true});
db.QuotePriceHistorySignal_240_2022.createIndex({"_id":1}, {"name":"_id_"});

// QuotePriceHistorySignal_30_2022
db.QuotePriceHistorySignal_30_2022.createIndex({"ProductArray":1,"SignType":1,"Stamp":1}, {"unique":true,"name":"ProductArray_1_SignType_1_Stamp_1","background":true});
db.QuotePriceHistorySignal_30_2022.createIndex({"_id":1}, {"name":"_id_"});

// QuotePriceHistorySignal_5_2022
db.QuotePriceHistorySignal_5_2022.createIndex({"ProductArray":1,"SignType":1,"Stamp":1}, {"unique":true,"name":"ProductArray_1_SignType_1_Stamp_1","background":true});
db.QuotePriceHistorySignal_5_2022.createIndex({"_id":1}, {"name":"_id_"});

// QuotePriceHistorySignal_60_2022
db.QuotePriceHistorySignal_60_2022.createIndex({"ProductArray":1,"SignType":1,"Stamp":1}, {"unique":true,"name":"ProductArray_1_SignType_1_Stamp_1","background":true});
db.QuotePriceHistorySignal_60_2022.createIndex({"_id":1}, {"name":"_id_"});

// QuoteTopSignalPrice
db.QuoteTopSignalPrice.createIndex({"_id":1}, {"name":"_id_"});
db.QuoteTopSignalPrice.createIndex({"productid":1,"signtype":1}, {"unique":true,"name":"productid_1_signtype_1","background":true});

// QuoteTradeDetail
db.QuoteTradeDetail.createIndex({"ProductId":1,"Index":1,"Serial":1}, {"unique":true,"name":"ProductId_1_Index_1_Serial_1","background":true});
db.QuoteTradeDetail.createIndex({"_id":1}, {"name":"_id_"});

// RiskctrlRule
db.RiskctrlRule.createIndex({"_id":1}, {"name":"_id_"});

// RiskctrlRuleAssign
db.RiskctrlRuleAssign.createIndex({"_id":1}, {"name":"_id_"});

// TimeCancelOrderRiskctrlSet
db.TimeCancelOrderRiskctrlSet.createIndex({"_id":1}, {"name":"_id_"});

// TimeRiskctrlSet
db.TimeRiskctrlSet.createIndex({"_id":1}, {"name":"_id_"});

// TradeInterOrder
db.TradeInterOrder.createIndex({"OrderId":1}, {"unique":true,"name":"OrderId_1","background":true});
db.TradeInterOrder.createIndex({"OrderStamp":1}, {"name":"OrderStamp_1","background":true});
db.TradeInterOrder.createIndex({"_id":1}, {"name":"_id_"});

// TradeNtyMatch
db.TradeNtyMatch.createIndex({"OrderId":1}, {"name":"OrderId_1","background":true});
db.TradeNtyMatch.createIndex({"_id":1}, {"name":"_id_"});

// TradeNtyState
db.TradeNtyState.createIndex({"OrderId":1}, {"name":"OrderId_1","background":true});
db.TradeNtyState.createIndex({"_id":1}, {"name":"_id_"});

// TradeRspInsert
db.TradeRspInsert.createIndex({"OrderId":1}, {"name":"OrderId_1","background":true});
db.TradeRspInsert.createIndex({"_id":1}, {"name":"_id_"});

// TradeRule
db.TradeRule.createIndex({"_id":1}, {"name":"_id_"});

// TradeUpAccount
db.TradeUpAccount.createIndex({"_id":1}, {"name":"_id_"});

// TradeUpCapital
db.TradeUpCapital.createIndex({"_id":1}, {"name":"_id_"});

// TradeUpMatch
db.TradeUpMatch.createIndex({"_id":1}, {"name":"_id_"});

// TradeUpOrder
db.TradeUpOrder.createIndex({"LocalNo":1}, {"name":"LocalNo_1","background":true});
db.TradeUpOrder.createIndex({"OrderStamp":1}, {"name":"OrderStamp_1","background":true});
db.TradeUpOrder.createIndex({"SystemNo":1}, {"name":"SystemNo_1","background":true});
db.TradeUpOrder.createIndex({"UpReqSerialNo":1}, {"name":"UpReqSerialNo_1","background":true});
db.TradeUpOrder.createIndex({"_id":1}, {"name":"_id_"});

// UpQuoteFeed
db.UpQuoteFeed.createIndex({"_id":1}, {"name":"_id_"});

// UpQuoteSource
db.UpQuoteSource.createIndex({"_id":1}, {"name":"_id_"});


use ph_routine_sl;

// CppAutoIncr
db.CppAutoIncr.createIndex({"_id":1}, {"name":"_id_"});

// GatewaySymbol
db.GatewaySymbol.createIndex({"_id":1}, {"name":"_id_"});

// QuoteInterSignalPrice
db.QuoteInterSignalPrice.createIndex({"_id":1}, {"name":"_id_"});

// QuotePriceCur
db.QuotePriceCur.createIndex({"_id":1}, {"name":"_id_"});

// QuoteTopSignalPrice
db.QuoteTopSignalPrice.createIndex({"_id":1}, {"name":"_id_"});

// QuoteTradeDetail
db.QuoteTradeDetail.createIndex({"_id":1}, {"name":"_id_"});

// RiskctrlRule
db.RiskctrlRule.createIndex({"_id":1}, {"name":"_id_"});

// RiskctrlRuleAssign
db.RiskctrlRuleAssign.createIndex({"_id":1}, {"name":"_id_"});

// TimeRiskctrlSet
db.TimeRiskctrlSet.createIndex({"_id":1}, {"name":"_id_"});

// TradeInterOrder
db.TradeInterOrder.createIndex({"OrderId":1}, {"unique":true,"name":"OrderId_1","background":true});
db.TradeInterOrder.createIndex({"OrderStamp":1}, {"name":"OrderStamp_1","background":true});
db.TradeInterOrder.createIndex({"_id":1}, {"name":"_id_"});

// TradeNtyMatch
db.TradeNtyMatch.createIndex({"OrderId":1}, {"name":"OrderId_1","background":true});
db.TradeNtyMatch.createIndex({"_id":1}, {"name":"_id_"});

// TradeNtyState
db.TradeNtyState.createIndex({"OrderId":1}, {"name":"OrderId_1","background":true});
db.TradeNtyState.createIndex({"_id":1}, {"name":"_id_"});

// TradeRspInsert
db.TradeRspInsert.createIndex({"OrderId":1}, {"name":"OrderId_1","background":true});
db.TradeRspInsert.createIndex({"_id":1}, {"name":"_id_"});

// TradeRule
db.TradeRule.createIndex({"_id":1}, {"name":"_id_"});

// TradeUpAccount
db.TradeUpAccount.createIndex({"_id":1}, {"name":"_id_"});

// TradeUpCapital
db.TradeUpCapital.createIndex({"_id":1}, {"name":"_id_"});

// TradeUpMatch
db.TradeUpMatch.createIndex({"_id":1}, {"name":"_id_"});

// TradeUpOrder
db.TradeUpOrder.createIndex({"LocalNo":1}, {"name":"LocalNo_1","background":true});
db.TradeUpOrder.createIndex({"OrderStamp":1}, {"name":"OrderStamp_1","background":true});
db.TradeUpOrder.createIndex({"SystemNo":1}, {"name":"SystemNo_1","background":true});
db.TradeUpOrder.createIndex({"UpReqSerialNo":1}, {"name":"UpReqSerialNo_1","background":true});
db.TradeUpOrder.createIndex({"_id":1}, {"name":"_id_"});

// UpQuoteFeed
db.UpQuoteFeed.createIndex({"_id":1}, {"name":"_id_"});

// UpQuoteSource
db.UpQuoteSource.createIndex({"_id":1}, {"name":"_id_"});


use ph_trade_simulator;

// ABQuotes
db.ABQuotes.createIndex({"SymbolId":1}, {"name":"SymbolId_1","background":true});
db.ABQuotes.createIndex({"_id":1}, {"name":"_id_"});

// Capital
db.Capital.createIndex({"ObjectId":1}, {"unique":true,"name":"ObjectId_1","background":true});
db.Capital.createIndex({"_id":1}, {"name":"_id_"});

// CapitalHistory
db.CapitalHistory.createIndex({"Key":1}, {"name":"Key_1","background":true});
db.CapitalHistory.createIndex({"_id":1}, {"name":"_id_"});

// Commodity
db.Commodity.createIndex({"Market":1,"Commodity":1}, {"unique":true,"name":"Market_1_Commodity_1","background":true});
db.Commodity.createIndex({"_id":1}, {"name":"_id_"});

// CommodityTradeTime
db.CommodityTradeTime.createIndex({"CommodityId":1}, {"unique":true,"name":"CommodityId_1","background":true});
db.CommodityTradeTime.createIndex({"Key":1}, {"unique":true,"name":"Key_1","background":true});
db.CommodityTradeTime.createIndex({"_id":1}, {"name":"_id_"});

// CppAutoIncr
db.CppAutoIncr.createIndex({"_id":1}, {"name":"_id_"});

// Currency
db.Currency.createIndex({"_id":1}, {"name":"_id_"});

// GlobalPara
db.GlobalPara.createIndex({"_id":1}, {"name":"_id_"});

// InFill
db.InFill.createIndex({"FillNo":1}, {"name":"FillNo_1","background":true});
db.InFill.createIndex({"FillTime":-1}, {"name":"FillTime_-1","background":true});
db.InFill.createIndex({"_id":1}, {"name":"_id_"});

// InOrder
db.InOrder.createIndex({"ClientOrderId":1}, {"name":"ClientOrderId_1","background":true});
db.InOrder.createIndex({"OrderTime":-1}, {"name":"OrderTime_-1","background":true});
db.InOrder.createIndex({"_id":1}, {"name":"_id_"});

// InPosition
db.InPosition.createIndex({"PositionId":1}, {"name":"PositionId_1","background":true});
db.InPosition.createIndex({"_id":1}, {"name":"_id_"});

// LiquidityProvider
db.LiquidityProvider.createIndex({"_id":1}, {"name":"_id_"});

// LpRoute
db.LpRoute.createIndex({"_id":1}, {"name":"_id_"});

// Market
db.Market.createIndex({"Market":1}, {"unique":true,"name":"Market_1","background":true});
db.Market.createIndex({"_id":1}, {"name":"_id_"});

// OtcQuotePara
db.OtcQuotePara.createIndex({"SymbolId":1}, {"unique":true,"name":"SymbolId_1","background":true});
db.OtcQuotePara.createIndex({"_id":1}, {"name":"_id_"});

// OutFill
db.OutFill.createIndex({"FillNo":1}, {"name":"FillNo_1","background":true});
db.OutFill.createIndex({"FillTime":-1}, {"name":"FillTime_-1","background":true});
db.OutFill.createIndex({"_id":1}, {"name":"_id_"});

// OutOrder
db.OutOrder.createIndex({"ClientOrderId":1}, {"name":"ClientOrderId_1","background":true});
db.OutOrder.createIndex({"OrderTime":-1}, {"name":"OrderTime_-1","background":true});
db.OutOrder.createIndex({"_id":1}, {"name":"_id_"});

// OutPosition
db.OutPosition.createIndex({"PositionId":1}, {"name":"PositionId_1","background":true});
db.OutPosition.createIndex({"_id":1}, {"name":"_id_"});

// StreamId
db.StreamId.createIndex({"_id":1}, {"name":"_id_"});

// Symbol
db.Symbol.createIndex({"_id":1}, {"name":"_id_"});




use ph_trade_simulator_sl;

// ABQuotes
db.ABQuotes.createIndex({"SymbolId":1}, {"name":"SymbolId_1","background":true});
db.ABQuotes.createIndex({"_id":1}, {"name":"_id_"});

// Capital
db.Capital.createIndex({"ObjectId":1}, {"unique":true,"name":"ObjectId_1","background":true});
db.Capital.createIndex({"_id":1}, {"name":"_id_"});

// CapitalHistory
db.CapitalHistory.createIndex({"Key":1}, {"name":"Key_1","background":true});
db.CapitalHistory.createIndex({"_id":1}, {"name":"_id_"});

// Commodity
db.Commodity.createIndex({"Market":1,"Commodity":1}, {"unique":true,"name":"Market_1_Commodity_1","background":true});
db.Commodity.createIndex({"_id":1}, {"name":"_id_"});

// CommodityTradeTime
db.CommodityTradeTime.createIndex({"CommodityId":1}, {"unique":true,"name":"CommodityId_1","background":true});
db.CommodityTradeTime.createIndex({"Key":1}, {"unique":true,"name":"Key_1","background":true});
db.CommodityTradeTime.createIndex({"_id":1}, {"name":"_id_"});

// CppAutoIncr
db.CppAutoIncr.createIndex({"_id":1}, {"name":"_id_"});

// Currency
db.Currency.createIndex({"_id":1}, {"name":"_id_"});

// GlobalPara
db.GlobalPara.createIndex({"_id":1}, {"name":"_id_"});

// InFill
db.InFill.createIndex({"FillNo":1}, {"name":"FillNo_1","background":true});
db.InFill.createIndex({"FillTime":-1}, {"name":"FillTime_-1","background":true});
db.InFill.createIndex({"_id":1}, {"name":"_id_"});

// InOrder
db.InOrder.createIndex({"ClientOrderId":1}, {"name":"ClientOrderId_1","background":true});
db.InOrder.createIndex({"OrderTime":-1}, {"name":"OrderTime_-1","background":true});
db.InOrder.createIndex({"_id":1}, {"name":"_id_"});

// InPosition
db.InPosition.createIndex({"PositionId":1}, {"name":"PositionId_1","background":true});
db.InPosition.createIndex({"_id":1}, {"name":"_id_"});

// LiquidityProvider
db.LiquidityProvider.createIndex({"_id":1}, {"name":"_id_"});

// LpRoute
db.LpRoute.createIndex({"_id":1}, {"name":"_id_"});

// Market
db.Market.createIndex({"Market":1}, {"unique":true,"name":"Market_1","background":true});
db.Market.createIndex({"_id":1}, {"name":"_id_"});

// OtcQuotePara
db.OtcQuotePara.createIndex({"SymbolId":1}, {"unique":true,"name":"SymbolId_1","background":true});
db.OtcQuotePara.createIndex({"_id":1}, {"name":"_id_"});

// OutFill
db.OutFill.createIndex({"FillNo":1}, {"name":"FillNo_1","background":true});
db.OutFill.createIndex({"FillTime":-1}, {"name":"FillTime_-1","background":true});
db.OutFill.createIndex({"_id":1}, {"name":"_id_"});

// OutOrder
db.OutOrder.createIndex({"ClientOrderId":1}, {"name":"ClientOrderId_1","background":true});
db.OutOrder.createIndex({"OrderTime":-1}, {"name":"OrderTime_-1","background":true});
db.OutOrder.createIndex({"_id":1}, {"name":"_id_"});

// OutPosition
db.OutPosition.createIndex({"PositionId":1}, {"name":"PositionId_1","background":true});
db.OutPosition.createIndex({"_id":1}, {"name":"_id_"});

// StreamId
db.StreamId.createIndex({"_id":1}, {"name":"_id_"});

// Symbol
db.Symbol.createIndex({"_id":1}, {"name":"_id_"});


