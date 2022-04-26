using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wre.hubspot.apiclient;
using wre.hubspot.apiclient.CRM.CustomObjects;
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
            client.CRM.CustomObjects = new HubspotCustomObjectClient<HubspotCustomObject>("");

            await client.CRM.CustomObjects.CreateAsync(new Jobsite()
            {
             JobsiteId   = 123
            });

            Assert.IsTrue(true);
        }
    }
}