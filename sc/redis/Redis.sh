#!/bin/bash
yum update -y
yum install -y epel-release
yum install -y redis
systemctl enable redis
systemctl start redis
firewall-cmd --zone=public --add-port=6379/tcp --permanent
firewall-cmd --reload
