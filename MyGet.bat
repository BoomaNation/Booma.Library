nuget.exe restore Booma.Library.sln -NoCache -NonInteractive -ConfigFile Nuget.config
msbuild Booma.Library.sln /p:Configuration=Release
nuget.exe spec src/Build.All/Build.All.csproj
nuget.exe spec src/Booma.Entity.Identity/Booma.Entity.Identity.csproj
nuget.exe pack src/Booma.Entity.Identity/Booma.Entity.Identity.csproj -IncludeReferencedProjects
nuget.exe pack src/Build.All/Build.All.csproj -IncludeReferencedProjects
PAUSE