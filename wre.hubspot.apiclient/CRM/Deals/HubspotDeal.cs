using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Deals;

public class HubspotDeal : HubspotCommonEntity, IHubspotCustomSerialization, IHubspotDeal
{
    public HubspotDeal() : base("objects/deals")
    {
    }

    public long? Id { get; set; }

    [JsonPropertyName("dealname")]
    public string? Name { get; set; }

    public string? Description { get; set; }
    public decimal Amount { get; set; }

    public string? Pipeline { get; set; }

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

    #region Remove

    public int CustomerId { get; set; }
    public int JobsiteId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ServiceDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public DateTime? CancelledDate { get; set; }
    public string PaymentType { get; set; }
    public string JobCategory { get; set; }
    public string JobSubCategory { get; set; }
    public string Region { get; set; }
    public string FieldOffice { get; set; }
    public string OrderId { get; set; }

    #endregion Remove
}