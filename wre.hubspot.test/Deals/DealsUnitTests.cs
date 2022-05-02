using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wre.hubspot.apiclient;
using wre.hubspot.apiclient.CRM.Deals;
using wre.hubspot.apiclient.CRM.Deals.Enums;
using wre.hubspot.apiclient.Extensions;

namespace wre.hubspot.test.Deals
{
    [TestClass]
    public class DealsUnitTests
    {
        public DealsUnitTests()
        {
            HubspotSettings.ApiToken = Environment.GetEnvironmentVariable("HubspotApiKey");
            HubspotSettings.BaseUrl = "https://api.hubapi.com";
        }

        [TestMethod]
        public async Task CanCreateDefaultDeal()
        {
            var client = new HubspotClient();
            _ = await client.CRM.Deals.CreateAsync<HubspotDeal>(new HubspotDeal()
            {
                Amount = 100,
                CloseDate = new DateTime(2022, 01, 01),
                Type = EDealType.ExistingBusiness.ToString().ToLower(),
                Description = "Test",
                Name = "0123456789",
                Stage = EDealStage.ClosedWon.ToString().ToLower(),
                Priority = EDealPriority.High.ToString().ToLower()
            });

            Assert.IsTrue(true);
        }
    }
}