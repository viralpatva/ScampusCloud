using ScampusCloud.DataBase;
using ScampusCloud.Models;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScampusCloud.Repository.VisitorAccessLevel
{
	public class VisitorAccessLevelRepository
	{
		private GeneralMethods objgm = new GeneralMethods();
		public VisitorAccessLevelModel AddEdit_VisitorAccessLevel(VisitorAccessLevelModel _VisitorAccessLevelModel)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = _VisitorAccessLevelModel.GetType().Name,
					StoredProcedureName = @"SP_VisitorAccessLevel_CURD",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				if (_VisitorAccessLevelModel.ActionType == "Remote")
				{
					objQueryBuilder.AddFieldValue("@Code", _VisitorAccessLevelModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _VisitorAccessLevelModel.CompanyId, DataTypes.Text, false);
				}
				else
				{
					objQueryBuilder.AddFieldValue("@Id", _VisitorAccessLevelModel.Id, DataTypes.Numeric, false);
					objQueryBuilder.AddFieldValue("@Name", _VisitorAccessLevelModel.Name, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _VisitorAccessLevelModel.CompanyId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Code", _VisitorAccessLevelModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Isactive", _VisitorAccessLevelModel.IsActive, DataTypes.Boolean, false);
					objQueryBuilder.AddFieldValue("@CreatedBy", _VisitorAccessLevelModel.CreatedBy, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@ModifiedBy", _VisitorAccessLevelModel.ModifiedBy, DataTypes.Text, false);
				}
				objQueryBuilder.AddFieldValue("@ActionType", _VisitorAccessLevelModel.ActionType, DataTypes.Text, false);

				return objgm.ExecuteObjectUsingSp<VisitorAccessLevelModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public List<VisitorAccessLevelModel> GetVisitorAccessLevelList(string searchtxt = "", int page = 1, int pagesize = 10)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_VisitorAccessLevel",
					StoredProcedureName = @"SP_GetVisitorAccessLevelData",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@page", page, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@pagesize", pagesize, DataTypes.Numeric, false);
				return objgm.GetListUsingSp<VisitorAccessLevelModel>(objQueryBuilder);
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
					TableName = "Tbl_Mstr_VisitorAccessLevel",
					StoredProcedureName = @"SP_GetVisitorAccessLevelData_Count",
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

		public DataTable GetVisitorAccessLevelData_Export(string searchtxt = "")
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "VisitorAccessLevel",
					StoredProcedureName = @"SP_DownloadVisitorAccessLevelData_Export",
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