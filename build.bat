dotnet restore Booma.Library.sln
dotnet publish src/Booma.Instance.Client/Booma.Instance.Client.csproj -c release
dotnet publish src/Booma.Instance.Server/Booma.Instance.Server.csproj -c release
dotnet publish src/Unity.Editor.Dependencies/Unity.Editor.Dependencies.csproj -c release

if not exist "build\client\release" mkdir build\client\release
if not exist "build\server\release" mkdir build\server\release
xcopy src\Booma.Instance.Client\bin\Release\net46\publish build\client\release /Y /EXCLUDE:BuildExclude.txt
xcopy src\Booma.Instance.Server\bin\Release\net46\publish build\server\release /Y /EXCLUDE:BuildExclude.txt

xcopy src\Unity.Editor.Dependencies\bin\Release\net46\publish build\client\release /Y /EXCLUDE:BuildExclude.txt
xcopy src\Unity.Editor.Dependencies\bin\Release\net46\publish build\server\release /Y /EXCLUDE:BuildExclude.txt

PAUSE