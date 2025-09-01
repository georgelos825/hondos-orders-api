HondosOrders API - README

0) What is this?
- .NET 8 Web API with one endpoint:
  GET /get_data?date=YYYY-MM-DD → returns orders for that date.
- Reads from SQL Server (view/table).
- Uses Dapper (fast, lightweight).
- Has Swagger UI for testing.

1) Run locally (developer PC)

A. Install once
1. Install .NET 8 SDK (if not already).
2. (Optional) Trust the local HTTPS dev cert:
   Windows: run 'dotnet dev-certs https --trust'

B. Configure DB
Edit HondosApplication/appsettings.Development.json:
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=HONDOSCENTER;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "UseExerciseTable": true
}

C. Start the API
dotnet restore
dotnet build
dotnet run --project HondosApplication/HondosOrders.Api.csproj

D. Test it
- Open Swagger: https://localhost:****/swagger
- Or call:
  https://localhost:****/get_data?date=2025-07-15

2) Build a publish folder (for server)

dotnet publish HondosApplication/HondosOrders.Api.csproj -c Release -o ./publish

This creates a publish/ folder with DLLs + web.config.

3) Deploy to IIS on Windows Server

A. One-time server setup
1. Install IIS (Web Server role).
2. Install .NET 8 Hosting Bundle (official Microsoft installer).

B. Copy publish folder
Copy publish/ to server, e.g. C:\inetpub\HondosOrders

C. Create site in IIS
1. Open IIS Manager.
2. Right click Sites → Add Website.
3. Site name: HondosOrders
4. Physical path: C:\inetpub\HondosOrders
5. Binding: HTTP, port 8080 (or 80 if free).
6. OK.

D. App Pool settings
- .NET CLR version: No Managed Code
- Pipeline mode: Integrated

E. Configure connection string
Option 1: appsettings.Production.json
{
  "ConnectionStrings": {
    "Default": "Server=SQLPROD;Database=HONDOSCENTER;User Id=svc_reader;Password=***;TrustServerCertificate=True;"
  },
  "UseExerciseTable": false
}

Option 2: Environment Variables
ASPNETCORE_ENVIRONMENT = Production
ConnectionStrings__Default = Server=SQLPROD;Database=HONDOSCENTER;User Id=svc_reader;Password=***;TrustServerCertificate=True;
UseExerciseTable = false

F. Folder permissions
Give IIS AppPool\HondosOrders user Read & Execute on C:\inetpub\HondosOrders

G. SQL permissions
Read-only user, grant SELECT only on production view.

H. Start site & test
http://<server-ip>:8080/get_data?date=2025-07-15

4) Switch Exercise / Production
Change UseExerciseTable in config or env var.

5) Logs & troubleshooting
Enable stdoutLogEnabled in web.config only temporarily.
Consider Serilog later.

6) Why Swagger uses HTTPS?
- launchSettings.json has https URL by default.
- Program.cs uses app.UseHttpsRedirection();
- IIS binding may redirect.

To use HTTP only:
- Remove https:// from launchSettings.json
- Or comment out app.UseHttpsRedirection()
- Or remove HTTPS binding in IIS

7) API contract
GET /get_data?date=YYYY-MM-DD
Success 200:
{ "data": [ ... ] }
Error 400: invalid or missing date

8) FAQ for review
- Why Dapper? Fast, no overhead.
- Why snake_case? Matches consumers.
- Is it safe? Yes, parameterized SQL, read-only user.
- How to change source? Config/env var, recycle app pool.
- Improvements? Scoped lifetimes, health checks, Serilog, paging, index on date.

9) Health check
GET /get_data?date=2025-01-01 → returns JSON (data can be empty).
