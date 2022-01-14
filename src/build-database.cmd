@echo off

echo Building database...
dotnet ef database update --project .\Howest.Prog.CoinChop.Infrastructure --startup-project .\Howest.Prog.CoinChop.Web
pause