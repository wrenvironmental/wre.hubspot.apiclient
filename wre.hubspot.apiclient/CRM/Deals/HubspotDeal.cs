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
    public decimal Amount { get; set; }

    public string? Pipeline { get; set; } = null!;

    public DateTime? CloseDate { get; set; }
    /// <summary>
    /// You can refer to EDealType
    /// </summary>
    [JsonPropertyName("dealtype")]
    public string? Type { get; set; }
    /// <summary>
    /// You can refer to EDealStage
    /// </summary>
    [JsonPropertyName("dealstage")]
    public string? Stage { get; set; }
    [JsonPropertyName("hs_priority")]
    public string? Priority { get; set; }
}