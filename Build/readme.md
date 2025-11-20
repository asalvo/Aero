## Task Examples
All examples assume you are in Aero\build\Aero.Build

- Clean: dotnet run --target=clean
- Build: dotnet run --target=build --appVersion=1.0.1.0
- Test: dotnet run --target=UnitTest
  - With the default configuration (Release), you need to run the build target first to ensure the bin/release folder is present.
- Push: dotnet run --target=NuGetPush --appVersion=1.0.1.0 --NuGetApiKey=""
