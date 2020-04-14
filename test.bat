cd %~dp0
rmdir /s /q TestResults
dotnet test -c Release --logger "trx;LogFileName=TestResults.trx" --results-directory ./TestResults /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=../TestResults/
reportgenerator -reports:TestResults/*.opencover.xml -targetdir:TestResults/Report