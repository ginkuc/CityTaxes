# CityTaxes

The CityTaxes application relies on having 5000 port available on your machine, having .Net Core 2.2 SDK installed, and a local mssql development server (specified in /CityTaxes/appsettings.json).

1. clone the repository
2. navigate to /CityTaxes and run "dotnet run ./CityTaxes.csproj" in a command line - this will compile and run the app and create and initialize the database.
3. navigate to /CityTaxesClient and run "dotnet run ./CityTaxesClient.csproj" in a command line - this will compile and run the client application which will insert initial taxes and query scheduled taxes for specific dates.

Additional functionality can be observed from Swagger portal(http://localhost:5000) - like CRUD for tax records and scheduled tax query for a municipality and date.

Application doesn't have "Import from file" functionality and it is not configured to be deployed as a Windows Service.