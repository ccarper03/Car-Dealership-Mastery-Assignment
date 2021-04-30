using System;

namespace GuildCars.Models.Tables
{
    public class Make
    {
        public string MakeId { get; set; }
        public string MakeName { get; set; }
        public DateTime DateAdded { get; set; }
        public string AddedBy { get; set; }
    }
}
