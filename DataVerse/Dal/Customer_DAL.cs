using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DataVerse.Models;

namespace DataVerse.Dal
{
    public class Customer_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["DataVerseEntities"].ToString();

        //Get All Customers
        public List<CustomerViewModel> GetAllCustomers()
        {
            List<CustomerViewModel> customerList = new List<CustomerViewModel>();

            using(SqlConnection connection=new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllCustomers";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtCustomers = new DataTable();
                
                connection.Open();
                sqlDA.Fill(dtCustomers);
                connection.Close();

                foreach(DataRow dr in dtCustomers.Rows)
                {
                   
                        var tmp = new CustomerViewModel();
 
                        tmp.id = Convert.ToInt32(dr["id"]);
                        tmp.FirstName = dr["FirstName"].ToString();
                        tmp.LastName = dr["LastName"].ToString();
                        tmp.Address = dr["Address"].ToString();
                        tmp.email = dr["email"].ToString();
                        if (dr["HomePhone"] != DBNull.Value)  tmp.HomePhone = Convert.ToInt64(dr["HomePhone"]);
                        if (dr["WorkPhone"] != DBNull.Value) tmp.WorkPhone = Convert.ToInt64(dr["WorkPhone"]);
                        if (dr["CellPhone"] != DBNull.Value) tmp.CellPhone = Convert.ToInt64(dr["CellPhone"]);

                        customerList.Add(tmp);                     
                    
                    
                }
            }

            return customerList;
        }

        //Insert Customers, Phones
        public bool InsertCustomer(CustomerViewModel customer)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertCustomers", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@email", customer.email);
                command.Parameters.AddWithValue("@HomePhone", customer.HomePhone);
                command.Parameters.AddWithValue("@WorkPhone", customer.WorkPhone);
                command.Parameters.AddWithValue("@CellPhone", customer.CellPhone);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
                if(id>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }


        //Get Customers by Id
        public List<CustomerViewModel> GetCustomerByID(int customerId)
        {
            List<CustomerViewModel> customerList = new List<CustomerViewModel>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetCustomerByID";
                command.Parameters.AddWithValue("@Id", customerId);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtCustomers = new DataTable();

                connection.Open();
                sqlDA.Fill(dtCustomers);
                connection.Close();

                foreach (DataRow dr in dtCustomers.Rows)
                {

                    var tmp = new CustomerViewModel();

                    tmp.id = Convert.ToInt32(dr["Id"]);
                    tmp.FirstName = dr["FirstName"].ToString();
                    tmp.LastName = dr["LastName"].ToString();
                    tmp.Address = dr["Address"].ToString();
                    tmp.email = dr["email"].ToString();
                    if (dr["HomePhone"] != DBNull.Value) tmp.HomePhone = Convert.ToInt64(dr["HomePhone"]);
                    if (dr["WorkPhone"] != DBNull.Value) tmp.WorkPhone = Convert.ToInt64(dr["WorkPhone"]);
                    if (dr["CellPhone"] != DBNull.Value) tmp.CellPhone = Convert.ToInt64(dr["CellPhone"]);

                    customerList.Add(tmp);


                }
            }

            return customerList;
        }

        //Update Customer
        public bool UpdateCustomer(CustomerViewModel customer)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateCustomers", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", customer.id);
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@email", customer.email);
                command.Parameters.AddWithValue("@HomePhone", customer.HomePhone);
                command.Parameters.AddWithValue("@WorkPhone", customer.WorkPhone);
                command.Parameters.AddWithValue("@CellPhone", customer.CellPhone);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
                if (id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }

        public string DeleteCustomer(int id)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.Add("@OutputMessage", SqlDbType.VarChar, 50).Direction= ParameterDirection.Output;
                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@OutputMessage"].Value.ToString();
                connection.Close();
            }

            return result;
        }

    }
}