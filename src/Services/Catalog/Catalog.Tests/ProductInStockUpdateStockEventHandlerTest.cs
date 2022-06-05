using Catalog.Tests.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catalog.Domain;
using Microsoft.Extensions.Logging;
using Catalog.Service.EventHandlers;
using Moq;
using Catalog.Service.EventHandlers.Commands;
using System.Threading;
using System.Collections.Generic;
using static Catalog.Common.Enums.Enums;
using System;
using Catalog.Service.EventHandlers.Exceptions;

namespace Catalog.Tests;

[TestClass]
public class ProductInStockUpdateStockEventHandlerTest
{
    private ILogger<ProductInStockUpdateStockEventHandler> GetLogger
    {
        get
        {
            return new Mock<ILogger<ProductInStockUpdateStockEventHandler>>()
                .Object;
        }
    }

    [TestMethod]
    public void TryToSubstractStockWhenProductHasStock()
    {
        var context = ApplicationDbContextInMemory.Get();

        var productInStockId = 1;
        var productId = 1;

        context.Stocks.Add(new ProductInStock
        {
            ProductInStockId = productInStockId,
            ProductId = productId,
            Stock = 1
        });

        context.SaveChanges();

        var handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

        handler.Handle(
            new ProductInStockUpdateStockCommand
            {
                Items = new List<ProductInStockUpdateStockCommandItem>()
                {
                    new ProductInStockUpdateStockCommandItem
                    {
                        ProductId = productId,
                        Action = ProductInStockAction.Substract,
                        Stock = 1
                    }
                }
            },
            new CancellationToken()).Wait();
    }

    [TestMethod]
    [ExpectedException(typeof(ProductInStockUpdateStockEventHandlerException))]
    public void TryToSubstractStockWhenProductHasntStock()
    {
        var context = ApplicationDbContextInMemory.Get();

        var productInStockId = 2;
        var productId = 2;

        context.Stocks.Add(new ProductInStock
        {
            ProductInStockId = productInStockId,
            ProductId = productId,
            Stock = 1
        });

        context.SaveChanges();

        var handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

        try
        {
            handler.Handle(
            new ProductInStockUpdateStockCommand
            {
                Items = new List<ProductInStockUpdateStockCommandItem>()
                {
                    new ProductInStockUpdateStockCommandItem
                    {
                        ProductId = productId,
                        Action = ProductInStockAction.Substract,
                        Stock = 2
                    }
                }
            },
            new CancellationToken()).Wait();
        }
        catch (AggregateException ae)
        {
            var exception = ae.GetBaseException();

            if (exception is ProductInStockUpdateStockEventHandlerException)
            {
                throw new ProductInStockUpdateStockEventHandlerException(exception?.InnerException?.Message);
            }
            throw;
        }
    }
}
