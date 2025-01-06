#!/bin/bash



echo "Killing Tomcat processes..."
kill -9 $(ps aux | grep '[t]omcat_v3.0_sl' | awk '{print $2}')
kill -9 $(ps aux | grep '[t]omcat_v3.0_trade' | awk '{print $2}')
kill -9 $(ps aux | grep '[t]omcat_v3.0_Pay' | awk '{print $2}')

sleep 10

echo "Starting Tomcat..."
sh /home/tomcat_v3.0_sl/bin/startup.sh
sh /home/tomcat_v3.0_trade/bin/startup.sh
sh /home/tomcat_v3.0_Pay/bin/startup.sh


echo "Tomcat restarted."