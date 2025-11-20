# Aero

A .NET library collection targeting .NET 10.0, providing common utilities and build automation tools.

## Packages

### Aero
[![NuGet](https://img.shields.io/nuget/v/Aero.svg)](https://www.nuget.org/packages/Aero/)

Common utilities and infrastructure interfaces for .NET applications, including:
- **CombGuid**: Sequential GUID generation for database performance
- **Crypto**: Cryptographic utilities with secure settings
- **SystemTime**: Testable time abstraction
- **RandomNumbers**: Random number generation utilities
- Infrastructure interfaces: `IAeroLogger`, `ITelemetry`, `IFileSystem`

### Aero.Cake
[![NuGet](https://img.shields.io/nuget/v/Aero.Cake.svg)](https://www.nuget.org/packages/Aero.Cake/)

Cake Frosting-based build automation library with:
- DotNet Core CLI wrappers for cleaner testing and abstraction
- Build services (NuGet, Version, ProjectFile management)
- Strongly-typed settings classes for build operations
- Features organized by technology domain

### Aero.Cake.TestSupport
[![NuGet](https://img.shields.io/nuget/v/Aero.Cake.TestSupport.svg)](https://www.nuget.org/packages/Aero.Cake.TestSupport/)

Test support utilities for Cake-based builds.

## Getting Started

### Installation

Install via NuGet Package Manager:

```bash
dotnet add package Aero
dotnet add package Aero.Cake
```

### Building from Source

All build commands must be run from the `Build/Aero.Build` directory:

```bash
# Build all projects and pack NuGet packages
dotnet run --target=build --appVersion=1.0.1.0

# Run unit tests
dotnet run --target=UnitTest

# Clean build artifacts
dotnet run --target=clean
```

For development, you can run individual test projects directly:

```bash
dotnet test Projects/Aero.Tests/Aero.Tests.csproj
dotnet test Projects/Aero.Cake.Tests/Aero.Cake.Tests.csproj
```

## Requirements

- .NET 10.0 SDK or later

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.