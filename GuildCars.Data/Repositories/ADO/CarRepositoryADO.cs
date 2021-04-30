using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.Tables;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using GuildCars.Models.Queries;

namespace GuildCars.Data.Repositories.ADO
{
    public class CarsRepositoryADO : ICarsRepository
    {
        public void Delete(int CarId)
        {
            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("CarDelete", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CarId", CarId);

                    dbConnection.Open();

                    cmd.ExecuteNonQuery();
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
        }

        public IEnumerable<Car> GetAllCars()
        {
            List<Car> cars = new List<Car>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllCars", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Car car = new Car();

                            car.CarId = (int)dr["CarId"];
                            car.ModelYear = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"])); ;
                            car.IsNew = (bool)dr["IsNew"];
                            car.IsFeatured = (bool)dr["IsFeatured"];
                            car.IsSold = (bool)dr["IsSold"];
                            car.BodyColorId = (int)dr["BodyColorId"];
                            car.BodyStyleId = (int)dr["BodyStyleId"];
                            car.InteriorColorId = (int)dr["InteriorColorId"];
                            car.ModelId = (int)dr["ModelId"];
                            car.MakeId = (int)dr["MakeId"];
                            car.MSRP = (decimal)dr["MSRP"];
                            car.SalePrice = (decimal)dr["SalePrice"];
                            car.Mileage = dr["Mileage"].ToString();
                            car.TransmissionId = (int)dr["TransmissionId"];
                            car.UnitsInStock = (int)dr["UnitsInStock"];
                            car.VehicleDetails = dr["VehicleDetails"].ToString();
                            car.VIN = dr["VIN"].ToString();
                            car.IMGFilePath = dr["IMGFILEPath"].ToString();

                            cars.Add(car);
                        }
                    }
                    return cars;
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
                return cars;
            }
        }

        public IEnumerable<FeaturedShortListItem> GetAllFeaturedCars()
        {
            List<FeaturedShortListItem> featuredCars = new List<FeaturedShortListItem>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectFeaturedCars", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@isFeatured", "true");

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            FeaturedShortListItem featuredCar = new FeaturedShortListItem();

                            featuredCar.CarId = (int)dr["CarId"];
                            featuredCar.ImageURL = dr["IMGFilePath"].ToString();
                            featuredCar.MakeId = (int)dr["MakeId"];
                            featuredCar.ModelId = (int)dr["ModelId"];
                            featuredCar.Make = dr["MakeName"].ToString();
                            featuredCar.Model = dr["ModelName"].ToString();
                            featuredCar.Year = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"]));
                            featuredCar.Price = (decimal)dr["SalePrice"];

                            featuredCars.Add(featuredCar);
                        }
                    }
                    return featuredCars;
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
                return featuredCars;
            }
        }

        public IEnumerable<Car> GetAllNewCars()
        {
            List<Car> cars = new List<Car>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllNewCars", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Car car = new Car();

                            car.CarId = (int)dr["CarId"];
                            car.ModelYear = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"]));
                            car.IsNew = (bool)dr["IsNew"];
                            car.IsFeatured = (bool)dr["IsFeatured"];
                            car.IsSold = (bool)dr["IsSold"];
                            car.BodyColorId = (int)dr["BodyColorId"];
                            car.BodyStyleId = (int)dr["BodyStyleId"];
                            car.InteriorColorId = (int)dr["InteriorColorId"];
                            car.ModelId = (int)dr["ModelId"];
                            car.MakeId = (int)dr["MakeId"];
                            car.MSRP = (decimal)dr["MSRP"];
                            car.SalePrice = (decimal)dr["SalePrice"];
                            car.Mileage = dr["Mileage"].ToString();
                            car.TransmissionId = (int)dr["TransmissionId"];
                            car.UnitsInStock = (int)dr["UnitsInStock"];
                            car.VehicleDetails = dr["VehicleDetails"].ToString();
                            car.VIN = dr["VIN"].ToString();
                            car.IMGFilePath = dr["IMGFILEPath"].ToString();

                            cars.Add(car);
                        }
                    }
                    return cars;
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
                return cars;
            }
        }

        public IEnumerable<Car> GetAllSoldCars()
        {
            List<Car> cars = new List<Car>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllSoldCars", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Car car = new Car();

                            car.CarId = (int)dr["CarId"];
                            car.ModelYear = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"]));
                            car.IsNew = (bool)dr["IsNew"];
                            car.IsFeatured = (bool)dr["IsFeatured"];
                            car.IsSold = (bool)dr["IsSold"];
                            car.BodyColorId = (int)dr["BodyColorId"];
                            car.BodyStyleId = (int)dr["BodyStyleId"];
                            car.InteriorColorId = (int)dr["InteriorColorId"];
                            car.ModelId = (int)dr["ModelId"];
                            car.MakeId = (int)dr["MakeId"];
                            car.MSRP = (decimal)dr["MSRP"];
                            car.SalePrice = (decimal)dr["SalePrice"];
                            car.Mileage = dr["Mileage"].ToString();
                            car.TransmissionId = (int)dr["TransmissionId"];
                            car.UnitsInStock = (int)dr["UnitsInStock"];
                            car.VehicleDetails = dr["VehicleDetails"].ToString();
                            car.VIN = dr["VIN"].ToString();
                            car.IMGFilePath = dr["IMGFILEPath"].ToString();

                            cars.Add(car);
                        }
                    }
                    return cars;
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
                return cars;
            }
        }

        public IEnumerable<Car> GetAllUnsoldCars()
        {
            List<Car> cars = new List<Car>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllUnsoldCars", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Car car = new Car();

                            car.CarId = (int)dr["CarId"];
                            car.ModelYear = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"]));
                            car.IsNew = (bool)dr["IsNew"];
                            car.IsFeatured = (bool)dr["IsFeatured"];
                            car.IsSold = (bool)dr["IsSold"];
                            car.BodyColorId = (int)dr["BodyColorId"];
                            car.BodyStyleId = (int)dr["BodyStyleId"];
                            car.InteriorColorId = (int)dr["InteriorColorId"];
                            car.ModelId = (int)dr["ModelId"];
                            car.MakeId = (int)dr["MakeId"];
                            car.MSRP = (decimal)dr["MSRP"];
                            car.SalePrice = (decimal)dr["SalePrice"];
                            car.Mileage = dr["Mileage"].ToString();
                            car.TransmissionId = (int)dr["TransmissionId"];
                            car.UnitsInStock = (int)dr["UnitsInStock"];
                            car.VehicleDetails = dr["VehicleDetails"].ToString();
                            car.VIN = dr["VIN"].ToString();
                            car.IMGFilePath = dr["IMGFILEPath"].ToString();

                            cars.Add(car);
                        }
                    }
                    return cars;
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
                return cars;
            }
        }

        public IEnumerable<Car> GetAllUsedCars()
        {
            List<Car> cars = new List<Car>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllUsedCars", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Car car = new Car();

                            car.CarId = (int)dr["CarId"];
                            car.ModelYear = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"]));
                            car.IsNew = (bool)dr["IsNew"];
                            car.IsFeatured = (bool)dr["IsFeatured"];
                            car.IsSold = (bool)dr["IsSold"];
                            car.BodyColorId = (int)dr["BodyColorId"];
                            car.BodyStyleId = (int)dr["BodyStyleId"];
                            car.InteriorColorId = (int)dr["InteriorColorId"];
                            car.ModelId = (int)dr["ModelId"];
                            car.MakeId = (int)dr["MakeId"];
                            car.MSRP = (decimal)dr["MSRP"];
                            car.SalePrice = (decimal)dr["SalePrice"];
                            car.Mileage = dr["Mileage"].ToString();
                            car.TransmissionId = (int)dr["TransmissionId"];
                            car.UnitsInStock = (int)dr["UnitsInStock"];
                            car.VehicleDetails = dr["VehicleDetails"].ToString();
                            car.VIN = dr["VIN"].ToString();
                            car.IMGFilePath = dr["IMGFILEPath"].ToString();

                            cars.Add(car);
                        }
                    }
                    return cars;
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
                return cars;
            }
        }

        public Car GetCarById(int CarId)
        {
            Car car = null;

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    dbConnection.Open();

                    SqlCommand cmd = new SqlCommand("SelectCarById", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CarId", CarId);



                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            car = new Car();
                            car.CarId = (int)dr["CarId"];
                            car.ModelYear = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"]));
                            car.IsNew = (bool)dr["IsNew"];
                            car.IsFeatured = (bool)dr["IsFeatured"];
                            car.IsSold = (bool)dr["IsSold"];
                            car.BodyColorId = (int)dr["BodyColorId"];
                            car.BodyStyleId = (int)dr["BodyStyleId"];
                            car.InteriorColorId = (int)dr["InteriorColorId"];
                            car.ModelId = (int)dr["ModelId"];
                            car.MakeId = (int)dr["MakeId"];
                            car.MSRP = (decimal)dr["MSRP"];
                            car.SalePrice = (decimal)dr["SalePrice"];
                            car.Mileage = dr["Mileage"].ToString();
                            car.TransmissionId = (int)dr["TransmissionId"];
                            car.UnitsInStock = (int)dr["UnitsInStock"];
                            car.VehicleDetails = dr["VehicleDetails"].ToString();
                            car.VIN = dr["VIN"].ToString();
                            car.IMGFilePath = dr["IMGFILEPath"].ToString();
                        }
                    }


                    return car;
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

                return car;
            }
        }

        public void Insert(Car car)
        {
            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("CarInsert", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@CarId", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(param);

                    cmd.Parameters.AddWithValue("@ModelYear", car.ModelYear);
                    cmd.Parameters.AddWithValue("@IsNew", car.IsNew);
                    cmd.Parameters.AddWithValue("@IsFeatured", car.IsFeatured);
                    cmd.Parameters.AddWithValue("@IsSold", car.IsSold);
                    cmd.Parameters.AddWithValue("@UnitsInStock", car.UnitsInStock);
                    cmd.Parameters.AddWithValue("@Mileage", car.Mileage);
                    cmd.Parameters.AddWithValue("@VIN", car.VIN);
                    cmd.Parameters.AddWithValue("@BodyColorId", car.BodyColorId);
                    cmd.Parameters.AddWithValue("@BodyStyleId", car.BodyStyleId);
                    cmd.Parameters.AddWithValue("@TransmissionId", car.TransmissionId);
                    cmd.Parameters.AddWithValue("@MakeId", car.MakeId);
                    cmd.Parameters.AddWithValue("@ModelId", car.ModelId);
                    cmd.Parameters.AddWithValue("@InteriorColorId", car.InteriorColorId);
                    cmd.Parameters.AddWithValue("@SalePrice", car.SalePrice);
                    cmd.Parameters.AddWithValue("@MSRP", car.MSRP);
                    cmd.Parameters.AddWithValue("@IMGFilePath", car.IMGFilePath);
                    cmd.Parameters.AddWithValue("@VehicleDetails", car.VehicleDetails);

                    dbConnection.Open();

                    cmd.ExecuteNonQuery();

                    car.CarId = (int)param.Value;
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
        }

        public void Update(Car Car)
        {
            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("CarUpdate", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CarId", Car.CarId);
                    cmd.Parameters.AddWithValue("@ModelYear", Car.ModelYear);
                    cmd.Parameters.AddWithValue("@IsNew", Car.IsNew);
                    cmd.Parameters.AddWithValue("@IsFeatured", Car.IsFeatured);
                    cmd.Parameters.AddWithValue("@IsSold", Car.IsSold);
                    cmd.Parameters.AddWithValue("@Mileage", Car.Mileage);
                    cmd.Parameters.AddWithValue("@UnitsInStock", Car.UnitsInStock);
                    cmd.Parameters.AddWithValue("@BodyColorId", Car.BodyColorId);
                    cmd.Parameters.AddWithValue("@BodyStyleId", Car.BodyStyleId);
                    cmd.Parameters.AddWithValue("@InteriorColorId", Car.InteriorColorId);
                    cmd.Parameters.AddWithValue("@VIN", Car.VIN);
                    cmd.Parameters.AddWithValue("@SalePrice", Car.SalePrice);
                    cmd.Parameters.AddWithValue("@MSRP", Car.MSRP);
                    cmd.Parameters.AddWithValue("@IMGFilePath", Car.IMGFilePath);
                    cmd.Parameters.AddWithValue("@VehicleDetails", Car.VehicleDetails);
                    cmd.Parameters.AddWithValue("@ModelId", Car.ModelId);
                    cmd.Parameters.AddWithValue("@MakeId", Car.MakeId);
                    cmd.Parameters.AddWithValue("@TransmissionId", Car.TransmissionId);

                    dbConnection.Open();

                    cmd.ExecuteNonQuery();
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
        }

        public IEnumerable<SearchResultItem> SearchCars(CarsSearchParameters Parameters)
        {
            List<SearchResultItem> carsSearchResults = new List<SearchResultItem>();
            bool parametersChosen = false;

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 20 c.CarId AS \"CarId\", c.ModelYear AS \"ModelYear\", mk.MakeName AS \"Make\",  md.ModelName AS \"Model\", c.IMGFilePath AS \"IMGURL\"," +
                    "ic.ColorName AS \"InteriorColor\", bc.ColorName AS \"BodyColor\", t.TransmissionType AS \"Transmission\","
                    + "c.Mileage AS \"Mileage\", c.VIN AS \"VIN\", c.SalePrice AS \"SalePrice\", c.MSRP AS \"MSRP\"" +
                    "FROM Cars c INNER JOIN Make mk ON mk.MakeId = c.MakeId INNER JOIN Model md ON md.MakeId = mk.MakeId INNER JOIN " +
                    "Color bc ON c.BodyColorId = bc.ColorId INNER JOIN Color ic ON ic.ColorId = c.InteriorColorId  INNER JOIN " +
                    "Transmission t ON t.TransmissionId = c.TransmissionId WHERE 1 = 1 ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbConnection;

                cmd.Parameters.AddWithValue("@IsNew", Parameters.IsNew);

                if (Parameters.MinYear.HasValue)
                {
                    query += "AND ModelYear >= @MinYear AND IsNew = @IsNew ";
                    cmd.Parameters.AddWithValue("@MinYear", Parameters.MinYear.Value);

                    parametersChosen = true;
                }

                if (Parameters.MaxYear.HasValue)
                {
                    query += "AND ModelYear <= @MaxYear AND IsNew = @IsNew  ";
                    cmd.Parameters.AddWithValue("@MaxYear", Parameters.MaxYear.Value);

                    parametersChosen = true;
                }

                if (Parameters.MinPrice.HasValue)
                {
                    query += "AND SalePrice >= @MinPrice AND IsNew = @IsNew ";
                    cmd.Parameters.AddWithValue("@MinPrice", Parameters.MinPrice.Value);

                    parametersChosen = true;
                }

                if (Parameters.MaxPrice.HasValue)
                {
                    query += "AND SalePrice <= @MaxPrice AND IsNew = @IsNew ";
                    cmd.Parameters.AddWithValue("@MaxPrice", Parameters.MaxPrice.Value);

                    parametersChosen = true;
                }

                if (!string.IsNullOrEmpty(Parameters.SearchTerm))
                {
                    query += "AND (MakeName LIKE @SearchTerm AND IsNew = @IsNew) OR (ModelName LIKE @SearchTerm AND IsNew = @IsNew) OR (ModelYear LIKE @SearchTerm AND IsNew = @IsNew) ";
                    cmd.Parameters.AddWithValue("@SearchTerm", Parameters.SearchTerm + '%');

                    parametersChosen = true;
                }

                if (parametersChosen)
                {
                    query += "GROUP BY CarId, MakeName, ModelName, ModelYear, IMGFilePath, bc.ColorName, ic.ColorName, TransmissionType, " +
                            "Mileage, VIN, SalePrice, MSRP Order by ModelYear ";
                    cmd.CommandText = query;
                }
                else
                {
                    query += "AND IsNew = @IsNew GROUP BY CarId, MakeName, ModelName, ModelYear, IMGFilePath, bc.ColorName, ic.ColorName, TransmissionType, " +
                            "Mileage, VIN, SalePrice, MSRP ORDER BY MSRP DESC ";
                    cmd.CommandText = query;
                }

                dbConnection.Open();
                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SearchResultItem carSearchResult = new SearchResultItem();

                            carSearchResult.CarId = (int)dr["CarId"];
                            carSearchResult.Year = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"]));
                            carSearchResult.Make = dr["Make"].ToString();
                            carSearchResult.Model = dr["Model"].ToString();
                            carSearchResult.IMGURL = dr["IMGURL"].ToString();
                            carSearchResult.InteriorColor = dr["InteriorColor"].ToString();
                            carSearchResult.BodyColor = dr["BodyColor"].ToString();
                            carSearchResult.Transmission = dr["Transmission"].ToString();
                            carSearchResult.Mileage = dr["Mileage"].ToString();
                            carSearchResult.VIN = dr["VIN"].ToString();
                            carSearchResult.SalePrice = (decimal)dr["SalePrice"];
                            carSearchResult.MSRP = (decimal)dr["MSRP"];


                            carsSearchResults.Add(carSearchResult);
                        }
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

            return carsSearchResults;
        }

    }
}
