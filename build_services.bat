dotnet restore Booma.Library.sln
dotnet publish src/Booma.GameServerList.Service/Booma.GameServerList.Service.csproj -c release
dotnet publish src/Booma.GameServerMediator.Service/Booma.GameServerMediator.Service.csproj -c release

if not exist "build\services\Authentication" mkdir build\services\Authentication
if not exist "build\services\GameServerList" mkdir build\services\GameServerList
if not exist "build\services\GameServerMediator" mkdir build\services\GameServerMediator
xcopy src\Booma.GameServerList.Service\bin\Release\netcoreapp1.1\publish build\services\GameServerList /Y /S
xcopy src\Booma.GameServerMediator.Service\bin\Release\netcoreapp1.1\publish build\services\GameServerMediator /Y /S

PAUSE