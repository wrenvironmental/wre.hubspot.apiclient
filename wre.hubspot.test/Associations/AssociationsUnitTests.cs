using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wre.hubspot.apiclient;
using wre.hubspot.apiclient.Associations;
using wre.hubspot.test.Associations.Dto;
using wre.hubspot.test.Contact;
using wre.hubspot.test.CustomObjects;
using wre.hubspot.test.CustomObjects.Dto;

namespace wre.hubspot.test.Associations
{
    [TestClass]
    public class AssociationsUnitTests
    {
        public AssociationsUnitTests()
        {
            HubspotSettings.AccessToken = Environment.GetEnvironmentVariable("HubspotAccessToken");
            HubspotSettings.BaseUrl = "https://api.hubapi.com";
        }

        [TestMethod]
        public async Task CanCreateSimpleAssociation()
        {
            var hubspotContact = await ContactsUnitTests.CreateNewContact();
            var hubspotCustomObject = await CustomObjectsUnitTests.CreateNewCustomObject(hubspotContact.LastName.Replace("Contact", string.Empty));
            Assert.IsTrue(hubspotContact.Id > 0);
            Assert.IsTrue(hubspotCustomObject.Id > 0);
            var objectFrom = new MyCustomObjectTo
            {
                AssociationId = hubspotCustomObject.Id?.ToString() ?? string.Empty
            };
            var objectTo = new ContactAssociation
            {
                AssociationId = hubspotContact.Id.ToString() ?? string.Empty
            };
            
            var client = new HubspotClient();
            var association = new HubspotAssociationEntity(objectFrom, objectTo);
            await client.Associations.CreateAsync<HubspotAssociationEntity>(association);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task CanCreateMultipleAssociations()
        {
            var client = new HubspotClient();
            var hubspotContact = await ContactsUnitTests.CreateNewContact();
            var listOfCustomObjects = new List<MyCustomObject>
            {
                await CustomObjectsUnitTests.CreateNewCustomObject(hubspotContact.LastName.Replace("Contact", string.Empty)),
                await CustomObjectsUnitTests.CreateNewCustomObject(hubspotContact.LastName.Replace("Contact", string.Empty))
            };

            Assert.IsTrue(listOfCustomObjects.Count(c => c?.Id > 0) == 2);

            var associations = new List<HubspotAssociationEntity>()
            {
                new HubspotAssociationEntity(new MyCustomObjectTo()
                    {
                        AssociationId = listOfCustomObjects.First().Id.ToString() ?? string.Empty
                    },
                    new ContactAssociation()
                    {
                        AssociationId = hubspotContact.Id.ToString() ?? string.Empty
                    }),
                new HubspotAssociationEntity(new MyCustomObjectTo()
                    {
                        AssociationId = listOfCustomObjects.Skip(1).First().Id.ToString() ?? string.Empty
                    },
                    new ContactAssociation()
                    {
                        AssociationId = hubspotContact.Id.ToString() ?? string.Empty
                    }),
            };

            var response = await client.Associations.CreateBatchAsync<HubspotAssociationEntity>(associations);
            Assert.IsTrue(true);
        }
    }
}