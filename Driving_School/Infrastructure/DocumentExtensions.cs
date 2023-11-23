using Microsoft.OpenApi.Models;

namespace Driving_School.Api.Infrastructure
{
    static internal class DocumentExtensions
    {
        public static void GetSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Employee", new OpenApiInfo { Title = "Сущность работника", Version = "v1" });
                c.SwaggerDoc("Person", new OpenApiInfo { Title = "Сущность персоны", Version = "v1" });
                c.SwaggerDoc("Place", new OpenApiInfo { Title = "Сущность площадки", Version = "v1" });
                c.SwaggerDoc("Course", new OpenApiInfo { Title = "Сущность курса", Version = "v1" });
                c.SwaggerDoc("Transport", new OpenApiInfo { Title = "Сущность транспорта", Version = "v1" });
                c.SwaggerDoc("Lesson", new OpenApiInfo { Title = "Сущность занятия", Version = "v1" });
                var filePath = Path.Combine(AppContext.BaseDirectory, "Driving_School.Api.xml");
                c.IncludeXmlComments(filePath);
            });
        }
        public static void GetSwaggerUI(this WebApplication web)
        {
            web.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Employee/swagger.json", "Работники");
                x.SwaggerEndpoint("Person/swagger.json", "Персоны");
                x.SwaggerEndpoint("Place/swagger.json", "Площадки");
                x.SwaggerEndpoint("Course/swagger.json", "Курсы");
                x.SwaggerEndpoint("Transport/swagger.json", "Транспорт");
                x.SwaggerEndpoint("Lesson/swagger.json", "Занятия");
            });
        }
    }
}
