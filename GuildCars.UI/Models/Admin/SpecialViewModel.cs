using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models.Admin
{
    public class SpecialViewModel
    {
        public IEnumerable<Special> Specials { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Details { get; set; }
    }
}