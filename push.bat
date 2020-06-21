echo off
dotnet nuget push Exmo\bin\Release\*.nupkg --api-key %1 --source https://api.nuget.org/v3/index.json