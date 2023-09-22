using Code_Project.Models;
using System.Linq;
using System.Web.Mvc;

namespace Code_Project.Controllers
{
    public class LoginController : Controller
    {
        Code_ProjectEntities2 entities = new Code_ProjectEntities2();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            //if username and password from registration is eqaul to username and password from the login
            var users = entities.Registrations.Where(x => x.Username == login.userName && x.Password == login.password).Count();

            if (users > 0)
            {
              //  entities.Login.Add(
              //  new Login()
              //  {
              //      userName = login.userName,
              //      password = login.password,
              //  }
              //);
               // entities.SaveChanges();
                return RedirectToAction("Index", "PatientInfo");
            }
            else
            {
                var username = entities.Registrations.Where(x => x.Username != login.userName).Count();
                var password = entities.Registrations.Where(x => x.Password != login.password).Count();

                if (username > 0)
                {
                    ModelState.AddModelError(nameof(login.userName), "Incorrect username");
                }
                if (password > 0)
                {
                    ModelState.AddModelError(nameof(login.password), "Incorrect password");
                }
                return View(login);
            }
        }
    }
}