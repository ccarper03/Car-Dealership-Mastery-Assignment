using System;

namespace GuildCars.Models.Queries
{
    public class FeaturedShortListItem
    {
        public int CarId { get; set; }
        public string ImageURL { get; set; }
        public DateTime Year { get; set; }
        public string Make { get; set; }
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
    }
}
