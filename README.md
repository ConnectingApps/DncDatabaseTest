# DncDatabaseTest
A demonstration of integration tests with a database dependency

To run the tests here is how:

````bash
git clone https://github.com/ConnectingApps/DncInstallScripts
dotnet build
dotnet restore
dotnet test
dotnet ef database update --project ConnectingApps.Project
````

If have not done yet, install dotnet ef first

````bash
dotnet tool install --global dotnet-ef
````