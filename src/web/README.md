# .NET Readme

You will need:
- .NET IDE (Visual Studio, Rider, etc)
- Docker
- Node v20 installed

1. Clone the repository
2. Open the solution in your IDE
3. Run the frontend build system as instructed in the Node [README](./CareLeavers.Web.Node/README.md)
4. Add contentful keys to your .NET user secrets
5. If using Redis as a distributed cache, ensure you have set the connection string (including short timeout values), eg: `redis-host:6379,ConnectTimeout=250,AsyncTimeout=250,SyncTimeout=250`
6. Run the web project