using Microsoft.Extensions.DependencyInjection;

namespace Driving_School.Common
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterModule<TModule>(this IServiceCollection services) where TModule : Module
        {
            var type = typeof(TModule);
            var instance = Activator.CreateInstance(type) as Module;
            if (instance == null)
            {
                return;
            }
            instance.CreateModule(services);
        }
    }
}
