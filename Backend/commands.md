**Add Migrations with the API project as a source & the Infrastructure project as the holder**

dotnet ef migrations add Init --project Cephiro.Identity -s Cephiro.Api -o Infrastructure/Data/Migrations

**Initialize the database from the source (API) with the Migrations (stored in Identity classlib)**
dotnet ef database update Init --project Cephiro.Identity -s Cephiro.Api