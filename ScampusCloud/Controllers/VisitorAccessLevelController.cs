using ClosedXML.Excel;
using ScampusCloud.Models;
using ScampusCloud.Repository.VisitorAccessLevel;
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
    public class VisitorAccessLevelController : Controller
    {
        #region Variable Declaration
        private readonly VisitorAccessLevelRepository _VisitorAccessLevelRepository;
        VisitorAccessLevelModel _VisitorAccessLevelModel = new VisitorAccessLevelModel();
        #endregion
        #region CTOR
        public VisitorAccessLevelController()
        {
            _VisitorAccessLevelRepository = new VisitorAccessLevelRepository();
        }
        #endregion

        #region Method
        // GET: VisitorAccessLevel
        public ActionResult VisitorAccessLevel()
        {
            ViewData["paging_size"] = 10;
            if (ViewData["currentPage"] == null)
                ViewData["currentPage"] = 1;
            string searchtxt = "NA";
            int totals = Convert.ToInt32(_VisitorAccessLevelRepository.GetAllCount(searchtxt));
            ViewData["totalrecords"] = totals;
            return View();
        }
        public ActionResult AddEditVisitorAccessLevel(string ID = "")
        {
            try
            {

                _VisitorAccessLevelModel.CreatedBy = SessionManager.UserId;
                _VisitorAccessLevelModel.ModifiedBy = SessionManager.UserId;
                _VisitorAccessLevelModel.CompanyId = SessionManager.CompanyId;

                if (!string.IsNullOrEmpty(ID) && ID != "0")
                {
                    #region Get Entity by id
                    _VisitorAccessLevelModel.ActionType = "Edit";
                    _VisitorAccessLevelModel.Id = Convert.ToInt32(ID);
                    _VisitorAccessLevelModel = _VisitorAccessLevelRepository.AddEdit_VisitorAccessLevel(_VisitorAccessLevelModel);
                    if (_VisitorAccessLevelModel != null)
                    {
                        _VisitorAccessLevelModel.IsEdit = true;

                        //if (model.Code == null)
                        //    model.Code = "";
                        //HttpContext.Session.SetString("Original_Id", model.Code);
                    }
                    else
                    {
                        _VisitorAccessLevelModel = new VisitorAccessLevelModel();
                        ViewBag.NoRecordExist = true;
                        _VisitorAccessLevelModel.Response_Message = "No record found";
                        //HttpContext.Session.SetString("Original_Id", "");
                    }
                    #endregion
                }
                //// If url Apeended with Querystring ID=0 then Redirect into current Action
                else if (!string.IsNullOrEmpty(ID) && ID == "0")
                {
                    return RedirectToAction("AddEditVisitorAccessLevel");
                }
                else
                {
                    _VisitorAccessLevelModel.IsEdit = false;
                    _VisitorAccessLevelModel.IsActive = true;
                    //HttpContext.Session.SetString("Original_Id", "");
                }
                return View(_VisitorAccessLevelModel);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
                throw;
            }

        }
        [HttpPost]
        public ActionResult AddEditVisitorAccessLevel(VisitorAccessLevelModel _VisitorAccessLevelModel, string saveAndExit = "")
        {
            if (_VisitorAccessLevelModel.Id > 0)
            {
                _VisitorAccessLevelModel.ActionType = "Update";
            }
            else
            {
                _VisitorAccessLevelModel.ActionType = "Insert";
            }
            var officemaster = _VisitorAccessLevelRepository.AddEdit_VisitorAccessLevel(_VisitorAccessLevelModel);
            if (!string.IsNullOrEmpty(saveAndExit))
            {
                return RedirectToAction("VisitorAccessLevel", "VisitorAccessLevel");
            }
            else if (_VisitorAccessLevelModel.IsEdit == true)
            {
                return RedirectToAction("AddEditVisitorAccessLevel", new { ID = _VisitorAccessLevelModel.Id });
            }
            else
            {
                return RedirectToAction("AddEditVisitorAccessLevel");
            }
            //return RedirectToAction("VisitorAccessLevel", "VisitorAccessLevel");
        }

        public ActionResult VisitorAccessLevelList(int page = 1, int pagesize = 10, string searchtxt = "")
        {
            try
            {
                searchtxt = string.IsNullOrEmpty(searchtxt) ? "" : searchtxt;
                int totals = Convert.ToInt32(_VisitorAccessLevelRepository.GetAllCount(searchtxt));
                var lstCountries = _VisitorAccessLevelRepository.GetVisitorAccessLevelList(searchtxt, page, pagesize);
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
                        string DeleteConfirmationEvent = "DeleteConfirmation('" + item.Id + "','VisitorAccessLevel','VisitorAccessLevel','Delete')";
                        strHTML.Append("<tr>");
                        strHTML.Append("<td>" + item.Code + "</td>");
                        strHTML.Append("<td>" + item.Name + "</td>");
                        if (item.IsActive)
                            strHTML.Append("<td><span><span class='label font-weight-bold label-lg label-light-primary label-inline'>Active</span></span></td>");
                        else
                            strHTML.Append("<td><span><span class='label font-weight-bold label-lg label-light-danger label-inline'>InActive</span></span></td>");

                        strHTML.Append("<td>");
                        strHTML.Append("<a class='btn btn-sm btn-icon btn-lg-light btn-text-primary btn-hover-light-primary mr-3' href= '/VisitorAccessLevel/AddEditVisitorAccessLevel?ID=" + item.Id + "'><i class='flaticon-edit'></i></a>");
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


            //return Json(VisitorAccessLevel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VisitorAccessLevelListCount()
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
                _VisitorAccessLevelModel.ActionType = "Delete";
                _VisitorAccessLevelModel.Id = Convert.ToInt32(Id);
                var response = _VisitorAccessLevelRepository.AddEdit_VisitorAccessLevel(_VisitorAccessLevelModel);

                return RedirectToAction("VisitorAccessLevel", "VisitorAccessLevel");
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
            DataTable dt = new DataTable("VisitorAccessLevel");
            try
            {
                dt = _VisitorAccessLevelRepository.GetVisitorAccessLevelData_Export(searchtxt);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VisitorAccessLevel.xlsx");
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