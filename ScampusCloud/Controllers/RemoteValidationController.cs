using ScampusCloud.Models;
using ScampusCloud.Repository.Admission;
using ScampusCloud.Repository.Campus;
using ScampusCloud.Repository.CardStatus;
using ScampusCloud.Repository.College;
using ScampusCloud.Repository.Country;
using ScampusCloud.Repository.Customer;
using ScampusCloud.Repository.DegreeType;
using ScampusCloud.Repository.JobTitle;
using ScampusCloud.Repository.Program;
using ScampusCloud.Repository.Staff;
using ScampusCloud.Repository.StaffDepartment;
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
        AdmissionModel Admission = new AdmissionModel();
        CampusModel Campus = new CampusModel();
        CardStatusModel CardStatus = new CardStatusModel();
        CollegeModel College = new CollegeModel();
        CountryModel Country = new CountryModel();
        CustomerModel Customer = new CustomerModel();
        DegreeTypeModel DegreeType = new DegreeTypeModel();
        JobTitleModel JobTitle = new JobTitleModel();
        ProgramModel Program = new ProgramModel();
        StaffDepartmentModel StaffDepartment = new StaffDepartmentModel();
        

        public RemoteValidationController()
        {
        }
        #region Staff
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
        #endregion

        #region Admission
        [HttpPost]
        public ActionResult IsAdmissionCodeExist(string Code = "")
        {
            AdmissionRepository _Repository = new AdmissionRepository();
            string Original_Code = SessionManager.Code;
            bool IsEditMode = !string.IsNullOrEmpty(Original_Code) ? true : false;
            string returnMsg = "";

            if (IsEditMode && !string.Equals(Original_Code, Code))
            {
                Admission.ActionType = "Remote";
                Admission.Code = Code;
                Admission.CompanyId = SessionManager.CompanyId;
                Admission = _Repository.AddEdit_Admission(Admission);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            else if (!IsEditMode)
            {
                Admission.ActionType = "Remote";
                Admission.Code = Code;
                Admission.CompanyId = SessionManager.CompanyId;
                Admission = _Repository.AddEdit_Admission(Admission);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            if (Admission == null)
                return Json(true);
            else
                return Json(false);

        }
        #endregion

        #region Campus
        [HttpPost]
        public ActionResult IsCampusCodeExist(string Code = "")
        {
            CampusRepository _Repository = new CampusRepository();
            string Original_Code = SessionManager.Code;
            bool IsEditMode = !string.IsNullOrEmpty(Original_Code) ? true : false;
            string returnMsg = "";

            if (IsEditMode && !string.Equals(Original_Code, Code))
            {
                Campus.ActionType = "Remote";
                Campus.Code = Code;
                Campus.CompanyId = SessionManager.CompanyId;
                Campus = _Repository.AddEdit_Campus(Campus);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            else if (!IsEditMode)
            {
                Campus.ActionType = "Remote";
                Campus.Code = Code;
                Campus.CompanyId = SessionManager.CompanyId;
                Campus = _Repository.AddEdit_Campus(Campus);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            if (Campus == null)
                return Json(true);
            else
                return Json(false);

        }
        #endregion

        #region CardStatus
        [HttpPost]
        public ActionResult IsCardStatusCodeExist(string Code = "")
        {
            CardStatusRepository _Repository = new CardStatusRepository();
            string Original_Code = SessionManager.Code;
            bool IsEditMode = !string.IsNullOrEmpty(Original_Code) ? true : false;
            string returnMsg = "";

            if (IsEditMode && !string.Equals(Original_Code, Code))
            {
                CardStatus.ActionType = "Remote";
                CardStatus.Code = Code;
                CardStatus.CompanyId = SessionManager.CompanyId;
                CardStatus = _Repository.AddEdit_CardStatus(CardStatus);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            else if (!IsEditMode)
            {
                CardStatus.ActionType = "Remote";
                CardStatus.Code = Code;
                CardStatus.CompanyId = SessionManager.CompanyId;
                CardStatus = _Repository.AddEdit_CardStatus(CardStatus);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            if (CardStatus == null)
                return Json(true);
            else
                return Json(false);

        }
        #endregion

        #region College
        [HttpPost]
        public ActionResult IsCollegeCodeExist(string Code = "")
        {
            CollegeRepository _Repository = new CollegeRepository();
            string Original_Code = SessionManager.Code;
            bool IsEditMode = !string.IsNullOrEmpty(Original_Code) ? true : false;
            string returnMsg = "";

            if (IsEditMode && !string.Equals(Original_Code, Code))
            {
                College.ActionType = "Remote";
                College.Code = Code;
                College.CompanyId = SessionManager.CompanyId;
                College = _Repository.AddEdit_College(College);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            else if (!IsEditMode)
            {
                College.ActionType = "Remote";
                College.Code = Code;
                College.CompanyId = SessionManager.CompanyId;
                College = _Repository.AddEdit_College(College);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            if (College == null)
                return Json(true);
            else
                return Json(false);

        }
        #endregion

        #region Country
        [HttpPost]
        public ActionResult IsCountryCodeExist(string Code = "")
        {
            CountryRepository _Repository = new CountryRepository();
            string Original_Code = SessionManager.Code;
            bool IsEditMode = !string.IsNullOrEmpty(Original_Code) ? true : false;
            string returnMsg = "";

            if (IsEditMode && !string.Equals(Original_Code, Code))
            {
                Country.ActionType = "Remote";
                Country.Code = Code;
                Country.CompanyId = SessionManager.CompanyId;
                Country = _Repository.AddEdit_Country(Country);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            else if (!IsEditMode)
            {
                Country.ActionType = "Remote";
                Country.Code = Code;
                Country.CompanyId = SessionManager.CompanyId;
                Country = _Repository.AddEdit_Country(Country);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            if (Country == null)
                return Json(true);
            else
                return Json(false);

        }
        #endregion

        #region Customer
        [HttpPost]
        public ActionResult IsCustomerEmailIdExist(string EmailId = "")
        {
            CustomerRepository _Repository = new CustomerRepository();
            string Original_EmailId = SessionManager.EmailId;
            bool IsEditMode = !string.IsNullOrEmpty(Original_EmailId) ? true : false;
            string returnMsg = "";

            if (IsEditMode && !string.Equals(Original_EmailId, EmailId))
            {
                Customer.ActionType = "Remote";
                Customer.EmailId = EmailId;
                Customer.CompanyId = SessionManager.CompanyId;
                Customer = _Repository.AddEdit_Customer(Customer);
                returnMsg = $"Code '{EmailId}' is already in use.";
            }
            else if (!IsEditMode)
            {
                Customer.ActionType = "Remote";
                Customer.EmailId = EmailId;
                Customer.CompanyId = SessionManager.CompanyId;
                Customer = _Repository.AddEdit_Customer(Customer);
                returnMsg = $"Code '{EmailId}' is already in use.";
            }
            if (Customer == null)
                return Json(true);
            else
                return Json(false);

        }
        #endregion

        #region DegreeType
        [HttpPost]
        public ActionResult IsDegreeTypeCodeExist(string Code = "")
        {
            DegreeTypeRepository _Repository = new DegreeTypeRepository();
            string Original_Code = SessionManager.Code;
            bool IsEditMode = !string.IsNullOrEmpty(Original_Code) ? true : false;
            string returnMsg = "";

            if (IsEditMode && !string.Equals(Original_Code, Code))
            {
                DegreeType.ActionType = "Remote";
                DegreeType.Code = Code;
                DegreeType.CompanyId = SessionManager.CompanyId;
                DegreeType = _Repository.AddEdit_DegreeType(DegreeType);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            else if (!IsEditMode)
            {
                DegreeType.ActionType = "Remote";
                DegreeType.Code = Code;
                DegreeType.CompanyId = SessionManager.CompanyId;
                DegreeType = _Repository.AddEdit_DegreeType(DegreeType);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            if (DegreeType == null)
                return Json(true);
            else
                return Json(false);

        }
        #endregion

        #region JobTitle
        [HttpPost]
        public ActionResult IsJobTitleCodeExist(string Code = "")
        {
            JobTitleRepository _Repository = new JobTitleRepository();
            string Original_Code = SessionManager.Code;
            bool IsEditMode = !string.IsNullOrEmpty(Original_Code) ? true : false;
            string returnMsg = "";

            if (IsEditMode && !string.Equals(Original_Code, Code))
            {
                JobTitle.ActionType = "Remote";
                JobTitle.Code = Code;
                JobTitle.CompanyId = SessionManager.CompanyId;
                JobTitle = _Repository.AddEdit_JobTitle(JobTitle);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            else if (!IsEditMode)
            {
                JobTitle.ActionType = "Remote";
                JobTitle.Code = Code;
                JobTitle.CompanyId = SessionManager.CompanyId;
                JobTitle = _Repository.AddEdit_JobTitle(JobTitle);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            if (JobTitle == null)
                return Json(true);
            else
                return Json(false);

        }
        #endregion

        #region JobTitle
        [HttpPost]
        public ActionResult IsProgramCodeExist(string Code = "")
        {
            ProgramRepository _Repository = new ProgramRepository();
            string Original_Code = SessionManager.Code;
            bool IsEditMode = !string.IsNullOrEmpty(Original_Code) ? true : false;
            string returnMsg = "";

            if (IsEditMode && !string.Equals(Original_Code, Code))
            {
                Program.ActionType = "Remote";
                Program.Code = Code;
                Program.CompanyId = SessionManager.CompanyId;
                Program = _Repository.AddEdit_Program(Program);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            else if (!IsEditMode)
            {
                Program.ActionType = "Remote";
                Program.Code = Code;
                Program.CompanyId = SessionManager.CompanyId;
                Program = _Repository.AddEdit_Program(Program);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            if (Program == null)
                return Json(true);
            else
                return Json(false);

        }
        #endregion

        #region StaffDepartment
        [HttpPost]
        public ActionResult IsStaffDepartmentExist(string Code = "")
        {
            StaffDepartmentRepository _Repository = new StaffDepartmentRepository();
            string Original_Code = SessionManager.Code;
            bool IsEditMode = !string.IsNullOrEmpty(Original_Code) ? true : false;
            string returnMsg = "";

            if (IsEditMode && !string.Equals(Original_Code, Code))
            {
                StaffDepartment.ActionType = "Remote";
                StaffDepartment.Code = Code;
                StaffDepartment.CompanyId = SessionManager.CompanyId;
                StaffDepartment = _Repository.AddEdit_StaffDepartment(StaffDepartment);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            else if (!IsEditMode)
            {
                StaffDepartment.ActionType = "Remote";
                StaffDepartment.Code = Code;
                StaffDepartment.CompanyId = SessionManager.CompanyId;
                StaffDepartment = _Repository.AddEdit_StaffDepartment(StaffDepartment);
                returnMsg = $"Code '{Code}' is already in use.";
            }
            if (StaffDepartment == null)
                return Json(true);
            else
                return Json(false);

        }
        #endregion
    }
}