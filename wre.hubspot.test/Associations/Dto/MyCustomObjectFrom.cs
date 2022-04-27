using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.test.Associations.Dto;

public class MyCustomObjectFrom : IHubspotAssociation
{
    public MyCustomObjectFrom()
    {
        AssociationId = string.Empty;
    }
    
    public string AssociationId { get; set; }
    public string EntityName => "orders";
    public string AssociationName => "work_order";
}