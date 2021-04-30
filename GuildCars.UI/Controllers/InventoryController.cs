using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using System;
using System.Linq;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class InventoryController : ApiController
    {
        private readonly ICarsRepository _carsRepo;

        public InventoryController()
        {
            _carsRepo = CarRepositoryFactory.GetRepository();
        }

        [AllowAnonymous]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchNewCars(string searchTerm, string minYear, string maxYear, decimal minPrice, decimal maxPrice)
        {
            var parameters = CreateVehicleParameter(searchTerm, minYear, maxYear, minPrice, maxPrice);

            parameters.IsNew = true;

            var SearchedCars = _carsRepo.SearchCars(parameters).ToList();

            if (SearchedCars.Count() == 0)
            {
                return NotFound();
            }
            return Ok(SearchedCars);
        }

        [AllowAnonymous]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchUsedCars(string searchTerm, string minYear, string maxYear, decimal minPrice, decimal maxPrice)
        {
            var parameters = CreateVehicleParameter(searchTerm, minYear, maxYear, minPrice, maxPrice);

            parameters.IsNew = false;

            var SearchedCars = _carsRepo.SearchCars(parameters).ToList();

            if (SearchedCars.Count() == 0)
            {
                return NotFound();
            }
            return Ok(SearchedCars);
        }


        [Authorize(Roles = "Sales")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SalesSearchCars(string searchTerm, string minYear, string maxYear, decimal minPrice, decimal maxPrice)
        {
            var parameters = CreateVehicleParameter(searchTerm, minYear, maxYear, minPrice, maxPrice);

            var SearchedCars = _carsRepo.SearchCars(parameters);

            SearchedCars = SearchedCars.Where(c => c.IsSold == false).ToList();

            if (SearchedCars.Count() == 0)
            {
                return NotFound();
            }
            return Ok(SearchedCars);
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("GET")]
        public IHttpActionResult AdminSearchCars(string searchTerm, string minYear, string maxYear, decimal minPrice, decimal maxPrice)
        {
            var parameters = CreateVehicleParameter(searchTerm, minYear, maxYear, minPrice, maxPrice);

            var SearchedCars = _carsRepo.SearchCars(parameters);

            SearchedCars = SearchedCars.Where(c => c.IsSold == false).ToList();

            if (SearchedCars.Count() == 0)
            {
                return NotFound();
            }
            return Ok(SearchedCars);
        }

        public static CarsSearchParameters CreateVehicleParameter(string searchTerm, string minYear,
            string maxYear, decimal minPrice, decimal maxPrice)
        {
            CarsSearchParameters parameters = new CarsSearchParameters();


            if (searchTerm == "null")
            {
                parameters.SearchTerm = "";
            }
            else
            {
                parameters.SearchTerm = searchTerm;
            }

            if (minYear == "null")
            {
                parameters.MinYear = null;
            }
            else
            {
                parameters.MinYear = DateTime.Parse(minYear + "/1/1");
            }

            if (maxYear == "null")
            {
                parameters.MaxYear = null;
            }
            else
            {
                parameters.MaxYear = DateTime.Parse(maxYear + "/1/1");
            }

            parameters.MinPrice = minPrice;

            parameters.MaxPrice = maxPrice;

            return parameters;
        }
    }
}
