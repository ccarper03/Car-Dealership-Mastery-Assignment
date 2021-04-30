using GuildCars.Models.Tables;
using GuildCustomerContacts.Data.Repositories.ADO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace GuildCars.Tests.CustomerContactRepositoryTests
{
    [TestFixture]
    public class CustomerContactRepositoryTestsADO
    {
        [SetUp]
        public void Init()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                using (dbConnection)
                {
                    var cmd = new SqlCommand
                    {
                        CommandText = "GuildCarsDBReset",
                        CommandType = System.Data.CommandType.StoredProcedure,

                        Connection = dbConnection
                    };
                    dbConnection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format(CultureInfo.CurrentCulture,
                          "Exception Type: {0}, Message: {1}{2}",
                          ex.GetType(),
                          ex.Message,
                          ex.InnerException == null ? String.Empty :
                          String.Format(CultureInfo.CurrentCulture,
                                       " InnerException Type: {0}, Message: {1}",
                                       ex.InnerException.GetType(),
                                       ex.InnerException.Message));

                System.Diagnostics.Debug.WriteLine(errorMessage);

                dbConnection.Close();
            }
        }

        [Test]
        public void CanGetCustomerContactById()
        {
            CustomerContactRepositoryADO repo = new CustomerContactRepositoryADO();
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
            CustomerContactRepositoryADO repo = new CustomerContactRepositoryADO();
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

            CustomerContactRepositoryADO repo = new CustomerContactRepositoryADO();
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

