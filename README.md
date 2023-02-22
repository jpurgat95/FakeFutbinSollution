# FakeFutbinSollution
### `Learning project which combines ASP.NET Web Core API and Blazor Web Assembly App`

#### Database creation
1) Two NuGet Packages: [Tools and Sql](https://user-images.githubusercontent.com/94840984/220696122-b70c0cd6-c6d1-4eda-b129-b046b39773e2.png) added.
2) [Entities](https://user-images.githubusercontent.com/94840984/220696557-b6148828-3982-448a-8aeb-2868ca74df04.png) which represent database tables created.
3) FakeFutbinDbContext class created: [Part 1](https://user-images.githubusercontent.com/94840984/220697934-89745529-045b-4bf6-aa52-93984551e9ba.png) and [Part 2](https://user-images.githubusercontent.com/94840984/220698255-73d74868-a73d-4c2a-996a-fa21306e8fc2.png) `This class uses ModelBuilder to create some new records for Players, PlayerNationalities and Positions tables. DbSet class was used to create tables with specified names.
4) [Connection String](https://user-images.githubusercontent.com/94840984/220695822-34894036-011b-41a5-a989-68f2d90f5c49.png) created. 
5) [FakeFutbinDbContext added to DbContextPool using SqlServer and custom connection string](https://user-images.githubusercontent.com/94840984/220700230-435d193d-26ed-49b6-8841-f1f44cd06dfd.png)
6) Migration created with `Package Manager Console` using `Add-Migration *MigrationName*` and `Update-Database` commands: [Database Tables](https://user-images.githubusercontent.com/94840984/220702488-41f27d45-f32b-402d-8a23-093530079c23.png).
Last *MigrationName* was PositionsTableAdded, but it was used multiple times during the project creation and development.
