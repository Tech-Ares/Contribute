#!/bin/bash
cat <<EOF > /etc/yum.repos.d/mongodb-org-4.4.repo
[mongodb-org-4.4]
name=MongoDB Repository
baseurl=https://repo.mongodb.org/yum/redhat/7Server/mongodb-org/4.4/x86_64/
gpgcheck=1
enabled=1
gpgkey=https://www.mongodb.org/static/pgp/server-4.4.asc
EOF
yum install -y mongodb-org
systemctl start mongod
systemctl enable mongod
firewall-cmd --zone=public --add-port=27017/tcp --permanent
firewall-cmd --reload
