#!/bin/bash
yum update -y
yum install -y epel-release
yum install -y zabbix-server-mysql zabbix-frontend-mysql zabbix-apache-conf-scl zabbix-agent -y
systemctl start mysqld
systemctl enable mysqld
mysql_secure_installation
mysql -uroot -p
create database zabbix character set utf8 collate utf8_bin;
create user zabbix@localhost identified by 'password';
grant all privileges on zabbix.* to zabbix@localhost;
flush privileges;
systemctl start zabbix-server
systemctl enable zabbix-server
firewall-cmd --zone=public --add-port=80/tcp --permanent
firewall-cmd --zone=public --add-port=443/tcp --permanent
firewall-cmd --reload
