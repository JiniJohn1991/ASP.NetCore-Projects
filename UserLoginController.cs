using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeManagementApplicationMain.Models;

namespace HomeManagementApplicationMain.Controllers
{
    [Route("HomeManagementApplicationMain/[controller]")]
    public class UserLoginController : Controller
    {
        // GET: Login
        private HMDBContext context;

        public UserLoginController(HMDBContext dbContext)
        {
            context = dbContext;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin loginDetails)
        {
            
            if (ModelState.IsValid)
            {
                if (loginDetails != null && !string.IsNullOrEmpty(loginDetails.UserName) &&
                    !string.IsNullOrEmpty(loginDetails.Password))
                {
                    var userNameInDB = context.UserLogin.Where(x => x.UserName.Equals(loginDetails.UserName)).FirstOrDefault();
                    if (userNameInDB != null)
                    {
                        if (userNameInDB.Password.Equals(loginDetails.Password))
                        {
                            RedirectToAction("Main");
                        }
                        else
                        {
                            ModelState.AddModelError("password", "Incorrect password");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("username", "Incorrect username");
                    }
                }
                else
                {
                    ModelState.AddModelError("errorMessage", "Please enter username and password to login");
                }
            }
            return View(loginDetails);
        }
    }
}