using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
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
                    SqlCommand cmd = new SqlCommand("CarDelete", dbConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

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
                    SqlCommand cmd = new SqlCommand("SelectAllCars", dbConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Car car = new Car
                            {
                                CarId = (int)dr["CarId"],
                                ModelYear = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"]))
                            };
                            ;
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
                    SqlCommand cmd = new SqlCommand("SelectFeaturedCars", dbConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@isFeatured", "true");

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            FeaturedShortListItem featuredCar = new FeaturedShortListItem
                            {
                                CarId = (int)dr["CarId"],
                                ImageURL = dr["IMGFilePath"].ToString(),
                                MakeId = (int)dr["MakeId"],
                                ModelId = (int)dr["ModelId"],
                                Make = dr["MakeName"].ToString(),
                                Model = dr["ModelName"].ToString(),
                                Year = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"])),
                                Price = (decimal)dr["SalePrice"]
                            };

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
                    SqlCommand cmd = new SqlCommand("SelectAllNewCars", dbConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Car car = new Car
                            {
                                CarId = (int)dr["CarId"],
                                ModelYear = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"])),
                                IsNew = (bool)dr["IsNew"],
                                IsFeatured = (bool)dr["IsFeatured"],
                                IsSold = (bool)dr["IsSold"],
                                BodyColorId = (int)dr["BodyColorId"],
                                BodyStyleId = (int)dr["BodyStyleId"],
                                InteriorColorId = (int)dr["InteriorColorId"],
                                ModelId = (int)dr["ModelId"],
                                MakeId = (int)dr["MakeId"],
                                MSRP = (decimal)dr["MSRP"],
                                SalePrice = (decimal)dr["SalePrice"],
                                Mileage = dr["Mileage"].ToString(),
                                TransmissionId = (int)dr["TransmissionId"],
                                UnitsInStock = (int)dr["UnitsInStock"],
                                VehicleDetails = dr["VehicleDetails"].ToString(),
                                VIN = dr["VIN"].ToString(),
                                IMGFilePath = dr["IMGFILEPath"].ToString()
                            };

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
                    SqlCommand cmd = new SqlCommand("SelectAllSoldCars", dbConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Car car = new Car
                            {
                                CarId = (int)dr["CarId"],
                                ModelYear = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"])),
                                IsNew = (bool)dr["IsNew"],
                                IsFeatured = (bool)dr["IsFeatured"],
                                IsSold = (bool)dr["IsSold"],
                                BodyColorId = (int)dr["BodyColorId"],
                                BodyStyleId = (int)dr["BodyStyleId"],
                                InteriorColorId = (int)dr["InteriorColorId"],
                                ModelId = (int)dr["ModelId"],
                                MakeId = (int)dr["MakeId"],
                                MSRP = (decimal)dr["MSRP"],
                                SalePrice = (decimal)dr["SalePrice"],
                                Mileage = dr["Mileage"].ToString(),
                                TransmissionId = (int)dr["TransmissionId"],
                                UnitsInStock = (int)dr["UnitsInStock"],
                                VehicleDetails = dr["VehicleDetails"].ToString(),
                                VIN = dr["VIN"].ToString(),
                                IMGFilePath = dr["IMGFILEPath"].ToString()
                            };

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
                    SqlCommand cmd = new SqlCommand("SelectAllUnsoldCars", dbConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Car car = new Car
                            {
                                CarId = (int)dr["CarId"],
                                ModelYear = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"])),
                                IsNew = (bool)dr["IsNew"],
                                IsFeatured = (bool)dr["IsFeatured"],
                                IsSold = (bool)dr["IsSold"],
                                BodyColorId = (int)dr["BodyColorId"],
                                BodyStyleId = (int)dr["BodyStyleId"],
                                InteriorColorId = (int)dr["InteriorColorId"],
                                ModelId = (int)dr["ModelId"],
                                MakeId = (int)dr["MakeId"],
                                MSRP = (decimal)dr["MSRP"],
                                SalePrice = (decimal)dr["SalePrice"],
                                Mileage = dr["Mileage"].ToString(),
                                TransmissionId = (int)dr["TransmissionId"],
                                UnitsInStock = (int)dr["UnitsInStock"],
                                VehicleDetails = dr["VehicleDetails"].ToString(),
                                VIN = dr["VIN"].ToString(),
                                IMGFilePath = dr["IMGFILEPath"].ToString()
                            };

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
                    SqlCommand cmd = new SqlCommand("SelectAllUsedCars", dbConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Car car = new Car
                            {
                                CarId = (int)dr["CarId"],
                                ModelYear = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"])),
                                IsNew = (bool)dr["IsNew"],
                                IsFeatured = (bool)dr["IsFeatured"],
                                IsSold = (bool)dr["IsSold"],
                                BodyColorId = (int)dr["BodyColorId"],
                                BodyStyleId = (int)dr["BodyStyleId"],
                                InteriorColorId = (int)dr["InteriorColorId"],
                                ModelId = (int)dr["ModelId"],
                                MakeId = (int)dr["MakeId"],
                                MSRP = (decimal)dr["MSRP"],
                                SalePrice = (decimal)dr["SalePrice"],
                                Mileage = dr["Mileage"].ToString(),
                                TransmissionId = (int)dr["TransmissionId"],
                                UnitsInStock = (int)dr["UnitsInStock"],
                                VehicleDetails = dr["VehicleDetails"].ToString(),
                                VIN = dr["VIN"].ToString(),
                                IMGFilePath = dr["IMGFILEPath"].ToString()
                            };

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

                    SqlCommand cmd = new SqlCommand("SelectCarById", dbConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@CarId", CarId);



                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            car = new Car
                            {
                                CarId = (int)dr["CarId"],
                                ModelYear = (dr["ModelYear"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModelYear"])),
                                IsNew = (bool)dr["IsNew"],
                                IsFeatured = (bool)dr["IsFeatured"],
                                IsSold = (bool)dr["IsSold"],
                                BodyColorId = (int)dr["BodyColorId"],
                                BodyStyleId = (int)dr["BodyStyleId"],
                                InteriorColorId = (int)dr["InteriorColorId"],
                                ModelId = (int)dr["ModelId"],
                                MakeId = (int)dr["MakeId"],
                                MSRP = (decimal)dr["MSRP"],
                                SalePrice = (decimal)dr["SalePrice"],
                                Mileage = dr["Mileage"].ToString(),
                                TransmissionId = (int)dr["TransmissionId"],
                                UnitsInStock = (int)dr["UnitsInStock"],
                                VehicleDetails = dr["VehicleDetails"].ToString(),
                                VIN = dr["VIN"].ToString(),
                                IMGFilePath = dr["IMGFILEPath"].ToString()
                            };
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
                    SqlCommand cmd = new SqlCommand("CarInsert", dbConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter param = new SqlParameter("@CarId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

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
            if (Car.IsSold == true)
            {
                if (Car.UnitsInStock >= 1)
                {
                    Car.UnitsInStock--;
                }
            }

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("CarUpdate", dbConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

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

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                //string query = "SELECT TOP 20 c.CarId AS \"CarId\", c.IsSold As \"Sold\", c.ModelYear AS \"ModelYear\", bs.BodyStyleType As \"BodyStyle\", mk.MakeName AS \"Make\",  md.ModelName AS \"Model\", c.IMGFilePath AS \"IMGURL\"," +
                //    "ic.ColorName AS \"InteriorColor\", bc.ColorName AS \"BodyColor\", t.TransmissionType AS \"Transmission\","
                //    + "c.Mileage AS \"Mileage\", c.VIN AS \"VIN\", c.SalePrice AS \"SalePrice\", c.MSRP AS \"MSRP\"" +
                //    "FROM Cars c INNER JOIN Make mk ON mk.MakeId = c.MakeId INNER JOIN BodyStyle bs ON bs.BodyStyleId = c.BodyStyleId INNER JOIN Model md ON md.MakeId = mk.MakeId INNER JOIN " +
                //    "Color bc ON c.BodyColorId = bc.ColorId INNER JOIN Color ic ON ic.ColorId = c.InteriorColorId  INNER JOIN " +
                //    "Transmission t ON t.TransmissionId = c.TransmissionId WHERE 1 = 1 ";

                string query = "SELECT TOP 20 c.CarId AS \"CarId\", c.IsSold As \"Sold\", c.ModelYear AS \"ModelYear\", bs.BodyStyleType As \"BodyStyle\", mk.MakeName AS \"Make\",  md.ModelName AS \"Model\", c.IMGFilePath AS \"IMGURL\"," +
                               "ic.ColorName AS \"InteriorColor\", bc.ColorName AS \"BodyColor\", t.TransmissionType AS \"Transmission\","
                               + "c.Mileage AS \"Mileage\", c.VIN AS \"VIN\", c.SalePrice AS \"SalePrice\", c.MSRP AS \"MSRP\"" +
                               "FROM Cars c INNER JOIN Make mk ON mk.MakeId = c.MakeId INNER JOIN BodyStyle bs ON bs.BodyStyleId = c.BodyStyleId INNER JOIN Model md ON md.ModelId = c.MakeId INNER JOIN " +
                               "Color bc ON c.BodyColorId = bc.ColorId INNER JOIN Color ic ON ic.ColorId = c.InteriorColorId  INNER JOIN " +
                               "Transmission t ON t.TransmissionId = c.TransmissionId WHERE 1 = 1 AND c.IsSold = 0 ";

                SqlCommand cmd = new SqlCommand
                {
                    Connection = dbConnection
                };

                bool isNewIsNotNull = Parameters.IsNew.HasValue;

                if (isNewIsNotNull)
                {
                    query += "AND IsNew = @IsNew ";
                    cmd.Parameters.AddWithValue("@IsNew", Parameters.IsNew);
                }

                if (Parameters.MinYear.HasValue && isNewIsNotNull)
                {
                    query += "AND ModelYear >= @MinYear AND IsNew = @IsNew ";
                    cmd.Parameters.AddWithValue("@MinYear", Parameters.MinYear.Value);
                }
                else if (Parameters.MinYear.HasValue)
                {
                    query += "AND ModelYear >= @MinYear ";
                    cmd.Parameters.AddWithValue("@MinYear", Parameters.MinYear.Value);
                }

                if (Parameters.MaxYear.HasValue && isNewIsNotNull)
                {
                    query += "AND ModelYear <= @MaxYear AND IsNew = @IsNew  ";
                    cmd.Parameters.AddWithValue("@MaxYear", Parameters.MaxYear.Value);
                }
                else if (Parameters.MaxYear.HasValue)
                {
                    query += "AND ModelYear <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", Parameters.MaxYear.Value);
                }

                if (Parameters.MinPrice > 0 && isNewIsNotNull)
                {
                    query += "AND SalePrice >= @MinPrice AND IsNew = @IsNew ";
                    cmd.Parameters.AddWithValue("@MinPrice", Parameters.MinPrice.Value);
                }
                else if (Parameters.MinPrice > 0)
                {
                    query += "AND SalePrice >= @MinPrice ";
                    cmd.Parameters.AddWithValue("@MinPrice ", Parameters.MinPrice.Value);
                }

                if (Parameters.MaxPrice > 0 && isNewIsNotNull)
                {
                    query += "AND SalePrice <= @MaxPrice AND IsNew = @IsNew ";
                    cmd.Parameters.AddWithValue("@MaxPrice", Parameters.MaxPrice.Value);
                }
                else if (Parameters.MaxPrice > 0)
                {
                    query += "AND SalePrice <= @MaxPrice ";
                    cmd.Parameters.AddWithValue("@MaxPrice", Parameters.MaxPrice.Value);
                }

                if (!string.IsNullOrEmpty(Parameters.SearchTerm) && isNewIsNotNull)
                {
                    query += "AND (MakeName LIKE @SearchTerm AND IsNew = @IsNew) OR (ModelName LIKE @SearchTerm AND IsNew = @IsNew) OR (ModelYear LIKE @SearchTerm AND IsNew = @IsNew) ";
                    cmd.Parameters.AddWithValue("@SearchTerm", Parameters.SearchTerm + '%');
                }
                else if (!string.IsNullOrEmpty(Parameters.SearchTerm))
                {
                    query += "AND (MakeName LIKE @SearchTerm) OR (ModelName LIKE @SearchTerm) OR (ModelYear LIKE @SearchTerm) ";
                    cmd.Parameters.AddWithValue("@SearchTerm", Parameters.SearchTerm + '%');
                }

                //query += "GROUP BY CarId, MakeName, ModelName, ModelYear, bs.BodyStyleType, IMGFilePath, bc.ColorName, ic.ColorName, TransmissionType, " +
                //        "Mileage, VIN, SalePrice, MSRP, IsSold ORDER BY MSRP DESC";
                query += "ORDER BY MSRP DESC";
                 cmd.CommandText = query;

                dbConnection.Open();

                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SearchResultItem carSearchResult = new SearchResultItem
                            {
                                CarId = (int)dr["CarId"],
                                Year = (dr["ModelYear"] == DBNull.Value ? "" : Convert.ToDateTime(dr["ModelYear"]).Year.ToString()),
                                Make = dr["Make"].ToString(),
                                Model = dr["Model"].ToString(),
                                IMGURL = dr["IMGURL"].ToString(),
                                InteriorColor = dr["InteriorColor"].ToString(),
                                BodyColor = dr["BodyColor"].ToString(),
                                Transmission = dr["Transmission"].ToString(),
                                BodyStyle = dr["BodyStyle"].ToString(),
                                Mileage = dr["Mileage"].ToString(),
                                VIN = dr["VIN"].ToString(),
                                SalePrice = (decimal)dr["SalePrice"],
                                MSRP = (decimal)dr["MSRP"],
                                IsSold = (bool)dr["Sold"]
                            };


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
