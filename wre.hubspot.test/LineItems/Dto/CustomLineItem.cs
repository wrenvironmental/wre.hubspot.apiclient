using wre.hubspot.apiclient.CRM.LineItems;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.test.LineItems.Dto
{
    internal class CustomLineItem : HubspotLineItem
    {
    }

    internal class LineItemAssociation : HubspotLineItem, IHubspotAssociation
    {
        public string AssociationId { get; set; }

        public string EntityName => "line_items";

        public string AssociationName => "line_item";
    }
}
