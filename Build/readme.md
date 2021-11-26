## Task Examples
All examples assume you are in Aero\build\Aero.Build

- Clean: dotnet run --target=clean
- Build: dotnet run --target=build --appVersion=1.0.1.0
- Test: dotnet run --target=UnitTest
- Push: dotnet run --target=NuGetPush --appVersion=1.0.1.0 --NuGetFeedPassword=""
