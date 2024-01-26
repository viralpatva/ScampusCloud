using ScampusCloud.DataBase;
using ScampusCloud.Models;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScampusCloud.Repository.VisitorReason
{
    public class VisitorReasonRepository
    {
		private GeneralMethods objgm = new GeneralMethods();
		public VisitorReasonModel AddEdit_VisitorReason(VisitorReasonModel _VisitorReasonModel)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = _VisitorReasonModel.GetType().Name,
					StoredProcedureName = @"SP_VisitorReason_CURD",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				if (_VisitorReasonModel.ActionType == "Remote")
				{
					objQueryBuilder.AddFieldValue("@Code", _VisitorReasonModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _VisitorReasonModel.CompanyId, DataTypes.Text, false);
				}
				else
				{
					objQueryBuilder.AddFieldValue("@Id", _VisitorReasonModel.Id, DataTypes.Numeric, false);
					objQueryBuilder.AddFieldValue("@Name", _VisitorReasonModel.Name, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _VisitorReasonModel.CompanyId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Code", _VisitorReasonModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Isactive", _VisitorReasonModel.IsActive, DataTypes.Boolean, false);
					objQueryBuilder.AddFieldValue("@CreatedBy", _VisitorReasonModel.CreatedBy, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@ModifiedBy", _VisitorReasonModel.ModifiedBy, DataTypes.Text, false);
				}
				objQueryBuilder.AddFieldValue("@ActionType", _VisitorReasonModel.ActionType, DataTypes.Text, false);

				return objgm.ExecuteObjectUsingSp<VisitorReasonModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public List<VisitorReasonModel> GetVisitorReasonList(string searchtxt = "", int page = 1, int pagesize = 10, string CompanyId = null)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_VisitorReason",
					StoredProcedureName = @"SP_GetVisitorReasonData",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@page", page, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@pagesize", pagesize, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
				return objgm.GetListUsingSp<VisitorReasonModel>(objQueryBuilder);
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
					TableName = "Tbl_Mstr_VisitorReason",
					StoredProcedureName = @"SP_GetVisitorReasonData_Count",
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

		public DataTable GetVisitorReasonData_Export(string searchtxt = "", string CompanyId = null)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "VisitorReason",
					StoredProcedureName = @"SP_DownloadVisitorReasonData_Export",
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