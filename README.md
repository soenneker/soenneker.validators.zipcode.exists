[![](https://img.shields.io/nuget/v/soenneker.validators.zipcode.exists.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.validators.zipcode.exists/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.validators.zipcode.exists/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.validators.zipcode.exists/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.validators.zipcode.exists.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.validators.zipcode.exists/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.validators.zipcode.exists/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.validators.zipcode.exists/actions/workflows/codeql.yml)

# Soenneker.Validators.ZipCode.Exists

Validates US ZIP codes against the data snapshot packaged with the library.

## Installation

```bash
dotnet add package Soenneker.Validators.ZipCode.Exists
```

## Registration

Register the validator as a singleton when the application can share one in-memory ZIP-code set:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Validators.ZipCode.Exists.Registrars;

services.AddZipCodeExistsValidatorAsSingleton();
```

A scoped registration is also available:

```csharp
services.AddZipCodeExistsValidatorAsScoped();
```

Each scoped instance loads and owns its own cached set. Prefer the singleton registration unless scopes need independent validator instances.

## Usage

```csharp
using Soenneker.Validators.ZipCode.Exists.Abstract;

public sealed class AddressService
{
    private readonly IZipCodeExistsValidator _zipCodeValidator;

    public AddressService(IZipCodeExistsValidator zipCodeValidator)
    {
        _zipCodeValidator = zipCodeValidator;
    }

    public ValueTask<bool> IsKnownZipCode(string zipCode, CancellationToken cancellationToken = default)
    {
        return _zipCodeValidator.Validate(zipCode, cancellationToken);
    }
}
```

```csharp
await validator.Validate("00611");       // true when present in the packaged data
await validator.Validate("00611-5353");  // checks "00611"
await validator.Validate("12345");       // false when absent from the packaged data
```

## Behavior

- Null, empty, and whitespace-only values return `false`.
- Values longer than five characters are truncated to their first five characters. This permits ZIP+4 input, but the suffix is not validated.
- Values of five characters or fewer are matched exactly against the packaged data.
- The data is loaded lazily on the first validation call and cached for the validator's lifetime.
- A `true` result means the five-digit value appears in the package's data snapshot. It does not verify that an address exists or that mail can currently be delivered there.
- Dispose the validator only when you own it directly. Instances resolved from dependency injection are disposed by the container.
