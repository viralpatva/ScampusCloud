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
    }
}