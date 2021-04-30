using GuildCars.Models.Tables;
using GuildCars.UI.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class AddVehicleViewModel
    {
        public Car Car { get; set; }
        public IEnumerable<Make> Makes { get; set; }
        public IEnumerable<Model> Models { get; set; }
        public IEnumerable<BodyStyle> BodyStyles { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<string> Types { get; set; }
        public IEnumerable<Transmission> Tranmissions { get; set; }
        public AdminFormModel AdminFormModel { get; set; }
        public HttpPostedFileBase Picture { get; set; }
    }
}