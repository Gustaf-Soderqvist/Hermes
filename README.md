# Hermes

A guestbook

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for developments.

### Installing

A step by step series how to get a development env running


Clone or download the project 
```
https://github.com/Gustaf-Soderqvist/Hermes.git
```

Run npm install
```
npm install
```

Build
```
Build soultion
```

- Create DB -
```

In Visual Studio, you can use the Package Manager Console to apply pending migrations to the database:

Run "Update-Database"
Alternatively, you can apply pending migrations from a command prompt at your project directory:

> dotnet ef database update


* If you have problem with creating the DB. Change cs in appsettings.json to:
connectionString="Server=(localdb)\mssqllocaldb;Database=Hermes;Trusted_Connection=True;" />
```

- Start the application -
```
dotnet run

Navigate to :
https://localhost:5001
or
http://localhost:5000
```




## Built With

* .NET Core 3.1 https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-1
* Entity Framework Core https://docs.microsoft.com/en-us/ef/core/
* ASP.NET Core SignalR https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-3.1
* Angular 8



## Authors

* **Gustaf Söderqvist** - *Software developer* - https://github.com/Gustaf-Soderqvist
