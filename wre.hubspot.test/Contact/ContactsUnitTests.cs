using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wre.hubspot.apiclient;
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
                Phone = "5085991234",
                Website = "www.me.com"
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

        [TestMethod]
        public async Task CanSearch()
        {
            var client = new HubspotClient();
            var contactSearch = new CustomContact()
            {
                Email = "me2@me2.com"
            };

            try
            {
                var results = await client.CRM.Contacts.SearchAsync<CustomContact, CustomContact>(contactSearch,
                    contact => contact.Email);

                Assert.IsTrue(results.Total == 1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            
        }
    }
}