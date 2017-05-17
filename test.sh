mono ./testrunner/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe ./test/Booma.Combat.Formula.Server.Tests/bin/Release/Booma.Combat.Formula.Server.Tests.dll
result1=$?

mono ./testrunner/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe ./test/Booma.Entity.Tests/bin/Release/Booma.Entity.Tests.dll
result2=$?

mono ./testrunner/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe ./test/Booma.Payloads.Tests/bin/Release/Booma.Payloads.Tests.dll
result3=$?

mono ./testrunner/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe ./test/Booma.Stats.Common.Tests/bin/Release/Booma.Stats.Common.Tests.dll
result4=$?

exit $result1 && $result2 && $result3 && $result4