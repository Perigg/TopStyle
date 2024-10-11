TopStyle API
TopStyle API är en modern webbapplikation byggd med ASP.NET Core och Entity Framework Core. Applikationen erbjuder API-tjänster för att hantera produkter, användare och beställningar, och använder JWT-autentisering för säker åtkomst.

Funktioner
Autentisering med JWT: Säker API-åtkomst genom JSON Web Tokens (JWT) med stöd för användarregistrering och inloggning.
ASP.NET Core Identity: Hanterar användarregistrering, inloggning och rollbaserad åtkomstkontroll.
Hantera produkter: Skapa, uppdatera, ta bort och hämta produktinformation via ProductController.
Beställningshantering: Skapa och hämta beställningar via OrderController.
Användarhantering: Registrera nya användare, logga in, och hantera användardata via UserController.
Swagger-dokumentation: Automatiskt genererad API-dokumentation med Swagger för enkel testning och åtkomst till API
.
Databasstöd med Entity Framework Core: Använder SQL Server för lagring av data med migrationshantering.
AutoMapper: Smidig mappning mellan entiteter och DTOs för att hantera dataöverföring på ett effektivt sätt.
Dependency Injection: Använder DI för att organisera applikationslogik och underlätta testning och skalbarhet.
Teknologi och verktyg
ASP.NET Core: Backend-ramverket för API
.
Entity Framework Core: ORM som hanterar databasoperationer.
JWT (JSON Web Tokens): För autentisering och säkerhet.
ASP.NET Core Identity: För användarhantering och autentisering.
Swagger: För att generera och dokumentera API
.
AutoMapper: För att mappa mellan entiteter och DTOs.
MSSQL: Databas som används för lagring.
Installation
Krav
.NET 8.0 SDK eller senare
MSSQL Server (eller annan kompatibel SQL-databas)
Visual Studio eller annan IDE som stödjer .NET-projekt
Steg för att komma igång
Klona projektet:

bash
Kopiera kod
git clone https://github.com/your-repo/topstyle-api.git
cd topstyle-api
Installera beroenden:

bash
Kopiera kod
dotnet restore
Uppdatera appsettings.json med din databasanslutning och JWT-konfiguration.

Applicera migrationer och uppdatera databasen:

bash
Kopiera kod
dotnet ef database update
Kör applikationen:

bash
Kopiera kod
dotnet run
Öppna Swagger-dokumentationen på http://localhost:5000/swagger för att testa API
.

Projektstruktur
Controllers: Hanterar inkommande API-förfrågningar (OrderController, ProductController, UserController).
Core: Tjänster och gränssnitt som implementerar affärslogik.
Data: Databasrelaterade klasser som TopStyleDbContext, repository-mönster, och migrationsfiler.
Domain: Entiteter och DTO-klasser för applikationens kärnmodeller.
Profiles: AutoMapper-profiler för att mappa mellan entiteter och DTOs.
