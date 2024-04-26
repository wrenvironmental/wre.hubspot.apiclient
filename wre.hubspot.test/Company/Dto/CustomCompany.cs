namespace wre.hubspot.test.Company.Dto
{
    public class CustomCompany : apiclient.CRM.Companies.HubspotCompany
    {
        public int JobsiteId { get; set; }
        public int? Gallons { get; set; }
    }
}
