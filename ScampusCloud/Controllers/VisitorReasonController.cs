using ClosedXML.Excel;
using ScampusCloud.Models;
using ScampusCloud.Repository.VisitorReason;
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
    public class VisitorReasonController : Controller
    {
        #region Variable Declaration
        private readonly VisitorReasonRepository _VisitorReasonRepository;
        VisitorReasonModel _VisitorReasonModel = new VisitorReasonModel();
        #endregion
        #region CTOR
        public VisitorReasonController()
        {
            _VisitorReasonRepository = new VisitorReasonRepository();
        }
        #endregion

        #region Method
        // GET: VisitorReason
        public ActionResult VisitorReason()
        {
            ViewData["paging_size"] = 10;
            if (ViewData["currentPage"] == null)
                ViewData["currentPage"] = 1;
            string searchtxt = "NA";
            int totals = Convert.ToInt32(_VisitorReasonRepository.GetAllCount(searchtxt, SessionManager.CompanyId.ToString()));
            ViewData["totalrecords"] = totals;
            return View();
        }
        public ActionResult AddEditVisitorReason(string ID = "")
        {
            try
            {

                _VisitorReasonModel.CreatedBy = SessionManager.UserId;
                _VisitorReasonModel.ModifiedBy = SessionManager.UserId;
                _VisitorReasonModel.CompanyId = SessionManager.CompanyId;

                if (!string.IsNullOrEmpty(ID) && ID != "0")
                {
                    #region Get Entity by id
                    _VisitorReasonModel.ActionType = "Edit";
                    _VisitorReasonModel.Id = Convert.ToInt32(ID);
                    _VisitorReasonModel = _VisitorReasonRepository.AddEdit_VisitorReason(_VisitorReasonModel);
                    if (_VisitorReasonModel != null)
                    {
                        _VisitorReasonModel.IsEdit = true;
                        SessionManager.Code = _VisitorReasonModel.Code;
                    }
                    else
                    {
                        _VisitorReasonModel = new VisitorReasonModel();
                        ViewBag.NoRecordExist = true;
                        _VisitorReasonModel.Response_Message = "No record found";
                        SessionManager.Code = null;
                    }
                    #endregion
                }
                //// If url Apeended with Querystring ID=0 then Redirect into current Action
                else if (!string.IsNullOrEmpty(ID) && ID == "0")
                {
                    return RedirectToAction("AddEditVisitorReason");
                }
                else
                {
                    _VisitorReasonModel.IsEdit = false;
                    _VisitorReasonModel.IsActive = true;
                    SessionManager.Code = null;
                }
                return View(_VisitorReasonModel);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
                throw;
            }

        }
        [HttpPost]
        public ActionResult AddEditVisitorReason(VisitorReasonModel _VisitorReasonModel, string saveAndExit = "")
        {
            if (_VisitorReasonModel.Id > 0)
            {
                _VisitorReasonModel.ActionType = "Update";
            }
            else
            {
                _VisitorReasonModel.ActionType = "Insert";
            }
            var officemaster = _VisitorReasonRepository.AddEdit_VisitorReason(_VisitorReasonModel);
            if (!string.IsNullOrEmpty(saveAndExit))
            {
                return RedirectToAction("VisitorReason", "VisitorReason");
            }
            else if (_VisitorReasonModel.IsEdit == true)
            {
                return RedirectToAction("AddEditVisitorReason", new { ID = _VisitorReasonModel.Id });
            }
            else
            {
                return RedirectToAction("AddEditVisitorReason");
            }
            //return RedirectToAction("VisitorReason", "VisitorReason");
        }

        public ActionResult VisitorReasonList(int page = 1, int pagesize = 10, string searchtxt = "")
        {
            try
            {
                searchtxt = string.IsNullOrEmpty(searchtxt) ? "" : searchtxt;
                int totals = Convert.ToInt32(_VisitorReasonRepository.GetAllCount(searchtxt, SessionManager.CompanyId.ToString()));
                var lstCountries = _VisitorReasonRepository.GetVisitorReasonList(searchtxt, page, pagesize, SessionManager.CompanyId.ToString());
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
                        string DeleteConfirmationEvent = "DeleteConfirmation('" + item.Id + "','VisitorReason','VisitorReason','Delete')";
                        strHTML.Append("<tr>");
                        strHTML.Append("<td>" + item.Code + "</td>");
                        strHTML.Append("<td>" + item.Name + "</td>");
                        if (item.IsActive)
                            strHTML.Append("<td><span><span class='label font-weight-bold label-lg label-light-primary label-inline'>Active</span></span></td>");
                        else
                            strHTML.Append("<td><span><span class='label font-weight-bold label-lg label-light-danger label-inline'>InActive</span></span></td>");

                        strHTML.Append("<td>");
                        strHTML.Append("<a class='btn btn-sm btn-icon btn-lg-light btn-text-primary btn-hover-light-primary mr-3' href= '/VisitorReason/AddEditVisitorReason?ID=" + item.Id + "'><i class='flaticon-edit'></i></a>");
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


            //return Json(VisitorReason, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VisitorReasonListCount()
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
                _VisitorReasonModel.ActionType = "Delete";
                _VisitorReasonModel.Id = Convert.ToInt32(Id);
                var response = _VisitorReasonRepository.AddEdit_VisitorReason(_VisitorReasonModel);

                return RedirectToAction("VisitorReason", "VisitorReason");
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
            DataTable dt = new DataTable("VisitorReason");
            try
            {
                dt = _VisitorReasonRepository.GetVisitorReasonData_Export(searchtxt, SessionManager.CompanyId.ToString());
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VisitorReason.xlsx");
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