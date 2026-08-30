using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Utils.File.Registrars;
using Soenneker.Utils.Paths.Resources.Registrars;
using Soenneker.Validators.ZipCode.Exists.Abstract;

namespace Soenneker.Validators.ZipCode.Exists.Registrars;

/// <summary>
/// Registers the ZIP-code existence validator and its dependencies.
/// </summary>
public static class ZipCodeExistsValidatorRegistrar
{
    /// <summary>
    /// Adds <see cref="IZipCodeExistsValidator"/> as a singleton service so one lazily loaded ZIP-code set is shared by the application.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddZipCodeExistsValidatorAsSingleton(this IServiceCollection services)
    {
        services.AddResourcesPathUtilAsSingleton().AddFileUtilAsSingleton().TryAddSingleton<IZipCodeExistsValidator, ZipCodeExistsValidator>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IZipCodeExistsValidator"/> as a scoped service. Each scope receives a validator with its own lazily loaded ZIP-code set.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddZipCodeExistsValidatorAsScoped(this IServiceCollection services)
    {
        services.AddResourcesPathUtilAsScoped().AddFileUtilAsScoped().TryAddScoped<IZipCodeExistsValidator, ZipCodeExistsValidator>();

        return services;
    }
}
