cat /etc/centos-release
rpm -qa | grep -i mariadb
rpm -e --nodeps mariadb-libs-5.5.68-1.el7.x86_64
rpm -qa | grep -i mysql
rm -rf /etc/my.cnf
rpm -ivh http://repo.mysql.com/mysql57-community-release-el7.rpm
ls /etc/yum.repos.d | grep mysql
yum list | grep mysql-community
rpm --import https://repo.mysql.com/RPM-GPG-KEY-mysql-2022
yum -y install mysql-community-client mysql-community-common mysql-community-devel mysql-community-libs mysql-community-libs-compat mysql-community-server mysql-community-test
mysqld --initialize --user=mysql --datadir=/var/lib/mysql

