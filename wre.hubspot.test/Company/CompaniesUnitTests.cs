using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wre.hubspot.apiclient;
using wre.hubspot.apiclient.CRM.Companies;
using wre.hubspot.test.Company.Dto;

namespace wre.hubspot.test.Company
{
    [TestClass]
    public class CompaniesUnitTests
    {
        public CompaniesUnitTests()
        {
            HubspotSettings.AccessToken = Environment.GetEnvironmentVariable("HubspotAccessToken");
            HubspotSettings.BaseUrl = "https://api.hubapi.com";
        }

        public static async Task<HubspotCompany> CreateNewCompany()
        {
            var rnd = new Random();
            var client = new HubspotClient();
            var hubspotCompany = new CustomCompany()
            {
                Name = "Company " + rnd.Next(1, 100),
                Phone = Guid.NewGuid().ToString()[10..].Replace("-", "") + "@me.com",
                Contact = "Test",
                Address = "100 Main Street",
                Address2 = null,
                City = "Boston",
                State = "MA",
                Zip = "01752",
                Gallons = 1000,
                JobsiteId = 123
            };

            var createdContact = await client.CRM.Companies.CreateAsync<HubspotCompany>(hubspotCompany);
            createdContact.Result.Id = createdContact.Id;
            return createdContact.Result;
        }

        [TestMethod]
        public async Task CanCreateDefaultContact()
        {
            var client = new HubspotClient();
            var hubspotCompany = await CreateNewCompany();
            var companySearch = new CustomCompany()
            {
                Name = hubspotCompany.Name
            };

            var existingContact = await client.CRM.Companies.SearchAsync(companySearch, company => company.Name);
            if (existingContact.Results.Count == 1)
            {
                hubspotCompany.Id = existingContact.Results[0].Id;
                await client.CRM.Companies.DeleteAsync(hubspotCompany, true);
            }

            var createdContact = await client.CRM.Companies.CreateAsync<HubspotCompany>(hubspotCompany);

            Assert.IsTrue(createdContact.Id > 0);
        }

        [TestMethod]
        public async Task CanCreateDefaultContactAndGetResponse()
        {
            var newContact = await CreateNewCompany();
            Assert.IsNotNull(newContact);
        }

        [TestMethod]
        public async Task CanSearch()
        {
            var client = new HubspotClient();
            var companySearch = new CustomCompany()
            {
                Name = "me2@me2.com"
            };

            var results = await client.CRM.Companies.SearchAsync(companySearch,
                contact => contact.Name);

            Assert.IsTrue(results.Total == 1);
        }
    }
}