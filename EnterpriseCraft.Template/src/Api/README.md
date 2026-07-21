Purpose of this folder
- Host / transport concerns only (CORS, Mapster, health checks, HTTP examples).
- Keep feature logic and DI inside modules (src/Modules/*) and shared utilities in src/Shared/*.
- Do NOT add Program.cs or module DI registrations here while the root Program.cs is the composition root.

Files
- DependencyInjection.cs: AddApi extension for host-level registrations.
- EndpointExtensions.cs: MapApiEndpoints helper for host endpoints (health, metrics).
- Api.http: sample requests for quick manual testing.