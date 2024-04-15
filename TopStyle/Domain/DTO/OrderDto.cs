namespace TopStyle.Domain.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
