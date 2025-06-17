using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.AsyncSingleton;
using Soenneker.Utils.File.Abstract;
using Soenneker.Validators.ZipCode.Exists.Abstract;

namespace Soenneker.Validators.ZipCode.Exists;

/// <inheritdoc cref="IZipCodeExistsValidator"/>
public sealed class ZipCodeExistsValidator : Validator.Validator, IZipCodeExistsValidator
{
    private readonly AsyncSingleton<HashSet<string>> _zipCodesSet;

    public ZipCodeExistsValidator(ILogger<ZipCodeExistsValidator> logger, IFileUtil fileUtil) : base(logger)
    {
        _zipCodesSet = new AsyncSingleton<HashSet<string>>(async (token, _) =>
        {
            // TODO: should be file -> hashset, not file -> list -> hashset
            List<string> list = await fileUtil.ReadAsLines(Path.Combine("Resources", "zipcodes.txt"), true, token).NoSync();
            return [..list];
        });
    }

    public async ValueTask<bool> Validate(string zipCode, CancellationToken cancellationToken = default)
    {
        if (zipCode.Length > 5)
        {
            zipCode = zipCode.Substring(0, 5);
            Logger.LogWarning("ZipCodes longer than 5 are not supported and are trimmed past 5 characters");
        }

        if ((await _zipCodesSet.Get(cancellationToken)).Contains(zipCode))
            return true;

        return false;
    }

    public ValueTask DisposeAsync()
    {
        return _zipCodesSet.DisposeAsync();
    }

    public void Dispose()
    {
        _zipCodesSet.Dispose();
    }
}
