#!/bin/bash
yum install -y java-1.8.0-openjdk-devel.x86_64
wget https://github.com/alibaba/Sentinel/releases/download/1.8.2/sentinel-dashboard-1.8.2.jar
nohup java -Dserver.port=8080 -Dcsp.sentinel.dashboard.server=localhost:8080 -Dproject.name=sentinel-dashboard -jar sentinel-dashboard-1.8.2.jar &
firewall-cmd --zone=public --add-port=8080/tcp --permanent
firewall-cmd --reload
