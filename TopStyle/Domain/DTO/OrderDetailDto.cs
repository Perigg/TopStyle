﻿namespace TopStyle.Domain.DTO
{
    public class OrderDetailDto
    {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductDto Product { get; set; }

    }
}
