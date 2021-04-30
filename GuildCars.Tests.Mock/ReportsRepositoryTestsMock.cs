using GuildCars.Data.Repositories.Mock;
using GuildCars.Models.Queries;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Tests.ReportsRepositoryTests
{
    [TestFixture]
    public class ReportsRepositoryTestsMock
    {
        ReportsRepositoryMock repo;

        [SetUp]
        public void Init()
        {
            repo = new ReportsRepositoryMock();
        }

        [TearDown]
        public void ResetRepo()
        {
            repo.ClearInventoryList();
            repo.ClearSalesList();
        }

        [Test]
        public void CanGetInventoryReport()
        {
            List<InventoryReportListingItem> inventoryReportsList = repo.GetInventory().ToList();

            Assert.AreEqual(10, inventoryReportsList.Count);

            Assert.IsTrue(inventoryReportsList[0].IsNew);
            Assert.AreEqual("Toyota", inventoryReportsList[0].Make);
            Assert.AreEqual("Tundra LX", inventoryReportsList[0].Model);
            Assert.AreEqual(3, inventoryReportsList[0].UnitsInStock);
            Assert.AreEqual(155445m, inventoryReportsList[0].StockValue);
            Assert.AreEqual(new DateTime(2017,1,1), inventoryReportsList[0].Year);
        }

        [Test]
        public void CanGetSalesReport()
        {
            List<SalesReportListingItem> salesReport = repo.GetSalesReport().ToList();

            Assert.AreEqual(2, salesReport.Count);

            Assert.AreEqual("11111111-1111-1111-1111-111111111111", salesReport[1].UserId);
            Assert.AreEqual("Sales Test User 2", salesReport[1].UserName);
            Assert.AreEqual(2, salesReport[1].CarsSold);
            Assert.AreEqual(18000m, salesReport[1].Sales);
        }

        [Test]
        public void CanSearchOnMinDateChange()
        {
            SalesSearchParameters parameters = new SalesSearchParameters
            {
                MaxDate = null,
                MinDate = new DateTime(2017, 1, 1),
                UserName = null
            };

            List<SalesReportListingItem> searchedSalesReport = repo.SearchSalesReports(parameters).ToList();

            Assert.AreEqual(1, searchedSalesReport.Count);

            Assert.AreEqual(27000, searchedSalesReport[0].Sales);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", searchedSalesReport[0].UserId);
            Assert.AreEqual("Sales Test User 1", searchedSalesReport[0].UserName);
        }

        [Test]
        public void CanSearchOnMaxDateChange()
        {
            SalesSearchParameters parameters = new SalesSearchParameters
            {
                MaxDate = new DateTime(2017, 1, 1),
                MinDate = null,
                UserName = null
            };

            List<SalesReportListingItem> searchedSalesReport = repo.SearchSalesReports(parameters).ToList();

            Assert.AreEqual(2, searchedSalesReport.Count);

            Assert.AreEqual(17000, searchedSalesReport[0].Sales);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", searchedSalesReport[0].UserId);
            Assert.AreEqual("Sales Test User 1", searchedSalesReport[0].UserName);
        }

        [Test]
        public void CanSearchOnUserName()
        {
            SalesSearchParameters parameters = new SalesSearchParameters
            {
                MaxDate = null,
                MinDate = null,
                UserName = "Sales Test User 1"
            };

            List<SalesReportListingItem> searchedSalesReport = repo.SearchSalesReports(parameters).ToList();

            Assert.AreEqual(1, searchedSalesReport.Count);

            Assert.AreEqual(27000, searchedSalesReport[0].Sales);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", searchedSalesReport[0].UserId);
            Assert.AreEqual("Sales Test User 1", searchedSalesReport[0].UserName);
        }

        [Test]
        public void CanSearchMinAndMaxDateChange()
        {
            SalesSearchParameters parameters = new SalesSearchParameters
            {
                MaxDate = new DateTime(2017, 4, 2),
                MinDate = new DateTime(2017, 1, 2),
                UserName = null
            };

            List<SalesReportListingItem> searchedSalesReport = repo.SearchSalesReports(parameters).ToList();

            Assert.AreEqual(1, searchedSalesReport.Count);

            Assert.AreEqual(10000, searchedSalesReport[0].Sales);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", searchedSalesReport[0].UserId);
            Assert.AreEqual("Sales Test User 1", searchedSalesReport[0].UserName);
        }

        [Test]
        public void CanSearchOnDateAndUserNameChange()
        {
            SalesSearchParameters parameters = new SalesSearchParameters
            {
                MaxDate = new DateTime(2017, 4, 2),
                MinDate = new DateTime(2017, 1, 2),
                UserName = "Sales Test User 1"
            };

            List<SalesReportListingItem> searchedSalesReport = repo.SearchSalesReports(parameters).ToList();

            Assert.AreEqual(1, searchedSalesReport.Count);

            Assert.AreEqual(10000, searchedSalesReport[0].Sales);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", searchedSalesReport[0].UserId);
            Assert.AreEqual("Sales Test User 1", searchedSalesReport[0].UserName);
        }

        [Test]
        public void CanReturnZeroResultsWrongUserName()
        {
            SalesSearchParameters parameters = new SalesSearchParameters
            {
                MaxDate = new DateTime(2017, 4, 2),
                MinDate = new DateTime(2017, 1, 2),
                UserName = "Sales Test User 2"
            };

            List<SalesReportListingItem> searchedSalesReport = repo.SearchSalesReports(parameters).ToList();

            Assert.AreEqual(0, searchedSalesReport.Count);
        }

        [Test]
        public void CanReturnZeroResultsOutOfDateRange()
        {
            SalesSearchParameters testParametersOne = new SalesSearchParameters
            {
                MaxDate = new DateTime(2015, 4, 2),
                MinDate = new DateTime(2014, 1, 2),
                UserName = null
            };

            SalesSearchParameters testParametersTwo = new SalesSearchParameters
            {
                MaxDate = new DateTime(2018, 4, 2),
                MinDate = new DateTime(2017, 4, 2),
                UserName = null
            };

            List<SalesReportListingItem> searchedSalesReportOne = repo.SearchSalesReports(testParametersOne).ToList();

            List<SalesReportListingItem> searchedSalesReportTwo = repo.SearchSalesReports(testParametersOne).ToList();

            Assert.AreEqual(0, searchedSalesReportOne.Count);
            Assert.AreEqual(0, searchedSalesReportTwo.Count);
        }
    }
}
