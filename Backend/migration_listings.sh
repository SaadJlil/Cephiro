dotnet ef database update --project ./Cephiro.Listings --startup-project ./Cephiro.Api 0
dotnet ef migrations remove --project ./Cephiro.Listings --startup-project ./Cephiro.Api
dotnet ef migrations add Initial  --project ./Cephiro.Listings --startup-project ./Cephiro.Api --output-dir Infrastructure/Data/Migrations 
dotnet ef database update --project ./Cephiro.Listings --startup-project ./Cephiro.Api
