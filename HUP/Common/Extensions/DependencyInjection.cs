using System.Reflection;

namespace HUP.Common.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Get the assembly where this code is running (The Application Layer)
        var assembly = Assembly.GetExecutingAssembly();

        // Find all classes that end with "Service" or "Repository" and are not abstract/interfaces
        var serviceTypes = assembly.GetTypes()
            .Where(t => (t.Name.EndsWith("Service") || t.Name.EndsWith("Repository")) 
                        && t is { IsClass: true, IsAbstract: false });

        foreach (var type in serviceTypes)
        {
            // Find the interface (Convention: IName)
            var interfaceType = type.GetInterface($"I{type.Name}");

            if (interfaceType == null) continue;
            // Exclude Singleton
            if (type.Name == "CacheService") continue; 

            services.AddScoped(interfaceType, type);
        }
        return services;
    }
}