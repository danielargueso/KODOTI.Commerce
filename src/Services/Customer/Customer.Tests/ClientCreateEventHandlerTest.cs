using System;
using System.Linq;
using System.Threading;
using Customer.Service.EventHandlers;
using Customer.Service.EventHandlers.Commands;
using Customer.Tests.Config;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Customer.Tests;

[TestClass]
public class ClientCreateEventHandlerTest
{
    private ILogger<ClientCreateEventHandler> GetLogger => new Mock<ILogger<ClientCreateEventHandler>>().Object;

    [TestMethod]
    public void TryToCreateClient()
    {
        var context = CustomerDbContextInMemory.Get();

        var randomClientName = $"{Guid.NewGuid()}::{new Random().Next(0,9999):0000}::{Guid.NewGuid()}";

        var handler = new ClientCreateEventHandler(context, GetLogger);

        handler.Handle(
            new ClientCreateCommand()
            {
                Name = randomClientName
            },
            new CancellationToken()).Wait();

        var clients = context.Clients.Where(x => x.Name == randomClientName);

        Assert.IsTrue(clients.Any() && clients.Count() == 1);
    }

}

