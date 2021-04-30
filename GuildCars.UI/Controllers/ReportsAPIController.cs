using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class ReportsAPIController : ApiController
    {
        private readonly IUserRepository _userRepo;
        private readonly IReportsRepository _reportsRepository;

        public ReportsAPIController()
        {
            _userRepo = UserRepositoryFactory.GetRepository();
            _reportsRepository = ReportsRepositoryFactory.GetRepository();
        }

        [Authorize(Roles = "Admin")]
        [Route("api/reports/sales/{userName}/{minDate}/{maxDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Sales(string userName, string minDate, string maxDate)
        {
            try
            {
                var parameters = CreateSalesParameter(userName, minDate, maxDate);

                var reports = _reportsRepository.SearchSalesReports(parameters);

                return Ok(reports);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public static SalesSearchParameters CreateSalesParameter(string userName, string minYear,
            string maxYear)
        {
            SalesSearchParameters parameters = new SalesSearchParameters();


            if (userName == "null")
            {
                parameters.UserName = "";
            }
            else
            {
                parameters.UserName = userName;
            }

            if (minYear == "null")
            {
                parameters.MinDate = null;
            }
            else
            {
                parameters.MinDate = DateTime.Parse(minYear + "/1/1");
            }

            if (maxYear == "null")
            {
                parameters.MaxDate = null;
            }
            else
            {
                parameters.MaxDate = DateTime.Parse(maxYear + "/1/1");
            }

            return parameters;
        }
    }
}
