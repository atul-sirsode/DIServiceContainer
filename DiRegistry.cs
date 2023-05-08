using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DIServiceContainer
{
    public interface IBaseService { }
    public static class DiRegistry
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            var enumerable = assemblies as Assembly[] ?? assemblies.ToArray();
            if (!enumerable.Any())
            {
                throw new ArgumentNullException($"no assemblies provided for registration");
            }

            var interFaceType = typeof(IBaseService);
            var implementations = enumerable
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && interFaceType.IsAssignableFrom(type));

            foreach (var implementation in implementations)
            {
                foreach (var implementedInterface in implementation.GetInterfaces().Where(i => i != interFaceType))
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(implementedInterface, implementation);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(implementedInterface, implementation);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(implementedInterface, implementation);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
                    }

                }
            }

            return services;
        }
    }
}