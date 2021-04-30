using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace GuildCars.Data.Repositories.ADO
{
    public class ModelRepositoryADO : IModelRepository
    {
        public IEnumerable<Model> GetAll()
        {
            List<Model> Models = new List<Model>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllModels", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Model Model = new Model();

                            Model.MakeId = (int)dr["MakeId"];
                            Model.ModelId = (int)dr["ModelId"];
                            Model.ModelName = dr["ModelName"].ToString();
                            Model.DateAdded = (DateTime)dr["DateAdded"];
                            Model.Addedby = dr["AddedBy"].ToString();

                            Models.Add(Model);
                        }
                    }
                    return Models;
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
                return Models;
            }
        }

        public Model GetModelById(int ModelId)
        {
            Model Model = null;

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    dbConnection.Open();

                    SqlCommand cmd = new SqlCommand("SelectModelById", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ModelId", ModelId);



                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Model = new Model();
                            Model.ModelId = (int)dr["ModelId"];
                            Model.ModelName = dr["ModelName"].ToString();
                            Model.DateAdded = (DateTime)dr["DateAdded"];
                            Model.Addedby = dr["AddedBy"].ToString();
                        }
                    }
                    return Model;
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

                return Model;
            }
        }

        public List<Model> GetModelsByMakeId(int MakeId)
        {
            List<Model> models = new List<Model>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    dbConnection.Open();

                    SqlCommand cmd = new SqlCommand("SelectModelByMakeId", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MakeId", MakeId);



                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        

                        while(dr.Read())
                        {
                            var model = new Model();
                            model.ModelId = (int)dr["ModelId"];
                            model.ModelName = dr["ModelName"].ToString();
                            model.MakeId = (int)dr["MakeId"];
                            model.DateAdded = (DateTime)dr["DateAdded"];
                            model.Addedby = dr["AddedBy"].ToString();

                            models.Add(model);
                        }
                    }

                    return models;
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

                return models;
            }
        }

        public void Insert(Model Model)
        {
            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ModelInsert", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@ModelId", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(param);

                    cmd.Parameters.AddWithValue("@ModelName", Model.ModelName);
                    cmd.Parameters.AddWithValue("@MakeId", Model.MakeId);
                    cmd.Parameters.AddWithValue("@DateAdded", Model.DateAdded = DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@AddedBy", Model.Addedby);

                    dbConnection.Open();

                    cmd.ExecuteNonQuery();

                    Model.ModelId = (int)param.Value;
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

