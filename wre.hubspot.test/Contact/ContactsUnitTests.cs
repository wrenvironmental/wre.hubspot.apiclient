using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wre.hubspot.apiclient;
using wre.hubspot.apiclient.Extensions;
using wre.hubspot.test.Contact.Dto;

namespace wre.hubspot.test.Contact
{
    [TestClass]
    public class ContactsUnitTests
    {
        public ContactsUnitTests()
        {
            HubspotSettings.ApiToken = Environment.GetEnvironmentVariable("HubspotApiKey");
            HubspotSettings.BaseUrl = "https://api.hubapi.com";
        }

        [TestMethod]
        public async Task CanCreateDefaultContact()
        {
            var client = new HubspotClient();
            await client.CRM.Contacts.CreateAsync(new apiclient.CRM.Contacts.Contact()
            {
                Company = "test",
                Email = "me@me.com",
                FirstName = "Michael",
                LastName = "Prado",
                Phone = "5085961345",
                Website = "www.eu.com"
            });

            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task CanCreateDefaultContactAndGetResponse()
        {
            var client = new HubspotClient();
            await client.CRM.Contacts.CreateAsync(new CustomContact()
            {
                CustomerId = 123,
                Company = "test",
                Email = "me2@me2.com",
                FirstName = "Michael",
                LastName = "Prado",
                Phone = "5085961345",
                Website = "www.eu.com"
            });
        }
    }
}