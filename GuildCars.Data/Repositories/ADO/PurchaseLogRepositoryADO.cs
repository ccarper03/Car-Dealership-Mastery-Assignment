using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using GuildCars.Models.Tables;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;

namespace GuildCars.Data.Repositories.ADO
{
    public class PurchaseLogRepositoryADO : IPurchaseLogRepository
    {
        public IEnumerable<PurchaseLog> GetPurchaseLogs()
        {
            List<PurchaseLog> purchaselogs = new List<PurchaseLog>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllPurchaseLogs", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PurchaseLog purchaselog = new PurchaseLog();

                            purchaselog.PurchaseLogId = (int)dr["PurchaselogId"];
                            purchaselog.PurchaseName = dr["PurchaserName"].ToString();
                            purchaselog.PurchasePrice = (decimal)dr["PurchasePrice"];
                            purchaselog.SalesPersonId = dr["SalesPersonId"].ToString();
                            purchaselog.CarId = (int)dr["CarId"];
                            purchaselog.DateSold = (DateTime)dr["DateSold"];
                            purchaselog.PurchaseType = dr["PurchaseType"].ToString();
                            purchaselog.Email = dr["Email"].ToString();
                            purchaselog.Phone = dr["Phone"].ToString();
                            purchaselog.AddressOne = dr["AddressOne"].ToString();
                            purchaselog.AddressTwo = dr["AddressTwo"].ToString();
                            purchaselog.City = dr["City"].ToString();
                            purchaselog.ZipCode = dr["ZipCode"].ToString();

                            purchaselogs.Add(purchaselog);
                        }
                    }
                    return purchaselogs;
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
                return purchaselogs;
            }
        }

        public void Insert(PurchaseLog PurchaseLog)
        {
            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("PurchaseLogInsert", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@PurchaseLogId", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(param);

                    cmd.Parameters.AddWithValue("@PurchaserName", PurchaseLog.PurchaseName);
                    cmd.Parameters.AddWithValue("@CarId", PurchaseLog.CarId);
                    cmd.Parameters.AddWithValue("@AddressOne", PurchaseLog.AddressOne);

                    if (String.IsNullOrEmpty(PurchaseLog.AddressTwo))
                    {
                        cmd.Parameters.AddWithValue("@AddressTwo", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@AddressTwo", PurchaseLog.AddressTwo);
                    }

                    if (String.IsNullOrEmpty(PurchaseLog.Phone))
                    {
                        cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Phone", PurchaseLog.Phone);
                    }

                    if (String.IsNullOrEmpty(PurchaseLog.Email))
                    {
                        cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Email", PurchaseLog.Email);
                    }

                    cmd.Parameters.AddWithValue("@City", PurchaseLog.City);
                    cmd.Parameters.AddWithValue("@ZipCode", PurchaseLog.ZipCode);
                    cmd.Parameters.AddWithValue("@PurchasePrice", PurchaseLog.PurchasePrice);
                    cmd.Parameters.AddWithValue("@PurchaseType", PurchaseLog.PurchaseType);
                    cmd.Parameters.AddWithValue("@SalesPersonId", PurchaseLog.SalesPersonId);
                    cmd.Parameters.AddWithValue("@DateSold", PurchaseLog.DateSold);

                    dbConnection.Open();

                    cmd.ExecuteNonQuery();

                    PurchaseLog.PurchaseLogId = (int)param.Value;
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
