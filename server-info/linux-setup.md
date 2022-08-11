# Linux (Ubuntu 20.04) Setup

### 1. Update linux:
1. `sudo apt update`
2. `sudo apt upgrade`
3. `sudo apt autoremove`

### 2. Install Node.js
1. `curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.1/install.sh | bash`
2. `source ~/.bashrc`
3. `nvm list-remote`
4. `nvm install v18.7.0` (or something else)

### 3. Install NPM & Yarn
1. `sudo apt install npm`
2. `npm install -g yarn`
3. Update NPM if needed (e.g. `npm install -g npm@8.17.0`)

### Install & setup PM2
1. `yarn global add pm2`
2. `sudo pm2 startup`
3. `pm2 link <pubKey> <priKey>` (found in [app.pm2.io](https://app.pm2.io/))
4. `pm2 install pm2-server-monit`

### Install .NET 6 SDK
1. `wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb`
2. `sudo dpkg -i packages-microsoft-prod.deb`
3. `rm packages-microsoft-prod.deb`
4. `sudo apt-get update`
5. `sudo apt-get install -y dotnet-sdk-6.0`

### Install MongoDB
1. `wget -qO - https://www.mongodb.org/static/pgp/server-6.0.asc | sudo apt-key add -`
2. `echo "deb [ arch=amd64,arm64 ] https://repo.mongodb.org/apt/ubuntu focal/mongodb-org/6.0 multiverse" | sudo tee /etc/apt/sources.list.d/mongodb-org-6.0.list`
3. `sudo apt-get update`
4. `sudo apt-get install -y mongodb-org`
5. `sudo service mongod start`

### Install Nginx & Adjust the Firewall
1. `sudo apt update`
2. `sudo apt install nginx`
3. `sudo ufw allow 'Nginx HTTP'`
4. `sudo ufw allow 'OpenSSH'`
5. `sudo ufw enable`

Other (Optional)
1. `apt install speedtest-cli`
