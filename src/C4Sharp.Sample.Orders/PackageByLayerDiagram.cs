using C4Sharp.Models;
using C4Sharp.Models.Diagrams.Core;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Sample.Orders
{
    using static Structures;

    public class PackageByLayerDiagram
    {
        public static ComponentDiagram Diagram => new ComponentDiagram()
        {
            Title = "Package By Layer Diagram",
            ShowLegend = false,
            Structures = new Structure[]
            {
                WebLayer(),
                ServiceLayer(),
                DataLayer,
            },
            Relationships = new[]
            {
                (OrdersController > IOrdersService)[Position.Down],
                (OrdersService > IOrdersRepository)[Position.Down],
            }
        };

        private static ContainerBoundary WebLayer()
        {
            var ordersController = OrdersController;
            ordersController.AddTag(Tag.Deprecated);

            return new("WebLayer", "web")
            {
                Components = new[]
            {
               ordersController
            },
            };
        }

        private static ContainerBoundary ServiceLayer()
        {
            var iOrdersService = IOrdersService;
            iOrdersService.AddTag(Tag.Planned);

            var ordersService = OrdersService;
            ordersService.AddTag(Tag.InProgress);

            return new("ServiceLayer", "service")
            {
                Components = new[]
                {
                    iOrdersService,
                    ordersService
                },
                Relationships = new[]
                {
                    (IOrdersService < OrdersService)["implements"],
                }
            };
        }

        private static ContainerBoundary DataLayer => new("DataLayer", "data")
        {
            Components = new[]
            {
                IOrdersRepository,
                OrdersRepository
            },
            Relationships = new[]
            {
                (IOrdersRepository < OrdersRepository)["implements"],
            }
        };
    }
}