using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Companies
{
    public interface IHubspotCompany : IHubspotEntity
    {
        string? Name { get; set; }
        string? Phone { get; set; }
        string? Contact { get; set; }
        string? Address { get; set; }
        string? Address2 { get; set; }
        string? City { get; set; }
        string? State { get; set; }
        string? Zip { get; set; }
    }
}
