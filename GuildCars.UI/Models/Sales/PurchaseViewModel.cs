using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models.Sales
{
    public class PurchaseViewModel
    {
        public Car Car { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string IntColor { get; set; }
        public string BodyColor { get; set; }
        public string BodyStyle { get; set; }
        public string Transmission { get; set; }
    }
}