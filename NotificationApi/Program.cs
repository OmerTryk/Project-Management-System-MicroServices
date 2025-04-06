using MassTransit;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using NotificationApi.Consumers.ProjectConsumer;
using NotificationApi.Consumers.UserConsumers;
using NotificationApi.Context;
using Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<NotificationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<UserCreatedEventConsumer>();
    configure.AddConsumer<ProjectCreatedEventConsumer>();
    configure.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host(builder.Configuration["RabbitMQ"]);

        _configurator.ReceiveEndpoint(RabbitMQSettings.UserCreatedEventQueue, e => e.ConfigureConsumer<UserCreatedEventConsumer>(context));
        _configurator.ReceiveEndpoint(RabbitMQSettings.ProjectCreatedEventQueue, e => e.ConfigureConsumer<ProjectCreatedEventConsumer>(context));
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
