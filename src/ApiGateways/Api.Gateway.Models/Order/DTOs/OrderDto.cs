using Api.Gateway.Models.Customer.DTOs;
using Api.Gateway.Models.Order.Common;

namespace Api.Gateway.Models.Order.DTOs;

public class OrderDto
{
    public int OrderId { get; set; } = 0;
    public ClientDto Client { get; set; } = new();
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public OrderPayment PaymentType { get; set; } = OrderPayment.CreditCard;
    public int ClientId { get; set; } = 0;
    public ICollection<OrderDetailDto> Items { get; set; } = new List<OrderDetailDto>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; } = 0;
}

