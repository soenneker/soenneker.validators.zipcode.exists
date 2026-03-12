using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Utils.File.Registrars;
using Soenneker.Utils.Paths.Resources.Registrars;
using Soenneker.Validators.ZipCode.Exists.Abstract;

namespace Soenneker.Validators.ZipCode.Exists.Registrars;

/// <summary>
/// A validation module checking for existence of US ZipCodes, updated daily (if available).
/// </summary>
public static class ZipCodeExistsValidatorRegistrar
{
    /// <summary>
    /// Adds <see cref="IZipCodeExistsValidator"/> as a singleton service. Recommended if you don't want to load the resource every time the validator is instantiated. <para/>
    /// </summary>
    public static IServiceCollection AddZipCodeExistsValidatorAsSingleton(this IServiceCollection services)
    {
        services.AddResourcesPathUtilAsSingleton().AddFileUtilAsSingleton().TryAddSingleton<IZipCodeExistsValidator, ZipCodeExistsValidator>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IZipCodeExistsValidator"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddZipCodeExistsValidatorAsScoped(this IServiceCollection services)
    {
        services.AddResourcesPathUtilAsScoped().AddFileUtilAsScoped().TryAddScoped<IZipCodeExistsValidator, ZipCodeExistsValidator>();

        return services;
    }
}