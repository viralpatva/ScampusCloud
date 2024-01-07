using ScampusCloud.Models;
using ScampusCloud.Repository.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScampusCloud.Controllers
{
    public class LoginController : Controller
    {
        #region Variable Declaration
        private readonly LoginRepository _loginRepository;
        LoginModel _LoginModel = new LoginModel();
        #endregion

        #region CTOR
        public LoginController()
        {
            _loginRepository = new LoginRepository();
        }
        #endregion

        #region Method
        // GET: Login
        public ActionResult Login()
        {
            return View(_LoginModel);
        }
        [HttpPost]
        public ActionResult Login(LoginModel _LoginModel)
        {

            var officemaster = _loginRepository.Get_User(_LoginModel);

            if (officemaster == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Dashboad","Dashboad");
            }
                
        }

        public ActionResult Logout()
        {
            //var redirectUrl = $"~/";
            //Clear All Session Values
            HttpContext.Session.Clear();
            ////Clear All Cookie Values
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        #endregion
    }
}