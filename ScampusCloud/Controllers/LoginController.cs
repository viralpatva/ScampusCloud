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
            return View();
        }
        #endregion
    }
}