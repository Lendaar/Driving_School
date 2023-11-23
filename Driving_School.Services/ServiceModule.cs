using Driving_School.Common;
using Driving_School.Services.Anchors;
using Driving_School.Services.Automappers;
using Driving_School.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Driving_School.Services
{
    public class ServiceModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IServiceAnchor>(ServiceLifetime.Scoped);
            service.RegisterAutoMapperProfile<ServiceProfile>();
        }
    }
}
