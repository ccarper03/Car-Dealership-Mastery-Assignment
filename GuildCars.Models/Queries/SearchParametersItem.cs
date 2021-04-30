using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class SearchParametersItem
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public string SearchTerm { get; set; }
    }
}
