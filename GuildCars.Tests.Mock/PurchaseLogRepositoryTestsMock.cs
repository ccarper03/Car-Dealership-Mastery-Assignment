using GuildCars.Data.Repositories.Mock;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Tests.PurchaseLogRepositoryTests
{
    [TestFixture]
    public class PurchaseLogRepositoryTestsMock
    {
        PurchaseLogRepositoryMock repo;

        [SetUp]
        public void Init()
        {
            repo = new PurchaseLogRepositoryMock();
        }

        [TearDown]
        public void ResetRepo()
        {
            repo.ClearPurchaseLogs();
        }

        [Test]
        public void CanGetAllPurchaseLogs()
        {
            PurchaseLogRepositoryMock repo = new PurchaseLogRepositoryMock();

            List<PurchaseLog> purchaseLogs = repo.GetPurchaseLogs().ToList();

            Assert.AreEqual(4, purchaseLogs.Count);

            Assert.AreEqual("Purchaser One", purchaseLogs[0].PurchaseName);
            Assert.AreEqual(1, purchaseLogs[0].PurchaseLogId);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", purchaseLogs[0].SalesPersonId);
            Assert.AreEqual("Hampton", purchaseLogs[0].City);
            Assert.AreEqual("123 Main Street", purchaseLogs[0].AddressOne);
            Assert.IsTrue(String.IsNullOrEmpty(purchaseLogs[0].AddressTwo));
            Assert.AreEqual("23652", purchaseLogs[0].ZipCode);
            Assert.AreEqual("Dealer Finance", purchaseLogs[0].PurchaseType);
            Assert.AreEqual(17000m, purchaseLogs[0].PurchasePrice);
            Assert.AreEqual( new DateTime(2017, 1, 1), purchaseLogs[0].DateSold);
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
                PurchasePrice = 12000m,
                CarId = 2,
                DateSold = new DateTime(2014, 1, 1),
                SalesPersonId = "11111111-1111-1111-1111-111111111111",
                AddressOne = "106 Test Road",
                City = "Gloucester",
                ZipCode = "23072",
                Email = "testpurchase5@test.com",
                Phone = "333-333-3333"
            };

            PurchaseLogRepositoryMock repo = new PurchaseLogRepositoryMock();

            repo.Insert(purchaseLog);

            var purchaseLogs = repo.GetPurchaseLogs().ToList();

            Assert.AreEqual(5, purchaseLogs.Count);

            Assert.AreEqual("Purchaser Five", purchaseLogs[4].PurchaseName);
            Assert.AreEqual(5, purchaseLogs[4].PurchaseLogId);
            Assert.AreEqual("11111111-1111-1111-1111-111111111111", purchaseLogs[4].SalesPersonId);
            Assert.AreEqual("Gloucester", purchaseLogs[4].City);
            Assert.AreEqual("106 Test Road", purchaseLogs[4].AddressOne);
            Assert.IsTrue(String.IsNullOrEmpty(purchaseLogs[4].AddressTwo));
            Assert.AreEqual("23072", purchaseLogs[4].ZipCode);
            Assert.AreEqual("Bank Finance", purchaseLogs[4].PurchaseType);
            Assert.AreEqual(12000m, purchaseLogs[4].PurchasePrice);
            Assert.AreEqual(new DateTime(2014, 1, 1), purchaseLogs[4].DateSold);
            Assert.AreEqual("testpurchase5@test.com", purchaseLogs[4].Email);
            Assert.AreEqual("333-333-3333", purchaseLogs[4].Phone);
            Assert.AreEqual(2, purchaseLogs[4].CarId);
        }
    }
}
