using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wre.hubspot.apiclient;
using wre.hubspot.apiclient.Extensions;
using wre.hubspot.test.Associations.Dto;
using wre.hubspot.test.Contact.Dto;

namespace wre.hubspot.test.Associations
{
    [TestClass]
    public class AssociationsUnitTests
    {
        public AssociationsUnitTests()
        {
            HubspotSettings.ApiToken = Environment.GetEnvironmentVariable("HubspotApiKey");
            HubspotSettings.BaseUrl = "https://api.hubapi.com";
        }

        [TestMethod]
        public async Task CanCreateDefaultContact()
        {
            var objectFrom = new MyCustomObjectFrom
            {
                Id = "1290680849"
            };
            var objectTo = new MyCustomObjectTo
            {
                Id = "1294700464"
            };
            var client = new HubspotClient();
            await client.Associations.Associate(objectFrom).WithAsync(objectTo);

            Assert.IsTrue(true);
        }
    }
}