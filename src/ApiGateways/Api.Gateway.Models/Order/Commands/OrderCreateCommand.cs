using Api.Gateway.Models.Order.Common;

namespace Api.Gateway.Models.Order.Commands;

public class OrderCreateCommand
{
    public OrderPayment PaymentType { get; set; }
    public int ClientId { get; set; }
    public ICollection<OrderDetailCreateCommand> Items { get; set; } = new List<OrderDetailCreateCommand>();
}

