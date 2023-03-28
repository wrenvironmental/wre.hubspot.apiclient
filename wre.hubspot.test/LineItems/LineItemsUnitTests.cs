using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using wre.hubspot.apiclient;
using wre.hubspot.apiclient.CRM.Deals.Enums;
using wre.hubspot.apiclient.CRM.Deals;
using wre.hubspot.apiclient.CRM.LineItems;
using wre.hubspot.test.LineItems.Dto;
using wre.hubspot.apiclient.Associations;

namespace wre.hubspot.test.LineItems
{
    [TestClass]
    public class LineItemsUnitTests
    {
        public LineItemsUnitTests()
        {
            HubspotSettings.AccessToken = Environment.GetEnvironmentVariable("HubspotAccessToken");
            HubspotSettings.BaseUrl = "https://api.hubapi.com";
        }

        private async Task<CustomLineItem?> CreateLineItemAsync(string? name, int? productId = null)
        {
            var client = new HubspotClient();
            var result = await client.CRM.LineItems.CreateAsync<CustomLineItem>(new CustomLineItem()
            {
                Price = 100,
                Name = name,
                Quantity = 100,
                ProductId = productId
            });

            result.Result.Id = result.Id;
            return result.Result;
        }

        [TestMethod]
        public async Task CanCreateLineItem()
        {
            var result = await CreateLineItemAsync("TEST_LINEITEM");
            Assert.IsTrue(result.Id > 0);
        }

        [TestMethod]
        public async Task CanDeleteLineItem()
        {
            var client = new HubspotClient();
            var createdLineItem = await CreateLineItemAsync("TEST_LINEITEM");
            if (createdLineItem == null)
            {
                Assert.Fail();
                return;
            }
            await client.CRM.LineItems.DeleteAsync(createdLineItem, true);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task CanSearchLineItem()
        {
            var client = new HubspotClient();
            var name = DateTime.Now.ToString("MMddyyyyHHmmss");
            var createdLineItem = await CreateLineItemAsync(name);
            if (createdLineItem == null)
            {
                Assert.Fail();
                return;
            }
            var searchResult = await client.CRM.LineItems.SearchAsync(new HubspotLineItem()
            {
                Name = name,
            }, item => item.Name);

            Assert.IsTrue(searchResult.Total > 0);
        }

        [TestMethod]
        public async Task CanAddLineItemsToDeal()
        {
            var client = new HubspotClient();
            var name = DateTime.Now.ToString("MMddyyyyHHmmss");
            var createdLineItem = await CreateLineItemAsync(name);
            if (createdLineItem == null)
            {
                Assert.Fail();
                return;
            }

            var dealName = "0123456789";
            var dealResult = await client.CRM.Deals.CreateAsync<HubspotDeal>(new HubspotDeal()
            {
                Amount = 100,
                CloseDate = new DateTime(2022, 01, 01),
                Type = EDealType.ExistingBusiness.ToString().ToLower(),
                Description = "Test",
                Name = dealName,
                Stage = EDealStage.ClosedWon.ToString().ToLower(),
                Priority = EDealPriority.High.ToString().ToLower()
            });
            Assert.IsTrue(dealResult.Id > 0);
            var lineItemResult = await CreateLineItemAsync(null, 1428820526);
            Assert.IsTrue(lineItemResult.Id > 0);
            var objectFrom = new LineItemAssociation
            {
                AssociationId = lineItemResult.Id?.ToString() ?? string.Empty
            };
            var objectTo = new DealAssociation
            {
                AssociationId = dealResult.Id.ToString() ?? string.Empty
            };

            var association = new HubspotAssociationEntity(objectFrom, objectTo);
            await client.Associations.CreateAsync(association);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task GetLineItemsFromDeal()
        {
            var client = new HubspotClient();
            var name = DateTime.Now.ToString("MMddyyyyHHmmss");
            var createdLineItem = await CreateLineItemAsync(name);
            if (createdLineItem == null)
            {
                Assert.Fail();
                return;
            }

            var dealName = "0123456789";
            var dealResult = await client.CRM.Deals.CreateAsync<HubspotDeal>(new HubspotDeal()
            {
                Amount = 100,
                CloseDate = new DateTime(2022, 01, 01),
                Type = EDealType.ExistingBusiness.ToString().ToLower(),
                Description = "Test",
                Name = dealName,
                Stage = EDealStage.ClosedWon.ToString().ToLower(),
                Priority = EDealPriority.High.ToString().ToLower()
            });
            Assert.IsTrue(dealResult.Id > 0);
            var lineItemResult = await CreateLineItemAsync(null, 1428820526);
            Assert.IsTrue(lineItemResult.Id > 0);
            var objectFrom = new LineItemAssociation
            {
                AssociationId = lineItemResult.Id?.ToString() ?? string.Empty
            };
            var objectTo = new DealAssociation
            {
                AssociationId = dealResult.Id.ToString() ?? string.Empty
            };

            var association = new HubspotAssociationEntity(objectFrom, objectTo);
            await client.Associations.CreateAsync(association);
            Assert.IsTrue(true);

            var objectFrom2 = new DealAssociation
            {
                Id = dealResult.Id
            };
            var objectTo2 = new LineItemAssociation();
            var results = await client.Associations.ListAsync<DealAssociation, LineItemAssociation>(objectFrom2, objectTo2);
            Assert.IsNotNull(results.Result.Count == 1);
        }
    }
}
