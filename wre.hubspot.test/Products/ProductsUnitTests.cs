using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wre.hubspot.apiclient;
using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.CRM.Products;
using wre.hubspot.apiclient.Extensions;

namespace wre.hubspot.test.Products
{
    [TestClass]
    public class ProductsUnitTests
    {
        public ProductsUnitTests()
        {
            HubspotSettings.ApiToken = Environment.GetEnvironmentVariable("HubspotApiKey");
            HubspotSettings.BaseUrl = "https://api.hubapi.com";
        }

        [TestMethod]
        public async Task CanCreateDefaultProduct()
        {
            var client = new HubspotClient();
            await client.CRM.Products.CreateAsync(new Product()
            {
                Description = "Test",
                Name = "0123456789",
                Code = "123"
            });

            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task CanCreateDefaultProductAndGetResult()
        {
            var client = new HubspotClient();
            var createdProduct
                = await client.CRM.Products.CreateAsync<Product, HubspotStandardResponseModel<Product>>(new Product
                {
                    Description = "Test",
                    Name = "0123456789",
                    Code = "123"
                });

            Assert.IsTrue(createdProduct.Id > 0);
        }

        [TestMethod]
        public async Task CanUpdateProduct()
        {
            var client = new HubspotClient();
            var updatedProduct
                = await client.CRM.Products.UpdateAsync<Product, HubspotStandardResponseModel<Product>>(1427866648, new Product
                {
                    Description = "Updated",
                    Name = "Updated",
                    Code = "Updated"
                });

            Assert.IsTrue(updatedProduct.Id > 0);
        }
    }
}