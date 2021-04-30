using GuildCars.Models.Queries;
using System.Collections.Generic;

namespace GuildCars.Data.Interfaces
{
    public interface IReportsRepository
    {
        IEnumerable<SalesReportListingItem> GetSalesReport();
        IEnumerable<InventoryReportListingItem> GetInventory();
        IEnumerable<SalesReportListingItem> SearchSalesReports(SalesSearchParameters Parameters);
    }
}
