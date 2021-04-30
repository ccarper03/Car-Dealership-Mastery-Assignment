using System;

namespace GuildCars.Models.Queries
{
    public class SalesSearchParameters
    {
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public string UserName { get; set; }
    }
}
