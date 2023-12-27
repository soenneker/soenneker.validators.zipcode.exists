using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Soenneker.Utils.AsyncSingleton;
using Soenneker.Utils.File.Abstract;
using Soenneker.Validators.ZipCode.Exists.Abstract;

namespace Soenneker.Validators.ZipCode.Exists;

/// <inheritdoc cref="IZipCodeExistsValidator"/>
public class ZipCodeExistsValidator : Validator.Validator, IZipCodeExistsValidator
{
    private readonly AsyncSingleton<HashSet<string>> _zipCodesSet;

    public ZipCodeExistsValidator(ILogger<Validator.Validator> logger, IFileUtil fileUtil) : base(logger)
    {
        _zipCodesSet = new AsyncSingleton<HashSet<string>>(async () =>
        {
            // TODO: should be file -> hashset, not file -> list -> hashset
            List<string> list = await fileUtil.ReadFileAsLines(Path.Combine("Resources", "zipcodes.txt"));
            var hashSet = new HashSet<string>(list);
            return hashSet;
        });
    }

    public async ValueTask<bool> Validate(string zipCode)
    {
        if ((await _zipCodesSet.Get()).Contains(zipCode))
            return true;

        return false;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        return _zipCodesSet.DisposeAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _zipCodesSet.Dispose();
    }
}
