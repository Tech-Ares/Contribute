#!/bin/bash
apt-get update
apt-get install -y docker.io
apt-get install -y docker-compose
mkdir -p /opt/zabbix
cd /opt/zabbix
cat <<EOF > docker-compose.yml
version: '3'

services:
  zabbix-server:
    image: zabbix/zabbix-server-mysql:alpine-5.0-latest
    container_name: zabbix-server
    ports:
      - "10051:10051"
    environment:
      DB_SERVER_HOST: "zabbix-db"
      MYSQL_USER: "zabbix"
      MYSQL_PASSWORD: "zabbix_password"
      MYSQL_DATABASE: "zabbix"
    depends_on:
      - zabbix-db

  zabbix-web:
    image: zabbix/zabbix-web-apache-mysql:alpine-5.0-latest
    container_name: zabbix-web
    ports:
      - "8080:8080"
    environment:
      DB_SERVER_HOST: "zabbix-db"
      MYSQL_USER: "zabbix"
      MYSQL_PASSWORD: "zabbix_password"
      MYSQL_DATABASE: "zabbix"
    depends_on:
      - zabbix-server

  zabbix-agent:
    image: zabbix/zabbix-agent:alpine-5.0-latest
    container_name: zabbix-agent
    environment:
      ZBX_HOSTNAME: "zabbix-agent"
      ZBX_SERVER_HOST: "zabbix-server"
    depends_on:
      - zabbix-server

  zabbix-db:
    image: mysql:5.7
    container_name: zabbix-db
    volumes:
      - zabbix-db-data:/var/lib/mysql
    environment:
      MYSQL_DATABASE: "zabbix"
      MYSQL_USER: "zabbix"
      MYSQL_PASSWORD: "zabbix_password"
      MYSQL_ROOT_PASSWORD: "root_password"

volumes:
  zabbix-db-data:
    driver: local
EOF
docker-compose up -d
