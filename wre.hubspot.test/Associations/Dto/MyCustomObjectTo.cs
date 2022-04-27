using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.test.Associations.Dto;

public class MyCustomObjectTo : IHubspotAssociation
{
    public MyCustomObjectTo()
    {
        AssociationId = string.Empty;
    }

    public string AssociationId { get; set; }
    public string EntityName => "jobsite";
    public string AssociationName => "jobsite";
}