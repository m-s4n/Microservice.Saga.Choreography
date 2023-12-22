using MassTransit;
using Payment.Service.Consumers;
using Shared.Settings;

var builder = WebApplication.CreateBuilder(args);


// RabbitMQ IoC Container'a eklenir
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<StockReservedEventConsumer>();

    config.UsingRabbitMq((context, _config) =>
    {
        _config.Host(builder.Configuration["RabbitMQ"]);
        _config.ReceiveEndpoint(
            RabbitMQSettings.Payment_StockReservedEventQueue,
            e => e.ConfigureConsumer<StockReservedEventConsumer>(context));
    });
});


var app = builder.Build();



app.Run();
