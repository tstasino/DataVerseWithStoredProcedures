using DataVerse.Models;
using System.Collections.Generic;

namespace DataVerse.Dal
{
    public interface ICustomer_DAL
    {
        string DeleteCustomer(int id);
        List<CustomerViewModel> GetAllCustomers();
        List<CustomerViewModel> GetCustomerByID(int customerId);
        bool InsertCustomer(CustomerViewModel customer);
        bool UpdateCustomer(CustomerViewModel customer);
    }
}