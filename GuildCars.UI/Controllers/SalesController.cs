using System;
using System.Collections.Generic;
using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using GuildCars.UI.Models.Sales;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class SalesController : Controller
    {
        private readonly ICarsRepository _carsRepo;
        private readonly ISpecialsRepository _specialsRepo;
        private readonly IMakeRepository _makeRepo;
        private readonly IModelRepository _modelRepo;
        private readonly IColorRepository _colorRepo;
        private readonly IBodyStyleRepository _bodyStyleRepository;
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly ICustomerContactRepository _customerContactRepository;
        private readonly IPurchaseLogRepository _purchaseLogRepository;

        public SalesController()
        {
            _carsRepo = CarRepositoryFactory.GetRepository();
            _specialsRepo = SpecialsRepositoryFactory.GetRepository();
            _makeRepo = MakeRepositoryFactory.GetRepository();
            _modelRepo = ModelRepositoryFactory.GetRepository();
            _colorRepo = ColorRepositoryFactory.GetRepository();
            _bodyStyleRepository = BodyStyleRepositoryFactory.GetRepository();
            _transmissionRepository = TransmissionRepositoryFactory.GetRepository();
            _customerContactRepository = CustomerContactRepositoryFactory.GetRepository();
            _purchaseLogRepository = PurchaseLogRepositoryFactory.GetRepository();
        }

        // GET: Sales
        [Authorize(Roles = "Sales")]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "Sales")]
        [AcceptVerbs("GET")]
        public ActionResult Purchase(int Id)
        {
            var model = CreatePurchaseVehicleModel(Id);

            return View("PurchaseView", model);
        }

        [Authorize(Roles = "Sales")]
        [AcceptVerbs("Post")]
        public ActionResult Purchase(PurchaseVehicleModel model)
        {
            if (ModelState.IsValid)
            {
                model.PurchaseViewModel = CreatePurchaseVehicleModel(model.CarId).PurchaseViewModel;
                model.PurchaseViewModel.Car.IsSold = true;

                Car car = new Car();

                car = model.PurchaseViewModel.Car;

                _carsRepo.Update(car);

                PurchaseLog log = new PurchaseLog()
                {
                    PurchaseName = model.Name,
                    PurchasePrice = model.PurchasePrice,
                    PurchaseType = model.PurchaseType,
                    AddressOne = model.StreetOne,
                    AddressTwo = model.StreetTwo,
                    Phone = model.Phone,
                    Email = model.Email,
                    City = model.City,
                    ZipCode = model.ZipCode,
                    CarId = model.CarId,
                    DateSold = DateTime.Today,
                    SalesPersonId = User.Identity.Name
                };

                _purchaseLogRepository.Insert(log);

                return View("PurchaseLogConfirmation", model);
            }
            else
            {
                model = CreatePurchaseVehicleModel(model.CarId);

                return View("PurchaseView", model);
            }
        }

        [Authorize(Roles = "Sales")]
        public ActionResult ChangePassword()
        {
            return RedirectToAction("ChangePassword", "Manage");
        }

        public PurchaseVehicleModel CreatePurchaseVehicleModel(int Id)
        {
            PurchaseVehicleModel model = new PurchaseVehicleModel();
            model.PurchaseViewModel = new PurchaseViewModel();
            model.States = new List<string>();
            model.PurchaseTypes = new List<string>();

            model.PurchaseTypes = PurchaseTypes.GetPurchaseTypes();
            model.States = States.GetStates();
            model.CarId = Id;
            model.PurchaseViewModel.Car = _carsRepo.GetCarById(Id);
            model.SalePrice = model.PurchaseViewModel.Car.SalePrice;
            model.MSRP = model.PurchaseViewModel.Car.MSRP;
            model.PurchaseViewModel.Model = _modelRepo.GetModelById(model.PurchaseViewModel.Car.ModelId).ModelName;
            model.PurchaseViewModel.Make = _makeRepo.GetMakeById(model.PurchaseViewModel.Car.MakeId.ToString()).MakeName;
            model.PurchaseViewModel.IntColor = _colorRepo.GetColorById(model.PurchaseViewModel.Car.InteriorColorId).ColorName;
            model.PurchaseViewModel.BodyColor = _colorRepo.GetColorById(model.PurchaseViewModel.Car.BodyColorId).ColorName;
            model.PurchaseViewModel.BodyStyle = _bodyStyleRepository.GetBodyStyleById(model.PurchaseViewModel.Car.BodyStyleId).BodyStyleType;
            model.PurchaseViewModel.Transmission = _transmissionRepository.GetTransmissionById(model.PurchaseViewModel.Car.TransmissionId).TransmissionType;

            return model;
        }
    }
}