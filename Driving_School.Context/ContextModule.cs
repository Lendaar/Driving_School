using Driving_School.Common;
using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Driving_School.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection services)
        {
            services.AddScoped<IDriving_SchoolContext, Driving_SchoolContext>();
            services.TryAddScoped<IDriving_SchoolContext>(provider => provider.GetRequiredService<Driving_SchoolContext>());
            services.TryAddScoped<IDbRead>(provider => provider.GetRequiredService<Driving_SchoolContext>());
            services.TryAddScoped<IDbWriter>(provider => provider.GetRequiredService<Driving_SchoolContext>());
            services.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<Driving_SchoolContext>());
        }
    }
}
