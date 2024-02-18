

using System.Runtime.CompilerServices;
using ContactsNet.Core.Dal.Extensions.MsSql;
using ContactsNet.Core.Dal.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
[assembly:InternalsVisibleTo("ContactsNet.Api")]

namespace ContactsNet.Core;

internal static class ServiceCollectionExtension
{
    internal static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMsSql();
        services.AddMsSql<ContactsNetDbContext>();
        return services;
    }
    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);

    }

    private static T GetOptions<T>(this IConfiguration configuration, string sectionName)
        where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}