cd %~dp0
setlocal
set GITHUB_ACTIONS=false
echo GITHUB_ACTIONS=%GITHUB_ACTIONS%
dotnet test -c Release --logger "trx;LogFileName=TestResults.trx" --results-directory ./TestResults /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=../TestResults/
echo dir TestResults
dir TestResults
reportgenerator -reports:TestResults/coverage.info -targetdir:TestResults/Report
echo dir TestResults
dir TestResults
endlocal