using DataVerse.Models;
using System.Collections.Generic;

namespace DataVerse.Dal
{
    public interface ICustomer_DAL
    {
        string DeleteCustomer(int id);
        List<CustomerVM> GetAllCustomers();
        List<CustomerVM> GetCustomerByID(int customerId);
        bool InsertCustomer(CustomerVM customer);
        bool UpdateCustomer(CustomerVM customer);
    }
}