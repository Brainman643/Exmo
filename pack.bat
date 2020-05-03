@echo off

for /f %%i in ('git describe --tags --abbrev^=0 --match v[0-9]^*') do set tag=%%i
echo tag: %tag%
set version=%tag:~1%
echo version: %version%

set version=1.1
dotnet pack Exmo -c Release -p:Version=%version%