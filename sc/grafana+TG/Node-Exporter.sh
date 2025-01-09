#!/bin/bash
useradd -M -r -s /bin/false nodeusr
wget https://github.com/prometheus/node_exporter/releases/download/v1.2.2/node_exporter-1.2.2.linux-amd64.tar.gz
tar -zxvf node_exporter-1.2.2.linux-amd64.tar.gz -C /usr/local/
ln -s /usr/local/node_exporter-1.2.2.linux-amd64 /usr/local/node_exporter
chown -R nodeusr:nodeusr /usr/local/node_exporter-1.2.2.linux-amd64
cat >/etc/systemd/system/node_exporter.service <<EOF
[Unit]
Description=Node Exporter
After=network.target

[Service]
Type=simple
User=nodeusr
ExecStart=/usr/local/node_exporter/node_exporter
Restart=on-failure

[Install]
WantedBy=multi-user.target
EOF
systemctl daemon-reload
systemctl start node_exporter
systemctl enable node_exporter
firewall-cmd --zone=public --add-port=9100/tcp --permanent
firewall-cmd --reload
