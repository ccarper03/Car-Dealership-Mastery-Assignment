using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models.Admin
{
    public class AdminEditFormModel : IValidatableObject
    {
        public string Type { get; set; }
        public bool isNew { get; set; }
        public string MakeId { get; set; }
        public int ModelId { get; set; }
        public string Year { get; set; }
        public DateTime ModelYear { get; set; }
        public int BodyStyleId { get; set; }
        public int BodyColorId { get; set; }
        public int InteriorColorId { get; set; }
        public int TransmissionId { get; set; }
        public string Mileage { get; set; }
        public string VIN { get; set; }
        public string SalePriceInput { get; set; }
        public decimal SalePrice { get; set; }
        public string MSRPInput { get; set; }
        public decimal MSRP { get; set; }
        public string Description { get; set; }
        public bool Featured { get; set; }
        public HttpPostedFileBase Picture { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            int mileage = IsInt(Mileage);
            int year = IsInt(Year);

            SalePrice = PurchasePriceIsNumber(SalePriceInput);
            MSRP = PurchasePriceIsNumber(MSRPInput);

            if (String.IsNullOrEmpty(Mileage))
            {
                errors.Add(new ValidationResult("Mileage of vehicle is required!",
                    new[] { "Mileage" }));
            }

            if (Type == "New" && mileage > 1000)
            {
                errors.Add(new ValidationResult("New vehicles can not have mileage over 1000 miles!",
                    new[] { "Type", "Mileage" }));
            }

            if (String.IsNullOrEmpty(Year))
            {
                errors.Add(new ValidationResult("Year can not be left empty!",
                    new[] { "Year" }));
            }
            else if (Year.Length != 4)
            {
                errors.Add(new ValidationResult("Year can only be four digits!",
                    new[] { "Year" }));
            }

            if (year < 2000 || year > DateTime.Today.Year + 1)
            {

                errors.Add(new ValidationResult("Year can not be earlier than the year 2000 and more than next year!",
                    new[] { "Year" }));
            }
            else
            {
                ModelYear = new DateTime(year, 1, 1);
            }

            if (String.IsNullOrEmpty(Description))
            {

                errors.Add(new ValidationResult("Description can not be left blank!",
                    new[] { "Description" }));
            }

            if (Picture != null && Picture.ContentLength > 0)
            {
                var extensions = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

                var extension = Path.GetExtension(Picture.FileName);

                if (!extensions.Contains(extension))
                {
                    errors.Add(new ValidationResult("Image file must be a jpg, png, gif, or jpeg.",
                        new[] { "Picture" }));
                }
            }

            if (MSRP < SalePrice)
            {
                errors.Add(new ValidationResult("Sale price cannot be more than the vehicle's MSRP",
                    new[] { "SalePriceInput" }));
            }

            if (MSRP <= 0)
            {
                errors.Add(new ValidationResult("MSRP can be positive numbers only and cannot be left Blank! Dollar sign and commas " +
                    "are ok to use. Example: $19,000.00",
                    new[] { "MSRPInput" }));
            }

            if (SalePrice <= 0)
            {
                errors.Add(new ValidationResult("Sale Price can be positive numbers only and cannot be left Blank! Dollar sign and commas " +
                    "are ok to use. Example: $19,000.00",
                    new[] { "SalePriceInput" }));
            }

            return errors;
        }

        public int IsInt(string fieldString)
        {
            if (int.TryParse(fieldString, out int result))
            {
                return result;
            }
            return 0;
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