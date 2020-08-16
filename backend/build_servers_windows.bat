rmdir "../bin/web" /S /Q

dotnet publish web.sln -c Publish -r win-x64
