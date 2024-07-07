using System;
using Soenneker.Validators.Validator.Abstract;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Validators.ZipCode.Exists.Abstract;

/// <summary>
/// A validation module checking for existence of US ZipCodes, updated daily (if available) <para/>
/// Thread-safe, disposable. Register as a singleton if you don't want to load the resource every time the validator is instantiated.
/// </summary>
public interface IZipCodeExistsValidator : IValidator, IAsyncDisposable, IDisposable
{
    /// <summary>
    /// Validates a 5-digit US ZipCode.
    /// </summary>
    /// <param name="zipCode">The 5-digit US ZipCode to validate.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>True if the ZipCode is valid, otherwise false.</returns>
    ValueTask<bool> Validate(string zipCode, CancellationToken cancellationToken = default);
}
