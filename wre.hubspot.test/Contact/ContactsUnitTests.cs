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
            HubspotSettings.AccessToken = Environment.GetEnvironmentVariable("HubspotAccessToken");
            HubspotSettings.BaseUrl = "https://api.hubapi.com";
        }

        public static async Task<HubspotContact> CreateNewContact()
        {
            var rnd = new Random();
            var client = new HubspotClient();
            var hubspotContact = new HubspotContact()
            {
                Company = "Company " + rnd.Next(1, 100),
                Email = Guid.NewGuid().ToString()[10..].Replace("-", "") + "@me.com",
                FirstName = "Test",
                LastName = "Contact" + rnd.Next(1, 100)
            };

            var createdContact = await client.CRM.Contacts.CreateAsync<HubspotContact>(hubspotContact);
            createdContact.Result.Id = createdContact.Id;
            return createdContact.Result;
        }

        [TestMethod]
        public async Task CanCreateDefaultContact()
        {
            var client = new HubspotClient();
            var hubspotContact = await CreateNewContact();
            var contactSearch = new CustomContact()
            {
                Email = hubspotContact.Email
            };

            var existingContact = await client.CRM.Contacts.SearchAsync(contactSearch, contact => contact.Email);
            if (existingContact.Results.Count == 1)
            {
                hubspotContact.Id = existingContact.Results[0].Id;
                await client.CRM.Contacts.DeleteAsync(hubspotContact, true);
            }
            
            var createdContact = await client.CRM.Contacts.CreateAsync<HubspotContact>(hubspotContact);

            Assert.IsTrue(createdContact.Id > 0);
        }

        [TestMethod]
        public async Task CanCreateDefaultContactAndGetResponse()
        {
            var newContact = await CreateNewContact();
            Assert.IsNotNull(newContact);
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