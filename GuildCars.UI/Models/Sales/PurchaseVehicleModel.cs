using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models.Sales
{
    public class PurchaseVehicleModel : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string StreetOne { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        public string StreetTwo { get; set; }
        [Phone]
        public string Phone { get; set; }
        public List<string> States { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PurchaseType { get; set; }
        public string InputPurchasePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public int CarId { get; set; }
        public decimal SalePrice { get; set; }
        public decimal MSRP { get; set; }
        public List<string> PurchaseTypes { get; set; }
        public PurchaseViewModel PurchaseViewModel { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            PurchasePrice = PurchasePriceIsNumber(InputPurchasePrice);

            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Phone))
            {
                errors.Add(new ValidationResult("You must enter either a phone number or email!",
                    new[] { "Email", "Phone" }));
            }

            if (int.TryParse(Phone, out int result))
            {
                if (Phone.Length < 10)
                    errors.Add(new ValidationResult("Phone Number must be 10 digits. Include area code.",
                        new[] { "Phone" }));
            }

            if (string.IsNullOrEmpty(ZipCode) || ZipCode.Length != 5)
            {
                errors.Add(new ValidationResult("Zip Code field cannot be left blank and must be exactly 5 digits in length!",
                    new[] { "ZipCode" }));
            }

            if (PurchasePrice == 0)
            {
                errors.Add(new ValidationResult("Purchase price can be positive numbers only and cannot be left Blank! Dollar sign and commas " +
                    "are ok to use. Example: $19,000.00",
                    new[] { "PurchasePrice" }));
            }

            if (PurchasePrice < (SalePrice - (SalePrice * .05m)))
            {
                errors.Add(new ValidationResult("Purchase price cannot be below 95% of the vehicle's sale price!" +
                    "are ok to use. Example: $19,000.00",
                    new[] { "PurchasePrice" }));
            }

            if (MSRP < PurchasePrice)
            {
                errors.Add(new ValidationResult("Purchase price cannot be more than the vehicle's MSRP",
                    new[] { "PurchasePrice" }));
            }

            if (!int.TryParse(ZipCode, out int res))
            {
                errors.Add(new ValidationResult("Zip Code must be numbers only!",
                    new[] { "ZipCode" }));
            }

            return errors;
        }

        public decimal PurchasePriceIsNumber(string price)
        {
            if (String.IsNullOrEmpty(price))
            {
                return 0;
            }

            NumberStyles style;
            CultureInfo culture;
            decimal number;


            // Parse currency value using en-GB culture.
            style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            culture = CultureInfo.CreateSpecificCulture("en-US");
            if (Decimal.TryParse(price, style, culture, out number))
            {
                return number;
            }

            return 0;
        }
    }
}