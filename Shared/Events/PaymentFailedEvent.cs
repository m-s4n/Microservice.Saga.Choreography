﻿using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class PaymentFailedEvent
    {
        public int OrderId { get; set; }
        public string Message { get; set; }
        public ICollection<OrderItemMessage> OrderItems { get; set; }
    }
}
