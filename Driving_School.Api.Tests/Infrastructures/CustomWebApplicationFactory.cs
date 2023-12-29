using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Driving_School.Context;

namespace Driving_School.Api.Tests.Infrastructures
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public static string EnvironmentName = "integration";

        /// <inheritdoc cref="WebApplicationFactory{TEntryPoint}.CreateHost"/>
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentName);
            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestAppConfiguration();
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<Driving_SchoolContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddSingleton(provider =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    var optionsBuilder = new DbContextOptionsBuilder<Driving_SchoolContext>()
                        .UseApplicationServiceProvider(provider)
                        .UseSqlServer(connectionString: string.Format(configuration.GetConnectionString("DefaultConnection"), Guid.NewGuid().ToString("N")));
                    return optionsBuilder.Options;
                });
            });
        }
    }
}
