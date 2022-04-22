using wre.hubspot.apiclient.Common;

namespace wre.hubspot.apiclient.CRM.CustomObjects;

public abstract class CustomObject : HubspotCommonEntity
{
    protected CustomObject() : base("objects") { }
}