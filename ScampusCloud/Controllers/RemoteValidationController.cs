using ScampusCloud.Models;
using ScampusCloud.Repository.Staff;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScampusCloud.Controllers
{
    [SessionTimeoutAttribute]
    public class RemoteValidationController : Controller
    {
        StaffModel Staff = new StaffModel();
        public RemoteValidationController()
        {
        }
        [HttpPost]
        public ActionResult IsStaffIdExist(string StaffId = "")
        {
            StaffRepository _StaffRepository = new StaffRepository();
            string Original_StaffId = SessionManager.StaffId;
            bool IsEditMode = !string.IsNullOrEmpty(Original_StaffId) ? true : false;
            string returnMsg = "";
            
            if (IsEditMode && !string.Equals(Original_StaffId, StaffId))
            {
                Staff.ActionType = "Remote";
                Staff.StaffId = StaffId;
                Staff.CompanyId = SessionManager.CompanyId;
                Staff = _StaffRepository.AddEdit_Staff(Staff);
                returnMsg = $"Staff Id '{StaffId}' is already in use.";
            }
            else if (!IsEditMode)
            {
                Staff.ActionType = "Remote";
                Staff.StaffId = StaffId;
                Staff.CompanyId = SessionManager.CompanyId;
                Staff = _StaffRepository.AddEdit_Staff(Staff);
                returnMsg = $"Staff Id '{StaffId}' is already in use.";
            }
            if (Staff == null)
                return Json(true);
            else
                return Json(false);

        }
        [HttpPost]
        public ActionResult IsEmailIdExist(string EmailId = "")
        {
            StaffRepository _StaffRepository = new StaffRepository();
            string Original_EmailId = SessionManager.EmailId;
            bool IsEditMode = !string.IsNullOrEmpty(Original_EmailId) ? true : false;
            string returnMsg = "";

            if (IsEditMode && !string.Equals(Original_EmailId, EmailId))
            {
                Staff.ActionType = "Remote";
                Staff.EmailId = EmailId;
                Staff.CompanyId = SessionManager.CompanyId;
                Staff = _StaffRepository.AddEdit_Staff(Staff);
                returnMsg = $"Email Id '{EmailId}' is already in use.";
            }
            else if (!IsEditMode)
            {
                Staff.ActionType = "Remote";
                Staff.EmailId = EmailId;
                Staff.CompanyId = SessionManager.CompanyId;
                Staff = _StaffRepository.AddEdit_Staff(Staff);
                returnMsg = $"Email Id '{EmailId}' is already in use.";
            }
            if (Staff == null)
                return Json(true);
            else
                return Json(false);

        }

        [HttpPost]
        public ActionResult IsCodeExist(string Code = "")
        {
            StaffRepository _StaffRepository = new StaffRepository();
            string Original_Code = SessionManager.Code;
            bool IsEditMode = !string.IsNullOrEmpty(Original_Code) ? true : false;
            string returnMsg = "";

            if (IsEditMode && !string.Equals(Original_Code, Code))
            {
                Staff.ActionType = "Remote";
                Staff.Code = Code;
                Staff.CompanyId = SessionManager.CompanyId;
                Staff = _StaffRepository.AddEdit_Staff(Staff);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            else if (!IsEditMode)
            {
                Staff.ActionType = "Remote";
                Staff.Code = Code;
                Staff.CompanyId = SessionManager.CompanyId;
                Staff = _StaffRepository.AddEdit_Staff(Staff);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            if (Staff == null)
                return Json(true);
            else
                return Json(false);

        }

    }
}