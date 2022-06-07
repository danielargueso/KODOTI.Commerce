using MediatR;
using static Order.Common.Enums.Enums;

namespace Order.Services.EventHandlers.Commands
{
    public class OrderCreateCommand : INotification
	{
        public OrderPayment PaymentType { get; set; }
        public int ClientId { get; set; }
        public ICollection<OrderDetailCreateCommand> Items { get; set; } = new List<OrderDetailCreateCommand>();
    }
}

