using MassTransit;
using Shared.Events;

namespace Stock.Service.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            Console.WriteLine("Ödeme İşlemi başarısız olduğu için Stock İşlemleri Geri Alındı");
        }
    }
}
