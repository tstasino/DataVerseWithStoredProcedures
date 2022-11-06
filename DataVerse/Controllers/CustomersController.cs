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



namespace DataVerse.Controllers
{
    public class CustomersController : Controller
    {
        //private DataVerseEntities db = new DataVerseEntities();
        Customer_DAL _customerDAL = new Customer_DAL();

        // GET: Customers
        public ActionResult Index()
        {
            var customerList = _customerDAL.GetAllCustomers();
            if(customerList.Count  == 0)
            {
                TempData["InfoMessage"] = "Currently there are no Customers in the Database";
            }
            //var customers = db.Customers.Include(c => c.Phone);
            return View(customerList);
        }




        //public ActionResult Create()
        //{
        //    CustomerViewModel customerVm = new CustomerViewModel();
        //    return PartialView("_CustomerPartialView",customerVm);
        //}

        [HttpPost]
        public ActionResult Create(CustomerViewModel customer)
        {
            bool isInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    isInserted = _customerDAL.InsertCustomer(customer);

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
            var customer = _customerDAL.GetCustomerByID(id).FirstOrDefault();
            
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
        public ActionResult Edit(CustomerViewModel customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isUpdated = _customerDAL.UpdateCustomer(customer);

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
                return View("Index");
                throw;
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var customer = _customerDAL.GetCustomerByID(id).FirstOrDefault();

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
                string result = _customerDAL.DeleteCustomer(id);
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
                return View("Index");
                
            }
        }

        
    }
}
