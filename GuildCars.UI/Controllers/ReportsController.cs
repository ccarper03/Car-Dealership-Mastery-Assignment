using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.UI.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IReportsRepository _reportsRepository;

        public ReportsController()
        {
            _userRepo = UserRepositoryFactory.GetRepository();
            _reportsRepository = ReportsRepositoryFactory.GetRepository();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("GET")]
        public ActionResult Sales()
        {
            SalesReportViewModel model = new SalesReportViewModel();

            model.Users = new SelectList(_userRepo.GetUsersByRole("Sales"), "UserName", "UserName", "-All-");

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("Get")]
        public ActionResult Inventory()
        {
            InventoryReportViewModel model = new InventoryReportViewModel();

            var reports = _reportsRepository.GetInventory();

            model.NewCarsReports = reports.Where(c => c.IsNew == true);

            model.UsedCarsReports = reports.Where(c => c.IsNew == false);

            return View(model);
        }


    }
}