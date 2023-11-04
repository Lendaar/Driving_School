using AutoMapper;
using Microsoft.OpenApi.Models;
using Driving_School.Context;
using Driving_School.Context.Contracts;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Repositories.Implementations;
using Driving_School.Services.Automappers;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Implementations;
using Driving_School.Context.Contracts.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("Instructor", new OpenApiInfo { Title = "�������� �����������", Version = "v1" });
    c.SwaggerDoc("Student", new OpenApiInfo { Title = "�������� ������������", Version = "v1" });
    c.SwaggerDoc("Place", new OpenApiInfo { Title = "�������� ��������", Version = "v1" });
    c.SwaggerDoc("Course", new OpenApiInfo { Title = "�������� �����", Version = "v1" });
    c.SwaggerDoc("Transport", new OpenApiInfo { Title = "�������� ����������", Version = "v1" });
    c.SwaggerDoc("Lesson", new OpenApiInfo { Title = "�������� �������", Version = "v1" });
});

builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<IInstructorReadRepository, InstructorReadRepository>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentReadRepository, StudentReadRepository>();

builder.Services.AddScoped<IPlaceService, PlaceService>();
builder.Services.AddScoped<IPlaceReadRepository, PlaceReadRepository>();

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseReadRepository, CourseReadRepository>();

builder.Services.AddScoped<ITransportService, TransportService>();
builder.Services.AddScoped<ITransportReadRepository, TransportReadRepository>();

builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<ILessonReadRepository, LessonReadRepositories>();

builder.Services.AddSingleton<IDriving_SchoolContext, Driving_SchoolContext>();

var mapperConfig = new MapperConfiguration(ms =>
{
    ms.AddProfile(new ServiceProfile());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("Instructor/swagger.json", "�����������");
        x.SwaggerEndpoint("Student/swagger.json", "�����������");
        x.SwaggerEndpoint("Place/swagger.json", "��������");
        x.SwaggerEndpoint("Course/swagger.json", "�����");
        x.SwaggerEndpoint("Transport/swagger.json", "���������");
        x.SwaggerEndpoint("Lesson/swagger.json", "�������");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
