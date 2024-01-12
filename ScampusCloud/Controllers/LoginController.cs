using ScampusCloud.Models;
using ScampusCloud.Repository.Login;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public ActionResult Login(bool IsSuccess=false,string Response_Message=null)
        {
            if (Response_Message != null)
            {
                _LoginModel.IsSuccess = IsSuccess;
                _LoginModel.Response_Message = Response_Message;
            }
            return View(_LoginModel);
        }
        [HttpPost]
        public ActionResult Login(LoginModel _LoginModel)
        {
            try
            {
                    var officemaster = _loginRepository.Get_User(_LoginModel);
                    if (officemaster != null)
                    {
                        Session["UserName"] = _LoginModel.EmailId;
                        Session["CompanyId"] = officemaster.ParentUserId;
                        Session["Name"] = officemaster.Name;
                        return RedirectToAction("Dashboad", "Dashboad");

                    }
                    else
                    {
                        //ModelState.AddModelError(string.Empty, "The user name or password is incorrect");
                        _LoginModel.IsSuccess = false;
                        _LoginModel.Response_Message = "The user name or password is incorrect";
                        return RedirectToAction("Login", "Login", new { IsSuccess = _LoginModel.IsSuccess, Response_Message= _LoginModel.Response_Message });
                    }
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
                throw;
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