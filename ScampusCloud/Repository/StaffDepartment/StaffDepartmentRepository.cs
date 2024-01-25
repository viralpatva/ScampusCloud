using ScampusCloud.DataBase;
using ScampusCloud.Models;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScampusCloud.Repository.StaffDepartment
{
	public class StaffDepartmentRepository
	{
		private GeneralMethods objgm = new GeneralMethods();
		public StaffDepartmentModel AddEdit_StaffDepartment(StaffDepartmentModel _StaffDepartmentModel)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = _StaffDepartmentModel.GetType().Name,
					StoredProcedureName = @"SP_StaffDepartment_CURD",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				if (_StaffDepartmentModel.ActionType == "Remote")
				{
					objQueryBuilder.AddFieldValue("@Code", _StaffDepartmentModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _StaffDepartmentModel.CompanyId, DataTypes.Text, false);
				}
				else
				{
					objQueryBuilder.AddFieldValue("@Id", _StaffDepartmentModel.Id, DataTypes.Numeric, false);
					objQueryBuilder.AddFieldValue("@Name", _StaffDepartmentModel.Name, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _StaffDepartmentModel.CompanyId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Code", _StaffDepartmentModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Isactive", _StaffDepartmentModel.IsActive, DataTypes.Boolean, false);
					objQueryBuilder.AddFieldValue("@CreatedBy", _StaffDepartmentModel.CreatedBy, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@ModifiedBy", _StaffDepartmentModel.ModifiedBy, DataTypes.Text, false);
				}
				objQueryBuilder.AddFieldValue("@ActionType", _StaffDepartmentModel.ActionType, DataTypes.Text, false);

				return objgm.ExecuteObjectUsingSp<StaffDepartmentModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public List<StaffDepartmentModel> GetStaffDepartmentList(string searchtxt = "", int page = 1, int pagesize = 10)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_StaffDepartment",
					StoredProcedureName = @"SP_GetStaffDepartmentData",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@page", page, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@pagesize", pagesize, DataTypes.Numeric, false);
				return objgm.GetListUsingSp<StaffDepartmentModel>(objQueryBuilder);
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
					TableName = "Tbl_Mstr_StaffDepartment",
					StoredProcedureName = @"SP_GetStaffDepartmentData_Count",
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

		public DataTable GetStaffDepartmentData_Export(string searchtxt = "")
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "StaffDepartment",
					StoredProcedureName = @"SP_DownloadStaffDepartmentData_Export",
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
	}
}