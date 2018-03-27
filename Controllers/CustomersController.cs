using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{

    public class CustomersController : Controller
    {
        /// <summary>
        /// Basics when creating DB!
        /// </summary>
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        // GET: Customer
        public ViewResult Index()
        {
            return View();
        }

        // THIS METHOD IS NOT IN USED!
        public ActionResult Details(int id)
        {
            var customer = _context.Customer.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if(customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }
        

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipType.ToList();

            var viewModel = new CustomerFormViewModel
            {
               // Customer = new Customer(),
                MembershipType = membershipTypes
            };
                

            return View("CustomerForm", viewModel);
        }

        // Selfie muna bago mag bigay ng error
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            // ENABLES VALIDATION
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipType = _context.MembershipType.ToList()
                };

                return View("CustomerForm", viewModel);
            }   

            if (customer.Id == 0)
            {
                _context.Customer.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customer.Single(c => c.Id == customer.Id);

                // Check in CustomerForm.cshtml
                customerInDb.Name = customer.Name;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerInDb.Birthdate = customer.Birthdate;

            }

            _context.SaveChanges();


            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customer.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipType = _context.MembershipType.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}