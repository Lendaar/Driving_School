using Driving_School.Common;
using Driving_School.Context;
using Driving_School.Repositories;
using Driving_School.Services;
using Driving_School.Shared;

namespace Driving_School.Infrastructure
{
    public static class ServiceCollectionExtensions
    { 
        public static void AddDependences(this IServiceCollection services)
        {
            services.RegisterModule<ServiceModule>();
            services.RegisterModule<ContextModule>();
            services.RegisterModule<RepositoriesModule>();

            services.RegisterAutoMapper();
        }
    }
}
