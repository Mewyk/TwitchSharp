# Contributing to TwitchSharp

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## Build and Test

```bash
dotnet build TwitchSharp.slnx
dotnet test --solution TwitchSharp.slnx
```

## Key Policies

TwitchSharp enforces strict code quality through build configuration and project policies. The build will fail on any warning (`TreatWarningsAsErrors`) and enforces code style (`EnforceCodeStyleInBuild`).

### Required

- **AOT compatible** -- all code must pass trim and AOT analyzers
- **Source-generated JSON only** -- use `JsonSerializerContext`, no reflection-based serialization
- **XML documentation** -- required on all public APIs
- **File-scoped namespaces** -- `namespace X;` not `namespace X { }`
- **Sealed record DTOs** -- API response and event payload types use `sealed record` with `{ get; init; }` and `[JsonPropertyName]` on every property
- **Descriptive names** -- no abbreviated or single-letter variable names (e.g., `exception` not `ex`, `serviceProvider` not `sp`)
- **`CancellationToken` last** -- always the final parameter in method signatures
- **`[LoggerMessage]` for logging** -- use the source generator, not direct `ILogger` calls
- **`TryAddSingleton`** -- prefer over `AddSingleton` in DI registrations

### Prohibited

- `#pragma warning disable`
- Null-forgiving operator (`!`)
- Bare `catch` blocks -- always specify the exception type
- Reflection or dynamic code generation
