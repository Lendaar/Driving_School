using Microsoft.Extensions.DependencyInjection;

namespace Driving_School.Common
{
    public abstract class Module
    {
        public abstract void CreateModule(IServiceCollection service);
    }
}