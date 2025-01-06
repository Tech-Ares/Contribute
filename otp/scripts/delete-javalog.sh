#!/bin/bash
find /home/version3.0/ -type d -name logs > /src/javalog

a=`cat /src/javalog`

find $a -name "*.log.gz" -type f -mtime +3 -exec rm -rf {} \;
find $a -name "202*" -type d -mtime +3 -exec rm -rf {} \;
