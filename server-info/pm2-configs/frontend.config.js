module.exports = {
  apps: [
    {
      name: 'app.mhs.sh',
      script: 'yarn',
      args: 'start',
      env_production: {
        NODE_ENV: 'production',
        PUBLIC_BACKEND_URL: 'https://api.mhs.sh/v1',
        CAPTCHA_PUB_KEY: '6LdnTWchAAAAAC0Av7BdUg9bbjvHaUcjjZONn5Uu'
      }
    }
  ]
};
