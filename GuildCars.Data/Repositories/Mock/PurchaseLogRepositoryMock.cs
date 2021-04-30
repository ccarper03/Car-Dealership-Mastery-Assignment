using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Data.Repositories.Mock
{
    public class PurchaseLogRepositoryMock : IPurchaseLogRepository
    {
        private static List<PurchaseLog> _purchaseLogs = new List<PurchaseLog>();

        private static PurchaseLog _purchaseLogOne = new PurchaseLog
        {
            Phone = "000-000-0000",
            Email = "testpurchase1@test.com",
            PurchaseLogId = 1,
            PurchaseName = "Purchaser One",
            PurchasePrice = 17000m,
            PurchaseType = "Dealer Finance",
            SalesPersonId = "00000000-0000-0000-0000-000000000000",
            DateSold = new DateTime(2017, 1, 1),
            AddressOne = "123 Main Street",
            AddressTwo = "",
            CarId = 1,
            City = "Hampton",
            ZipCode = "23652"
        };

        private static PurchaseLog _purchaseLogTwo = new PurchaseLog
        {
            Phone = "111-111-1111",
            Email = "testpurchase2@test.com",
            PurchaseLogId = 2,
            PurchaseName = "Purchaser Two",
            PurchasePrice = 4000m,
            PurchaseType = "Dealer Finance",
            SalesPersonId = "11111111-1111-1111-1111-111111111111",
            DateSold = new DateTime(2016, 1, 1),
            AddressOne = "123 Elm Street",
            AddressTwo = "Apartment 33D",
            CarId = 4,
            City = "York",
            ZipCode = "23692"
        };

        private static PurchaseLog _purchaseLogThree = new PurchaseLog
        {
            Phone = "000-000-0000",
            Email = "testpurchase3@test.com",
            PurchaseLogId = 3,
            PurchaseName = "Purchaser Three",
            PurchasePrice = 10000m,
            PurchaseType = "Dealer Finance",
            SalesPersonId = "00000000-0000-0000-0000-000000000000",
            DateSold = new DateTime(2017, 4, 1),
            AddressOne = "123 Main Street",
            AddressTwo = "",
            CarId = 1,
            City = "Hampton",
            ZipCode = "23652"
        };

        private static PurchaseLog _purchaseLogFour = new PurchaseLog
        {
            Phone = "111-111-1111",
            Email = "testpurchas4@test.com",
            PurchaseLogId = 4,
            PurchaseName = "Purchaser Four",
            PurchasePrice = 14000m,
            PurchaseType = "Dealer Finance",
            SalesPersonId = "11111111-1111-1111-1111-111111111111",
            DateSold = new DateTime(2016, 1, 1),
            AddressOne = "123 Elm Street",
            AddressTwo = "Apartment 33D",
            CarId = 4,
            City = "York",
            ZipCode = "23692"
        };
        public PurchaseLogRepositoryMock()
        {
            if(_purchaseLogs.Count == 0)
            {
                _purchaseLogs.Add(_purchaseLogOne);
                _purchaseLogs.Add(_purchaseLogTwo);
                _purchaseLogs.Add(_purchaseLogThree);
                _purchaseLogs.Add(_purchaseLogFour);
            }
        }

        public IEnumerable<PurchaseLog> GetPurchaseLogs()
        {
            return _purchaseLogs;
        }

        public void Insert(PurchaseLog PurchaseLog)
        {
            PurchaseLog.PurchaseLogId = _purchaseLogs.Max(p => p.PurchaseLogId) + 1;

            _purchaseLogs.Add(PurchaseLog);
        }

        public void ClearPurchaseLogs()
        {
            _purchaseLogs.Clear();
        }
    }
}
