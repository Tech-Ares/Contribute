#!/bin/bash

# Redis连接信息
redis_host="your_redis_host"
redis_port="6379"  # Redis默认端口
redis_password="your_redis_password"  # 如果有密码，否则设为空

# MySQL连接信息
mysql_host="your_mysql_host"
mysql_user="your_mysql_user"
mysql_password="your_mysql_password"
mysql_database="ts-trade"

# 从Redis获取值
redis_value=$(redis-cli -h $redis_host -p $redis_port -a $redis_password GET trade:global:sid)

if [ -n "$redis_value" ]; then
    # 同步到MySQL
    mysql_query="INSERT INTO trade_global_serial_id (GLOBAL_SERIALID) VALUES ('$redis_value');"
    mysql -h $mysql_host -u $mysql_user -p$mysql_password $mysql_database -e "$mysql_query"

    echo "已将Redis值 '$redis_value' 同步到MySQL."
else
    echo "Redis键不存在或没有值可同步。"
fi
