using System;

namespace GuildCars.Models.Queries
{
    public class InventoryReportListingItem
    {
        public DateTime Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal StockValue { get; set; }
        public int UnitsInStock { get; set; }
        public bool IsNew { get; set; }
    }
}
