dotnet restore GaiaOnline.Unity.Library.sln
dotnet publish src/GaiaOnline.Authentication.Service/GaiaOnline.Authentication.Service.csproj -c release
dotnet publish src/GaiaOnline.GameServerList.Service/GaiaOnline.GameServerList.Service.csproj -c release

if not exist "build" mkdir build
if not exist "build\services\" mkdir build\services
if not exist "build\services\Authentication" mkdir build\services\Authentication
if not exist "build\services\GameServerList" mkdir build\services\GameServerList
xcopy src\GaiaOnline.Authentication.Service\bin\Release\netcoreapp1.1\publish build\services\Authentication /Y
xcopy src\GaiaOnline.GameServerList.Service\bin\Release\netcoreapp1.1\publish build\services\GameServerList /Y

PAUSE