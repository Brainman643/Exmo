cd %~dp0
rmdir /s /q TestResults
echo GITHUB_ACTIONS=%GITHUB_ACTIONS%
echo GITHUB_ACTIONS1=%GITHUB_ACTIONS1%
dotnet test -c Release --logger "trx;LogFileName=TestResults.trx" --results-directory ./TestResults /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=../TestResults/
echo dir TestResults
dir TestResults
reportgenerator -reports:TestResults/coverage.info -targetdir:TestResults/Report
echo dir TestResults
dir TestResults