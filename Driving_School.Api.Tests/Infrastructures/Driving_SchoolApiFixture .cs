using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context;
using Driving_School.Context.Contracts.Interface;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Driving_School.Api.Tests.Infrastructures
{
    public class Driving_SchoolApiFixture : IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory factory;
        private Driving_SchoolContext? driving_schoolContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Driving_SchoolApiFixture"/>
        /// </summary>
        public Driving_SchoolApiFixture()
        {
            factory = new CustomWebApplicationFactory();
        }

        Task IAsyncLifetime.InitializeAsync() => Driving_schoolContext.Database.MigrateAsync();

        async Task IAsyncLifetime.DisposeAsync()
        {
            await Driving_schoolContext.Database.EnsureDeletedAsync();
            await Driving_schoolContext.Database.CloseConnectionAsync();
            await Driving_schoolContext.DisposeAsync();
            await factory.DisposeAsync();
        }

        public CustomWebApplicationFactory Factory => factory;

        public IDriving_SchoolContext Context => Driving_schoolContext;

        public IUnitOfWork UnitOfWork => Driving_schoolContext;

        internal Driving_SchoolContext Driving_schoolContext
        {
            get
            {
                if (driving_schoolContext != null)
                {
                    return driving_schoolContext;
                }

                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                driving_schoolContext = scope.ServiceProvider.GetRequiredService<Driving_SchoolContext>();
                return driving_schoolContext;
            }
        }
    }
}
