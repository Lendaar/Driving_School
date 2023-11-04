using AutoMapper;
using Driving_School.Context.Contracts.Interface;
using Driving_School.Context;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Repositories.Implementations;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Implementations;
using Microsoft.OpenApi.Models;
using Driving_School.Common;
using Driving_School.Services;
using Driving_School.Repositories;

namespace Driving_School.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void GetSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Instructor", new OpenApiInfo { Title = "Сущность инструктора", Version = "v1" });
                c.SwaggerDoc("Student", new OpenApiInfo { Title = "Сущность обучающегося", Version = "v1" });
                c.SwaggerDoc("Place", new OpenApiInfo { Title = "Сущность площадки", Version = "v1" });
                c.SwaggerDoc("Course", new OpenApiInfo { Title = "Сущность курса", Version = "v1" });
                c.SwaggerDoc("Transport", new OpenApiInfo { Title = "Сущность транспорта", Version = "v1" });
                c.SwaggerDoc("Lesson", new OpenApiInfo { Title = "Сущность занятия", Version = "v1" });
            });
        }
        public static void GetSwaggerUI(this WebApplication web)
        {
            web.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Instructor/swagger.json", "Инструкторы");
                x.SwaggerEndpoint("Student/swagger.json", "Обучающиеся");
                x.SwaggerEndpoint("Place/swagger.json", "Площадки");
                x.SwaggerEndpoint("Course/swagger.json", "Курсы");
                x.SwaggerEndpoint("Transport/swagger.json", "Транспорт");
                x.SwaggerEndpoint("Lesson/swagger.json", "Занятия");
            });
        }

        public static void AddDependences(this IServiceCollection services)
        {
            services.RegisterModule<ServiceModule>();
            services.RegisterModule<ContextModule>();
            services.RegisterModule<RepositoriesModule>();
        }

        public static void RegisterModule<TModule>(this IServiceCollection services) where TModule : Module
        {
            var type = typeof(TModule);
            var instance = Activator.CreateInstance(type) as Module; 
            if(instance == null)
            {
                return;
            }
            instance.CreateModule(services);
        }
    }
}
