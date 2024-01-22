using ClosedXML.Excel;
using ScampusCloud.Models;
using ScampusCloud.Repository.StudentFacility;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ScampusCloud.Controllers
{
    [SessionTimeoutAttribute]
    public class StudentFacilityController : Controller
    {
        #region Variable Declaration
        private readonly StudentFacilityRepository _StudentFacilityRepository;
        StudentFacilityModel _StudentFacilityModel = new StudentFacilityModel();
        #endregion
        #region CTOR
        public StudentFacilityController()
        {
            _StudentFacilityRepository = new StudentFacilityRepository();
        }
        #endregion

        #region Method
        // GET: StudentFacility
        public ActionResult StudentFacility()
        {
            ViewData["paging_size"] = 10;
            if (ViewData["currentPage"] == null)
                ViewData["currentPage"] = 1;
            string searchtxt = "NA";
            int totals = Convert.ToInt32(_StudentFacilityRepository.GetAllCount(searchtxt));
            ViewData["totalrecords"] = totals;
            return View();
        }
        public ActionResult AddEditStudentFacility(string ID = "")
        {
            try
            {

                _StudentFacilityModel.CreatedBy = SessionManager.UserId;
                _StudentFacilityModel.ModifiedBy = SessionManager.UserId;
                _StudentFacilityModel.CompanyId = SessionManager.CompanyId;

                if (!string.IsNullOrEmpty(ID) && ID != "0")
                {
                    #region Get Entity by id
                    _StudentFacilityModel.ActionType = "Edit";
                    _StudentFacilityModel.Id = Convert.ToInt32(ID);
                    _StudentFacilityModel = _StudentFacilityRepository.AddEdit_StudentFacility(_StudentFacilityModel);
                    if (_StudentFacilityModel != null)
                    {
                        _StudentFacilityModel.IsEdit = true;

                        //if (model.Code == null)
                        //    model.Code = "";
                        //HttpContext.Session.SetString("Original_Id", model.Code);
                    }
                    else
                    {
                        _StudentFacilityModel = new StudentFacilityModel();
                        ViewBag.NoRecordExist = true;
                        _StudentFacilityModel.Response_Message = "No record found";
                        //HttpContext.Session.SetString("Original_Id", "");
                    }
                    #endregion
                }
                //// If url Apeended with Querystring ID=0 then Redirect into current Action
                else if (!string.IsNullOrEmpty(ID) && ID == "0")
                {
                    return RedirectToAction("AddEditStudentFacility");
                }
                else
                {
                    _StudentFacilityModel.IsEdit = false;
                    _StudentFacilityModel.IsActive = true;
                    //HttpContext.Session.SetString("Original_Id", "");
                }
                return View(_StudentFacilityModel);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
                throw;
            }

        }
        [HttpPost]
        public ActionResult AddEditStudentFacility(StudentFacilityModel _StudentFacilityModel, string saveAndExit = "")
        {
            if (_StudentFacilityModel.Id > 0)
            {
                _StudentFacilityModel.ActionType = "Update";
            }
            else
            {
                _StudentFacilityModel.ActionType = "Insert";
            }
            var officemaster = _StudentFacilityRepository.AddEdit_StudentFacility(_StudentFacilityModel);
            if (!string.IsNullOrEmpty(saveAndExit))
            {
                return RedirectToAction("StudentFacility", "StudentFacility");
            }
            else if (_StudentFacilityModel.IsEdit == true)
            {
                return RedirectToAction("AddEditStudentFacility", new { ID = _StudentFacilityModel.Id });
            }
            else
            {
                return RedirectToAction("AddEditStudentFacility");
            }
            //return RedirectToAction("StudentFacility", "StudentFacility");
        }

        public ActionResult StudentFacilityList(int page = 1, int pagesize = 10, string searchtxt = "")
        {
            try
            {
                searchtxt = string.IsNullOrEmpty(searchtxt) ? "" : searchtxt;
                int totals = Convert.ToInt32(_StudentFacilityRepository.GetAllCount(searchtxt));
                var lstCountries = _StudentFacilityRepository.GetStudentFacilityList(searchtxt, page, pagesize);
                Session["totalrecords"] = Convert.ToString(totals);
                Session["paging_size"] = Convert.ToString(pagesize);
                ViewData["totalrecords"] = totals;
                ViewData["paging_size"] = pagesize;
                StringBuilder strHTML = new StringBuilder();
                if (lstCountries.Count > 0)
                {
                    strHTML.Append("<table class='datatable-bordered datatable-head-custom datatable-table' id='kt_datatable'>");
                    strHTML.Append("<thead class='datatable-head'>");
                    strHTML.Append("<tr class='datatable-row'>");
                    strHTML.Append("<th class='datatable-cell'>Code</th>");
                    strHTML.Append("<th class='datatable-cell'>Name</th>");
                    strHTML.Append("<th class='datatable-cell'>Status</th>");
                    strHTML.Append("<th class='datatable-cell'>Action</th>");
                    strHTML.Append("</tr>");
                    strHTML.Append("</thead>");
                    strHTML.Append("<tbody class='datatable-body custom-scroll'>");
                    foreach (var item in lstCountries)
                    {
                        string DeleteConfirmationEvent = "DeleteConfirmation('" + item.Id + "','StudentFacility','StudentFacility','Delete')";
                        strHTML.Append("<tr>");
                        strHTML.Append("<td>" + item.Code + "</td>");
                        strHTML.Append("<td>" + item.Name + "</td>");
                        if (item.IsActive)
                            strHTML.Append("<td><span><span class='label font-weight-bold label-lg label-light-primary label-inline'>Active</span></span></td>");
                        else
                            strHTML.Append("<td><span><span class='label font-weight-bold label-lg label-light-danger label-inline'>InActive</span></span></td>");

                        strHTML.Append("<td>");
                        strHTML.Append("<a class='btn btn-sm btn-icon btn-lg-light btn-text-primary btn-hover-light-primary mr-3' href= '/StudentFacility/AddEditStudentFacility?ID=" + item.Id + "'><i class='flaticon-edit'></i></a>");
                        strHTML.Append("<a id = 'del_" + item.Id + "' class='btn btn-sm btn-icon btn-lg-light btn-text-danger btn-hover-light-danger' onclick=" + DeleteConfirmationEvent + "><i class='flaticon-delete'></i></a>");
                        strHTML.Append("</td>");
                        strHTML.Append("</tr>");
                    }
                    strHTML.Append("</tbody>");
                    strHTML.Append("</table>");
                }
                else
                {
                    strHTML.Append("<center>No data available in table</center>");
                }
                return Content(strHTML.ToString());
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
                throw;
            }


            //return Json(StudentFacility, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentFacilityListCount()
        {
            try
            {
                string result = string.Empty;
                int totals = Convert.ToInt32(Session["totalrecords"]);
                int pagesize = Convert.ToInt32(Session["paging_size"]);
                ViewData["paging_size"] = Convert.ToInt32(Session["paging_size"]);

                int noofpages = 1;
                if (totals > 0 && pagesize > 0)
                    noofpages = (totals / pagesize) + (totals % pagesize != 0 ? 1 : 0);
                result = "{\"noofpages\":" + noofpages + ",\"NoOfTotalRecords\":" + totals + "}";

                return Content((result));
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Delete(string Id)
        {
            try
            {
                _StudentFacilityModel.ActionType = "Delete";
                _StudentFacilityModel.Id = Convert.ToInt32(Id);
                var response = _StudentFacilityRepository.AddEdit_StudentFacility(_StudentFacilityModel);

                return RedirectToAction("StudentFacility", "StudentFacility");
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        //[HttpPost]
        //public string PreviewSelectedImage(IFormFile file)
        //{
        //    string base64OfThumbImg = string.Empty, originalImageBase64 = string.Empty;
        //    if (file != null)
        //    {
        //        //originalImageBase64 = ImageCompressor.OriginalBase64String(file).Result;
        //        base64OfThumbImg = ImageCompressor.GetBase64StringAsync(file, 300, 300).Result;
        //    }
        //    return base64OfThumbImg;
        //}

        [HttpGet]
        public FileResult Export(string searchtxt = "")
        {
            DataTable dt = new DataTable("StudentFacility");
            try
            {
                dt = _StudentFacilityRepository.GetStudentFacilityData_Export(searchtxt);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StudentFacility.xlsx");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
        #endregion
    }
}