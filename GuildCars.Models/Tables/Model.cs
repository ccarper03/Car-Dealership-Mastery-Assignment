using System;

namespace GuildCars.Models.Tables
{
    public class Model
    {
        public int ModelId { get; set; }
        public int MakeId { get; set; }
        public string ModelName { get; set; }
        public DateTime DateAdded { get; set; }
        public string Addedby { get; set; }
    }
}
