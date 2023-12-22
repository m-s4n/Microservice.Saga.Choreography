using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Service.DataAccess.Contexts;
using Order.Service.DataAccess.Entities;
using Order.Service.DataAccess.Enums;
using Order.Service.DTOs;
using Shared.Events;
using Shared.Messages;
using Shared.Settings;
using OrderEntity = Order.Service.DataAccess.Entities.Order;

namespace Order.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(AppDbContext _context, IPublishEndpoint _publishEndpoint) : ControllerBase
    {
        [HttpPost("create-order")]
        public async Task CreateOrder(CreateOrderDto model)
        {
            OrderEntity order = new()
            {
                BuyerId = model.BuyerId,
                OrderItems = model.OrderItems.Select(oi => new OrderItem()
                {
                    ProductId = oi.ProductId,
                    Price = oi.Price,
                    Count = oi.Count,
                }).ToList(),

                OrderStatus = OrderStatus.Suspend,
                CreatedDate = DateTime.UtcNow,
                TotalPrice = model.OrderItems.Sum(oi => oi.Price * oi.Count)
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            
            OrderCreatedEvent orderCreatedEvent = new()
            {
                BuyerId = order.BuyerId,
                OrderItems = order.OrderItems.Select(oi => new OrderItemMessage()
                {
                    Count = oi.Count,
                    Price = oi.Price,
                    ProductId = oi.ProductId,
                }).ToList(),
                OrderId = order.Id,
                TotalPrice = order.TotalPrice,
            };

            await _publishEndpoint.Publish(orderCreatedEvent);

        }
    }
}
