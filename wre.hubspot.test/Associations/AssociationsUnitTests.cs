using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wre.hubspot.apiclient;
using wre.hubspot.apiclient.Associations;
using wre.hubspot.apiclient.Common;
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
                AssociationId = "1347772089"
            };
            var objectTo = new MyCustomObjectTo
            {
                AssociationId = "1346559110"
            };
            var client = new HubspotClient();
            var association = new HubspotAssociationEntity(objectFrom, objectTo);
            await client.Associations.CreateAsync(association);

            Assert.IsTrue(true);
        }
    }
}