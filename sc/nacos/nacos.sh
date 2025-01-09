#!/bin/bash
yum install -y java-1.8.0-openjdk-devel.x86_64
wget https://github.com/alibaba/nacos/releases/download/2.0.3/nacos-server-2.0.3.tar.gz
tar -zxvf nacos-server-2.0.3.tar.gz
cd nacos
echo "spring.datasource.platform=mysql
db.num=1
db.url.0=jdbc:mysql://localhost:3306/nacos?characterEncoding=utf8&connectTimeout=1000&socketTimeout=3000&autoReconnect=true
db.user.0=your_db_username
db.password.0=your_db_password" > conf/application.properties
cd bin
sh startup.sh -m standalone
firewall-cmd --zone=public --add-port=8848/tcp --permanent
firewall-cmd --reload
