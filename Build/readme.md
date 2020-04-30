## Task Examples
All examples assume you are in Aero\build\Aero.Build

- Clean: dotnet run --target=clean
- Build: dotnet run --target=build --appVersion=1.0.1
- Test: dotnet run --target=UnitTest
- Pack: dotnet run --target=NuGetPack --appVersion=1.0.1
- Push: dotnet run --target=NuGetPush --appVersion=1.0.1 --NuGetFeedPassword=""
