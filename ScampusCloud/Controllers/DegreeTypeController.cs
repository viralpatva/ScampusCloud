using ClosedXML.Excel;
using ScampusCloud.Models;
using ScampusCloud.Repository.DegreeType;
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
    public class DegreeTypeController : Controller
    {
        #region Variable Declaration
        private readonly DegreeTypeRepository _DegreeTypeRepository;
        DegreeTypeModel _DegreeTypeModel = new DegreeTypeModel();
        #endregion
        #region CTOR
        public DegreeTypeController()
        {
            _DegreeTypeRepository = new DegreeTypeRepository();
        }
        #endregion

        #region Method
        // GET: DegreeType
        public ActionResult DegreeType()
        {
            ViewData["paging_size"] = 10;
            if (ViewData["currentPage"] == null)
                ViewData["currentPage"] = 1;
            string searchtxt = "NA";
            int totals = Convert.ToInt32(_DegreeTypeRepository.GetAllCount(searchtxt));
            ViewData["totalrecords"] = totals;
            return View();
        }
        public ActionResult AddEditDegreeType(string ID = "")
        {
            try
            {

                _DegreeTypeModel.CreatedBy = SessionManager.UserId;
                _DegreeTypeModel.ModifiedBy = SessionManager.UserId;
                _DegreeTypeModel.CompanyId = SessionManager.CompanyId;

                if (!string.IsNullOrEmpty(ID) && ID != "0")
                {
                    #region Get Entity by id
                    _DegreeTypeModel.ActionType = "Edit";
                    _DegreeTypeModel.Id = Convert.ToInt32(ID);
                    _DegreeTypeModel = _DegreeTypeRepository.AddEdit_DegreeType(_DegreeTypeModel);
                    if (_DegreeTypeModel != null)
                    {
                        _DegreeTypeModel.IsEdit = true;

                        //if (model.Code == null)
                        //    model.Code = "";
                        //HttpContext.Session.SetString("Original_Id", model.Code);
                    }
                    else
                    {
                        _DegreeTypeModel = new DegreeTypeModel();
                        ViewBag.NoRecordExist = true;
                        _DegreeTypeModel.Response_Message = "No record found";
                        //HttpContext.Session.SetString("Original_Id", "");
                    }
                    #endregion
                }
                //// If url Apeended with Querystring ID=0 then Redirect into current Action
                else if (!string.IsNullOrEmpty(ID) && ID == "0")
                {
                    return RedirectToAction("AddEditDegreeType");
                }
                else
                {
                    _DegreeTypeModel.IsEdit = false;
                    _DegreeTypeModel.IsActive = true;
                    //HttpContext.Session.SetString("Original_Id", "");
                }
                return View(_DegreeTypeModel);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
                throw;
            }

        }
        [HttpPost]
        public ActionResult AddEditDegreeType(DegreeTypeModel _DegreeTypeModel, string saveAndExit = "")
        {
            if (_DegreeTypeModel.Id > 0)
            {
                _DegreeTypeModel.ActionType = "Update";
            }
            else
            {
                _DegreeTypeModel.ActionType = "Insert";
            }
            var officemaster = _DegreeTypeRepository.AddEdit_DegreeType(_DegreeTypeModel);
            if (!string.IsNullOrEmpty(saveAndExit))
            {
                return RedirectToAction("DegreeType", "DegreeType");
            }
            else if (_DegreeTypeModel.IsEdit == true)
            {
                return RedirectToAction("AddEditDegreeType", new { ID = _DegreeTypeModel.Id });
            }
            else
            {
                return RedirectToAction("AddEditDegreeType");
            }
            //return RedirectToAction("DegreeType", "DegreeType");
        }

        public ActionResult DegreeTypeList(int page = 1, int pagesize = 10, string searchtxt = "")
        {
            try
            {
                searchtxt = string.IsNullOrEmpty(searchtxt) ? "" : searchtxt;
                int totals = Convert.ToInt32(_DegreeTypeRepository.GetAllCount(searchtxt));
                var lstCountries = _DegreeTypeRepository.GetDegreeTypeList(searchtxt, page, pagesize);
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
                        string DeleteConfirmationEvent = "DeleteConfirmation('" + item.Id + "','DegreeType','DegreeType','Delete')";
                        strHTML.Append("<tr>");
                        strHTML.Append("<td>" + item.Code + "</td>");
                        strHTML.Append("<td>" + item.Name + "</td>");
                        if (item.IsActive)
                            strHTML.Append("<td><span><span class='label font-weight-bold label-lg label-light-primary label-inline'>Active</span></span></td>");
                        else
                            strHTML.Append("<td><span><span class='label font-weight-bold label-lg label-light-danger label-inline'>InActive</span></span></td>");

                        strHTML.Append("<td>");
                        strHTML.Append("<a class='btn btn-sm btn-icon btn-lg-light btn-text-primary btn-hover-light-primary mr-3' href= '/DegreeType/AddEditDegreeType?ID=" + item.Id + "'><i class='flaticon-edit'></i></a>");
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


            //return Json(DegreeType, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DegreeTypeListCount()
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
                _DegreeTypeModel.ActionType = "Delete";
                _DegreeTypeModel.Id = Convert.ToInt32(Id);
                var response = _DegreeTypeRepository.AddEdit_DegreeType(_DegreeTypeModel);

                return RedirectToAction("DegreeType", "DegreeType");
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
            DataTable dt = new DataTable("DegreeType");
            try
            {
                dt = _DegreeTypeRepository.GetDegreeTypeData_Export(searchtxt);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DegreeType.xlsx");
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