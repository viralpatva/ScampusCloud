using ClosedXML.Excel;
using ScampusCloud.Models;
using ScampusCloud.Repository.VisitorStatus;
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
    public class VisitorStatusController : Controller
    {
        #region Variable Declaration
        private readonly VisitorStatusRepository _VisitorStatusRepository;
        VisitorStatusModel _VisitorStatusModel = new VisitorStatusModel();
        #endregion
        #region CTOR
        public VisitorStatusController()
        {
            _VisitorStatusRepository = new VisitorStatusRepository();
        }
        #endregion

        #region Method
        // GET: VisitorStatus
        public ActionResult VisitorStatus()
        {
            ViewData["paging_size"] = 10;
            if (ViewData["currentPage"] == null)
                ViewData["currentPage"] = 1;
            string searchtxt = "NA";
            int totals = Convert.ToInt32(_VisitorStatusRepository.GetAllCount(searchtxt, SessionManager.CompanyId.ToString()));
            ViewData["totalrecords"] = totals;
            return View();
        }
        public ActionResult AddEditVisitorStatus(string ID = "")
        {
            try
            {

                _VisitorStatusModel.CreatedBy = SessionManager.UserId;
                _VisitorStatusModel.ModifiedBy = SessionManager.UserId;
                _VisitorStatusModel.CompanyId = SessionManager.CompanyId;

                if (!string.IsNullOrEmpty(ID) && ID != "0")
                {
                    #region Get Entity by id
                    _VisitorStatusModel.ActionType = "Edit";
                    _VisitorStatusModel.Id = Convert.ToInt32(ID);
                    _VisitorStatusModel = _VisitorStatusRepository.AddEdit_VisitorStatus(_VisitorStatusModel);
                    if (_VisitorStatusModel != null)
                    {
                        _VisitorStatusModel.IsEdit = true;
                        SessionManager.Code = _VisitorStatusModel.Code;
                    }
                    else
                    {
                        _VisitorStatusModel = new VisitorStatusModel();
                        ViewBag.NoRecordExist = true;
                        _VisitorStatusModel.Response_Message = "No record found";
                        SessionManager.Code = null;
                    }
                    #endregion
                }
                //// If url Apeended with Querystring ID=0 then Redirect into current Action
                else if (!string.IsNullOrEmpty(ID) && ID == "0")
                {
                    return RedirectToAction("AddEditVisitorStatus");
                }
                else
                {
                    _VisitorStatusModel.IsEdit = false;
                    _VisitorStatusModel.IsActive = true;
                    SessionManager.Code = null;
                }
                return View(_VisitorStatusModel);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
                throw;
            }

        }
        [HttpPost]
        public ActionResult AddEditVisitorStatus(VisitorStatusModel _VisitorStatusModel, string saveAndExit = "")
        {
            if (_VisitorStatusModel.Id > 0)
            {
                _VisitorStatusModel.ActionType = "Update";
            }
            else
            {
                _VisitorStatusModel.ActionType = "Insert";
            }
            var officemaster = _VisitorStatusRepository.AddEdit_VisitorStatus(_VisitorStatusModel);
            if (!string.IsNullOrEmpty(saveAndExit))
            {
                return RedirectToAction("VisitorStatus", "VisitorStatus");
            }
            else if (_VisitorStatusModel.IsEdit == true)
            {
                return RedirectToAction("AddEditVisitorStatus", new { ID = _VisitorStatusModel.Id });
            }
            else
            {
                return RedirectToAction("AddEditVisitorStatus");
            }
            //return RedirectToAction("VisitorStatus", "VisitorStatus");
        }

        public ActionResult VisitorStatusList(int page = 1, int pagesize = 10, string searchtxt = "")
        {
            try
            {
                searchtxt = string.IsNullOrEmpty(searchtxt) ? "" : searchtxt;
                int totals = Convert.ToInt32(_VisitorStatusRepository.GetAllCount(searchtxt, SessionManager.CompanyId.ToString()));
                var lstCountries = _VisitorStatusRepository.GetVisitorStatusList(searchtxt, page, pagesize, SessionManager.CompanyId.ToString());
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
                        string DeleteConfirmationEvent = "DeleteConfirmation('" + item.Id + "','VisitorStatus','VisitorStatus','Delete')";
                        strHTML.Append("<tr>");
                        strHTML.Append("<td>" + item.Code + "</td>");
                        strHTML.Append("<td>" + item.Name + "</td>");
                        if (item.IsActive)
                            strHTML.Append("<td><span><span class='label font-weight-bold label-lg label-light-primary label-inline'>Active</span></span></td>");
                        else
                            strHTML.Append("<td><span><span class='label font-weight-bold label-lg label-light-danger label-inline'>InActive</span></span></td>");

                        strHTML.Append("<td>");
                        strHTML.Append("<a class='btn btn-sm btn-icon btn-lg-light btn-text-primary btn-hover-light-primary mr-3' href= '/VisitorStatus/AddEditVisitorStatus?ID=" + item.Id + "'><i class='flaticon-edit'></i></a>");
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


            //return Json(VisitorStatus, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VisitorStatusListCount()
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
                _VisitorStatusModel.ActionType = "Delete";
                _VisitorStatusModel.Id = Convert.ToInt32(Id);
                var response = _VisitorStatusRepository.AddEdit_VisitorStatus(_VisitorStatusModel);

                return RedirectToAction("VisitorStatus", "VisitorStatus");
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
            DataTable dt = new DataTable("VisitorStatus");
            try
            {
                dt = _VisitorStatusRepository.GetVisitorStatusData_Export(searchtxt, SessionManager.CompanyId.ToString());
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VisitorStatus.xlsx");
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