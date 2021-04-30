using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using GuildCars.Models.Tables;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace GuildCars.Data.Repositories.ADO
{
    public class TransmissionRepositoryADO : ITransmissionRepository
    {
        public IEnumerable<Transmission> GetAll()
        {
            List<Transmission> Transmissions = new List<Transmission>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllTransmissions", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Transmission Transmission = new Transmission();

                            Transmission.TransmissionId = (int)dr["TransmissionId"];
                            Transmission.TransmissionType = dr["TransmissionType"].ToString();

                            Transmissions.Add(Transmission);
                        }
                    }
                    return Transmissions;
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
                return Transmissions;
            }
        }

        public Transmission GetTransmissionById(int TransmissionId)
        {
            Transmission Transmission = null;

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    dbConnection.Open();

                    SqlCommand cmd = new SqlCommand("SelectTransmissionById", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TransmissionId", TransmissionId);



                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Transmission = new Transmission();
                            Transmission.TransmissionId = (int)dr["TransmissionId"];
                            Transmission.TransmissionType = dr["TransmissionType"].ToString();
                        }
                    }

                    return Transmission;
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

                return Transmission;
            }
        }
    }
}

