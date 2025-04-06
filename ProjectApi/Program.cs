using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Context;
using ProjectApi.Mappers;
using ProjectApi.Mappers.impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ProjectDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddMassTransit(configure =>
{
    configure.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host(builder.Configuration["RabbitMQ"]);
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()  // Tüm kaynaklardan gelen taleplere izin verir
               .AllowAnyMethod()  // Tüm HTTP metodlarýna izin verir
               .AllowAnyHeader(); // Tüm baþlýklara izin verir
    });
});

builder.Services.AddScoped<IMapper,Mapper>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Ana sayfayý Swagger UI olarak belirler
    });
}
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();


app.MapControllers();

app.Run();
