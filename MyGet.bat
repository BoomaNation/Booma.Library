nuget.exe restore Booma.Library.sln -NoCache -NonInteractive -ConfigFile Nuget.config
msbuild Booma.Library.sln /p:Configuration=Release
nuget.exe pack src/Build.All/Build.All.csproj -IncludeReferencedProjects