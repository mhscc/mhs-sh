module.exports = {
  apps: [
    {
      name: 'mhs.sh',
      script: 'dotnet',
      args: 'Backend.dll --urls=http://localhost:5000'
    }
  ]
};
