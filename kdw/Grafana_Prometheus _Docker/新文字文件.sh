docker pull prom/prometheus
docker run -d --name  prom/prometheus
docker cp -a prometheus:/etc/prometheus/ $PWD/prometheus
docker rm -f prom
docker run -d --name prom -p 9090:9090 -v $PWD/prometheus:/etc/prometheus prom/prometheus

#firewall-cmd --zone=public --add-port=9090/tcp --permanent
#firewall-cmd --reload
http://192.168.1.113:9090

---


docker pull prom/node-exporter
docker run -d --name node-exporter -p 9100:9100 -v "/proc:/host/proc:ro" -v "/sys:/host/sys:ro" -v "/:/rootfs:ro " --net="host" prom/node-exporter

#firewall-cmd --zone=public --add-port=9100/tcp --permanent
#firewall-cmd --reload
http://192.168.1.113:9100/metrics 
vim prometheus/prometheus.yml

```
scrape_configs:
  # The job name is added as a label `job=<job_name>` to any timeseries scraped from this config.
  - job_name: 'prometheus'
 
    # metrics_path defaults to '/metrics'
    # scheme defaults to 'http'.
 
    static_configs:
    - targets: ['192.168.1.113:9090']
      labels:
          instance: prometheus
 
  - job_name: 'centos_1'
    static_configs:
    - targets: ['192.168.1.113:9100']
      labels:
         instance: centos_1
```
docker restart prom
http://192.168.1.113:9090/targets

---

docker pull grafana/grafana
mkdir grafana
chmod 777 -R ./grafana/
docker run -d --name=grafana -p 3000:3000 -v $PWD/grafana:/var/lib/grafana grafana/grafana
#firewall-cmd --zone=public --add-port=3000/tcp --permanent
#firewall-cmd --reload
http://192.168.1.113:3000
admin/admin

---

docker pull google/cadvisor

docker run -d \
--name=cadvisor \
--volume=/:/rootfs:ro \
--volume=/var/run:/var/run:rw \
--volume=/sys:/sys:ro \
--volume=/var/lib/docker/:/var/lib/docker:ro \
--volume=/cgroup:/cgroup:ro \
--publish=9595:8080 \
--detach=true \
--privileged=true \
google/cadvisor

#firewall-cmd --zone=public --add-port=9595/tcp --permanent
#firewall-cmd --reload
http://192.168.1.113:9595/