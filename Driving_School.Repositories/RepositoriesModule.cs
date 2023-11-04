using Driving_School.Common;
using Driving_School.Repositories.Anchors;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Repositories.Implementations;
using Driving_School.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Driving_School.Repositories
{
    public class RepositoriesModule : Module
    {
        public override void CreateModule(IServiceCollection services)
        {
            services.AssemblyInterfaceAssignableTo<IRepositoriesAnchor>(ServiceLifetime.Scoped);
        }
    }
}
