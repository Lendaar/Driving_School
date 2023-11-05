using Driving_School.Common;
using Driving_School.Context.Contracts.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Driving_School.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection services)
        {
            services.AddScoped<IDriving_SchoolContext, Driving_SchoolContext>();
        }
    }
}
