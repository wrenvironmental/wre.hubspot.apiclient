using System;
using System.Collections.Generic;
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
            var listOfCustomObjects = new List<MyCustomObject>
            {
                new()
                {
                    Address = "test",
                    SiteName = "test",
                    JobsiteId = 123
                },
                new()
                {
                    JobsiteId = 456,
                    Address = "test",
                    SiteName = "test"
                }
            };
            var batchCreated = await client.CRM.CustomObjects.CreateAsync<MyCustomObject>(listOfCustomObjects);

            Assert.IsTrue(batchCreated.Result.Count == listOfCustomObjects.Count);
        }
    }
}