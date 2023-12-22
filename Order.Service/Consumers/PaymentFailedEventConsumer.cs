using MassTransit;
using Order.Service.DataAccess.Contexts;
using Order.Service.DataAccess.Enums;
using Shared.Events;

namespace Order.Service.Consumers
{
    public class PaymentFailedEventConsumer(AppDbContext _context) : IConsumer<PaymentFailedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            var order = await _context.Orders.FindAsync(context.Message.OrderId);
            order.OrderStatus = OrderStatus.Fail;

            await _context.SaveChangesAsync();

            Console.WriteLine("Ödeme İşlemi başarısız olduğu için Order Başarısız olarak güncellendi");
        }
    }
}
