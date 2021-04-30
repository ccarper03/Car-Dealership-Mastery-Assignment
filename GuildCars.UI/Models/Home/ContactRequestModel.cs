using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class ContactRequestModel : IValidatableObject
    {   
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string Message { get; set; }
        public string VIN { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Phone))
            {
                errors.Add(new ValidationResult("You must enter either a phone number or email to so we can contact you.",
                    new[] { "Email", "Phone" }));
            }

            if (int.TryParse(Phone, out int result))
            {
                if(Phone.Length < 10)
                errors.Add(new ValidationResult("Phone Number must be 10 digits. Include area code.",
                    new[] {  "Phone" }));
            }

            return errors;
        }
    }
}