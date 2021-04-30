using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models.Admin
{

    public class AddMakeViewModel : IValidatableObject
    {

        public string Make { get; set; }
        public IEnumerable<Make> Makes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> addErrors = new List<ValidationResult>();
            if (String.IsNullOrEmpty(Make))
            {
                addErrors.Add(new ValidationResult("The make field can not be left blank!",
                    new[] { "Make" }));
            }
            return addErrors;
        }
    }
}