cd %~dp0
rmdir /s /q TestResults
echo GITHUB_ACTIONS=%GITHUB_ACTIONS%
dotnet test -c Release --logger "trx;LogFileName=TestResults.trx" --results-directory ./TestResults /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=../TestResults/
dir TestResults
reportgenerator -reports:TestResults/coverage.info -targetdir:TestResults/Report
dir TestResults