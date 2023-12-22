using MassTransit;
using Shared.Events;
using Shared.Settings;

namespace Payment.Service.Consumers
{
    public class StockReservedEventConsumer(
        IPublishEndpoint _publishEndpoint, 
        ISendEndpointProvider _sendEndpointProvider) : IConsumer<StockReservedEvent>
    {
        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            if (true)
            {
                // ödeme başarılı
                PaymentCompletedEvent paymentCompletedEvent = new()
                {
                    OrderId = context.Message.OrderId,
                };

                var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new($"queue:{RabbitMQSettings.Order_PaymentCompletedEventQueue}"));
                await sendEndpoint.Send(paymentCompletedEvent);
                Console.WriteLine("Ödeme Başarılı");
            }
            else
            {
                // ödeme başarısız
                PaymentFailedEvent paymentFailedEvent = new()
                {
                    OrderId = context.Message.OrderId,
                    OrderItems = context.Message.OrderItems,
                    Message = "Bakiye Yetersiz"
                };

                await _publishEndpoint.Publish(paymentFailedEvent);
                Console.WriteLine("Ödeme Başarısız");
            }
        }
    }
}
