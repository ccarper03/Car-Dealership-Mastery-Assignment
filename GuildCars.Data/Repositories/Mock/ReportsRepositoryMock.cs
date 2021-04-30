using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Data.Repositories.Mock
{
    public class ReportsRepositoryMock : IReportsRepository
    {
        private static List<SalesReportListingItem> _salesReports = new List<SalesReportListingItem>();
        private static List<InventoryReportListingItem> _inventoryReports = new List<InventoryReportListingItem>();

        public PurchaseLogRepositoryMock purchaseRepo = new PurchaseLogRepositoryMock();
        public CarsRepositoryMock carRepo = new CarsRepositoryMock();
        public MakeRepositoryMock makeRepo = new MakeRepositoryMock();
        public ModelRepositoryMock modelRepo = new ModelRepositoryMock();


        public IEnumerable<InventoryReportListingItem> GetInventory()
        {
            List<Car> cars = carRepo.GetAllCars().ToList();

            foreach (var car in cars)
            {
                InventoryReportListingItem reportItem = new InventoryReportListingItem
                {
                    IsNew = car.IsNew,
                    Make = makeRepo.GetMakeById(car.MakeId.ToString()).MakeName,
                    Model = modelRepo.GetModelById(car.ModelId).ModelName,
                    UnitsInStock = car.UnitsInStock,
                    StockValue = car.MSRP * car.UnitsInStock,
                    Year = car.ModelYear
                };

                _inventoryReports.Add(reportItem);
            }
            return _inventoryReports;
        }

        public IEnumerable<SalesReportListingItem> GetSalesReport()
        {
            List<PurchaseLog> purchases = purchaseRepo.GetPurchaseLogs().ToList();

            UserRepositoryMock userRepo = new UserRepositoryMock();

            foreach (var purchase in purchases)
            {
                SalesReportListingItem salesReportItem = new SalesReportListingItem
                {
                    Sales = purchase.PurchasePrice,
                    CarsSold = 0,
                    UserId = purchase.SalesPersonId,
                    UserName = userRepo.GetUserById(purchase.SalesPersonId).UserName
                };

                if (_salesReports.Exists(s => s.UserId == purchase.SalesPersonId))
                {
                    int index = _salesReports.FindIndex(s => s.UserId == purchase.SalesPersonId);

                    _salesReports[index].CarsSold += 1;

                    _salesReports[index].Sales += salesReportItem.Sales;
                }
                else
                {
                    salesReportItem.CarsSold += 1;
                    _salesReports.Add(salesReportItem);
                }
            }
            return _salesReports;
        }

        public IEnumerable<SalesReportListingItem> SearchSalesReports(SalesSearchParameters Parameters)
        {
            List<PurchaseLog> purchaseLogs = new List<PurchaseLog>();
            
            PurchaseLogRepositoryMock purchaseRepo = new PurchaseLogRepositoryMock();

            purchaseLogs = purchaseRepo.GetPurchaseLogs().ToList();

            UserRepositoryMock userRepo = new UserRepositoryMock();

            List<User> users = userRepo.GetUsers().ToList();

            List<PurchaseLog> queriedPurchaseLogs = QueryPurchaseLogs(Parameters, purchaseLogs, users);
            

            foreach (var purchase in queriedPurchaseLogs)
            {
                SalesReportListingItem salesReportItem = new SalesReportListingItem
                {
                    Sales = purchase.PurchasePrice,
                    CarsSold = 0,
                    UserId = purchase.SalesPersonId,
                    UserName = userRepo.GetUserById(purchase.SalesPersonId).UserName
                };

                if (_salesReports.Exists(s => s.UserId == purchase.SalesPersonId))
                {
                    int index = _salesReports.FindIndex(s => s.UserId == purchase.SalesPersonId);

                    _salesReports[index].CarsSold += 1;

                    _salesReports[index].Sales += salesReportItem.Sales;
                }
                else
                {
                    salesReportItem.CarsSold += 1;
                    _salesReports.Add(salesReportItem);
                }
            }

            return _salesReports;
        }

        public List<PurchaseLog> QueryPurchaseLogs(SalesSearchParameters Parameters, List<PurchaseLog> purchaseLogs, List<User> users)
        {
            List<PurchaseLog> queriedPurchaseLogs = new List<PurchaseLog>();

            if (Parameters.MaxDate.HasValue && Parameters.MinDate.HasValue && !string.IsNullOrEmpty(Parameters.UserName))
            {
                User userNameQuery = users.FirstOrDefault(user => user.UserName == Parameters.UserName);

                queriedPurchaseLogs = purchaseLogs.Where(p => p.DateSold >= Parameters.MinDate && p.DateSold
                <= Parameters.MaxDate && p.SalesPersonId == userNameQuery.Id).ToList();
            }
            else if (Parameters.MinDate.HasValue && Parameters.MaxDate.HasValue && string.IsNullOrEmpty(Parameters.UserName))
            {
                queriedPurchaseLogs = purchaseLogs.Where(p => p.DateSold >= Parameters.MinDate && p.DateSold <= Parameters.MaxDate).ToList();
            }
            else if (Parameters.MinDate.HasValue)
            {
                queriedPurchaseLogs = purchaseLogs.Where(p => p.DateSold >= Parameters.MinDate).ToList();
            }
            else if (Parameters.MaxDate.HasValue)
            {
                queriedPurchaseLogs = purchaseLogs.Where(p => p.DateSold <= Parameters.MaxDate).ToList();
            }
            else if (!String.IsNullOrEmpty(Parameters.UserName))
            {
                User userNameQuery = users.FirstOrDefault(user => user.UserName == Parameters.UserName);

                queriedPurchaseLogs = purchaseLogs.Where(p => p.SalesPersonId == userNameQuery.Id).ToList();
            }
            return queriedPurchaseLogs;
        }

        public void ClearSalesList()
        {
            _salesReports.Clear();
        }

        public void ClearInventoryList()
        {
            _inventoryReports.Clear();
        }
    }
}
