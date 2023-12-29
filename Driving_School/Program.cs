using Driving_School.Api.Infrastructure;
using Driving_School.Context;
using Driving_School.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(x =>
{
    x.Filters.Add<Driving_SchoolExceptionFilter>();
})
  .AddControllersAsServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.GetSwaggerGen();
builder.Services.AddDependences();

var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<Driving_SchoolContext>(options => options.UseSqlServer(conString), ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.GetSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }