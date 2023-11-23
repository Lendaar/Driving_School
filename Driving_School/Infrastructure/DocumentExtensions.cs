using Microsoft.OpenApi.Models;

namespace Driving_School.Api.Infrastructure
{
    static internal class DocumentExtensions
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
    }
}
