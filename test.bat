cd %~dp0
rmdir /s /q TestResults
dotnet test -c Release --logger "trx;LogFileName=TestResults.trx" --results-directory ./TestResults /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=../TestResults/
reportgenerator -reports:TestResults/coverage.info -targetdir:TestResults/Report