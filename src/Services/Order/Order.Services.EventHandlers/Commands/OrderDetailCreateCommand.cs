using System;
using MediatR;

namespace Order.Services.EventHandlers.Commands
{
	public class OrderDetailCreateCommand : INotification
	{
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

