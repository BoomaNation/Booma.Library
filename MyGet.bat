nuget.exe restore Booma.Library.sln -NoCache -NonInteractive -ConfigFile Nuget.config
msbuild Booma.Library.sln /p:Configuration=Release
dotnet-pack Booma.Library.sln --no-build