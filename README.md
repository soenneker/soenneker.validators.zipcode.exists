[![](https://img.shields.io/nuget/v/soenneker.validators.zipcode.exists.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.validators.zipcode.exists/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.validators.zipcode.exists/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.validators.zipcode.exists/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.validators.zipcode.exists.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.validators.zipcode.exists/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.validators.zipcode.exists/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.validators.zipcode.exists/actions/workflows/codeql.yml)

# Soenneker.Validators.ZipCode.Exists

A validation module checking for existence of US ZipCodes, updated daily (if available) Thread-safe, disposable. Register as a singleton if you don't want to load the resource every time the validator is instantiated.

## Install

```bash
dotnet add package Soenneker.Validators.ZipCode.Exists
```

## Quick start

```csharp
using Soenneker.Validators.ZipCode.Exists.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddZipCodeExistsValidatorAsSingleton();
```

Adds `IZipCodeExistsValidator` as a singleton service. Recommended if you don't want to load the resource every time the validator is instantiated.

## What you get

- `IZipCodeExistsValidator` — A validation module checking for existence of US ZipCodes, updated daily (if available) Thread-safe, disposable. Register as a singleton if you don't want to load the resource every time the validator is instantiated.
- `ZipCodeExistsValidatorRegistrar` — A validation module checking for existence of US ZipCodes, updated daily (if available).

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IZipCodeExistsValidator.Validate(zipCode, cancellationToken)` | Validates a 5-digit US ZipCode. | True if the ZipCode is valid, otherwise false. |
| `ZipCodeExistsValidatorRegistrar.AddZipCodeExistsValidatorAsSingleton(services)` | Adds `IZipCodeExistsValidator` as a singleton service. Recommended if you don't want to load the resource every time the validator is instantiated. | The same service collection, so additional registrations can be chained. |
| `ZipCodeExistsValidatorRegistrar.AddZipCodeExistsValidatorAsScoped(services)` | Adds `IZipCodeExistsValidator` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
