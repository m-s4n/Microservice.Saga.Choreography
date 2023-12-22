using MassTransit;
using Order.Service.DataAccess.Contexts;
using Order.Service.DataAccess.Enums;
using Shared.Events;

namespace Order.Service.Consumers
{
    public class StockNotReservedEventConsumer(AppDbContext _context) : IConsumer<StockNotReservedEvent>
    {
        public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
        {
            var order = await _context.Orders.FindAsync(context.Message.OrderId);

            order.OrderStatus = OrderStatus.Fail;

            await _context.SaveChangesAsync();

            Console.WriteLine("Stok Yetersiz Olduğu için sipariş iptal edildi");
        }
    }
}
