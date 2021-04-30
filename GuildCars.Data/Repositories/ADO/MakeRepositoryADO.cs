using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using GuildCars.Models.Tables;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace GuildCars.Data.Repositories.ADO
{
    public class MakeRepositoryADO : IMakeRepository
    {
        public IEnumerable<Make> GetAll()
        {
            List<Make> makes = new List<Make>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllMakes", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Make Make = new Make();

                            Make.MakeId = dr["MakeId"].ToString();
                            Make.MakeName = dr["MakeName"].ToString();
                            Make.DateAdded = (DateTime)dr["DateAdded"];
                            Make.AddedBy = dr["AddedBy"].ToString();

                            makes.Add(Make);
                        }
                    }
                    return makes;
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
                return makes;
            }
        }

        public Make GetMakeById(string MakeId)
        {
            Make make = null;

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    dbConnection.Open();

                    SqlCommand cmd = new SqlCommand("SelectMakeById", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MakeId", MakeId);



                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            make = new Make();
                            make.MakeId = dr["MakeId"].ToString();
                            make.MakeName = dr["MakeName"].ToString();
                            make.DateAdded = (DateTime)dr["DateAdded"];
                            make.AddedBy = dr["AddedBy"].ToString();
                        }
                    }

                    return make;
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

                return make;
            }
        }

            public void Insert(Make Make)
        {
            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("MakeInsert", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@MakeId", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(param);
                    
                    cmd.Parameters.AddWithValue("@MakeName", Make.MakeName);
                    cmd.Parameters.AddWithValue("@DateAdded", Make.DateAdded = DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@AddedBy", Make.AddedBy);
                    

                    dbConnection.Open();

                    cmd.ExecuteNonQuery();

                    Make.MakeId = param.Value.ToString();
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
    }
}
