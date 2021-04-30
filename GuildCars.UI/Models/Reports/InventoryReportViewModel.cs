using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models.Reports
{
    public class InventoryReportViewModel
    {
        public IEnumerable<InventoryReportListingItem> NewCarsReports { get; set; }
        public IEnumerable<InventoryReportListingItem> UsedCarsReports { get; set; }
    }
}