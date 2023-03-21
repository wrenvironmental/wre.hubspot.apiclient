using System;
using wre.hubspot.apiclient.CRM.Deals;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.test.LineItems.Dto
{
    internal class CustomDeal : HubspotDeal
    {
        
    }

    internal class DealAssociation : HubspotDeal, IHubspotAssociation
    {
        public string AssociationId { get; set; }

        public string EntityName => "deals";

        public string AssociationName => "deal";
    }
}
