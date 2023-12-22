using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Service.Consumers;
using Order.Service.DataAccess.Contexts;
using Shared.Settings;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//RabbitMQ
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<PaymentCompletedEventConsumer>();
    config.AddConsumer<PaymentFailedEventConsumer>();
    config.AddConsumer<StockNotReservedEventConsumer>();

    config.UsingRabbitMq((context, _config) =>
    {
        _config.Host(builder.Configuration["RabbitMQ"]);

        _config.ReceiveEndpoint(
            RabbitMQSettings.Order_PaymentCompletedEventQueue, 
            e => e.ConfigureConsumer<PaymentCompletedEventConsumer>(context));

        _config.ReceiveEndpoint(
            RabbitMQSettings.Order_PaymentFailedEventQueue, 
            e => e.ConfigureConsumer<PaymentFailedEventConsumer>(context));

        _config.ReceiveEndpoint(
            RabbitMQSettings.Order_StockNotReservedEventQueue,
            e => e.ConfigureConsumer<StockNotReservedEventConsumer>(context));
    });
});

//PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();



app.Run();

