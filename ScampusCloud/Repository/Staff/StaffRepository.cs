using ScampusCloud.DataBase;
using ScampusCloud.Models;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ScampusCloud.Repository.Staff
{
	public class StaffRepository
	{
		private GeneralMethods objgm = new GeneralMethods();
		public StaffModel AddEdit_Staff(StaffModel _StaffModel)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = _StaffModel.GetType().Name,
					StoredProcedureName = @"SP_Staff_CURD",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				if (_StaffModel.ActionType == "Edit" || _StaffModel.ActionType == "Delete")
				{
					objQueryBuilder.AddFieldValue("@Id", _StaffModel.Id, DataTypes.Numeric, false);
				}
				else if (_StaffModel.ActionType == "Remote")
				{
					objQueryBuilder.AddFieldValue("@Id", _StaffModel.Id, DataTypes.Numeric, false);
					objQueryBuilder.AddFieldValue("@StaffId", _StaffModel.StaffId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _StaffModel.CompanyId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@EmailId", _StaffModel.EmailId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Code", _StaffModel.Code, DataTypes.Text, false);
				}
				else
				{
					objQueryBuilder.AddFieldValue("@Id", _StaffModel.Id, DataTypes.Numeric, false);
					objQueryBuilder.AddFieldValue("@StaffId", _StaffModel.StaffId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@FullName", _StaffModel.FullName, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _StaffModel.CompanyId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Code", _StaffModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@FullNameAmharic", _StaffModel.FullNameAmharic, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@BirthDate", _StaffModel.BirthDate, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Gender", _StaffModel.Gender, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@BloodGroup", _StaffModel.BloodGroup, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@PersonalPhone", _StaffModel.PersonalPhone, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@EmailId", _StaffModel.EmailId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Password", EncryptionDecryption.GetEncrypt(_StaffModel.Password), DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Address", _StaffModel.Address, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CountryId", _StaffModel.CountryId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@DepartmentId", _StaffModel.DepartmentId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@JobTitleId", _StaffModel.JobTitleId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyMstrId", _StaffModel.CompanyMstrId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@FacilityId", _StaffModel.FacilityId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CanteenId", _StaffModel.CanteenId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CardStatusId", _StaffModel.CardStatusId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@IssueDate", _StaffModel.IssueDate, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@UID", _StaffModel.UID, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@VehicleNumber", _StaffModel.VehicleNumber, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CardNumber", _StaffModel.CardNumber, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Woreda", _StaffModel.Woreda, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@SubCity", _StaffModel.SubCity, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@HouseNumber", _StaffModel.HouseNumber, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@ImagePath", _StaffModel.ImagePath, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@SignaturePath", _StaffModel.SignaturePath, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@ImageBase64", _StaffModel.ImageBase64, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@SignatureBase64", _StaffModel.SignatureBase64, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Isactive", _StaffModel.Isactive, DataTypes.Boolean, false);
					objQueryBuilder.AddFieldValue("@CreatedBy", _StaffModel.CreatedBy, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@ModifiedBy", _StaffModel.ModifiedBy, DataTypes.Text, false);
				}
				objQueryBuilder.AddFieldValue("@ActionType", _StaffModel.ActionType, DataTypes.Text, false);
				return objgm.ExecuteObjectUsingSp<StaffModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public List<StaffModel> GetStaffList(string searchtxt = "", int page = 1, int pagesize = 10,string CompanyId=null)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_Staff",
					StoredProcedureName = @"SP_GetStaffData",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@page", page, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@pagesize", pagesize, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
				return objgm.GetListUsingSp<StaffModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public string GetAllCount(string searchtxt = "",string CompanyId=null)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_Staff",
					StoredProcedureName = @"SP_GetStaffData_Count",
					SetQueryType = QueryBuilder.QueryType.SELECT,
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
				return objgm.ExcecuteScalarUsingSp(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}

		}

		public DataTable GetStaffData_Export(string searchtxt = "", string CompanyId = null)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Staff",
					StoredProcedureName = @"SP_DownloadStaffData_Export",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
				return objgm.ExecuteDataTableUsingSp(objQueryBuilder, true);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}
		public List<SelectListItem> BindCountryDropDown(string CompanyId)
		{
			QueryBuilder objQueryBuilder = new QueryBuilder
			{
				TableName = "tbl_Country",
				StoredProcedureName = @"Sps_Load_Country_Dropdown",
				SetQueryType = QueryBuilder.QueryType.SELECT,
			};
			objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
			return objgm.GetListUsingSp<SelectListItem>(objQueryBuilder);
		}

		public List<SelectListItem> BindStaffDepartmentDropDown(string CompanyId)
		{
			QueryBuilder objQueryBuilder = new QueryBuilder
			{
				TableName = "tbl_StaffDepartment",
				StoredProcedureName = @"Sps_Load_StaffDepartment_Dropdown",
				SetQueryType = QueryBuilder.QueryType.SELECT,
			};
			objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
			return objgm.GetListUsingSp<SelectListItem>(objQueryBuilder);
		}

		public List<SelectListItem> BindJobTitleDropDown(string CompanyId)
		{
			QueryBuilder objQueryBuilder = new QueryBuilder
			{
				TableName = "tbl_JobTitle",
				StoredProcedureName = @"Sps_Load_JobTitle_Dropdown",
				SetQueryType = QueryBuilder.QueryType.SELECT,
			};
			objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
			return objgm.GetListUsingSp<SelectListItem>(objQueryBuilder);
		}

		public List<SelectListItem> BindStudentCompanyDropDown(string CompanyId)
		{
			QueryBuilder objQueryBuilder = new QueryBuilder
			{
				TableName = "tbl_StudentCompany",
				StoredProcedureName = @"Sps_Load_StudentCompany_Dropdown",
				SetQueryType = QueryBuilder.QueryType.SELECT,
			};
			objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
			return objgm.GetListUsingSp<SelectListItem>(objQueryBuilder);
		}
		public List<SelectListItem> BindFacilityDropDown(string CompanyId)
		{
			QueryBuilder objQueryBuilder = new QueryBuilder
			{
				TableName = "tbl_Facility",
				StoredProcedureName = @"Sps_Load_Facility_Dropdown",
				SetQueryType = QueryBuilder.QueryType.SELECT,
			};
			objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
			return objgm.GetListUsingSp<SelectListItem>(objQueryBuilder);
		}

		public List<SelectListItem> BindCardStatusDropDown(string CompanyId)
		{
			QueryBuilder objQueryBuilder = new QueryBuilder
			{
				TableName = "tbl_CardStatus",
				StoredProcedureName = @"Sps_Load_CardStatus_Dropdown",
				SetQueryType = QueryBuilder.QueryType.SELECT,
			};
			objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
			return objgm.GetListUsingSp<SelectListItem>(objQueryBuilder);
		}
		public List<SelectListItem> BindCanteenDropDown(string CompanyId)
		{
			QueryBuilder objQueryBuilder = new QueryBuilder
			{
				TableName = "tbl_Canteen",
				StoredProcedureName = @"Sps_Load_Canteen_Dropdown",
				SetQueryType = QueryBuilder.QueryType.SELECT,
			};
			objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
			return objgm.GetListUsingSp<SelectListItem>(objQueryBuilder);
		}
	}
}