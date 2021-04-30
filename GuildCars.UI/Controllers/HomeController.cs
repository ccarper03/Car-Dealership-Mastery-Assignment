using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using System.Linq;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : Controller
    {
        private readonly ICarsRepository _carsRepo;
        private readonly ISpecialsRepository _specialsRepo;
        private readonly IMakeRepository _makeRepo;
        private readonly IModelRepository _modelRepo;
        private readonly IColorRepository _colorRepo;
        private readonly IBodyStyleRepository _bodyStyleRepository;
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly ICustomerContactRepository _customerContactRepository;

        public HomeController()
        {
            _carsRepo = CarRepositoryFactory.GetRepository();
            _specialsRepo = SpecialsRepositoryFactory.GetRepository();
            _makeRepo = MakeRepositoryFactory.GetRepository();
            _modelRepo = ModelRepositoryFactory.GetRepository();
            _colorRepo = ColorRepositoryFactory.GetRepository();
            _bodyStyleRepository = BodyStyleRepositoryFactory.GetRepository();
            _transmissionRepository = TransmissionRepositoryFactory.GetRepository();
            _customerContactRepository = CustomerContactRepositoryFactory.GetRepository();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = new IndexPageViewModel()
            {
                FeaturedCars = _carsRepo.GetAllFeaturedCars(),
                Specials = _specialsRepo.GetAll(),
            };
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult NewInventory()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult UsedInventory()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Specials()
        {
            SpecialsViewModel model = new SpecialsViewModel
            {
                Specials = _specialsRepo.GetAll().ToList()
            };

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Contact(string Id)
        {
            ContactRequestModel model = new ContactRequestModel();
            if (!string.IsNullOrEmpty(Id))
            {
                model.Message = "Inquiry about vehicle with VIN #: " + Id;
            }
            else
            {
                model.Message = "";
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Contact(ContactRequestModel model)
        {
            if (ModelState.IsValid)
            {
                CustomerContact contact = new CustomerContact()
                {
                    ContactName = model.Name,
                    Phone = model.Phone,
                    Email = model.Email,
                    MessageBody = model.Message
                };

                _customerContactRepository.Insert(contact);

                return View("ContactConfirmation", model);
            }
            else
            {
                return View("Contact", model);
            }
        }

        [AllowAnonymous]
        public ActionResult Details(int Id)
        {
            DetailsViewModel model = new DetailsViewModel
            {
                Car = _carsRepo.GetCarById(Id)
            };
            model.Model = _modelRepo.GetModelById(model.Car.ModelId).ModelName;
            model.Make = _makeRepo.GetMakeById(model.Car.MakeId.ToString()).MakeName;
            model.IntColor = _colorRepo.GetColorById(model.Car.InteriorColorId).ColorName;
            model.BodyColor = _colorRepo.GetColorById(model.Car.BodyColorId).ColorName;
            model.BodyStyle = _bodyStyleRepository.GetBodyStyleById(model.Car.BodyStyleId).BodyStyleType;
            model.Transmission = _transmissionRepository.GetTransmissionById(model.Car.TransmissionId).TransmissionType;
            return View(model);
        }
    }
}