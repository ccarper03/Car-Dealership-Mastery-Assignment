using System;

namespace GuildCars.Models.Tables
{
    public class Car
    {
        public int CarId { get; set; }
        public bool IsNew { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsSold { get; set; }
        public int UnitsInStock { get; set; }
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public DateTime ModelYear { get; set; }
        public string IMGFilePath { get; set; }
        public int BodyStyleId { get; set; }
        public int InteriorColorId { get; set; }
        public int BodyColorId { get; set; }
        public int TransmissionId { get; set; }
        public string Mileage { get; set; }
        public string VIN { get; set; }
        public decimal SalePrice { get; set; }
        public decimal MSRP { get; set; }
        public string VehicleDetails { get; set; }
    }
}
