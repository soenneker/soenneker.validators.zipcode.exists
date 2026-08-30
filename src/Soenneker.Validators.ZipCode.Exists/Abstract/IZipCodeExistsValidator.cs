using System;
using Soenneker.Validators.Validator.Abstract;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Validators.ZipCode.Exists.Abstract;

/// <summary>
/// Validates US ZIP codes against the data snapshot packaged with the library.
/// The data is loaded lazily and cached for the lifetime of the validator.
/// </summary>
public interface IZipCodeExistsValidator : IValidator, IAsyncDisposable, IDisposable
{
    /// <summary>
    /// Determines whether the first five characters of a US ZIP code appear in the packaged data.
    /// </summary>
    /// <param name="zipCode">The ZIP code to check. Values longer than five characters are truncated; null, empty, and whitespace-only values return <see langword="false"/>.</param>
    /// <param name="cancellationToken">Token used to cancel loading the packaged data.</param>
    /// <returns><see langword="true"/> when the five-digit value appears in the packaged data; otherwise, <see langword="false"/>.</returns>
    ValueTask<bool> Validate(string zipCode, CancellationToken cancellationToken = default);
}
