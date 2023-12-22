using Shared.Messages;

namespace Shared.Events
{
    public class OrderCreatedEvent
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderItemMessage> OrderItems { get; set; }

    }
}
