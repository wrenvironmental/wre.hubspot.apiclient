﻿using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.LineItems;

public class HubspotLineItem : HubspotCommonEntity, IHubspotCustomSerialization, IHubspotLineItem
{
    public HubspotLineItem() : base("objects/line_items") { }
    public long? Id { get; set; }
    public decimal Amount { get; set; }
    public decimal Quantity { get; set; }
    public string? Name { get; set; }
    [JsonPropertyName("hs_product_id")]
    public int? ProductId { get; set; }
}