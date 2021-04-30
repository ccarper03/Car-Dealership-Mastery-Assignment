using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System.Collections.Generic;

namespace GuildCars.UI.Models
{
    public class IndexPageViewModel
    {
        public IEnumerable<FeaturedShortListItem> FeaturedCars { get; set; }
        public IEnumerable<Special> Specials { get; set; }
    }
}