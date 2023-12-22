namespace Order.Service.DTOs
{
    public record CreateOrderDto
    {
        public int BuyerId { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
