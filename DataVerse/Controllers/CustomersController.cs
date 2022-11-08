using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataVerse;
using DataVerse.Dal;
using DataVerse.Models;
using PagedList;





namespace DataVerse.Controllers
{
    public class CustomersController : Controller
    {
        readonly ICustomer_DAL customer_DAL;

        public CustomersController(ICustomer_DAL repository)
        {
            this.customer_DAL = repository;
        }

        //Customer_DAL _customerDAL = new Customer_DAL();

        // GET: Customers
        public ActionResult Index(int? page)
        {
            var customerList = customer_DAL.GetAllCustomers();
            if(customerList.Count  == 0)
            {
                TempData["InfoMessage"] = "Currently there are no Customers in the Database";
            }
            //var customers = db.Customers.Include(c => c.Phone);
            return View(customerList.ToPagedList(page ?? 1, 6));
        }




        //public ActionResult Create()
        //{
        //    CustomerVM customerVm = new CustomerVM();
        //    return PartialView("_CustomerPartialView",customerVm);
        //}

        [HttpPost]
        public ActionResult Create(CustomerVM customer)
        {
            bool isInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    if (customer.FirstName.Trim() == null) throw new Exception("Customer was not Inserted. Fist Name cannot be null");
                    if (customer.LastName.Trim() == null) throw new Exception("Customer was not Inserted. Last Name cannot be null");
                    if (customer.Address.Trim() == null) throw new Exception("Customer was not Inserted. Address cannot be null");
                    if (customer.email.Trim() == null) throw new Exception("Customer was not Inserted. Email cannot be null");
                    if (customer.HomePhone == null && customer.WorkPhone == null && customer.CellPhone == null)
                        throw new Exception("Customer was not Inserted. One of the three Phones must not be null");

                    isInserted = customer_DAL.InsertCustomer(customer);

                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "Customer details saved successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the Customer details";

                    }
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View("Index");
            }

            return View("Index");

  
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = customer_DAL.GetCustomerByID(id).FirstOrDefault();
            
            if(customer == null)
            {
                TempData["InfoMessage"] = "Customer not available with id " + id;
                return RedirectToAction("Index");
            }
            return View(customer);

        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(CustomerVM customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (customer.FirstName.Trim() == null) throw new Exception("Customer was not updated. Fist Name cannot be null");
                    if (customer.LastName.Trim() == null) throw new Exception("Customer was not updated. Last Name cannot be null");
                    if (customer.Address.Trim() == null) throw new Exception("Customer was not updated. Address cannot be null");
                    if (customer.email.Trim() == null) throw new Exception("Customer was not updated. Email cannot be null");
                    if (customer.HomePhone == null && customer.WorkPhone == null && customer.CellPhone == null)
                        throw new Exception("Customer was not Updated. One of the three Phones must not be null");

                    bool isUpdated = customer_DAL.UpdateCustomer(customer);

                    if (isUpdated)
                    {
                        TempData["SuccessMessage"] = "Customer Updated successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Customer was not Updated";
                    }


                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
                throw;
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var customer = customer_DAL.GetCustomerByID(id).FirstOrDefault();

                if (customer == null)
                {
                    TempData["InfoMessage"] = "Customer not available with id " + id;
                    return RedirectToAction("Index");
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View("Index");
            }
            
        }

        // POST: Customers/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                string result = customer_DAL.DeleteCustomer(id);
                if (result.Contains("Deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
                
            }
        }

        
    }
}
