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

				objQueryBuilder.AddFieldValue("@Id", _StaffModel.Id, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@CompanyId", _StaffModel.CompanyId, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@Name", _StaffModel.FullName, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@Address", _StaffModel.Address, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@Website", _StaffModel.FullNameAmharic, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@EmailId", _StaffModel.EmailId, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@PhoneNumber", _StaffModel.PersonalPhone, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@CreatedBy", _StaffModel.CreatedBy, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@ModifiedBy", _StaffModel.ModifiedBy, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@ActionType", _StaffModel.ActionType, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@AdminUserEmailId", _StaffModel.EmailId, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@AdminUserPhoneNumber", _StaffModel.PersonalPhone, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@AdminUserPassword", EncryptionDecryption.GetEncrypt(_StaffModel.Password), DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@Isactive", _StaffModel.Isactive, DataTypes.Boolean, false);

				return objgm.ExecuteObjectUsingSp<StaffModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public List<StaffModel> GetStaffList(string searchtxt = "", int page = 1, int pagesize = 10)
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
				return objgm.GetListUsingSp<StaffModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public string GetAllCount(string searchtxt = "")
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
				return objgm.ExcecuteScalarUsingSp(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}

		}

		public DataTable GetStaffData_Export(string searchtxt = "")
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