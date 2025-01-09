#!/bin/bash
yum install -y git gcc gcc-c++ make cmake wget unzip
wget https://github.com/happyfish100/libfastcommon/archive/V1.0.42.tar.gz
tar -zxvf V1.0.42.tar.gz
cd libfastcommon-1.0.42
./make.sh
./make.sh install
wget https://github.com/happyfish100/fastdfs/archive/V5.11.tar.gz
tar -zxvf V5.11.tar.gz
cd fastdfs-5.11
./make.sh
./make.sh install
cp /etc/fdfs/tracker.conf.sample /etc/fdfs/tracker.conf
cp /etc/fdfs/storage.conf.sample /etc/fdfs/storage.conf
cp /etc/fdfs/client.conf.sample /etc/fdfs/client.conf
firewall-cmd --zone=public --add-port=22122/tcp --permanent
firewall-cmd --zone=public --add-port=23000/tcp --permanent
firewall-cmd --zone=public --add-port=8080/tcp --permanent
firewall-cmd --reload
