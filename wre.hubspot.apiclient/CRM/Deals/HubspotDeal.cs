using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Deals;

public class HubspotDeal : HubspotCommonEntity, IHubspotCustomSerialization, IHubspotDeal
{
    public HubspotDeal() : base("objects/deals") { }
    public long? Id { get; set; }

    [JsonPropertyName("dealname")]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public Decimal Amount { get; set; }

    public string? Pipeline { get; set; }

    public DateTime? CloseDate { get; set; }

    [JsonPropertyName("dealtype")]
    public string? Type { get; set; }

    [JsonPropertyName("dealstage")]
    public string? Stage { get; set; }

    [JsonPropertyName("hs_priority")]
    public string? Priority { get; set; }

    public int CustomerId { get; set; }

    public int JobsiteId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ServiceDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public DateTime? ClosedDate { get; set; }

    public DateTime? CancelledDate { get; set; }

    public string PaymentType { get; set; } = null!;

    public string JobCategory { get; set; } = null!;

    public string JobSubCategory { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string FieldOffice { get; set; } = null!;

    public string OrderId { get; set; } = null!;
}