using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using GuildCars.Models.Tables;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;

namespace GuildCars.Data.Repositories.ADO
{
    public class ColorRepositoryADO : IColorRepository
    {
        public IEnumerable<Color> GetAll()
        {
            List<Color> Colors = new List<Color>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllColors", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Color Color = new Color();

                            Color.ColorId = (int)dr["ColorId"];
                            Color.ColorName = dr["ColorName"].ToString();

                            Colors.Add(Color);
                        }
                    }
                    return Colors;
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
                return Colors;
            }
        }

        public Color GetColorById(int ColorId)
        {
            Color Color = null;

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    dbConnection.Open();

                    SqlCommand cmd = new SqlCommand("SelectColorById", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ColorId", ColorId);



                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Color = new Color();
                            Color.ColorId = (int)dr["ColorId"];
                            Color.ColorName = dr["ColorName"].ToString();
                        }
                    }

                    return Color;
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

                return Color;
            }
        }
    }
}
