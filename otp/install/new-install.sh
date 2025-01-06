
#!/bin/sh
mkdir -p /src
cd /src
ulimit -n 65535
sed -i '1i\* soft nofile 65535' /etc/security/limits.conf
sed -i '1i\* hard nofile 65535' /etc/security/limits.conf
yum install epel-release -y
yum install gcc gcc-c++ -y
yum install pcre pcre-devel -y
yum install zlib zlib-devel -y
yum install openssl openssl-devel -y
yum install lua lua-devel libxml2 libxml2-devel -y
yum install epel-release perl-devel perl-ExtUtils-Embed perl-XML-LibXML perl-XML-LibXSLT libxslt-python libXpm-devel -y
yum install htop vim ipset make unzip wget gperftools git net-tools zip rdate telnet lsof lrzsz iftop tcpdump java-1.8.0-openjdk-devel -y
yum install GeoIP GeoIP-devel GeoIP-data -y
yum install redhat-rpm-config libxslt-devel gd-devel gperftools-devel -y
curl -L https://github.com/dundee/gdu/releases/latest/download/gdu_linux_amd64.tgz | tar xz
chmod +x gdu_linux_amd64
sudo mv gdu_linux_amd64 /usr/bin/gdu
yum update -y
systemctl stop firewalld
systemctl disable firewalld
wget 10.10.100.151:8888/jdk1.8.0_281.tar
tar zxf jdk1.8.0_281.tar
mkdir /usr/lib/jvm
mv /src/jdk1.8.0_281 /usr/lib/jvm/
echo 'export JAVA_HOME=/usr/lib/jvm/jdk1.8.0_281' >> /etc/profile
echo 'export CLASSPATH=.:$JAVA_HOME/jre/lib/rt.jar:$JAVA_HOME/lib/dt.jar:$JAVA_HOME/lib/tools.jar' >> /etc/profile
echo 'export PATH=$JAVA_HOME/bin:$PATH' >> /etc/profile
source /etc/profile
