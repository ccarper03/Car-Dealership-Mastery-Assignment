using GuildCars.Models.Tables;
using GuildCars.UI.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class EditVehicleViewModel
    {
        public Car Car { get; set; }
        public SelectList Makes { get; set; }
        public SelectList Models { get; set; }
        public SelectList BodyStyles { get; set; }
        public SelectList IntColors { get; set; }
        public SelectList Colors { get; set; }
        public IEnumerable<string> Types { get; set; }
        public SelectList Transmissions { get; set; }
        public AdminEditFormModel AdminEditFormModel { get; set; }
        public HttpPostedFileBase Picture { get; set; }
    }
}