# DncDatabaseTest
A demonstration of integration tests with a database dependency. A detailed explanation can be found [on codeproject](https://www.codeproject.com/script/Reputation/List.aspx?mid=9921176).

If have not done it yet, [install the .NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) 

and the EF Core tooling:

````bash
dotnet tool install --global dotnet-ef
````

To run the tests here is how:

````bash
git clone https://github.com/ConnectingApps/DncInstallScripts
dotnet build
dotnet restore
dotnet test
dotnet ef database update --project ConnectingApps.Project
````

