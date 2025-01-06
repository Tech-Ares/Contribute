#!/bin/bash

# Redis連接信息
redis_host="your_redis_host"
redis_port="6379"  # Redis默認端口
redis_password="your_redis_password"  # 如果有密碼，否則設為空

# MySQL連接信息
mysql_host="your_mysql_host"
mysql_user="your_mysql_user"
mysql_password="your_mysql_password"
mysql_database="ts-trade"

# 從Redis獲取值
redis_value=$(redis-cli -h $redis_host -p $redis_port -a $redis_password GET trade:global:sid)

if [ -n "$redis_value" ]; then
    # 同步到MySQL
    mysql_query="INSERT INTO trade_global_serial_id (GLOBAL_SERIALID) VALUES ('$redis_value');"
    mysql -h $mysql_host -u $mysql_user -p$mysql_password $mysql_database -e "$mysql_query"

    echo "已將Redis值 '$redis_value' 同步到MySQL."
else
    echo "Redis鍵不存在或沒有值可同步。"
fi
