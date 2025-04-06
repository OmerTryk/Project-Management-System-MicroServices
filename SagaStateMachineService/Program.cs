using MassTransit;
using Microsoft.EntityFrameworkCore;
using SagaStateMachineService.StateDbContexts;
using SagaStateMachineService.StateInstance;
using SagaStateMachineService.StateMachine;
using Shared.Settings;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<ProjectStateDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectDbConnection")));

builder.Services.AddDbContext<UserStateDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserDbConnection")));

builder.Services.AddMassTransit(configure =>
{
    configure.AddSagaStateMachine<UserStateMachine, UserStateInstance>()
        .EntityFrameworkRepository(options =>
        {
            options.ExistingDbContext<UserStateDbContext>(); 
            options.ConcurrencyMode = ConcurrencyMode.Pessimistic; 
        });

    configure.AddSagaStateMachine<ProjectStateMachine, ProjectStateInstance>()
        .EntityFrameworkRepository(options =>
        {
            options.ExistingDbContext<ProjectStateDbContext>();
            options.ConcurrencyMode = ConcurrencyMode.Pessimistic;
        });

    configure.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration["RabbitMQ"]);

        configurator.ReceiveEndpoint(RabbitMQSettings.StateMachineQueue, e =>
        {
            e.ConfigureSaga<UserStateInstance>(context);
        });

        configurator.ReceiveEndpoint(RabbitMQSettings.ProjectStateMachineQueue, e =>
        {
            e.ConfigureSaga<ProjectStateInstance>(context);
        });
    });
});

var host = builder.Build();
host.Run();
