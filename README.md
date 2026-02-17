# TwitchSharp

A .NET 10 library for the Twitch Helix API and EventSub WebSocket, designed for Native AOT from the ground up.

WARNING: This is the first alpha release, which is not fully tested. More updates to come in the next few weeks when the documentation is pushed.

## Features

- **Full Helix API coverage** -- 30 endpoint categories with typed request/response models
- **EventSub WebSocket client** -- real-time events with automatic reconnection and keepalive monitoring
- **Native AOT compatible** -- trimming-safe with source-generated JSON serialization (zero reflection)
- **Built-in resilience** -- automatic retries with exponential backoff, rate limiting, and token management
- **Dependency injection ready** -- first-class integration with `Microsoft.Extensions.Hosting`
- **Modern C# 14** -- sealed records, file-scoped namespaces, collection expressions, and `field` keyword

## Packages

| Package | Description |
|---------|-------------|
| `TwitchSharp` | Core primitives, exceptions, and pagination helpers |
| `TwitchSharp.Api` | Helix API client with authentication and rate limiting |
| `TwitchSharp.EventSub` | EventSub WebSocket client for real-time events |
| `TwitchSharp.Hosting` | Dependency injection and hosted service integration |
| `TwitchSharp.Extensions.Authentication` | Interactive OAuth PKCE flow for desktop/CLI apps |

## Quick Start

### Standalone usage

```csharp
await using var client = TwitchApiClient.Create(new TwitchApiClientOptions
{
    ClientId = "your_client_id",
    ClientSecret = "your_client_secret"
});

var users = await client.Users.GetUsersAsync(logins: ["twitchdev"]);
```

### With dependency injection

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.AddTwitchApi();
builder.AddTwitchEventSub();

var app = builder.Build();
app.Run();
```

Configure via `appsettings.json`:

```json
{
  "Twitch": {
    "ClientId": "your_client_id",
    "ClientSecret": "your_client_secret"
  }
}
```

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for development setup and coding guidelines.

## License

[MIT](LICENSE)
