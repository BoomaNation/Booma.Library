dotnet restore GaiaOnline.Unity.Library.sln
dotnet publish src/GaiaOnline.Game.Client/GaiaOnline.Game.Client.csproj -c debug
dotnet publish src/GaiaOnline.Game.Common/GaiaOnline.Game.Common.csproj -c debug
dotnet publish src/GaiaOnline.Game.Network.Client/GaiaOnline.Game.Network.Client.csproj -c debug

if not exist "build" mkdir build
xcopy src\GaiaOnline.Game.Client\bin\debug\net46\publish build /Y /EXCLUDE:BuildExclude.txt
xcopy src\GaiaOnline.Game.Common\bin\debug\net46\publish build /Y /EXCLUDE:BuildExclude.txt
xcopy src\GaiaOnline.Game.Network.Client\bin\debug\net46\publish build /Y /EXCLUDE:BuildExclude.txt

PAUSE