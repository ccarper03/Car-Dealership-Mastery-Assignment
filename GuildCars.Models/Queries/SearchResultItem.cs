using System;

namespace GuildCars.Models.Queries
{
    public class SearchResultItem
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string IMGURL { get; set; }
        public string InteriorColor { get; set; }
        public string BodyColor { get; set; }
        public string BodyStyle { get; set; }
        public string Transmission { get; set; }
        public string Mileage { get; set; }
        public string VIN { get; set; }
        public decimal SalePrice { get; set; }
        public decimal  MSRP { get; set; }
        public bool IsSold { get; set; }
    }
}
