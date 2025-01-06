#!/bin/sh  


TGGRP="-781121203"
TGBOT="1787191786:AAEWq-_hSejTWMVww-Y-tJbH97TqERSgp1k"
TGGRP2="-1001755163237"
TGBOT2="5982596685:AAEd1aa0LGKp_cn9IRlmVHKjneP2xd5Yyok"
ENV="dev"
SERVERIP="10.10.100.116"

routine_base() {
    ICEPID=`ps -ef | grep icegridnode |grep "routine-base.conf" | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$ICEPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-base.conf 進程消失 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl -s -X POST "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"  > /dev/null 2>&1
        sh /home/version3.0/scripts/00_start-routine-base.sh
    fi
    BOXPID=`ps -ef | grep BaseIceBox | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$BOXPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0ABaseIceBox 進程消失 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"
        sh /home/version3.0/scripts/00_start-routine-base.sh
    fi
    NEWPID=`ps -ef | grep icegridnode |grep "routine-base.conf" | grep -v "grep" |  awk '{ print $2 }' |wc -l `
    if [ "$NEWPID" -ge 2 ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-base.conf 發現多個進程 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        sh /home/version3.0/scripts/00_start-routine-base.sh
    fi    
}

routine_config() {
    ICEPID=`ps -ef | grep icegridnode | grep "routine-config.conf" | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$ICEPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-config.conf 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl -s -X POST "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"  > /dev/null 2>&1
        sh /home/version3.0/scripts/01_start-routine-config.sh
    fi    
    BOXPID=`ps -ef | grep ConfigIceBox | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$BOXPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0AConfigIceBox 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"
        sh /home/version3.0/scripts/01_start-routine-config.sh
    fi
    NEWPID=`ps -ef | grep icegridnode |grep "routine-config.conf" | grep -v "grep" |  awk '{ print $2 }' |wc -l `
    if [ "$NEWPID" -ge 2 ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-config.conf 發現多個進程 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        sh /home/version3.0/scripts/01_start-routine-config.sh
    fi        
}		

routine_rabc() {
    ICEPID=`ps -ef | grep icegridnode | grep "routine-rbac.conf" | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$ICEPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-rbac.conf 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl -s -X POST "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"  > /dev/null 2>&1
        sh /home/version3.0/scripts/02_start-routine-rbac.sh
    fi    
    BOXPID=`ps -ef | grep RbacIceBox | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$BOXPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0ARbacIceBox 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"
        sh /home/version3.0/scripts/02_start-routine-rbac.sh
    fi
    NEWPID=`ps -ef | grep icegridnode |grep "routine-rbac.conf" | grep -v "grep" |  awk '{ print $2 }' |wc -l `
    if [ "$NEWPID" -ge 2 ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-rbac.conf 發現多個進程 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        sh /home/version3.0/scripts/02_start-routine-rbac.sh
    fi          
}		

routine_support() {
    ICEPID=`ps -ef | grep icegridnode | grep "routine-support.conf" | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$ICEPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-support.conf 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl -s -X POST "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"  > /dev/null 2>&1
        sh /home/version3.0/scripts/03_start-routine-support.sh
    fi    
    BOXPID=`ps -ef | grep SupportIceBox | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$BOXPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0ASupportIceBox 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"
        sh /home/version3.0/scripts/03_start-routine-support.sh
    fi
    NEWPID=`ps -ef | grep icegridnode |grep "routine-support.conf" | grep -v "grep" |  awk '{ print $2 }' |wc -l `
    if [ "$NEWPID" -ge 2 ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-support.conf 發現多個進程 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        sh /home/version3.0/scripts/03_start-routine-support.sh
    fi         
}	

routine_broker_commission() {
    ICEPID=`ps -ef | grep icegridnode | grep "routine-broker-commission.conf" | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$ICEPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-broker-commission.conf 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl -s -X POST "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"  > /dev/null 2>&1
        sh /home/version3.0/scripts/start-routine-broker-commission.sh
    fi    
    BOXPID=`ps -ef | grep BrokerCommissionIceBox | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$BOXPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0ABrokerCommissionIceBox 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"
        sh /home/version3.0/scripts/start-routine-broker-commission.sh
    fi
    NEWPID=`ps -ef | grep icegridnode |grep "routine-broker-commission.conf" | grep -v "grep" |  awk '{ print $2 }' |wc -l `
    if [ "$NEWPID" -ge 2 ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-broker-commission.conf 發現多個進程 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        sh /home/version3.0/scripts/start-routine-broker-commission.sh
    fi            
}	

routine_campay() {
    ICEPID=`ps -ef | grep icegridnode | grep "routine-campay.conf"| grep -v "grep"  |  awk '{ print $2 }'`
    if [ -z "$ICEPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-campay.conf 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl -s -X POST "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"  > /dev/null 2>&1
        sh /home/version3.0/scripts/start-routine-campay.sh
    fi    
    BOXPID=`ps -ef | grep CamExtIceBox | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$BOXPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0ACamExtIceBox 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"
        sh /home/version3.0/scripts/start-routine-campay.sh
    fi
    NEWPID=`ps -ef | grep icegridnode |grep "routine-campay.conf" | grep -v "grep" |  awk '{ print $2 }' |wc -l `
    if [ "$NEWPID" -ge 2 ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-campay.conf 發現多個進程 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        sh /home/version3.0/scripts/start-routine-campay.sh
    fi          
}	

routine_cam() {
    ICEPID=`ps -ef | grep icegridnode | grep "routine-cam.conf" | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$ICEPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-cam.conf 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl -s -X POST "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"  > /dev/null 2>&1
        sh /home/version3.0/scripts/start-routine-cam.sh
    fi    
    BOXPID=`ps -ef | grep CamIceBox | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$BOXPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0ACamIceBox 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"
        sh /home/version3.0/scripts/start-routine-cam.sh
    fi
    NEWPID=`ps -ef | grep icegridnode |grep "routine-cam.conf" | grep -v "grep" |  awk '{ print $2 }' |wc -l `
    if [ "$NEWPID" -ge 2 ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-cam.conf 發現多個進程 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        sh /home/version3.0/scripts/start-routine-cam.sh
    fi              
}	

routine_crm() {
    ICEPID=`ps -ef | grep icegridnode | grep "routine-crm.conf" | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$ICEPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-crm.conf 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl -s -X POST "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"  > /dev/null 2>&1
        sh /home/version3.0/scripts/start-routine-crm.sh
    fi    
    BOXPID=`ps -ef | grep CrmIceBox | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$BOXPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0ACrmIceBox 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"
        sh /home/version3.0/scripts/start-routine-crm.sh
    fi
    NEWPID=`ps -ef | grep icegridnode |grep "routine-crm.conf" | grep -v "grep" |  awk '{ print $2 }' |wc -l `
    if [ "$NEWPID" -ge 2 ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-crm.conf 發現多個進程 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        sh /home/version3.0/scripts/start-routine-crm.sh
    fi               
}

routine_fsr() {
    ICEPID=`ps -ef | grep icegridnode | grep "routine-fsr.conf" | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$ICEPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-fsr.conf 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl -s -X POST "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"  > /dev/null 2>&1
        sh /home/version3.0/scripts/start-routine-fsr.sh
    fi    
    BOXPID=`ps -ef | grep FsrIceBox | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$BOXPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0AFsrIceBox 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"
        sh /home/version3.0/scripts/start-routine-fsr.sh
    fi
    NEWPID=`ps -ef | grep icegridnode |grep "routine-fsr.conf" | grep -v "grep" |  awk '{ print $2 }' |wc -l `
    if [ "$NEWPID" -ge 2 ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-fsr.conf 發現多個進程 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        sh /home/version3.0/scripts/start-routine-fsr.sh
    fi                   
}	

routine_trade() {
    ICEPID=`ps -ef | grep icegridnode | grep "routine-trade.conf"  | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$ICEPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-trade.conf 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl -s -X POST "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"  > /dev/null 2>&1
        sh /home/version3.0/scripts/start-routine-trade.sh
    fi    
    BOXPID=`ps -ef | grep TradeIceBox | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$BOXPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0ATradeIceBox 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"
        sh /home/version3.0/scripts/start-routine-trade.sh
    fi
    NEWPID=`ps -ef | grep icegridnode |grep "routine-trade.conf" | grep -v "grep" |  awk '{ print $2 }' |wc -l `
    if [ "$NEWPID" -ge 2 ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Aroutine-trade.conf 發現多個進程 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        sh /home/version3.0/scripts/start-routine-trade.sh
    fi          
}

gateway_config() {
    ICEPID=`ps -ef | grep icegridnode | grep "gateway-config.conf" | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$ICEPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Agateway-config.conf 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl -s -X POST "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"  > /dev/null 2>&1
        sh /home/version3.0/scripts/start-gateway-config.sh
    fi
    BOXPID=`ps -ef | grep configGatewayNode1 | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$BOXPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0AconfigGatewayNode1 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"
        sh /home/version3.0/scripts/start-gateway-config.sh
    fi
    NEWPID=`ps -ef | grep icegridnode |grep "gateway-config.conf" | grep -v "grep" |  awk '{ print $2 }' |wc -l `
    if [ "$NEWPID" -ge 2 ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Agateway-config.conf 發現多個進程 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        sh /home/version3.0/scripts/start-gateway-config.sh
    fi        
}

gateway_rbac() {
    ICEPID=`ps -ef | grep icegridnode | grep "gateway-rbac.conf" | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$ICEPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Agateway-rbac.conf 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl -s -X POST "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"  > /dev/null 2>&1
        sh /home/version3.0/scripts/start-gateway-rbac.sh
    fi    
    BOXPID=`ps -ef | grep rbacGatewayNode1 | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$BOXPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0ArbacGatewayNode1 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"
        sh /home/version3.0/scripts/start-gateway-rbac.sh
    fi
    NEWPID=`ps -ef | grep icegridnode |grep "gateway-rbac.conf" | grep -v "grep" |  awk '{ print $2 }' |wc -l `
    if [ "$NEWPID" -ge 2 ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Agateway-rbac.conf 發現多個進程 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        sh /home/version3.0/scripts/start-gateway-rbac.sh
    fi        
}

gateway_support() {
    ICEPID=`ps -ef | grep icegridnode | grep "gateway-support.conf" | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$ICEPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Agateway-support.conf 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl -s -X POST "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"  > /dev/null 2>&1
        sh /home/version3.0/scripts/start-gateway-support.sh
    fi    
    BOXPID=`ps -ef | grep supportGatewayNode1 | grep -v "grep" |  awk '{ print $2 }'`
    if [ -z "$BOXPID" ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0AsupportGatewayNode1 進程消失 執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        curl "https://api.telegram.org/bot$TGBOT2/sendMessage?chat_id=$TGGRP2&text=$EVENT"
        sh /home/version3.0/scripts/start-gateway-support.sh
    fi
    NEWPID=`ps -ef | grep icegridnode |grep "gateway-support.conf" | grep -v "grep" |  awk '{ print $2 }' |wc -l `
    if [ "$NEWPID" -ge 2 ];then
        EVENT="守護進程腳本通知: %0A環境: $ENV %0A發出告警位置: $SERVERIP %0Agateway-support.conf 發現多個進程 已執行重啟腳本"
        curl "https://api.telegram.org/bot$TGBOT/sendMessage?chat_id=$TGGRP&text=$EVENT"
        sh /home/version3.0/scripts/start-gateway-support.sh
    fi       
}
routine_base
routine_config
routine_rabc
routine_support
routine_broker_commission
routine_campay
routine_cam
routine_crm
routine_fsr
routine_trade
gateway_config
gateway_rbac
gateway_support
