#!/bin/bash
ulimit -n 65535
sed -i '1i\* soft nofile 65535' /etc/security/limits.conf
sed -i '1i\* hard nofile 65535' /etc/security/limits.conf
sed -i '1i\* soft nproc 65535' /etc/security/limits.conf
sed -i '1i\* hard nproc 65535' /etc/security/limits.conf
yum install yum-utils -y
yum install epel-release -y
yum install pcre pcre-devel -y
yum install zlib zlib-devel -y
yum install openssl openssl-devel -y
yum install htop vim ipset make unzip wget gperftools git net-tools zip sshpass rdate telnet lsof lrzsz iftop tcpdump mtr java-1.8.0-openjdk-devel -y
yum install redhat-rpm-config libxslt-devel gd-devel gperftools-devel -y
curl -L https://github.com/dundee/gdu/releases/latest/download/gdu_linux_amd64.tgz | tar xz
chmod +x gdu_linux_amd64
sudo mv gdu_linux_amd64 /usr/bin/gdu
yum update -y
systemctl stop firewalld
systemctl disable firewalld
rm -rf /root/install.sh