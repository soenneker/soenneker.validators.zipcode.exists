using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Soenneker.Extensions.String;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.AsyncSingleton;
using Soenneker.Utils.File.Abstract;
using Soenneker.Utils.Paths.Resources;
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
            string path = ResourcesPathUtil.GetResourceFilePath("zipcodes.txt");

            return await fileUtil.ReadToHashSet(path, StringComparer.OrdinalIgnoreCase, cancellationToken: token)
                .NoSync();
        });
    }

    public async ValueTask<bool> Validate(string zipCode, CancellationToken cancellationToken = default)
    {
        if (zipCode.IsNullOrWhiteSpace())
            return false;

        if (zipCode.Length > 5)
        {
            zipCode = zipCode.Substring(0, 5);
            Logger.LogWarning("ZipCodes longer than 5 are not supported and are trimmed past 5 characters");
        }

        if ((await _zipCodesSet.Get(cancellationToken).NoSync()).Contains(zipCode))
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