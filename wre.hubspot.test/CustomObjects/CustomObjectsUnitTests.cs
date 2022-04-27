using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wre.hubspot.apiclient;
using wre.hubspot.test.CustomObjects.Dto;

namespace wre.hubspot.test.CustomObjects
{
    [TestClass]
    public class CustomObjectsUnitTests
    {
        public CustomObjectsUnitTests()
        {
            HubspotSettings.ApiToken = Environment.GetEnvironmentVariable("HubspotApiKey");
            HubspotSettings.BaseUrl = "https://api.hubapi.com";
        }

        [TestMethod]
        public async Task CanCreateCustomObject()
        {
            var client = new HubspotClient();
            var customObj = new MyCustomObject
            {
                Address = "test",
                SiteName = "test",
                JobsiteId = new Random().Next(int.MinValue, int.MaxValue)
            };
            var batchCreated = await client.CRM.CustomObjects.CreateAsync<MyCustomObject>(customObj);

            Assert.IsTrue(batchCreated.Id > 0);
        }

        [TestMethod]
        public async Task CanCreateListOfCustomObject()
        {
            var client = new HubspotClient();
            var rnd = new Random();
            var listOfCustomObjects = new List<MyCustomObject>
            {
                new()
                {
                    Address = "test",
                    SiteName = "test",
                    JobsiteId = rnd.Next(int.MinValue, int.MaxValue)
                },
                new()
                {
                    JobsiteId = rnd.Next(int.MinValue, int.MaxValue),
                    Address = "test",
                    SiteName = "test"
                }
            };
            var batchCreated = await client.CRM.CustomObjects.CreateAsync<MyCustomObject>(listOfCustomObjects);

            Assert.IsTrue(batchCreated.Result.Count == listOfCustomObjects.Count);
        }

        [TestMethod]
        public async Task CanCreateAndDeleteListOfCustomObject()
        {
            var client = new HubspotClient();
            var rnd = new Random();
            var listOfCustomObjects = new List<MyCustomObject>
            {
                new()
                {
                    Address = new Guid().ToString(),
                    SiteName = "test",
                    JobsiteId = rnd.Next(int.MinValue, int.MaxValue)
                },
                new()
                {
                    JobsiteId = rnd.Next(int.MinValue, int.MaxValue),
                    Address = new Guid().ToString(),
                    SiteName = "test"
                }
            };
            var batchCreated = await client.CRM.CustomObjects.CreateAsync<MyCustomObject>(listOfCustomObjects);
            var itemsToBeDeleted = new List<MyCustomObject>();
            foreach (var item in batchCreated.Result)
            {
                item.Result.Id = item.Id;
                itemsToBeDeleted.Add(item.Result);
            }
            Assert.IsTrue(batchCreated.Result.Count == listOfCustomObjects.Count);
            await client.CRM.CustomObjects.DeleteAsync(itemsToBeDeleted);


            var search1 = await client.CRM.Contacts.SearchAsync(itemsToBeDeleted.First(), customObj => customObj.Address);
            var search2 = await client.CRM.Contacts.SearchAsync(itemsToBeDeleted.Skip(1).First(), customObj => customObj.Address);

            Assert.IsTrue(search1.Total == 0 && search2.Total == 0);
        }
    }
}