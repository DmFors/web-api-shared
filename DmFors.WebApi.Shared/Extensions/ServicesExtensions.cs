using System.Reflection;
using DmFors.WebApi.Shared.CQS;
using Microsoft.Extensions.DependencyInjection;

namespace DmFors.WebApi.Shared.Extensions;

public static class ServicesExtensions
{
    public static void AddHandlers(this IServiceCollection services, Assembly assembly)
    {
        services.AddCommandHandlers(assembly);
        services.AddQueryHandlers(assembly);
    }
    
    /// <summary>
    /// Добавление всех Command Handlers в DI как self и interfaces в Scoped
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    public static void AddCommandHandlers(this IServiceCollection services, Assembly assembly)
    {
        services.ScanAssemblyTypes(assembly, typeof(ICommandHandler<>), typeof(ICommandHandler<,>));
    }
    
    /// <summary>
    /// Добавление всех Query Handlers в DI как self и interfaces в Scoped
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    public static void AddQueryHandlers(this IServiceCollection services, Assembly assembly)
    {
        services.ScanAssemblyTypes(assembly, typeof(IQueryHandler<,>));
    }
    
    public static void ScanAssemblyTypes(this IServiceCollection services, Assembly assembly, params Type[] types) =>
        services.Scan(
            scan => scan.FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableToAny(types))
                .AsSelfWithInterfaces().WithScopedLifetime());
}