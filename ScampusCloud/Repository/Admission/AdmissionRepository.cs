using ScampusCloud.DataBase;
using ScampusCloud.Models;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScampusCloud.Repository.Admission
{
	public class AdmissionRepository
	{
		private GeneralMethods objgm = new GeneralMethods();
		public AdmissionModel AddEdit_Admission(AdmissionModel _AdmissionModel)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = _AdmissionModel.GetType().Name,
					StoredProcedureName = @"SP_Admission_CURD",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				if (_AdmissionModel.ActionType == "Remote")
				{
					objQueryBuilder.AddFieldValue("@Code", _AdmissionModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _AdmissionModel.CompanyId, DataTypes.Text, false);
				}
				else
				{
					objQueryBuilder.AddFieldValue("@Id", _AdmissionModel.Id, DataTypes.Numeric, false);
					objQueryBuilder.AddFieldValue("@Name", _AdmissionModel.Name, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Type", _AdmissionModel.Type, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _AdmissionModel.CompanyId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Code", _AdmissionModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Isactive", _AdmissionModel.IsActive, DataTypes.Boolean, false);
					objQueryBuilder.AddFieldValue("@CreatedBy", _AdmissionModel.CreatedBy, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@ModifiedBy", _AdmissionModel.ModifiedBy, DataTypes.Text, false);
				}
				objQueryBuilder.AddFieldValue("@ActionType", _AdmissionModel.ActionType, DataTypes.Text, false);
				return objgm.ExecuteObjectUsingSp<AdmissionModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public List<AdmissionModel> GetAdmissionList(string searchtxt = "", int page = 1, int pagesize = 10, string CompanyId = null)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_Admission",
					StoredProcedureName = @"SP_GetAdmissionData",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@page", page, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@pagesize", pagesize, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
				return objgm.GetListUsingSp<AdmissionModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public string GetAllCount(string searchtxt = "", string CompanyId = null)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_Admission",
					StoredProcedureName = @"SP_GetAdmissionData_Count",
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

		public DataTable GetAdmissionData_Export(string searchtxt = "", string CompanyId = null)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Admission",
					StoredProcedureName = @"SP_DownloadAdmissionData_Export",
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
	}
}