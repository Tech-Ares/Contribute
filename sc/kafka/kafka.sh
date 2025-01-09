#!/bin/bash
yum install -y java-1.8.0-openjdk-devel.x86_64
wget https://archive.apache.org/dist/kafka/2.8.0/kafka_2.13-2.8.0.tgz
tar -zxvf kafka_2.13-2.8.0.tgz -C /usr/local/
mv /usr/local/kafka_2.13-2.8.0 /usr/local/kafka
echo "export KAFKA_HOME=/usr/local/kafka
export PATH=\$PATH:\$KAFKA_HOME/bin" >> ~/.bashrc
source ~/.bashrc
nohup \$KAFKA_HOME/bin/zookeeper-server-start.sh \$KAFKA_HOME/config/zookeeper.properties &
nohup \$KAFKA_HOME/bin/kafka-server-start.sh \$KAFKA_HOME/config/server.properties &
firewall-cmd --zone=public --add-port=9092/tcp --permanent
firewall-cmd --zone=public --add-port=2181/tcp --permanent
firewall-cmd --reload
