using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.test.Associations.Dto;

public class MyCustomObjectFrom : IHubspotAssociation
{
    public MyCustomObjectFrom()
    {
        Id = string.Empty;
    }
    
    public string Id { get; set; }
    public string EntityName => "orders";
    public string AssociationName => "work_order";
}