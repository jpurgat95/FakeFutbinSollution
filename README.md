# FakeFutbinSollution
### `Learning project which combines ASP.NET Web Core API and Blazor Web Assembly App`

#### I Database creation
1) Two NuGet Packages: [Tools and Sql](https://user-images.githubusercontent.com/94840984/220696122-b70c0cd6-c6d1-4eda-b129-b046b39773e2.png) added.
2) [Entities](https://user-images.githubusercontent.com/94840984/220696557-b6148828-3982-448a-8aeb-2868ca74df04.png) which represent database tables created.
3) `FakeFutbinDbContext.cs` class created: [Part 1](https://user-images.githubusercontent.com/94840984/220697934-89745529-045b-4bf6-aa52-93984551e9ba.png) and [Part 2](https://user-images.githubusercontent.com/94840984/220698255-73d74868-a73d-4c2a-996a-fa21306e8fc2.png). This class uses `ModelBuilder` to create some new records for `Players`, `PlayerNationalities` and `Positions` tables. `DbSet.cs` class was used to create tables and specify their names.
4) [Connection String](https://user-images.githubusercontent.com/94840984/220695822-34894036-011b-41a5-a989-68f2d90f5c49.png) written. 
5) [`FakeFutbinDbContext.cs` added to `DbContextPool` using `SqlServer` and custom connection string](https://user-images.githubusercontent.com/94840984/220700230-435d193d-26ed-49b6-8841-f1f44cd06dfd.png).
6) Migration created with `Package Manager Console` using `Add-Migration MigrationName` and `Update-Database` commands: [Database Tables](https://user-images.githubusercontent.com/94840984/220702488-41f27d45-f32b-402d-8a23-093530079c23.png).
##### Last *MigrationName* was `PositionsTableAdded`, but it was used multiple times during the project creation and development.
#### II `FakeFutbin.Api` (ASP.NET Web Core API) project combined with `FakeFutbin.Web` (Blazor Web Assembly App) project.
1) [Avoiding Cors Policy](https://user-images.githubusercontent.com/94840984/220705767-fa2d7eff-849c-47f8-b49b-4b2d41929167.png) in `FakeFutbin.Api` project.
2) [Local host address](https://user-images.githubusercontent.com/94840984/220705993-3ad7a6c1-bc33-4239-8026-3ff5c9dbc756.png) added in `FakeFutbin.Web` project.
#### III `PlayerRepository.cs`, data transfer objects, conversions and `PlayerController.cs`
1) [`IRepository.cs`](https://user-images.githubusercontent.com/94840984/220715593-5dd5c15a-ea94-44d7-9d9a-43ab88420c8d.png) interface created.
2) [`RepositoryClass.cs`](https://user-images.githubusercontent.com/94840984/220715790-8a43c9d4-39c5-4e4f-8d50-f9f4b261c0fa.png) which implements `IRepository.cs` interface created. `FakeFutbinDbContext.cs` dependencies injected into `PlayerRepository.cs` class in constructor. Due methods written in this class data from database tables:  `Players` and `PlayerNationalities` could be used in controller.
3) [`IPlayerRepository.cs` interface and `PlayerRepository.cs` class registred for dependency injection](https://user-images.githubusercontent.com/94840984/220720059-aa4546bc-23c4-4ec5-9571-3518d3a2906e.png)
4) [Data transfer objects `PlayerDto.cs` and `PlayerNationalityDto.cs`](https://user-images.githubusercontent.com/94840984/220721956-2e57257a-cb94-4e01-9a6f-7335a7a3f3b1.png) created.
5) Data transfer objects conversions for `PlayerController.cs` class: [for `GetPlayers()` and `GetPlayersByNationality()` methods](https://user-images.githubusercontent.com/94840984/220720652-8cf709c3-f934-41c1-9cd8-f59d19c5e37c.png), [for `GetPlayer()` method](https://user-images.githubusercontent.com/94840984/220721387-a404b2dc-17b1-4dba-ac6f-c4ed65cb0bd9.png) and [for `GetPlayerNationalities()` method](https://user-images.githubusercontent.com/94840984/220721706-8b54f2e8-5e12-4c23-aaa3-466c7e0c321c.png).
6) API controller: [`PlayerController.cs` P1](https://user-images.githubusercontent.com/94840984/220723455-a348a5f9-192c-4feb-a6d6-c95f457dad97.png), [`PlayerController.cs` P2](https://user-images.githubusercontent.com/94840984/220723948-5efddfa1-61b6-4788-8469-cc1cbde4d339.png) and [`PlayerController.cs` P3](https://user-images.githubusercontent.com/94840984/220724292-dc0cd6b7-1cd4-4106-bd33-5b8bf4efe9fd.png). This controller includes `GET` methods which only enable geting data from database using `PlayerRepository.cs` class methods and conversion methods from `DtoConversions.cs` class. `IPlayerRepository.cs` dependencies was injected in controller class constructor. 