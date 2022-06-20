﻿namespace Api.Gateway.Models.Order.DTOs;

public class OrderDetailDto
{
    public int OrderDetailId { get; set; } = 0;
    public int ProductId { get; set; } = 0;
    public int Quantity { get; set; } = 0;
    public decimal UnitPrice { get; set; } = 0;
    public decimal Total { get; set; } = 0;
}