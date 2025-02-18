#!/bin/bash
#先停止 再啟動

PROCESS_NAME=BaseIceBox
PID_FILE=/var/run/${PROCESS_NAME}.pid
CONF_NAME=routine-base.conf


if ! grep -q '@@@@@' "${PID_FILE}"; then
	echo "kill 進程"
    ps -ef |grep $CONF_NAME | grep -v grep |awk '{print $2}'|xargs kill -9
    ps -ef |grep $PROCESS_NAME | grep -v grep |awk '{print $2}'|xargs kill -9
	echo '@@@@@' > ${PID_FILE}
	sleep 10s
	su - iceuser -c 'nohup icegridnode --Ice.Config=/home/version3.0/iceconfig/'"$CONF_NAME"' 2>&1 &'
	echo -e "正在檢查服務是否正常啟動. . . . ."
	for i in {1..4}; do
		sleep 3s
		ps -ef | grep ${PROCESS_NAME} | grep -v "grep" |  awk '{ print $2 }' >${PID_FILE}
		if [ ! -s "${PID_FILE}" ];then
        	echo -e "启动失败：\e[1;31m$0\e[0m"
			echo '启动失败' > ${PID_FILE}
	        exit 1
    	fi
	done
	echo -e "啟動完成：\e[1;31m$0\e[0m"
	PID=$(cat ${PID_FILE})
	echo -e "服務進程：\e[1;31m$PID\e[0m"
else
	SCRIPT_RUN=`ps -ef|grep $0 |grep -v grep |awk '{ print $2 }' |wc -l`
	if [[ "$SCRIPT_RUN" -ge 3 ]];then
		echo -e "已經有同樣腳本正在執行中.....稍後再嘗試"
		ps -ef|grep $0 |grep -v grep 
	else
		: > $PID_FILE
		sh $0
	fi

fi
