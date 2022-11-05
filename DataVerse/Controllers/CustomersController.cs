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

        //// GET: Customers/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = db.Customers.Where(c => c.id == id).FirstOrDefault();

        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Phone phone = db.Phones.Where(c => c.Customer_id == id).FirstOrDefault();

        //    customer.Phone.Customer_id = phone.Customer_id;
        //    return PartialView("_editCustomerPartialView", customer);
        //}

        //// POST: Customers/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //public ActionResult Edit(Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        customer.Phone.Customer_id = customer.id;
        //        db.Entry(customer).State = EntityState.Modified;
        //        db.Entry(customer.Phone).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(customer);
        //}

        //// GET: Customers/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = db.Customers.Find(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}

        //// POST: Customers/Delete/5
        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Customer customer = db.Customers.Find(id);
        //    db.Customers.Remove(customer);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
