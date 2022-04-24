using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Deals;

public interface IHubspotDeal : IHubspotEntity
{
    string? Name { get; set; }
    string? Description { get; set; }
    decimal Amount { get; set; }
    string Pipeline { get; set; }
    DateTime CloseDate { get; set; }
    string? Type { get; set; }
    string? Stage { get; set; }
    string? Priority { get; set; }
}