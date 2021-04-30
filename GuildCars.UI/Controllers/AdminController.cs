using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using GuildCars.UI.Models.Account;
using GuildCars.UI.Models.Admin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class AdminController : Controller
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
        private readonly IUserRepository _userRepository;

        public AdminController()
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
            _userRepository = UserRepositoryFactory.GetRepository();
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("GET")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("GET")]
        public ActionResult Add()
        {
            AddVehicleViewModel model = new AddVehicleViewModel
            {
                Car = new Car(),
                Makes = _makeRepo.GetAll().ToList(),
                Models = new List<Model>(),
                Types = new List<string>() { "New", "Used" },
                Tranmissions = _transmissionRepository.GetAll().ToList(),
                Colors = _colorRepo.GetAll().ToList(),
                BodyStyles = _bodyStyleRepository.GetAll().ToList()
            };

            return View("AddVehicle", model);
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("POST")]
        public ActionResult Add(AddVehicleViewModel model)
        {
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            if (ModelState.IsValid)
            {
                try
                {
                    int newCarId = 0;
                    if (model.AdminFormModel.Picture != null && model.AdminFormModel.Picture.ContentLength > 0)
                    {
                        Car car = new Car()
                        {
                            MakeId = model.AdminFormModel.MakeId,
                            ModelId = model.AdminFormModel.ModelId,
                            IsNew = model.AdminFormModel.isNew,
                            BodyColorId = model.AdminFormModel.BodyColorId,
                            InteriorColorId = model.AdminFormModel.InteriorColorId,
                            BodyStyleId = model.AdminFormModel.BodyStyleId,
                            IsFeatured = false,
                            IsSold = false,
                            ModelYear = model.AdminFormModel.ModelYear,
                            Mileage = model.AdminFormModel.Mileage,
                            MSRP = model.AdminFormModel.MSRP,
                            SalePrice = model.AdminFormModel.SalePrice,
                            VehicleDetails = model.AdminFormModel.Description,
                            TransmissionId = model.AdminFormModel.TransmissionId,
                            VIN = model.AdminFormModel.VIN
                        };

                        if (model.AdminFormModel.Type == "New")
                        {
                            car.IsNew = true;
                        }

                        var savepath = Server.MapPath("~/Images");

                        string fileName = "inventory-";
                        string extension = Path.GetExtension(model.AdminFormModel.Picture.FileName);

                        var filePath = Path.Combine(savepath, fileName + extension);

                        car.IMGFilePath = filePath;

                        car.UnitsInStock += 1;

                        _carsRepo.Insert(car);

                        newCarId = _carsRepo.GetAllCars().LastOrDefault().CarId;

                        car.IMGFilePath = Path.Combine(savepath, fileName + car.CarId + extension);

                        model.AdminFormModel.Picture.SaveAs(car.IMGFilePath);

                        filePath = "/Images/" + Path.GetFileName(car.IMGFilePath);

                        car.IMGFilePath = filePath;

                        _carsRepo.Update(car);

                        return Edit(newCarId);
                    }
                    return View("AddVehicle", model);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                model.Models = _modelRepo.GetModelsByMakeId(model.AdminFormModel.MakeId);
                model.Makes = _makeRepo.GetAll().ToList();
                model.Types = new List<string>() { "New", "Used" };
                model.Tranmissions = _transmissionRepository.GetAll().ToList();
                model.Colors = _colorRepo.GetAll().ToList();
                model.BodyStyles = _bodyStyleRepository.GetAll().ToList();

                return View("AddVehicle", model);
            }
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("GET")]
        public ActionResult Edit(int Id)
        {
            EditVehicleViewModel model = new EditVehicleViewModel
            {
                Car = _carsRepo.GetCarById(Id)
            };

            string newOrUsed;
            if (model.Car.IsNew)
            {
                newOrUsed = "New";
            }
            else
            {
                newOrUsed = "Used";
            }

            model.AdminEditFormModel = new AdminEditFormModel
            {
                BodyColorId = model.Car.BodyColorId,
                InteriorColorId = model.Car.InteriorColorId,
                VIN = model.Car.VIN,
                MakeId = model.Car.MakeId.ToString(),
                ModelId = model.Car.ModelId,
                Mileage = model.Car.Mileage,
                ModelYear = model.Car.ModelYear,
                Year = model.Car.ModelYear.Year.ToString(),
                BodyStyleId = model.Car.BodyStyleId,
                Description = model.Car.VehicleDetails,
                TransmissionId = model.Car.TransmissionId,
                Type = newOrUsed,
                MSRPInput = model.Car.MSRP.ToString(),
                SalePriceInput = model.Car.SalePrice.ToString()
            };

            model.Makes = new SelectList(_makeRepo.GetAll(), "MakeId", "MakeName", model.AdminEditFormModel.MakeId);
            model.Models = new SelectList(_modelRepo.GetAll(), "ModelId", "ModelName", model.AdminEditFormModel.ModelId);
            model.Transmissions = new SelectList(_transmissionRepository.GetAll(), "TransmissionId", "TransmissionType", model.AdminEditFormModel.TransmissionId);
            model.Types = new List<string>() { "New", "Used" };
            model.BodyStyles = new SelectList(_bodyStyleRepository.GetAll(), "BodyStyleId", "BodystyleType", model.AdminEditFormModel.BodyStyleId);
            model.Colors = new SelectList(_colorRepo.GetAll(), "ColorId", "ColorName", model.AdminEditFormModel.BodyColorId);
            model.IntColors = new SelectList(_colorRepo.GetAll(), "ColorId", "ColorName", model.AdminEditFormModel.InteriorColorId);

            return View("EditVehicle", model);
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("POST")]
        public ActionResult EditVehicle(EditVehicleViewModel model)
        {
            var oldCar = _carsRepo.GetCarById(model.Car.CarId);

            if (ModelState.IsValid)
            {
                try
                {
                    {
                        Car car = new Car()
                        {
                            MakeId = int.Parse(model.AdminEditFormModel.MakeId),
                            ModelId = model.AdminEditFormModel.ModelId,
                            IsNew = model.AdminEditFormModel.isNew,
                            BodyColorId = model.AdminEditFormModel.BodyColorId,
                            InteriorColorId = model.AdminEditFormModel.InteriorColorId,
                            BodyStyleId = model.AdminEditFormModel.BodyStyleId,
                            IsFeatured = model.AdminEditFormModel.Featured,
                            IsSold = false,
                            ModelYear = model.AdminEditFormModel.ModelYear,
                            Mileage = model.AdminEditFormModel.Mileage,
                            MSRP = model.AdminEditFormModel.MSRP,
                            SalePrice = model.AdminEditFormModel.SalePrice,
                            VehicleDetails = model.AdminEditFormModel.Description,
                            TransmissionId = model.AdminEditFormModel.TransmissionId,
                            VIN = model.AdminEditFormModel.VIN,
                            IMGFilePath = oldCar.IMGFilePath,
                            CarId = oldCar.CarId,
                            UnitsInStock = oldCar.UnitsInStock

                        };

                        if (model.AdminEditFormModel.Type == "New")
                        {
                            car.IsNew = true;
                        }

                        model.Car = car;

                        if (model.AdminEditFormModel.Picture == null)
                        {
                            _carsRepo.Update(car);

                            model.Makes = new SelectList(_makeRepo.GetAll(), "MakeId", "MakeName", model.AdminEditFormModel.MakeId);
                            model.Models = new SelectList(_modelRepo.GetAll(), "ModelId", "ModelName", model.AdminEditFormModel.ModelId);
                            model.Transmissions = new SelectList(_transmissionRepository.GetAll(), "TransmissionId", "TransmissionType", model.AdminEditFormModel.TransmissionId);
                            model.Types = new List<string>() { "New", "Used" };
                            model.BodyStyles = new SelectList(_bodyStyleRepository.GetAll(), "BodyStyleId", "BodystyleType", model.AdminEditFormModel.BodyStyleId);
                            model.Colors = new SelectList(_colorRepo.GetAll(), "ColorId", "ColorName", model.AdminEditFormModel.BodyColorId);
                            model.IntColors = new SelectList(_colorRepo.GetAll(), "ColorId", "ColorName", model.AdminEditFormModel.InteriorColorId);

                            TempData["Success"] = "Edited Successfully! Add or search another car to edit!";

                            return RedirectToAction("Index", "Admin");
                        }

                        if (model.AdminEditFormModel.Picture != null && model.AdminEditFormModel.Picture.ContentLength > 0)
                        {

                            var savepath = Server.MapPath("~/Images");

                            string fileName = "inventory-";
                            string extension = Path.GetExtension(model.AdminEditFormModel.Picture.FileName);

                            var filePath = Path.Combine(savepath, fileName + oldCar.CarId + extension);

                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }

                            car.IMGFilePath = filePath;
                        }
                        else
                        {
                            // they did not replace the old file, so keep the old file name
                            model.Car.IMGFilePath = oldCar.IMGFilePath;
                        }

                        model.AdminEditFormModel.Picture.SaveAs(model.Car.IMGFilePath);

                        var newFilePath = "/Images/" + Path.GetFileName(model.Car.IMGFilePath);

                        model.Car.IMGFilePath = newFilePath;

                        _carsRepo.Update(car);

                        model.Makes = new SelectList(_makeRepo.GetAll(), "MakeId", "MakeName", model.AdminEditFormModel.MakeId);
                        model.Models = new SelectList(_modelRepo.GetAll(), "ModelId", "ModelName", model.AdminEditFormModel.ModelId);
                        model.Transmissions = new SelectList(_transmissionRepository.GetAll(), "TransmissionId", "TransmissionType", model.AdminEditFormModel.TransmissionId);
                        model.Types = new List<string>() { "New", "Used" };
                        model.BodyStyles = new SelectList(_bodyStyleRepository.GetAll(), "BodyStyleId", "BodystyleType", model.AdminEditFormModel.BodyStyleId);
                        model.Colors = new SelectList(_colorRepo.GetAll(), "ColorId", "ColorName", model.AdminEditFormModel.BodyColorId);
                        model.IntColors = new SelectList(_colorRepo.GetAll(), "ColorId", "ColorName", model.AdminEditFormModel.InteriorColorId);

                        TempData["Success"] = "Edited Successfully! Add or search another car to edit!";
                        return RedirectToAction("Index", "Admin");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                model.Makes = new SelectList(_makeRepo.GetAll(), "MakeId", "MakeName", model.AdminEditFormModel.MakeId);
                model.Models = new SelectList(_modelRepo.GetAll(), "ModelId", "ModelName", model.AdminEditFormModel.ModelId);
                model.Transmissions = new SelectList(_transmissionRepository.GetAll(), "TransmissionId", "TransmissionType", model.AdminEditFormModel.TransmissionId);
                model.Types = new List<string>() { "New", "Used" };
                model.BodyStyles = new SelectList(_bodyStyleRepository.GetAll(), "BodyStyleId", "BodystyleType", model.AdminEditFormModel.BodyStyleId);
                model.Colors = new SelectList(_colorRepo.GetAll(), "ColorId", "ColorName", model.AdminEditFormModel.BodyColorId);
                model.IntColors = new SelectList(_colorRepo.GetAll(), "ColorId", "ColorName", model.AdminEditFormModel.InteriorColorId);
                model.Car = oldCar;

                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult GetModels(int Id)
        {
            //var temp = _makeRepo.GetAll();
            //var modelID =
            //    from make in _makeRepo.GetAll()
            //    where make.MakeName == Id
            //    select make;

            //if (String.IsNullOrEmpty(Id))
            //{
            //    return null;
            //}

            _ = new List<Model>();
            List<Model> models = _modelRepo.GetModelsByMakeId(Id);
            SelectList selectModels = new SelectList(models, "ModelId", "ModelName", models[0].ModelId);
            return Json(selectModels, JsonRequestBehavior.AllowGet);

        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("DELETE")]
        public ActionResult Delete(int Id)
        {

            var car = _carsRepo.GetCarById(Id);

            var savepath = Server.MapPath("~/Images");

            string fileName = "inventory-";
            string extension = Path.GetExtension(car.IMGFilePath);

            var filePath = Path.Combine(savepath, fileName + car.CarId + extension);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _carsRepo.Delete(Id);

            TempData["Success"] = "Vehicle Deleted Successfully! Add or search another car to edit!";

            return RedirectToAction("Index", "Admin");
        }

        //Users
        [Authorize(Roles = "Admin")]
        [AcceptVerbs("GET")]
        public ActionResult Users()
        {
            UserIndexPageViewModel model = new UserIndexPageViewModel
            {
                Users = _userRepository.GetUsers().ToList()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("GET")]
        public ActionResult AddUser()
        {
            return RedirectToAction("Register", "Account");
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("GET")]
        public ActionResult EditUser(string id)
        {
            var user = _userRepository.GetUserById(id);

            EditUserViewModel model = new EditUserViewModel
            {
                UserId = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserRole = user.UserRole,

                UserRoles = new List<string>() { "Admin", "Sales", "Disabled" }
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("POST")]
        public ActionResult EditUser(EditUserViewModel model)
        {

            var updatedUser = new User();
            var oldUser = _userRepository.GetUserById(model.UserId);

            if (ModelState.IsValid)
            {
                updatedUser.Id = model.UserId;
                updatedUser.UserRole = model.UserRole;
                updatedUser.UserName = model.Email;
                updatedUser.Email = model.Email;
                updatedUser.LastName = model.LastName;
                updatedUser.FirstName = model.FirstName;
                updatedUser.SecurityStamp = Guid.NewGuid().ToString();
                PasswordHasher hasher = new PasswordHasher();

                if (!String.IsNullOrEmpty(model.Password))
                {
                    updatedUser.PasswordHash = hasher.HashPassword(model.Password);
                }
                else
                {
                    updatedUser.PasswordHash = oldUser.PasswordHash;
                }

                _userRepository.Update(updatedUser);

                UserIndexPageViewModel userIndexModel = new UserIndexPageViewModel
                {
                    Users = _userRepository.GetUsers().ToList()
                };

                TempData["Success"] = "User edited successfully!";

                return View("Users", userIndexModel);
            }

            model.UserRoles = new List<string>() { "Admin", "Sales", "Disabled" };

            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult ChangePassword()
        {
            return RedirectToAction("ChangePassword", "Manage");
        }

        //Add Make and Model controllers

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("GET")]
        public ActionResult Makes()
        {
            AddMakeViewModel model = new AddMakeViewModel
            {
                Makes = _makeRepo.GetAll()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("POST")]
        public ActionResult Makes(AddMakeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Make newMake = new Make()
                {
                    AddedBy = User.Identity.Name,
                    DateAdded = DateTime.Today,
                    MakeName = model.Make
                };

                _makeRepo.Insert(newMake);

                model.Makes = _makeRepo.GetAll();

                TempData["Success"] = "Make Successfully Added!";

                return View(model);
            }
            model.Makes = _makeRepo.GetAll();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("GET")]
        public ActionResult Models()
        {
            AddModelViewModel model = new AddModelViewModel()
            {
                Models = _modelRepo.GetAll(),
                Makes = _makeRepo.GetAll()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("POST")]
        public ActionResult Models(AddModelViewModel AddModel)
        {
            if (ModelState.IsValid)
            {
                Model newModel = new Model()
                {
                    MakeId = AddModel.MakeId,
                    Addedby = User.Identity.Name,
                    DateAdded = DateTime.Today,
                    ModelName = AddModel.Model
                };

                _modelRepo.Insert(newModel);

                AddModel.Makes = _makeRepo.GetAll();
                AddModel.Models = _modelRepo.GetAll();

                TempData["Success"] = "Model Successfully Added!";

                return View(AddModel);
            }
            AddModel.Makes = _makeRepo.GetAll();
            AddModel.Models = _modelRepo.GetAll();

            return View(AddModel);
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("GET")]
        public ActionResult Specials()
        {
            SpecialViewModel model = new SpecialViewModel
            {
                Specials = _specialsRepo.GetAll()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("DELETE")]
        public ActionResult DeleteSpecial(int id)
        {
            _specialsRepo.Delete(id);
            _ = new SpecialViewModel
            {
                Specials = _specialsRepo.GetAll()
            };

            return Json(new { Success = true });
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("Post")]
        public ActionResult Specials(SpecialViewModel special)
        {
            if (ModelState.IsValid)
            {
                Special newSpecial = new Special()
                {
                    SpecialDetails = special.Details,
                    Title = special.Title
                };

                _specialsRepo.Insert(newSpecial);

                return RedirectToAction("Specials", "Admin");
            }
            return View(special);
        }

        public int ParseMakeId(string make)
        {
            switch (make)
            {
                case "Acura":
                    return 2;
                case "Toyota":
                    return 1;
                case "Ford":
                    return 3;
                case "Dodge":
                    return 4;
                case "Mock":
                    return 5;
                case "2":
                    return 2;
                case "1":
                    return 1;
                case "3":
                    return 3;
                case "4":
                    return 4;
                case "5":
                    return 5;
                default:
                    return 1;
            }


        }
    }
}