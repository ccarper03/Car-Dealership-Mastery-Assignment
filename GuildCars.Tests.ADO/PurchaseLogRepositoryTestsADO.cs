using GuildCars.Data.Repositories.ADO;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace GuildCars.Tests.PurchaseLogRepositoryTests
{
    [TestFixture]
    public class PurchaseLogRepositoryTestsADO
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
        public void CanGetAllPurchaseLogs()
        {
            PurchaseLogRepositoryADO repo = new PurchaseLogRepositoryADO();

            List<PurchaseLog> purchaseLogs = repo.GetPurchaseLogs().ToList();

            Assert.AreEqual(4, purchaseLogs.Count);

            Assert.AreEqual("Purchaser One", purchaseLogs[0].PurchaseName);
            Assert.AreEqual(1, purchaseLogs[0].PurchaseLogId);
            Assert.AreEqual("sales1@test.com", purchaseLogs[0].SalesPersonId);
            Assert.AreEqual("Hampton", purchaseLogs[0].City);
            Assert.AreEqual("123 Main Street", purchaseLogs[0].AddressOne);
            Assert.IsTrue(String.IsNullOrEmpty(purchaseLogs[0].AddressTwo));
            Assert.AreEqual("23652", purchaseLogs[0].ZipCode);
            Assert.AreEqual("Dealer Finance", purchaseLogs[0].PurchaseType);
            Assert.AreEqual(17000m, purchaseLogs[0].PurchasePrice);
            Assert.AreEqual(new DateTime(2017, 1, 1), purchaseLogs[0].DateSold);
            Assert.AreEqual("testpurchase1@test.com", purchaseLogs[0].Email);
            Assert.AreEqual("000-000-0000", purchaseLogs[0].Phone);
            Assert.AreEqual(1, purchaseLogs[0].CarId);
        }

        [Test]
        public void CanInsertPurchaseLog()
        {
            PurchaseLog purchaseLog = new PurchaseLog
            {
                PurchaseType = "Bank Finance",
                PurchaseName = "Purchaser Five",
                PurchasePrice = 12000.00m,
                CarId = 2,
                DateSold = new DateTime(2014, 1, 1),
                SalesPersonId = "sales2@test.com",
                AddressOne = "106 Test Road",
                City = "Gloucester",
                ZipCode = "23072",
                Email = "testpurchase3@test.com",
                Phone = "333-333-3333"
            };

            PurchaseLogRepositoryADO repo = new PurchaseLogRepositoryADO();

            repo.Insert(purchaseLog);

            var purchaseLogs = repo.GetPurchaseLogs().ToList();

            Assert.AreEqual(5, purchaseLogs.Count);

            Assert.AreEqual("Purchaser Five", purchaseLogs[4].PurchaseName);
            Assert.AreEqual(5, purchaseLogs[4].PurchaseLogId);
            Assert.AreEqual("sales2@test.com", purchaseLogs[4].SalesPersonId);
            Assert.AreEqual("Gloucester", purchaseLogs[4].City);
            Assert.AreEqual("106 Test Road", purchaseLogs[4].AddressOne);
            Assert.IsTrue(String.IsNullOrEmpty(purchaseLogs[4].AddressTwo));
            Assert.AreEqual("23072", purchaseLogs[4].ZipCode);
            Assert.AreEqual("Bank Finance", purchaseLogs[4].PurchaseType);
            Assert.AreEqual(12000m, purchaseLogs[4].PurchasePrice);
            Assert.AreEqual(new DateTime(2014, 1, 1), purchaseLogs[4].DateSold);
            Assert.AreEqual("testpurchase3@test.com", purchaseLogs[4].Email);
            Assert.AreEqual("333-333-3333", purchaseLogs[4].Phone);
            Assert.AreEqual(2, purchaseLogs[4].CarId);
        }
    }
}
