#!/bin/bash

sudo yum update -y
sudo yum install -y epel-release
sudo yum install -y nginx postgresql postgresql-server postgresql-contrib php php-fpm php-pgsql php-intl php-gd php-xml php-mbstring wget

sudo postgresql-setup initdb
sudo systemctl start postgresql
sudo systemctl enable postgresql

sudo -i -u postgres psql -c "CREATE USER mediawikiuser WITH PASSWORD 'erjgioerjgioerjgiojeroq;pgjioeqrjiogjqerojgoperjgrpoer';"
sudo -i -u postgres psql -c "CREATE DATABASE mediawikidb OWNER mediawikiuser;"

cd /var/www
sudo wget https://releases.wikimedia.org/mediawiki/1.41/mediawiki-1.41.1.tar.gz
sudo tar -xvzf mediawiki-1.41.1.tar.gz
sudo mv mediawiki-1.41.1 mediawiki
sudo chown -R nginx:nginx /var/www/mediawiki
sudo chmod -R 755 /var/www/mediawiki

sudo sed -i 's/user = apache/user = nginx/g' /etc/php-fpm.d/www.conf
sudo sed -i 's/group = apache/group = nginx/g' /etc/php-fpm.d/www.conf
sudo systemctl start php-fpm
sudo systemctl enable php-fpm

cat <<EOL | sudo tee /etc/nginx/conf.d/mediawiki.conf
server {
    listen 80;
    server_name dftitoc;
    root /var/www/mediawiki;
    index index.php index.html index.htm;
    location / {
        try_files \$uri \$uri/ =404;
    }
    location ~ \.php$ {
        include fastcgi_params;
        fastcgi_pass unix:/run/php-fpm/www.sock;
        fastcgi_param SCRIPT_FILENAME \$document_root\$fastcgi_script_name;
        include fastcgi_params;
    }
    location ~* \.(js|css|png|jpg|jpeg|gif|ico|svg)$ {
        try_files \$uri /index.php;
    }
}
EOL

sudo systemctl start nginx
sudo systemctl enable nginx

sudo yum install -y certbot python2-certbot-nginx
sudo certbot --nginx -d dftitoc.com
