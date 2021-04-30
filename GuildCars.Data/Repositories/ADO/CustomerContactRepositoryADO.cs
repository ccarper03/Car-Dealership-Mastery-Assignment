using System;
using System.Collections.Generic;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using GuildCars.Models.Tables;
using GuildCars.Data.Interfaces;
using GuildCars.Data;

namespace GuildCustomerContacts.Data.Repositories.ADO
{
    public class CustomerContactRepositoryADO : ICustomerContactRepository
    {
        public IEnumerable<CustomerContact> GetAllContacts()
        {
            List<CustomerContact> CustomerContacts = new List<CustomerContact>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllCustomerContacts", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CustomerContact CustomerContact = new CustomerContact();

                            CustomerContact.ContactId = (int)dr["ContactId"];
                            CustomerContact.ContactName = dr["ContactName"].ToString();
                            CustomerContact.Email = dr["Email"].ToString();
                            CustomerContact.MessageBody = dr["MessageBody"].ToString();
                            CustomerContact.Phone= dr["Phone"].ToString();
                           
                            CustomerContacts.Add(CustomerContact);
                        }
                    }
                    return CustomerContacts;
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
                return CustomerContacts;
            }
        }

        public CustomerContact GetContactById(int ContactId)
        {
            CustomerContact CustomerContact = null;

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    dbConnection.Open();

                    SqlCommand cmd = new SqlCommand("SelectCustomerContactById", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ContactId", ContactId);



                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            CustomerContact = new CustomerContact();
                            CustomerContact.ContactId = (int)dr["ContactId"];
                            CustomerContact.ContactName = dr["ContactName"].ToString();
                            CustomerContact.MessageBody = dr["MessageBody"].ToString();
                            CustomerContact.Email = dr["Email"].ToString();
                            CustomerContact.Phone = dr["Phone"].ToString();
                        }
                    }

                    return CustomerContact;
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

                return CustomerContact;
            }
        }

        public void Insert(CustomerContact CustomerContact)
        {
            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("CustomerContactInsert", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@ContactId", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(param);

                    cmd.Parameters.AddWithValue("@ContactName", CustomerContact.ContactName);
                    cmd.Parameters.AddWithValue("@Phone", CustomerContact.Phone);
                    cmd.Parameters.AddWithValue("@Email", CustomerContact.Email);
                    cmd.Parameters.AddWithValue("@MessageBody", CustomerContact.MessageBody);
                   
                    dbConnection.Open();

                    cmd.ExecuteNonQuery();

                    CustomerContact.ContactId = (int)param.Value;
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
