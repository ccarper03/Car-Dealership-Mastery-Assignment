using System;

namespace GuildCars.Models.Queries
{
    public class CarsSearchParameters
    {
        public bool? IsNew { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? MinYear { get; set; }
        public DateTime? MaxYear { get; set; }
        public string SearchTerm { get; set; }
    }
}
