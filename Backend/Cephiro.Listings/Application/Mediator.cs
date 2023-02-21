using System.Reflection;
using Cephiro.Listings.Application.Catalog;
using Cephiro.Listings.Application.Reservation;
using MassTransit;

namespace Cephiro.Listings.Application;

public static class Mediator
{
    public static void AddListingMediator(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddConsumers(Assembly.GetExecutingAssembly());
        
        cfg.AddCatalogMediator();
        cfg.AddReservationMediator();
    }
}