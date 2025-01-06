#!/bin/bash
#yes | cp -rf /opt/Ice-3.6.3/lib64/* /usr/lib64/
#yes | cp -rf /opt/Ice-3.6.3/include/* /usr/include/
#yes | cp -rf /opt/Ice-3.6.3/bin/* /usr/bin/
sh /home/version3.0/scripts/00_start-routine-base.sh
sh /home/version3.0/scripts/01_start-routine-config.sh
sh /home/version3.0/scripts/02_start-routine-rbac.sh
sh /home/version3.0/scripts/03_start-routine-support.sh
sh /home/version3.0/scripts/start-gateway-config.sh
sh /home/version3.0/scripts/start-gateway-rbac.sh
sh /home/version3.0/scripts/start-gateway-support.sh
sh /home/version3.0/scripts/start-routine-broker-commission.sh
sh /home/version3.0/scripts/start-routine-campay.sh
sh /home/version3.0/scripts/start-routine-cam.sh
sh /home/version3.0/scripts/start-routine-crm.sh
sh /home/version3.0/scripts/start-routine-fsr.sh
sh /home/version3.0/scripts/start-routine-trade.sh
