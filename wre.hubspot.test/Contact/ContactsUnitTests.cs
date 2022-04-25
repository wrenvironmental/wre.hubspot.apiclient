using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wre.hubspot.apiclient;
using wre.hubspot.apiclient.CRM.Contacts;
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
            var hubspotContact = new HubspotContact()
            {
                Company = "test",
                Email = "me@me.com",
                FirstName = "Michael",
                LastName = "Prado",
                Phone = "5085991234",
                Website = "www.me.com"
            };
            var contactSearch = new CustomContact()
            {
                Email = hubspotContact.Email
            };

            var existingContact = await client.CRM.Contacts.SearchAsync(contactSearch, contact => contact.Email);
            if (existingContact.Results.Count == 1)
            {
                await client.CRM.Contacts.DeleteAsync(hubspotContact, existingContact.Results[0].Id ?? 0, true);
            }
            
            var createdContact = await client.CRM.Contacts.CreateAsync<HubspotContact>(hubspotContact);

            Assert.IsTrue(createdContact.Id > 0);
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

        [TestMethod]
        public async Task CanSearch()
        {
            var client = new HubspotClient();
            var contactSearch = new CustomContact()
            {
                Email = "me2@me2.com"
            };

            var results = await client.CRM.Contacts.SearchAsync(contactSearch,
                contact => contact.Email);

            Assert.IsTrue(results.Total == 1);
        }
    }
}