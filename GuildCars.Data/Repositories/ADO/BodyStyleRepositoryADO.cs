using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using GuildCars.Models.Tables;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace GuildCars.Data.Repositories.ADO
{
    public class BodyStyleRepositoryADO : IBodyStyleRepository
    {
        public IEnumerable<BodyStyle> GetAll()
        {
            List<BodyStyle> BodyStyles = new List<BodyStyle>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllBodyStyles", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BodyStyle bodyStyle = new BodyStyle();

                            bodyStyle.BodyStyleId = (int)dr["BodyStyleId"];
                            bodyStyle.BodyStyleType = dr["BodyStyleType"].ToString();

                            BodyStyles.Add(bodyStyle);
                        }
                    }
                    return BodyStyles;
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
                return BodyStyles;
            }
        }

        public BodyStyle GetBodyStyleById(int BodyStyleId)
        {
            BodyStyle BodyStyle = null;

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    dbConnection.Open();

                    SqlCommand cmd = new SqlCommand("SelectBodyStyleById", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BodyStyleId", BodyStyleId);



                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            BodyStyle = new BodyStyle();
                            BodyStyle.BodyStyleId = (int)dr["BodyStyleId"];
                            BodyStyle.BodyStyleType = dr["BodyStyleType"].ToString();
                        }
                    }

                    return BodyStyle;
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

                return BodyStyle;
            }
        }
    }
}
