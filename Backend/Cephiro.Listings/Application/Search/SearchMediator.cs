using Cephiro.Listings.Application.Search.Queries;
using MassTransit;


namespace Cephiro.Listings.Application.Search;

public static class SearchMediator
{
    public static void AddSearchMediator(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddSearchQueries();
    }
}