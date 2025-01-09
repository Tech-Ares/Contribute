#!/bin/bash
yum update -y
yum install -y yum-utils
yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
yum install -y docker-ce docker-ce-cli containerd.io
systemctl start docker
systemctl enable docker
firewall-cmd --zone=public --add-port=2375/tcp --permanent
firewall-cmd --reload
