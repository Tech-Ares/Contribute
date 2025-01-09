#!/bin/bash
yum install -y wget
wget -P /usr/local/src https://github.com/prometheus/prometheus/releases/download/v2.30.3/prometheus-2.30.3.linux-amd64.tar.gz
tar -zxvf /usr/local/src/prometheus-2.30.3.linux-amd64.tar.gz -C /usr/local
ln -s /usr/local/prometheus-2.30.3.linux-amd64 /usr/local/prometheus
mkdir -p /var/lib/prometheus
chown -R prometheus:prometheus /var/lib/prometheus
useradd -r prometheus
chown -R prometheus:prometheus /usr/local/prometheus
cat >/etc/systemd/system/prometheus.service <<EOF
[Unit]
Description=Prometheus
After=network.target

[Service]
Type=simple
User=prometheus
ExecStart=/usr/local/prometheus/prometheus --config.file=/usr/local/prometheus/prometheus.yml --storage.tsdb.path=/var/lib/prometheus
Restart=on-failure

[Install]
WantedBy=multi-user.target
EOF
systemctl start prometheus
systemctl enable prometheus
firewall-cmd --zone=public --add-port=9090/tcp --permanent
firewall-cmd --reload
