dotnet restore GaiaOnline.Unity.Library.sln
dotnet publish src/GaiaOnline.Game.Client/GaiaOnline.Game.Client.csproj -c release
dotnet publish src/GaiaOnline.Game.Common/GaiaOnline.Game.Common.csproj -c release
dotnet publish src/GaiaOnline.Game.Network.Client/GaiaOnline.Game.Network.Client.csproj -c release

if not exist "build" mkdir build
if not exist "build\release" mkdir build\release
xcopy src\GaiaOnline.Game.Client\bin\Release\net46\publish build\release /Y /EXCLUDE:BuildExclude.txt
xcopy src\GaiaOnline.Game.Common\bin\Release\net46\publish build\release /Y /EXCLUDE:BuildExclude.txt
xcopy src\GaiaOnline.Game.Network.Client\bin\Release\net46\publish build\release /Y /EXCLUDE:BuildExclude.txt

PAUSE