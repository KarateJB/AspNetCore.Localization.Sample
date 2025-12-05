# AspNetCore.Localization.Sample
Sample codes of localization in ASP.NET Core 8.0

## Requirements
- .NET 8.0 SDK

## Build and Run
```bash
cd AspNetCore.Localization
dotnet build
dotnet run --project AspNetCore.Localization.WebApi
```

## Test
```bash
cd AspNetCore.Localization
dotnet test
```

## Features
- Request culture provider based on route values
- Localization middleware filter for controllers
- JSON serialization using System.Text.Json
