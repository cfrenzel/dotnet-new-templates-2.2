DotNetCore.Console.Template_2.2
=======

.NET Core 2.2 Console Application Template

Supports:
* Microsoft.Extensions.DependencyInjection
* Configuration
  * appsettings.json
  * secrets
  * environment variables
* Logging 
  * appsettings.json
  ```
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "System": "Warning",
      "Microsoft": "Warning",
      "DotNetCore.ConsoleApp" :  "Information"
    }
  ```
* EFCore / Migrations
