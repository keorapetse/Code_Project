using Code_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Code_Project.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index (Registration registration)
        {
            Code_ProjectEntities2 entities = new Code_ProjectEntities2();
            if (registration != null)
            {
                //If registation was successful, log details on the database and direct user to the login page
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
            }
            return RedirectToAction("Index", "Login");
        }
    }
}