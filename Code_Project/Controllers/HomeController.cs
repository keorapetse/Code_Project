using Code_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Code_Project.Controllers
{
    public class HomeController : Controller
    {
        Code_ProjectEntities entities = new Code_ProjectEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Index(Registration registration)
        {
            if (registration != null)
            {
                //If registation was successful, log details on the database
                entities.Registrations.Add(
                    new Registration()
                    {
                        FirstName = registration.FirstName,
                        LastName = registration.LastName,
                        Email = registration.Email,
                        Username = registration.Username,
                        Password = registration.Password,
                    }
                );
                entities.SaveChanges();

                return RedirectToAction("Index", "Login");

            }
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}