#!/bin/bash
yum install -y epel-release
yum install -y grafana
systemctl start grafana-server
systemctl enable grafana-server
firewall-cmd --zone=public --add-port=3000/tcp --permanent
firewall-cmd --reload
