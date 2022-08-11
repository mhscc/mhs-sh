# Nginx Info

### Config
```
server {
  listen 80 default_server;
  listen [::]:80 default_server;
  server_name "";
  return 444;
}

server {
  listen 80;
  server_name mhs.sh;
  
  location = / {
    return 301 $scheme://app.mhs.sh;
  }
  
  location ^~ / {
    rewrite ^/(.*)$ /redirect?slug=$1 break;
    proxy_pass http://localhost:5000;
  }
}

server {
  listen 80;
  server_name api.mhs.sh;
  
  location ^~ /v1/ {
    rewrite ^/v1/(.*)$ /$1 break;
    proxy_pass http://localhost:5000;
  }
}

server {
  listen 80;
  server_name app.mhs.sh;
	
  location / {
    proxy_pass http://localhost:3000;
  }
}
```
