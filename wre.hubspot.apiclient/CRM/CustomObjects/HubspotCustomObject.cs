using wre.hubspot.apiclient.Common;

namespace wre.hubspot.apiclient.CRM.CustomObjects;

public abstract class HubspotCustomObject : HubspotCommonEntity
{
    protected HubspotCustomObject() : base("objects") { }
}