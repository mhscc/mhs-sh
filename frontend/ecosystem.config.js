module.exports = {
  apps: [
    {
      name: 'app.msh.sh',
      script: 'yarn',
      args: 'start',
      env_production: {
        NODE_ENV: 'production',
        PRIVATE_AUTH_BACKEND_URL: 'http://localhost:5001',
        PUBLIC_AUTH_BACKEND_URL: 'https://api.mhs.sh/v1/'
      }
    }
  ]
};
