namespace Cephiro.Listings.Infrastructure.Data;

public class DapperConfig
{
    public const string SectionName = "ConnectionStrings";
    public string ListingsConnection { get; set; } = null!;
}