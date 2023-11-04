using AutoMapper;
using Driving_School.Common;
using Driving_School.Services.Anchors;
using Driving_School.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Driving_School.Services
{
    public class ServiceModule : Module
    {
        public override void CreateModule(IServiceCollection services)
        {
            services.AssemblyInterfaceAssignableTo<IServiceAnchor>(ServiceLifetime.Scoped);
            services.AddMapper<Profile>();
        }
    }
}
