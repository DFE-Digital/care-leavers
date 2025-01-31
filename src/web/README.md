# .NET Readme

You will need:
- .NET IDE (Visual Studio, Rider, etc)
- Docker
- Node v20 installed

1. Clone the repository
2. Open the solution in your IDE
3. Run "yarn install" under "CareLeavers.Web". You can install Yarn with ```npm install -g yarn```
4. Ensure gulp is installed globally ```npm install -g gulp```
5. Run the "dev" gulp task to build the front end assets (Either from Rider or ```gulp dev``` in the command line).
6. Add contentful keys to your .NET user secrets
7. Run the web project