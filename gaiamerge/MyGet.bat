%NUGET% restore GaiaOnline.Unity.Library.sln -NoCache -NonInteractive -ConfigFile Nuget.config
msbuild GaiaOnline.Unity.Library.sln /p:Configuration=Release