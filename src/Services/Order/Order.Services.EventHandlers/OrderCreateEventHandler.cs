using System;
using Common.Helpers.MathHelpers;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Persistence.Database;
using Order.Services.EventHandlers.Commands;

namespace Order.Services.EventHandlers
{
	public class OrderCreateEventHandler :
		INotificationHandler<OrderCreateCommand>
	{
		private readonly OrderDbContext _context;
		private readonly ILogger<OrderCreateEventHandler> _logger;
		
        public OrderCreateEventHandler(OrderDbContext context, ILogger<OrderCreateEventHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(OrderCreateCommand notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("New order creation started");
            var entry = new Domain.Order();

            _logger.LogDebug("Init new transaction for order creation");
            using var trx = await _context.Database.BeginTransactionAsync(cancellationToken);

            // 01. Prepare detail
            _logger.LogInformation("Preparing detail");
            PrepareDetail(entry, notification);

            // 02. Prepare header
            _logger.LogInformation("Preparing header");
            PrepareHeader(entry, notification);

            // 03. Create order
            _logger.LogInformation("Creating order");
            _ = await _context.AddAsync(entry, cancellationToken);
            _ = await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Order {entry.OrderId} was created");

            // 04. Update Stocks
            _logger.LogInformation("Updating stock");

            // Lógica para actualizar el stock

            await trx.CommitAsync(cancellationToken);
        }

        private void PrepareHeader(Domain.Order entry, OrderCreateCommand notification)
        {
            // Header information
            entry.Status = Common.Enums.Enums.OrderStatus.Pending;
            entry.PaymentType = notification.PaymentType;
            entry.ClientId = notification.ClientId;
            entry.CreatedAt = DateTime.UtcNow;

            // Sum
            entry.Total = entry.Items.Sum(x => x.Total).Round(2);
        }

        private void PrepareDetail(Domain.Order entry, OrderCreateCommand notification)
        {
            entry.Items = notification.Items.Select(x => new Domain.OrderDetail
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice.Round(2),
                Total = (x.UnitPrice.Round(2) * x.Quantity).Round(2)
            }).ToList();
        }
    }
}

