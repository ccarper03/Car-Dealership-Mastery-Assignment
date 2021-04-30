using GuildCars.Data.Repositories.Mock;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Tests.CustomerContactRepositoryTests
{
    [TestFixture]
    public class CustomerContactRepositoryMockTests
    {
        CustomerContactRepositoryMock repo;

        [SetUp]
        public void Init()
        {
            repo = new CustomerContactRepositoryMock();
        }

        [TearDown]
        public void ResetRepo()
        {
            repo.ClearContactsList();
        }

        [Test]
        public void CanGetCustomerContactById()
        {
            CustomerContactRepositoryMock repo = new CustomerContactRepositoryMock();
            CustomerContact customerContact = repo.GetContactById(2);

            Assert.IsNotNull(customerContact);

            Assert.AreEqual(2, customerContact.ContactId);
            Assert.AreEqual("Test Contact 2", customerContact.ContactName);
            Assert.AreEqual("Test Contact Message 2", customerContact.MessageBody);
            Assert.AreEqual("777-777-7777", customerContact.Phone);
            Assert.AreEqual("test2@test.com", customerContact.Email);
        }

        [Test]
        public void CanGetAllCustomerContacts()
        {
            CustomerContactRepositoryMock repo = new CustomerContactRepositoryMock();
            List<CustomerContact> contacts = repo.GetAllContacts().ToList();

            Assert.AreEqual(3, contacts.Count);

            Assert.AreEqual(2, contacts[1].ContactId);
            Assert.AreEqual("Test Contact 2", contacts[1].ContactName);
            Assert.AreEqual("Test Contact Message 2", contacts[1].MessageBody);
            Assert.AreEqual("777-777-7777", contacts[1].Phone);
            Assert.AreEqual("test2@test.com", contacts[1].Email);
        }

        [Test]
        public void CanAddContact()
        {
            CustomerContact contact = new CustomerContact
            {
                ContactName = "Test Contact 4",
                Phone = "222-222-2222",
                Email = "test4@test.com",
                MessageBody = "Test Contact Message 4"
            };

            CustomerContactRepositoryMock repo = new CustomerContactRepositoryMock();
            repo.Insert(contact);

            List<CustomerContact> contacts = repo.GetAllContacts().ToList();
            Assert.AreEqual(4, contacts.Count);

            Assert.AreEqual(4, contacts[3].ContactId);
            Assert.AreEqual(contact.ContactName, contacts[3].ContactName);
            Assert.AreEqual(contact.Phone, contacts[3].Phone);
            Assert.AreEqual(contact.Email, contacts[3].Email);
            Assert.AreEqual(contact.MessageBody, contacts[3].MessageBody);
        }
    }
}
