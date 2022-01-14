#!/bin/bash
echo 'Building database...'

dotnet ef database update --project ./Howest.Prog.CoinChop.Infrastructure --startup-project ./Howest.Prog.CoinChop.Web

read -p 'Press [enter] to exit'