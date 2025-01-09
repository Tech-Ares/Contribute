#!/bin/bash
yum install -y java-1.8.0-openjdk-devel.x86_64
wget https://downloads.apache.org/storm/apache-storm-2.2.0/apache-storm-2.2.0.tar.gz
tar -zxvf apache-storm-2.2.0.tar.gz
mv apache-storm-2.2.0 /usr/local/storm
echo "export STORM_HOME=/usr/local/storm
export PATH=\$PATH:\$STORM_HOME/bin" >> ~/.bashrc
source ~/.bashrc
