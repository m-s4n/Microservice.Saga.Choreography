using MassTransit;
using Order.Service.DataAccess.Contexts;
using Order.Service.DataAccess.Enums;
using Shared.Events;

namespace Order.Service.Consumers
{
    public class PaymentCompletedEventConsumer(AppDbContext _context) : IConsumer<PaymentCompletedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var order = await _context.Orders.FindAsync(context.Message.OrderId);

            order.OrderStatus = OrderStatus.Completed;

            await _context.SaveChangesAsync();
            Console.WriteLine("Order başarılı olarak güncellendi");
        }
    }
}
