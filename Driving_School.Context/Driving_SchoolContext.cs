using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.ContextConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Driving_School.Context
{
    public class Driving_SchoolContext : DbContext,
        IDriving_SchoolContext,
        IDbRead,
        IDbWriter,
        IUnitOfWork
    {
        /// <summary>
        /// Контекст работы с БД
        /// </summary>
        /// <remarks>
        /// 1) dotnet tool install --global dotnet-ef
        /// 2) dotnet tool update --global dotnet-ef
        /// 3) dotnet ef migrations add [name] --project TimeTable203.Context\TimeTable203.Context.csproj
        /// 4) dotnet ef database update --project TimeTable203.Context\TimeTable203.Context.csproj
        /// 5) dotnet ef database update [targetMigrationName] --TimeTable203.Context\TimeTable203.Context.csproj
        /// </remarks>
        public Driving_SchoolContext(DbContextOptions<Driving_SchoolContext> options) : base (options) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Transport> Transports { get; set; }

        public DbSet<Course> Courses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IContextConfigurationAnchor).Assembly);
        }

        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        void IDbWriter.Add<TEntities>(TEntities entity)
            => base.Entry(entity).State = EntityState.Added;

        void IDbWriter.Update<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Modified;

        void IDbWriter.Delete<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Deleted;


        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }
    }
}