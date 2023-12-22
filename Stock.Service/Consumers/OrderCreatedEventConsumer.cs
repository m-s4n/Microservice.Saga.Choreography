using MassTransit;
using Shared.Events;
using Shared.Settings;

namespace Stock.Service.Consumers
{
    public class OrderCreatedEventConsumer(
        ISendEndpointProvider _sendEndpointProvider,
        IPublishEndpoint  _publishEndpoint) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var sendEndPoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.Payment_StockReservedEventQueue}"));

            if (true)
            {
                // Stock başarılı
                // Payment'ı uyaracak event fırlatılır
                StockReservedEvent stockReservedEvent = new()
                {
                    BuyerId = context.Message.BuyerId,
                    OrderId = context.Message.OrderId,
                    OrderItems = context.Message.OrderItems,
                    TotalPrice = context.Message.TotalPrice,
                };
                await sendEndPoint.Send(stockReservedEvent);
                Console.WriteLine("Stok İşlemleri Başarılı");
            }
            else
            {
                // Stock başarısız
                // Order'ı uyaracak event fırlatılır
                // Bu durum ile ilgilenen birden çok servis olabilir direkt bir kuyruğa göndermek yerine exchange'e gönderilebilir
                // yani publish edilebilir
                StockNotReservedEvent stockNotReservedEvent = new()
                {
                    BuyerId = context.Message.BuyerId,
                    OrderId = context.Message.OrderId,
                    Message = "Stock Yetersiz"
                };

                await _publishEndpoint.Publish(stockNotReservedEvent);
                Console.WriteLine("Stok İşlemleri Başarısız");
            }
        }
    }
}
