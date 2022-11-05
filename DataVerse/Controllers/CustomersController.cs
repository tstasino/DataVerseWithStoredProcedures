using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataVerse;
using DataVerse.Models;

namespace DataVerse.Controllers
{
    public class CustomersController : Controller
    {
        private DataVerseEntities db = new DataVerseEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Phone);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        //public ActionResult Create()
        //{
        //    ViewBag.id = new SelectList(db.Phones, "Customer_id", "Customer_id");
        //    return View();
        //}

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FirstName,LastName,Address,email,id")] Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Customers.Add(customer);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.id = new SelectList(db.Phones, "Customer_id", "Customer_id", customer.id);
        //    return View(customer);
        //}

        public ActionResult Create()
        {
            CustomerViewModel customerVm = new CustomerViewModel();
            return PartialView("_CustomerPartialView",customerVm);
        }
        
        [HttpPost]
        public ActionResult Create(CustomerViewModel customerVm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Something went wront in validation New Customer";
                return RedirectToAction("Index", "Customers");
            }
            Customer c = new Customer();
            c.FirstName = customerVm.FirstName;
            c.LastName = customerVm.LastName;
            c.Address = customerVm.Address;
            c.email = customerVm.email;
            db.Customers.Add(c);

            db.SaveChanges();

            Phone ph = new Phone();
            ph.Customer_id = c.id;
            ph.HomePhone = customerVm.HomePhone;
            ph.WorkPhone = customerVm.WorkPhone;
            ph.CellPhone = customerVm.CellPhone;
            db.Phones.Add(ph);

            db.SaveChanges();    
            
            return RedirectToAction("Index", "Customers");
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Where(c => c.id == id).FirstOrDefault();

            if (customer == null)
            {
                return HttpNotFound();
            }
            Phone phone = db.Phones.Where(c => c.Customer_id == id).FirstOrDefault();
            
            customer.Phone.Customer_id = phone.Customer_id;
            return PartialView("_editCustomerPartialView", customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
             
                customer.Phone.Customer_id = customer.id;
                db.Entry(customer).State = EntityState.Modified;
                db.Entry(customer.Phone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
