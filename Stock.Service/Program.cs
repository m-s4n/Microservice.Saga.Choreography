using MassTransit;
using Shared.Settings;
using Stock.Service.Consumers;
using Stock.Service.Services;

var builder = WebApplication.CreateBuilder(args);


// RabbitMQ IoC Container'a eklendi
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<OrderCreatedEventConsumer>();
    config.AddConsumer<PaymentFailedEventConsumer>();
    config.UsingRabbitMq((context, _config) =>
    {
        _config.Host(builder.Configuration["RabbitMQ"]);

        _config.ReceiveEndpoint(
            RabbitMQSettings.Stock_OrderCreatedEventQueue, 
            e => e.ConfigureConsumer<OrderCreatedEventConsumer>(context));

        _config.ReceiveEndpoint(
            RabbitMQSettings.Stock_PaymentFailedEventQueue,
            e => e.ConfigureConsumer<PaymentFailedEventConsumer>(context));


    });
});

// MongoDBService IoC Container'a eklenir
builder.Services.AddSingleton<MongoDBService>();

var app = builder.Build();




app.Run();

