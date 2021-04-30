using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using GuildCars.Models.Queries;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace GuildCars.Data.Repositories.ADO
{
    public class ReportsRepositoryADO : IReportsRepository
    {
        public IEnumerable<InventoryReportListingItem> GetInventory()
        {
            List<InventoryReportListingItem> inventoryReportItems = new List<InventoryReportListingItem>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllInventoryForReport", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            InventoryReportListingItem inventoryReportItem = new InventoryReportListingItem();

                            inventoryReportItem.Year = (dr["Year"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["Year"])); 
                            inventoryReportItem.IsNew = dr.GetBoolean(dr.GetOrdinal("IsNew"));
                            inventoryReportItem.StockValue = (decimal)dr["StockValue"];
                            inventoryReportItem.UnitsInStock = (int)dr["UnitsInStock"];
                            inventoryReportItem.Make = dr["Make"].ToString();
                            inventoryReportItem.Model = dr["Model"].ToString();

                            inventoryReportItems.Add(inventoryReportItem);
                        }
                    }
                    return inventoryReportItems;
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
                return inventoryReportItems;
            }
        }

        public IEnumerable<SalesReportListingItem> GetSalesReport()
        {
            List<SalesReportListingItem> salesReportItems = new List<SalesReportListingItem>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllSalesReports", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SalesReportListingItem salesReportItem = new SalesReportListingItem();

                            salesReportItem.UserId = dr["UserId"].ToString();
                            salesReportItem.UserName = dr["UserName"].ToString();
                            salesReportItem.CarsSold = (int)dr["TotalCarsSold"];
                            salesReportItem.Sales = (decimal)dr["TotalSales"];

                            salesReportItems.Add(salesReportItem);
                        }
                    }
                    return salesReportItems;
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
                return salesReportItems;
            }
        }

        public IEnumerable<SalesReportListingItem> SearchSalesReports(SalesSearchParameters Parameters)
        {
            List<SalesReportListingItem> salesReports = new List<SalesReportListingItem>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT a.UserName As \"UserName\", a.Id AS \"UserId\",  SUM(p.PurchasePrice) AS \"TotalSales\", COUNT(p.SalesPersonId) AS \"TotalCarsSold\" " +
                    "FROM PurchaseLog p INNER JOIN AspNetUsers a ON a.UserName = p.SalesPersonId WHERE 1 = 1 ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbConnection;

                if (Parameters.MinDate.HasValue)
                {
                    query += "AND DateSold >= @MinDate ";
                    cmd.Parameters.AddWithValue("@MinDate", Parameters.MinDate.Value);
                }

                if (Parameters.MaxDate.HasValue)
                {
                    query += "AND DateSold <= @MaxDate ";
                    cmd.Parameters.AddWithValue("@MaxDate", Parameters.MaxDate.Value);
                }


                if (!string.IsNullOrEmpty(Parameters.UserName))
                {
                    query += "AND UserName = @UserName ";
                    cmd.Parameters.AddWithValue("@UserName", Parameters.UserName);
                }
                
                query += "GROUP BY UserName, Id ORDER BY TotalSales DESC";
                cmd.CommandText = query;

                dbConnection.Open();

                try {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SalesReportListingItem salesReportItem = new SalesReportListingItem();

                            salesReportItem.UserName = dr["UserName"].ToString();
                            salesReportItem.UserId = dr["UserId"].ToString();
                            salesReportItem.Sales = (decimal)dr["TotalSales"];
                            salesReportItem.CarsSold = (int)dr["TotalCarsSold"];

                            salesReports.Add(salesReportItem);
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

            return salesReports;
        }
    }
}
