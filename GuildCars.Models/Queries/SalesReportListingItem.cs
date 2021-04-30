namespace GuildCars.Models.Queries
{
    public class SalesReportListingItem
    {
        public string UserId { get; set; }
        public decimal Sales { get; set; }
        public string UserName { get; set; }
        public int CarsSold { get; set; }
    }
}
