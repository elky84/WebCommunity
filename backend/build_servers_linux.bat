rmdir "../bin/web" /S /Q

dotnet publish -c Publish -r ubuntu.18.04-x64
