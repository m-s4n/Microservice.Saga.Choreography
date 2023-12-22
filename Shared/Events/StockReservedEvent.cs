using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class StockReservedEvent
    {
        public int BuyerId { get; set; }
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderItemMessage> OrderItems { get; set; }
    }
}
