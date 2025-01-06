#!/bin/bash
#先停止 再啟動

PROCESS_NAME=supportGatewayNode1
PID_FILE=/var/run/${PROCESS_NAME}.pid

if ! grep -q '@@@@@' "${PID_FILE}"; then
	echo "kill 進程"
    ps -ef |grep gateway-support.conf | grep -v grep |awk '{print $2}'| xargs kill -9
    ps -ef |grep supportGatewayNode1 | grep -v grep |awk '{print $2}'| xargs kill -9
	echo '@@@@@' > ${PID_FILE}
	sleep 10s
	su - iceuser -c 'nohup icegridnode --Ice.Config=/home/version3.0/iceconfig/gateway-support.conf 2>&1 &'
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
fi
