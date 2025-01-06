#!/bin/sh
cd /src
useradd kafka
useradd zk
wget 10.10.100.151:8888/113/zookeeper.tar
wget 10.10.100.151:8888/113/kafka.tar
tar xvf zookeeper.tar
tar xvf kafka.tar
mv zookeeper /usr/local
mv kafka /usr/local
chown -R zk:zk /usr/local/zookeeper
chown -R kafka:kafka /usr/local/kafka
echo "10.10.100.113   dev-kafka01-113     dev-kafka01-113" >> /etc/hosts
echo "10.10.100.114   dev-kafka02-114     dev-kafka02-114" >> /etc/hosts
echo "10.10.100.115   dev-kafka03-115     dev-kafka03-115" >> /etc/hosts
wget 10.10.100.151:8888/113/zk.service
wget 10.10.100.151:8888/113/kafka.service
mv *.service /etc/systemd/system
#broker.id=1
sed -i 's/host.name=10.10.100.123/host.name=10.10.100.113/g' /usr/local/kafka/config/server.properties
sed -i 's/PLAINTEXT:\/\/10.10.100.123:9092/PLAINTEXT:\/\/10.10.100.113:9092/g' /usr/local/kafka/config/server.properties
sed -i 's/10.10.100.123:2181,10.10.100.124:2181,10.10.100.125:2181/10.10.100.113:2181,10.10.100.114:2181,10.10.100.115:2181/g'  /usr/local/kafka/config/server.properties
sed -i 's/server.1=10.10.100.123/server.1=10.10.100.113/g' /usr/local/zookeeper/conf/zoo.cfg
sed -i 's/server.2=10.10.100.124/server.2=10.10.100.114/g' /usr/local/zookeeper/conf/zoo.cfg
sed -i 's/server.3=10.10.100.125/server.3=10.10.100.115/g' /usr/local/zookeeper/conf/zoo.cfg
mkdir -p /var/lib/zookeeper
chown zk:zk /var/lib/zookeeper
echo '1' > /var/lib/zookeeper/myid
#可通用 但是 kafka 三個地方需要修正 broker.id host.name  PLAINTEXT
#systemctl start zk
systemctl enable zk
#/usr/local/kafka/bin/kafka-server-start.sh /usr/local/kafka/config/server.properties &
