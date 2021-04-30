using GuildCars.Data.Repositories.ADO;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace GuildCars.Tests
{
    [TestFixture]
    public class CarsRepositoryTestsADO
    {
        [SetUp]
        public void Init()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                using (dbConnection)
                {
                    var cmd = new SqlCommand
                    {
                        CommandText = "GuildCarsDBReset",
                        CommandType = System.Data.CommandType.StoredProcedure,

                        Connection = dbConnection
                    };
                    dbConnection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format(CultureInfo.CurrentCulture,
                          "Exception Type: {0}, Message: {1}{2}",
                          ex.GetType(),
                          ex.Message,
                          ex.InnerException == null ? String.Empty :
                          String.Format(CultureInfo.CurrentCulture,
                                       " InnerException Type: {0}, Message: {1}",
                                       ex.InnerException.GetType(),
                                       ex.InnerException.Message));

                System.Diagnostics.Debug.WriteLine(errorMessage);

                dbConnection.Close();
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                using (dbConnection)
                {
                    var cmd = new SqlCommand
                    {
                        CommandText = "GuildCarsDBReset",
                        CommandType = System.Data.CommandType.StoredProcedure,

                        Connection = dbConnection
                    };
                    dbConnection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format(CultureInfo.CurrentCulture,
                          "Exception Type: {0}, Message: {1}{2}",
                          ex.GetType(),
                          ex.Message,
                          ex.InnerException == null ? String.Empty :
                          String.Format(CultureInfo.CurrentCulture,
                                       " InnerException Type: {0}, Message: {1}",
                                       ex.InnerException.GetType(),
                                       ex.InnerException.Message));

                System.Diagnostics.Debug.WriteLine(errorMessage);

                dbConnection.Close();
            }
        }

        [Test]
        public void CanGetCarById()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();
            Car car = repo.GetCarById(2);

            Assert.IsNotNull(car);

            Assert.AreEqual("2ABC2ABC2ABC2ABC2", car.VIN);
            Assert.AreEqual(2, car.CarId);
            Assert.AreEqual(new DateTime(2018, 1, 1), car.ModelYear);
            Assert.IsTrue(car.IsNew);
            Assert.IsFalse(car.IsSold);
            Assert.IsTrue(car.IsFeatured);
            Assert.AreEqual(5, car.UnitsInStock);
            Assert.AreEqual("200", car.Mileage);
            Assert.AreEqual(2, car.BodyColorId);
            Assert.AreEqual(2, car.BodyStyleId);
            Assert.AreEqual(2, car.TransmissionId);
            Assert.AreEqual(2, car.MakeId);
            Assert.AreEqual(3, car.ModelId);
            Assert.AreEqual(3, car.InteriorColorId);
            Assert.AreEqual(33000.00m, car.SalePrice);
            Assert.AreEqual(34150.00m, car.MSRP);
            Assert.AreEqual("/Images/2018AcuraTLX.png", car.IMGFilePath);
            Assert.AreEqual("A silver bullet of power and dependability.", car.VehicleDetails);
        }

        [Test]
        public void CanGetAllCars()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();
            List<Car> cars = repo.GetAllCars().ToList();

            Assert.AreEqual(10, cars.Count);

            Assert.AreEqual("2ABC2ABC2ABC2ABC2", cars[1].VIN);
            Assert.AreEqual(2, cars[1].CarId);
            Assert.AreEqual(new DateTime(2018, 1, 1), cars[1].ModelYear);
            Assert.IsTrue(cars[1].IsNew);
            Assert.IsFalse(cars[1].IsSold);
            Assert.IsTrue(cars[1].IsFeatured);
            Assert.AreEqual(5, cars[1].UnitsInStock);
            Assert.AreEqual("200", cars[1].Mileage);
            Assert.AreEqual(2, cars[1].BodyColorId);
            Assert.AreEqual(2, cars[1].BodyStyleId);
            Assert.AreEqual(2, cars[1].TransmissionId);
            Assert.AreEqual(2, cars[1].MakeId);
            Assert.AreEqual(3, cars[1].ModelId);
            Assert.AreEqual(3, cars[1].InteriorColorId);
            Assert.AreEqual(33000.00m, cars[1].SalePrice);
            Assert.AreEqual(34150.00m, cars[1].MSRP);
            Assert.AreEqual("/Images/2018AcuraTLX.png", cars[1].IMGFilePath);
            Assert.AreEqual("A silver bullet of power and dependability.", cars[1].VehicleDetails);
        }

        [Test]
        public void CanGetFeaturedCars()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();
            List<FeaturedShortListItem> featuredCars = repo.GetAllFeaturedCars().ToList();

            Assert.AreEqual(8, featuredCars.Count);

            Assert.AreEqual(2, featuredCars[0].CarId);
            Assert.AreEqual(new DateTime(2018, 1, 1), featuredCars[0].Year);
            Assert.AreEqual(2, featuredCars[0].MakeId);
            Assert.AreEqual(3, featuredCars[0].ModelId);
            Assert.AreEqual(33000.00m, featuredCars[0].Price);
            Assert.AreEqual("/Images/2018AcuraTLX.png", featuredCars[0].ImageURL);
            Assert.AreEqual("Acura", featuredCars[0].Make);
            Assert.AreEqual("TLX", featuredCars[0].Model);
        }

        [Test]
        public void CanGetNewCars()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();
            List<Car> cars = repo.GetAllNewCars().ToList();

            Assert.AreEqual(5, cars.Count);

            Assert.AreEqual("2ABC2ABC2ABC2ABC2", cars[1].VIN);
            Assert.AreEqual(2, cars[1].CarId);
            Assert.AreEqual(new DateTime(2018, 1, 1), cars[1].ModelYear);
            Assert.IsTrue(cars[1].IsNew);
            Assert.IsFalse(cars[1].IsSold);
            Assert.IsTrue(cars[1].IsFeatured);
            Assert.AreEqual(5, cars[1].UnitsInStock);
            Assert.AreEqual("200", cars[1].Mileage);
            Assert.AreEqual(2, cars[1].BodyColorId);
            Assert.AreEqual(2, cars[1].BodyStyleId);
            Assert.AreEqual(2, cars[1].TransmissionId);
            Assert.AreEqual(2, cars[1].MakeId);
            Assert.AreEqual(3, cars[1].ModelId);
            Assert.AreEqual(3, cars[1].InteriorColorId);
            Assert.AreEqual(33000.00m, cars[1].SalePrice);
            Assert.AreEqual(34150.00m, cars[1].MSRP);
            Assert.AreEqual("/Images/2018AcuraTLX.png", cars[1].IMGFilePath);
            Assert.AreEqual("A silver bullet of power and dependability.", cars[1].VehicleDetails);
        }

        [Test]
        public void CanGetUsedCars()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();
            List<Car> cars = repo.GetAllUsedCars().ToList();

            Assert.AreEqual(5, cars.Count);

            Assert.AreEqual("4ABC4ABC4ABC4ABC4", cars[1].VIN);
            Assert.AreEqual(4, cars[1].CarId);
            Assert.AreEqual(new DateTime(2005, 1, 1), cars[1].ModelYear);
            Assert.IsFalse(cars[1].IsNew);
            Assert.IsFalse(cars[1].IsSold);
            Assert.IsFalse(cars[1].IsFeatured);
            Assert.AreEqual(1, cars[1].UnitsInStock);
            Assert.AreEqual("111200", cars[1].Mileage);
            Assert.AreEqual(5, cars[1].BodyColorId);
            Assert.AreEqual(4, cars[1].BodyStyleId);
            Assert.AreEqual(1, cars[1].TransmissionId);
            Assert.AreEqual(4, cars[1].MakeId);
            Assert.AreEqual(4, cars[1].ModelId);
            Assert.AreEqual(4, cars[1].InteriorColorId);
            Assert.AreEqual(4000.00m, cars[1].SalePrice);
            Assert.AreEqual(5000.00m, cars[1].MSRP);
            Assert.AreEqual("/Images/2005DodgeGrandCaravan.jpg", cars[1].IMGFilePath);
            Assert.AreEqual("Certified and ready to take your family anywhere.", cars[1].VehicleDetails);
        }

        [Test]
        public void CanGetUnsoldCars()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();
            List<Car> cars = repo.GetAllUnsoldCars().ToList();

            Assert.AreEqual(8, cars.Count);

            Assert.AreEqual("4ABC4ABC4ABC4ABC4", cars[1].VIN);
            Assert.AreEqual(4, cars[1].CarId);
            Assert.AreEqual(new DateTime(2005, 1, 1), cars[1].ModelYear);
            Assert.IsFalse(cars[1].IsNew);
            Assert.IsFalse(cars[1].IsSold);
            Assert.IsFalse(cars[1].IsFeatured);
            Assert.AreEqual(1, cars[1].UnitsInStock);
            Assert.AreEqual("111200", cars[1].Mileage);
            Assert.AreEqual(5, cars[1].BodyColorId);
            Assert.AreEqual(4, cars[1].BodyStyleId);
            Assert.AreEqual(1, cars[1].TransmissionId);
            Assert.AreEqual(4, cars[1].MakeId);
            Assert.AreEqual(4, cars[1].ModelId);
            Assert.AreEqual(4, cars[1].InteriorColorId);
            Assert.AreEqual(4000.00m, cars[1].SalePrice);
            Assert.AreEqual(5000.00m, cars[1].MSRP);
            Assert.AreEqual("/Images/2005DodgeGrandCaravan.jpg", cars[1].IMGFilePath);
            Assert.AreEqual("Certified and ready to take your family anywhere.", cars[1].VehicleDetails);
        }

        [Test]
        public void CanGetSoldCars()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();
            List<Car> cars = repo.GetAllSoldCars().ToList();

            Assert.AreEqual(2, cars.Count);

            Assert.AreEqual("3ABC3ABC3ABC3ABC3", cars[1].VIN);
            Assert.AreEqual(3, cars[1].CarId);
            Assert.AreEqual(new DateTime(2017, 1, 1), cars[1].ModelYear);
            Assert.IsFalse(cars[1].IsNew);
            Assert.IsTrue(cars[1].IsSold);
            Assert.IsTrue(cars[1].IsFeatured);
            Assert.AreEqual(1, cars[1].UnitsInStock);
            Assert.AreEqual("1200", cars[1].Mileage);
            Assert.AreEqual(5, cars[1].BodyColorId);
            Assert.AreEqual(3, cars[1].BodyStyleId);
            Assert.AreEqual(1, cars[1].TransmissionId);
            Assert.AreEqual(3, cars[1].MakeId);
            Assert.AreEqual(2, cars[1].ModelId);
            Assert.AreEqual(5, cars[1].InteriorColorId);
            Assert.AreEqual(22669.00m, cars[1].SalePrice);
            Assert.AreEqual(24500.00m, cars[1].MSRP);
            Assert.AreEqual("/Images/2017FordEscape.png", cars[1].IMGFilePath);
            Assert.AreEqual("Loaded! Used Price for Brand New Quality.", cars[1].VehicleDetails);
        }

        [Test]
        public void CanAddCar()
        {
            Car car = new Car
            {
                ModelYear = new DateTime(2015, 1, 1),
                IsNew = false,
                IsFeatured = true,
                IsSold = false,
                UnitsInStock = 1,
                Mileage = "20000",
                VIN = "5ABC5ABC5ABC5ABC5",
                BodyColorId = 5,
                BodyStyleId = 3,
                TransmissionId = 2,
                MakeId = 3,
                ModelId = 2,
                InteriorColorId = 5,
                SalePrice = 19500m,
                MSRP = 21000m,
                IMGFilePath = "Images/placeholder.png",
                VehicleDetails = "2015 Ford Escape. Fully Loaded!"
            };

            CarsRepositoryADO repo = new CarsRepositoryADO();
            repo.Insert(car);

            List<Car> cars = repo.GetAllCars().ToList();
            Assert.AreEqual(11, cars.Count);

            Assert.AreEqual(11, cars[10].CarId);
            Assert.AreEqual(car.ModelYear, cars[10].ModelYear);
            Assert.AreEqual(car.IsNew, cars[10].IsNew);
            Assert.AreEqual(car.IsFeatured, cars[10].IsFeatured);
            Assert.AreEqual(car.IsSold, cars[10].IsSold);
            Assert.AreEqual(car.UnitsInStock, cars[10].UnitsInStock);
            Assert.AreEqual(car.Mileage, cars[10].Mileage);
            Assert.AreEqual(car.VIN, cars[10].VIN);
            Assert.AreEqual(car.BodyColorId, cars[10].BodyColorId);
            Assert.AreEqual(car.BodyStyleId, cars[10].BodyStyleId);
            Assert.AreEqual(car.TransmissionId, cars[10].TransmissionId);
            Assert.AreEqual(car.MakeId, cars[10].MakeId);
            Assert.AreEqual(car.ModelId, cars[10].ModelId);
            Assert.AreEqual(car.InteriorColorId, cars[10].InteriorColorId);
            Assert.AreEqual(car.SalePrice, cars[10].SalePrice);
            Assert.AreEqual(car.MSRP, cars[10].MSRP);
            Assert.AreEqual(car.IMGFilePath, cars[10].IMGFilePath);
            Assert.AreEqual(car.VehicleDetails, cars[10].VehicleDetails);

        }

        [Test]
        public void CanDeleteCar()
        {
            Car car = new Car
            {
                ModelYear = new DateTime(2015, 1, 1),
                IsNew = false,
                IsFeatured = true,
                IsSold = false,
                UnitsInStock = 1,
                Mileage = "20000",
                VIN = "5ABC5ABC5ABC5ABC5",
                BodyColorId = 5,
                BodyStyleId = 3,
                TransmissionId = 2,
                MakeId = 3,
                ModelId = 2,
                InteriorColorId = 5,
                SalePrice = 19500m,
                MSRP = 21000m,
                IMGFilePath = "Images/placeholder.png",
                VehicleDetails = "2015 Ford Escape. Fully Loaded!"
            };

            CarsRepositoryADO repo = new CarsRepositoryADO();

            repo.Insert(car);

            var newCar = repo.GetCarById(11);

            Assert.IsNotNull(newCar);

            repo.Delete(11);

            newCar = repo.GetCarById(11);

            Assert.IsNull(newCar);

        }

        [Test]
        public void CanUpdateCar()
        {
            Car car = new Car
            {
                ModelYear = new DateTime(2015, 1, 1),
                IsNew = false,
                IsFeatured = true,
                IsSold = false,
                UnitsInStock = 1,
                Mileage = "20000",
                VIN = "5ABC5ABC5ABC5ABC5",
                BodyColorId = 5,
                BodyStyleId = 3,
                TransmissionId = 2,
                MakeId = 3,
                ModelId = 2,
                InteriorColorId = 5,
                SalePrice = 19500m,
                MSRP = 21000m,
                IMGFilePath = "Images/placeholder.png",
                VehicleDetails = "2015 Ford Escape. Fully Loaded!"
            };

            CarsRepositoryADO repo = new CarsRepositoryADO();

            repo.Insert(car);

            car.BodyColorId = 2;
            car.InteriorColorId = 5;
            car.SalePrice = 17500m;
            car.MSRP = 19200m;
            car.IMGFilePath = "Images/updatedImage.png";
            car.IsSold = true;
            car.IsNew = true;
            car.IsFeatured = true;
            car.VIN = "6ABC6ABC6ABC6ABC6";
            car.VehicleDetails = "Updated";
            car.Mileage = "3";
            car.ModelYear = new DateTime(2018, 1, 1);
            car.MakeId = 2;
            car.ModelId = 3;
            car.TransmissionId = 1;
            car.UnitsInStock = 9;
            car.BodyStyleId = 2;

            repo.Update(car);

            var updatedCar = repo.GetCarById(11);

            Assert.AreEqual(updatedCar.BodyStyleId, 2);
            Assert.AreEqual(updatedCar.BodyColorId, 2);
            Assert.AreEqual(updatedCar.InteriorColorId, 5);
            Assert.AreEqual(updatedCar.IMGFilePath, "Images/updatedImage.png");
            Assert.AreEqual(updatedCar.SalePrice, 17500m);
            Assert.AreEqual(updatedCar.MSRP, 19200m);
            Assert.AreEqual(updatedCar.IsNew, true);
            Assert.AreEqual(updatedCar.IsFeatured, true);
            Assert.AreEqual(updatedCar.IsSold, true);
            Assert.AreEqual(updatedCar.VIN, "6ABC6ABC6ABC6ABC6");
            Assert.AreEqual(updatedCar.VehicleDetails, "Updated");
            Assert.AreEqual(updatedCar.Mileage, "3");
            Assert.AreEqual(updatedCar.ModelYear, new DateTime(2018, 1, 1));
            Assert.AreEqual(updatedCar.MakeId, 2);
            Assert.AreEqual(updatedCar.ModelId, 3);
            Assert.AreEqual(updatedCar.TransmissionId, 1);
            Assert.AreEqual(updatedCar.UnitsInStock, 8);
        }

        [Test]
        public void CanSearchForNewCarOnMinYear()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();

            CarsSearchParameters searchParameters = new CarsSearchParameters
            {
                IsNew = true,
                MinYear = new DateTime(2017,1,1),
                MaxYear =  null,
                MaxPrice = null,
                MinPrice = null,
                SearchTerm = null
            };

            List<SearchResultItem> searchCarResults = repo.SearchCars(searchParameters).ToList();

            Assert.AreEqual(5, searchCarResults.Count);

            Assert.AreEqual(1, searchCarResults[1].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[1].Year);
            Assert.AreEqual("Toyota", searchCarResults[1].Make);
            Assert.AreEqual("Tundra LX", searchCarResults[1].Model);
            Assert.AreEqual("Black", searchCarResults[1].BodyColor);
            Assert.AreEqual("Black", searchCarResults[1].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[1].Transmission);
            Assert.AreEqual("/Images/2017ToyotaTundra1794.jpg", searchCarResults[1].IMGURL);
            Assert.AreEqual("1ABC1ABC1ABC1ABC1", searchCarResults[1].VIN);
            Assert.AreEqual("0", searchCarResults[1].Mileage);
            Assert.AreEqual(50315m, searchCarResults[1].SalePrice);
            Assert.AreEqual(51815m, searchCarResults[1].MSRP);
        }

        [Test]
        public void CanSearchForNewCarOnMaxYear()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();

            CarsSearchParameters searchParameters = new CarsSearchParameters
            {
                IsNew = true,
                MinYear = null,
                MaxYear = new DateTime(2017,1,1),
                MaxPrice = null,
                MinPrice = null,
                SearchTerm = null
            };

            List<SearchResultItem> searchCarResults = repo.SearchCars(searchParameters).ToList();

            Assert.AreEqual(4, searchCarResults.Count);

            Assert.AreEqual(1, searchCarResults[1].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[1].Year);
            Assert.AreEqual("Toyota", searchCarResults[1].Make);
            Assert.AreEqual("Tundra LX", searchCarResults[1].Model);
            Assert.AreEqual("Black", searchCarResults[1].BodyColor);
            Assert.AreEqual("Black", searchCarResults[1].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[1].Transmission);
            Assert.AreEqual("/Images/2017ToyotaTundra1794.jpg", searchCarResults[1].IMGURL);
            Assert.AreEqual("1ABC1ABC1ABC1ABC1", searchCarResults[1].VIN);
            Assert.AreEqual("0", searchCarResults[1].Mileage);
            Assert.AreEqual(50315m, searchCarResults[1].SalePrice);
            Assert.AreEqual(51815m, searchCarResults[1].MSRP);
        }

        [Test]
        public void CanSearchForNewCarOnMaxPrice()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();

            CarsSearchParameters searchParameters = new CarsSearchParameters
            {
                IsNew = true,
                MinYear = null,
                MaxYear = null,
                MaxPrice = 51000m,
                MinPrice = null,
                SearchTerm = null
            };

            List<SearchResultItem> searchCarResults = repo.SearchCars(searchParameters).ToList();

            Assert.AreEqual(4, searchCarResults.Count);

            Assert.AreEqual(1, searchCarResults[0].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[0].Year);
            Assert.AreEqual("Toyota", searchCarResults[0].Make);
            Assert.AreEqual("Tundra LX", searchCarResults[0].Model);
            Assert.AreEqual("Black", searchCarResults[0].BodyColor);
            Assert.AreEqual("Black", searchCarResults[0].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[0].Transmission);
            Assert.AreEqual("/Images/2017ToyotaTundra1794.jpg", searchCarResults[0].IMGURL);
            Assert.AreEqual("1ABC1ABC1ABC1ABC1", searchCarResults[0].VIN);
            Assert.AreEqual("0", searchCarResults[0].Mileage);
            Assert.AreEqual(50315m, searchCarResults[0].SalePrice);
            Assert.AreEqual(51815m, searchCarResults[0].MSRP);
        }

        [Test]
        public void CanSearchForNewCarOnMinPrice()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();

            CarsSearchParameters searchParameters = new CarsSearchParameters
            {
                IsNew = true,
                MinYear = null,
                MaxYear = null,
                MaxPrice = null,
                MinPrice = 50000m,
                SearchTerm = null
            };

            List<SearchResultItem> searchCarResults = repo.SearchCars(searchParameters).ToList();

            Assert.AreEqual(3, searchCarResults.Count);

            Assert.AreEqual(1, searchCarResults[1].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[1].Year);
            Assert.AreEqual("Toyota", searchCarResults[1].Make);
            Assert.AreEqual("Tundra LX", searchCarResults[1].Model);
            Assert.AreEqual("Black", searchCarResults[1].BodyColor);
            Assert.AreEqual("Black", searchCarResults[1].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[1].Transmission);
            Assert.AreEqual("/Images/2017ToyotaTundra1794.jpg", searchCarResults[1].IMGURL);
            Assert.AreEqual("1ABC1ABC1ABC1ABC1", searchCarResults[1].VIN);
            Assert.AreEqual("0", searchCarResults[1].Mileage);
            Assert.AreEqual(50315m, searchCarResults[1].SalePrice);
            Assert.AreEqual(51815m, searchCarResults[1].MSRP);
        }

        [Test]
        public void CanSearchForNewCarSearchTerm()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();

            CarsSearchParameters searchParametersMake = new CarsSearchParameters
            {
                IsNew = true,
                MinYear = null,
                MaxYear = null,
                MaxPrice = null,
                MinPrice = null,
                SearchTerm = "Toy"
            };

            CarsSearchParameters searchParametersModel = new CarsSearchParameters
            {
                IsNew = true,
                MinYear = null,
                MaxYear = null,
                MaxPrice = null,
                MinPrice = null,
                SearchTerm = "Tundra LX"
            };

            CarsSearchParameters searchParametersYear = new CarsSearchParameters
            {
                IsNew = true,
                MinYear = null,
                MaxYear = null,
                MaxPrice = null,
                MinPrice = null,
                SearchTerm = "2017"
            };

            List<SearchResultItem> searchCarResults = repo.SearchCars(searchParametersMake).ToList();

            Assert.AreEqual(1, searchCarResults.Count);

            Assert.AreEqual(1, searchCarResults[0].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[0].Year);
            Assert.AreEqual("Toyota", searchCarResults[0].Make);
            Assert.AreEqual("Tundra LX", searchCarResults[0].Model);
            Assert.AreEqual("Black", searchCarResults[0].BodyColor);
            Assert.AreEqual("Black", searchCarResults[0].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[0].Transmission);
            Assert.AreEqual("/Images/2017ToyotaTundra1794.jpg", searchCarResults[0].IMGURL);
            Assert.AreEqual("1ABC1ABC1ABC1ABC1", searchCarResults[0].VIN);
            Assert.AreEqual("0", searchCarResults[0].Mileage);
            Assert.AreEqual(50315m, searchCarResults[0].SalePrice);
            Assert.AreEqual(51815m, searchCarResults[0].MSRP);

            searchCarResults.Clear();

            searchCarResults = repo.SearchCars(searchParametersModel).ToList();

            Assert.AreEqual(1, searchCarResults.Count);

            Assert.AreEqual(1, searchCarResults[0].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[0].Year);
            Assert.AreEqual("Toyota", searchCarResults[0].Make);
            Assert.AreEqual("Tundra LX", searchCarResults[0].Model);
            Assert.AreEqual("Black", searchCarResults[0].BodyColor);
            Assert.AreEqual("Black", searchCarResults[0].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[0].Transmission);
            Assert.AreEqual("/Images/2017ToyotaTundra1794.jpg", searchCarResults[0].IMGURL);
            Assert.AreEqual("1ABC1ABC1ABC1ABC1", searchCarResults[0].VIN);
            Assert.AreEqual("0", searchCarResults[0].Mileage);
            Assert.AreEqual(50315m, searchCarResults[0].SalePrice);
            Assert.AreEqual(51815m, searchCarResults[0].MSRP);

            searchCarResults.Clear();

            searchCarResults = repo.SearchCars(searchParametersYear).ToList();

            Assert.AreEqual(4, searchCarResults.Count);

            Assert.AreEqual(1, searchCarResults[1].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[1].Year);
            Assert.AreEqual("Toyota", searchCarResults[1].Make);
            Assert.AreEqual("Tundra LX", searchCarResults[1].Model);
            Assert.AreEqual("Black", searchCarResults[1].BodyColor);
            Assert.AreEqual("Black", searchCarResults[1].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[1].Transmission);
            Assert.AreEqual("/Images/2017ToyotaTundra1794.jpg", searchCarResults[1].IMGURL);
            Assert.AreEqual("1ABC1ABC1ABC1ABC1", searchCarResults[1].VIN);
            Assert.AreEqual("0", searchCarResults[1].Mileage);
            Assert.AreEqual(50315m, searchCarResults[1].SalePrice);
            Assert.AreEqual(51815m, searchCarResults[1].MSRP);
        }

        [Test]
        public void CanSearchForUsedCarOnMinYear()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();

            CarsSearchParameters searchParameters = new CarsSearchParameters
            {
                IsNew = false,
                MinYear = new DateTime(2017, 1, 1),
                MaxYear = null,
                MaxPrice = null,
                MinPrice = null,
                SearchTerm = null
            };

            List<SearchResultItem> searchCarResults = repo.SearchCars(searchParameters).ToList();

            Assert.AreEqual(4, searchCarResults.Count);

            Assert.AreEqual(3, searchCarResults[1].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[1].Year);
            Assert.AreEqual("Ford", searchCarResults[1].Make);
            Assert.AreEqual("Escape", searchCarResults[1].Model);
            Assert.AreEqual("White", searchCarResults[1].BodyColor);
            Assert.AreEqual("White", searchCarResults[1].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[1].Transmission);
            Assert.AreEqual("/Images/2017FordEscape.png", searchCarResults[1].IMGURL);
            Assert.AreEqual("3ABC3ABC3ABC3ABC3", searchCarResults[1].VIN);
            Assert.AreEqual("1200", searchCarResults[1].Mileage);
            Assert.AreEqual(22669m, searchCarResults[1].SalePrice);
            Assert.AreEqual(24500m, searchCarResults[1].MSRP);
        }

        [Test]
        public void CanSearchForUsedCarOnMaxYear()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();

            CarsSearchParameters searchParameters = new CarsSearchParameters
            {
                IsNew = false,
                MinYear = null,
                MaxYear = new DateTime(2017, 1, 1),
                MaxPrice = null,
                MinPrice = null,
                SearchTerm = null
            };

            List<SearchResultItem> searchCarResults = repo.SearchCars(searchParameters).ToList();

            Assert.AreEqual(5, searchCarResults.Count);

            Assert.AreEqual(3, searchCarResults[1].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[1].Year);
            Assert.AreEqual("Ford", searchCarResults[1].Make);
            Assert.AreEqual("Escape", searchCarResults[1].Model);
            Assert.AreEqual("White", searchCarResults[1].BodyColor);
            Assert.AreEqual("White", searchCarResults[1].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[1].Transmission);
            Assert.AreEqual("/Images/2017FordEscape.png", searchCarResults[1].IMGURL);
            Assert.AreEqual("3ABC3ABC3ABC3ABC3", searchCarResults[1].VIN);
            Assert.AreEqual("1200", searchCarResults[1].Mileage);
            Assert.AreEqual(22669m, searchCarResults[1].SalePrice);
            Assert.AreEqual(24500m, searchCarResults[1].MSRP);
        }

        [Test]
        public void CanSearchForUsedCarOnMaxPrice()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();

            CarsSearchParameters searchParameters = new CarsSearchParameters
            {
                IsNew = false,
                MinYear = null,
                MaxYear = null,
                MaxPrice = 51000m,
                MinPrice = null,
                SearchTerm = null
            };

            List<SearchResultItem> searchCarResults = repo.SearchCars(searchParameters).ToList();

            Assert.AreEqual(5, searchCarResults.Count);

            Assert.AreEqual(3, searchCarResults[1].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[1].Year);
            Assert.AreEqual("Ford", searchCarResults[1].Make);
            Assert.AreEqual("Escape", searchCarResults[1].Model);
            Assert.AreEqual("White", searchCarResults[1].BodyColor);
            Assert.AreEqual("White", searchCarResults[1].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[1].Transmission);
            Assert.AreEqual("/Images/2017FordEscape.png", searchCarResults[1].IMGURL);
            Assert.AreEqual("3ABC3ABC3ABC3ABC3", searchCarResults[1].VIN);
            Assert.AreEqual("1200", searchCarResults[1].Mileage);
            Assert.AreEqual(22669m, searchCarResults[1].SalePrice);
            Assert.AreEqual(24500m, searchCarResults[1].MSRP);
        }

        [Test]
        public void CanSearchForUsedCarOnMinPrice()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();

            CarsSearchParameters searchParameters = new CarsSearchParameters
            {
                IsNew = false,
                MinYear = null,
                MaxYear = null,
                MaxPrice = null,
                MinPrice = 4000m,
                SearchTerm = null
            };

            List<SearchResultItem> searchCarResults = repo.SearchCars(searchParameters).ToList();

            Assert.AreEqual(5, searchCarResults.Count);

            Assert.AreEqual(3, searchCarResults[1].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[1].Year);
            Assert.AreEqual("Ford", searchCarResults[1].Make);
            Assert.AreEqual("Escape", searchCarResults[1].Model);
            Assert.AreEqual("White", searchCarResults[1].BodyColor);
            Assert.AreEqual("White", searchCarResults[1].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[1].Transmission);
            Assert.AreEqual("/Images/2017FordEscape.png", searchCarResults[1].IMGURL);
            Assert.AreEqual("3ABC3ABC3ABC3ABC3", searchCarResults[1].VIN);
            Assert.AreEqual("1200", searchCarResults[1].Mileage);
            Assert.AreEqual(22669m, searchCarResults[1].SalePrice);
            Assert.AreEqual(24500m, searchCarResults[1].MSRP);
        }

        [Test]
        public void CanSearchForUsedCarSearchTerm()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();

            CarsSearchParameters searchParametersMake = new CarsSearchParameters
            {
                IsNew = false,
                MinYear = null,
                MaxYear = null,
                MaxPrice = null,
                MinPrice = null,
                SearchTerm = "For"
            };

            CarsSearchParameters searchParametersModel = new CarsSearchParameters
            {
                IsNew = false,
                MinYear = null,
                MaxYear = null,
                MaxPrice = null,
                MinPrice = null,
                SearchTerm = "Esc"
            };

            CarsSearchParameters searchParametersYear = new CarsSearchParameters
            {
                IsNew = false,
                MinYear = null,
                MaxYear = null,
                MaxPrice = null,
                MinPrice = null,
                SearchTerm = "2017"
            };

            List<SearchResultItem> searchCarResults = repo.SearchCars(searchParametersMake).ToList();

            Assert.AreEqual(1, searchCarResults.Count);

            Assert.AreEqual(3, searchCarResults[0].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[0].Year);
            Assert.AreEqual("Ford", searchCarResults[0].Make);
            Assert.AreEqual("Escape", searchCarResults[0].Model);
            Assert.AreEqual("White", searchCarResults[0].BodyColor);
            Assert.AreEqual("White", searchCarResults[0].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[0].Transmission);
            Assert.AreEqual("/Images/2017FordEscape.png", searchCarResults[0].IMGURL);
            Assert.AreEqual("3ABC3ABC3ABC3ABC3", searchCarResults[0].VIN);
            Assert.AreEqual("1200", searchCarResults[0].Mileage);
            Assert.AreEqual(22669m, searchCarResults[0].SalePrice);
            Assert.AreEqual(24500m, searchCarResults[0].MSRP);

            searchCarResults.Clear();

            searchCarResults = repo.SearchCars(searchParametersModel).ToList();

            Assert.AreEqual(1, searchCarResults.Count);

            Assert.AreEqual(3, searchCarResults[0].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[0].Year);
            Assert.AreEqual("Ford", searchCarResults[0].Make);
            Assert.AreEqual("Escape", searchCarResults[0].Model);
            Assert.AreEqual("White", searchCarResults[0].BodyColor);
            Assert.AreEqual("White", searchCarResults[0].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[0].Transmission);
            Assert.AreEqual("/Images/2017FordEscape.png", searchCarResults[0].IMGURL);
            Assert.AreEqual("3ABC3ABC3ABC3ABC3", searchCarResults[0].VIN);
            Assert.AreEqual("1200", searchCarResults[0].Mileage);
            Assert.AreEqual(22669m, searchCarResults[0].SalePrice);
            Assert.AreEqual(24500m, searchCarResults[0].MSRP);

            searchCarResults.Clear();

            searchCarResults = repo.SearchCars(searchParametersYear).ToList();

            Assert.AreEqual(4, searchCarResults.Count);

            Assert.AreEqual(3, searchCarResults[1].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[1].Year);
            Assert.AreEqual("Ford", searchCarResults[1].Make);
            Assert.AreEqual("Escape", searchCarResults[1].Model);
            Assert.AreEqual("White", searchCarResults[1].BodyColor);
            Assert.AreEqual("White", searchCarResults[1].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[1].Transmission);
            Assert.AreEqual("/Images/2017FordEscape.png", searchCarResults[1].IMGURL);
            Assert.AreEqual("3ABC3ABC3ABC3ABC3", searchCarResults[1].VIN);
            Assert.AreEqual("1200", searchCarResults[1].Mileage);
            Assert.AreEqual(22669m, searchCarResults[1].SalePrice);
            Assert.AreEqual(24500m, searchCarResults[1].MSRP);
        }

        [Test]
        public void CanReturnNewCarsWithoutParameters()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();

            CarsSearchParameters searchParameters = new CarsSearchParameters
            {
                IsNew = true,
                MinYear = null,
                MaxYear = null,
                MaxPrice = null,
                MinPrice = null,
                SearchTerm = null 
            };

            List<SearchResultItem> searchCarResults = repo.SearchCars(searchParameters).ToList();

            Assert.AreEqual(5, searchCarResults.Count);

            Assert.AreEqual(1, searchCarResults[1].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[1].Year);
            Assert.AreEqual("Toyota", searchCarResults[1].Make);
            Assert.AreEqual("Tundra LX", searchCarResults[1].Model);
            Assert.AreEqual("Black", searchCarResults[1].BodyColor);
            Assert.AreEqual("Black", searchCarResults[1].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[1].Transmission);
            Assert.AreEqual("/Images/2017ToyotaTundra1794.jpg", searchCarResults[1].IMGURL);
            Assert.AreEqual("1ABC1ABC1ABC1ABC1", searchCarResults[1].VIN);
            Assert.AreEqual("0", searchCarResults[1].Mileage);
            Assert.AreEqual(50315m, searchCarResults[1].SalePrice);
            Assert.AreEqual(51815m, searchCarResults[1].MSRP);
        }

        [Test]
        public void CanReturnUsedCarsWithoutParameters()
        {
            CarsRepositoryADO repo = new CarsRepositoryADO();

            CarsSearchParameters searchParameters = new CarsSearchParameters
            {
                IsNew = false,
                MinYear = null,
                MaxYear = null,
                MaxPrice = null,
                MinPrice = null,
                SearchTerm = null
            };

            List<SearchResultItem> searchCarResults = repo.SearchCars(searchParameters).ToList();

            Assert.AreEqual(5, searchCarResults.Count);

            Assert.AreEqual(3, searchCarResults[1].CarId);
            Assert.AreEqual(new DateTime(2017,1,1).Year.ToString(), searchCarResults[1].Year);
            Assert.AreEqual("Ford", searchCarResults[1].Make);
            Assert.AreEqual("Escape", searchCarResults[1].Model);
            Assert.AreEqual("White", searchCarResults[1].BodyColor);
            Assert.AreEqual("White", searchCarResults[1].InteriorColor);
            Assert.AreEqual("Automatic", searchCarResults[1].Transmission);
            Assert.AreEqual("/Images/2017FordEscape.png", searchCarResults[1].IMGURL);
            Assert.AreEqual("3ABC3ABC3ABC3ABC3", searchCarResults[1].VIN);
            Assert.AreEqual("1200", searchCarResults[1].Mileage);
            Assert.AreEqual(22669m, searchCarResults[1].SalePrice);
            Assert.AreEqual(24500m, searchCarResults[1].MSRP);
        }
    }
}

