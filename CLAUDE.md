# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Aero is a .NET library collection targeting .NET 10.0, consisting of two main NuGet packages:
- **Aero**: Common utilities and infrastructure interfaces for .NET applications
- **Aero.Cake**: Cake Frosting-based build automation library with DotNet Core wrappers and services

## Solution Structure

The solution contains:
- **Projects/Aero**: Core library with utilities (CombGuid, Crypto, SystemTime, RandomNumbers) and infrastructure interfaces (IAeroLogger, ITelemetry, IFileSystem)
- **Projects/Aero.Cake**: Build automation library using Cake Frosting framework with DotNet Core wrappers and services
- **Projects/Aero.Cake.TestSupport**: Test support utilities for Cake-based builds
- **Projects/Aero.Tests**: Unit tests for Aero library
- **Projects/Aero.Cake.Tests**: Unit tests for Aero.Cake library
- **Build/Aero.Build**: Cake Frosting build project that orchestrates building, testing, and publishing

## Build Commands

All commands must be run from the `Build/Aero.Build` directory:

```bash
# Clean build artifacts
dotnet run --target=clean

# Build all projects (packs main projects into NuGet packages)
dotnet run --target=build --appVersion=1.0.1.0

# Run unit tests (assumes projects are already built)
dotnet run --target=UnitTest

# Push NuGet packages
dotnet run --target=NuGetPush --appVersion=1.0.1.0 --NuGetFeedPassword="<password>"
```

### Running Tests During Development

To run a single test project directly:
```bash
dotnet test Projects/Aero.Tests/Aero.Tests.csproj
dotnet test Projects/Aero.Cake.Tests/Aero.Cake.Tests.csproj
```

## Build Architecture

The build system uses **Cake Frosting** (C#-based Cake) instead of traditional Cake scripts:

### Build Task Flow

1. **Build Task**: Packs non-test projects (Aero, Aero.Cake, Aero.Cake.TestSupport) into NuGet packages, then builds test projects. This ensures version information is preserved in packages before tests run.
2. **UnitTest Task**: Runs tests with `NoBuild=true` since projects were already built in Build Task.

### AeroContext Pattern

The build system uses an abstract `AeroContext` class (from Aero.Cake) that extends `FrostingContext`:
- **Purpose**: Normalizes file paths based on runtime working directory
- **Implementation**: Each build project creates a concrete context (e.g., `MyContext`) that implements `GetNormalizedPath()`
- **Why**: Working directory varies depending on how the build is executed (debug, command line, different starting directories)

Example from `MyContext.GetNormalizedPath()`:
```csharp
if (workDirPath.EndsWith("build/aero.build/bin/debug"))
    return Path.Combine("../../../../projects", relativePath);
if (workDirPath.EndsWith("build/aero.build"))
    return Path.Combine("../../projects", relativePath);
```

### Dependency Injection in Builds

The build uses Microsoft.Extensions.DependencyInjection:
- Context registered via `.UseContext<MyContext>()`
- Services and wrappers registered in `Program.ConfigureServices()`
- Tasks receive dependencies via constructor injection

## Key Design Patterns

### Aero.Cake Architecture

- **Wrappers**: Abstract Cake CLI tools (e.g., `IDotNetCoreWrapper`) for cleaner testing and abstraction
- **Services**: Business logic for build operations (e.g., `NuGetService`, `VersionService`, `ProjectFileService`)
- **Settings Classes**: Strongly-typed configuration for build operations (e.g., `BuildSettings`, `TestSettings`, `PackSettings`)
- **Features**: Organized by technology domain (e.g., `Features/DotNet`, `Features/DbUp`)

### Version Management

The `VersionService` parses `--appVersion` argument and applies versions to project files during pack operations, ensuring consistent versioning across all packages.

## Development Notes

- The solution targets .NET 10.0 as of the latest commit
- Build configuration defaults to "Release" to avoid circular dependencies when Aero.Build references Aero.Cake
- Test projects use `NoBuild=true` to prevent rebuilding dependencies (which would lose version info)
- NuGet packages include PDB files for debugging (`AllowedOutputExtensionsInPackageBuildOutputFolder` includes `.pdb`)
