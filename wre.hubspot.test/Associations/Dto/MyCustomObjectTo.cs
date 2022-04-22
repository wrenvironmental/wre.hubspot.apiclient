using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.test.Associations.Dto;

public class MyCustomObjectTo : IHubspotAssociation
{
    public MyCustomObjectTo()
    {
        Id = string.Empty;
    }

    public string Id { get; set; }
    public string EntityName => "jobsite";
    public string AssociationName => "jobsite";
}