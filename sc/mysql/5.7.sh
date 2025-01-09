#!/bin/bash
wget https://dev.mysql.com/get/mysql57-community-release-el7-11.noarch.rpm
rpm -ivh mysql57-community-release-el7-11.noarch.rpm
yum install -y mysql-server
systemctl start mysqld
systemctl enable mysqld
grep 'temporary password' /var/log/mysqld.log
mysql_secure_installation
firewall-cmd --zone=public --add-port=3306/tcp --permanent
firewall-cmd --reload
