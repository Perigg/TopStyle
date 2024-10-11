

# TopStyle API

![TopStyle API Swagger UI](https://github.com/Perigg/TopStyle/blob/master/TopStyleAPI.png)

TopStyle API är en modern webbapplikation byggd med ASP.NET Core och Entity Framework Core. Applikationen erbjuder API-tjänster för att hantera produkter, användare och beställningar, och använder JWT-autentisering för säker åtkomst.

## Funktioner
- **Autentisering med JWT och ASP.NET Core Identity**: Säker API-åtkomst genom JSON Web Tokens (JWT) tillsammans med ASP.NET Core Identity för hantering av användarautentisering, registrering och rollhantering.
- **Hantera produkter**: Sök produktinformation via ProductController.
- **Beställningshantering**: Skapa och hämta användarens egna beställningar via OrderController.
- **Användarhantering**: Registrera nya användare och logga in via UserController.
- **Swagger-dokumentation**: Automatiskt genererad API-dokumentation med Swagger för enkel testning och åtkomst till API.
- **Databasstöd med Entity Framework Core**: Använder SQL Server för lagring av data med migrationshantering.
- **AutoMapper**: Smidig mappning mellan entiteter och DTOs för att hantera dataöverföring på ett effektivt sätt.
- **Dependency Injection**: Använder DI för att organisera applikationslogik och underlätta testning och skalbarhet.

## Teknologi och verktyg
- **ASP.NET Core**: Backend-ramverket för API.
- **Entity Framework Core**: ORM som hanterar databasoperationer.
- **JWT (JSON Web Tokens)**: För autentisering och säkerhet.
- **ASP.NET Core Identity**: För att hantera användarregistrering, inloggning och rollbaserad åtkomstkontroll.
- **Swagger**: För att generera och dokumentera API.
- **AutoMapper**: För att mappa mellan entiteter och DTOs.
- **MSSQL**: Databas som används för lagring.

## Installation

### Krav
- .NET 8.0 SDK eller senare
- MSSQL Server (eller annan kompatibel SQL-databas)
- Visual Studio eller annan IDE som stödjer .NET-projekt

### Steg för att komma igång

1. Klona projektet:
   ```bash
   git clone https://github.com/PerIgg/topstyle-api.git
   cd topstyle-api
   ```

2. Installera beroenden:
   ```bash
   dotnet restore
   ```

3. Uppdatera `appsettings.json` med din databasanslutning och JWT-konfiguration.

4. Applicera migrationer och uppdatera databasen:
   ```bash
   dotnet ef database update
   ```

5. Kör applikationen:
   ```bash
   dotnet run
   ```

6. Öppna Swagger-dokumentationen på `http://localhost:5000/swagger` för att testa API.

## Projektstruktur
- **Controllers**: Hanterar inkommande API-förfrågningar (OrderController, ProductController, UserController).
- **Core**: Tjänster och gränssnitt som implementerar affärslogik.
- **Data**: Databasrelaterade klasser som `TopStyleDbContext`, repository-mönster, och migrationsfiler.
- **Domain**: Entiteter och DTO-klasser för applikationens kärnmodeller.
- **Profiles**: AutoMapper-profiler för att mappa mellan entiteter och DTOs.

## API-slutpunkter

### Autentisering och användarhantering
- `POST /api/user/login` – Logga in en användare.
- `POST /api/user/register` – Registrera en ny användare.

### Produkter
- `GET /api/products/search` – Sök efter produkter.

### Beställningar
- `POST /api/orders` – Skapa en ny beställning.
- `GET /api/orders/my-orders` – Hämta användarens egna beställningar.

## Licens

Detta projekt är licensierat under MIT-licensen.
