using ScampusCloud.DataBase;
using ScampusCloud.Models;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScampusCloud.Repository.VisitorType
{
    public class VisitorTypeRepository
    {
		private GeneralMethods objgm = new GeneralMethods();
		public VisitorTypeModel AddEdit_VisitorType(VisitorTypeModel _VisitorTypeModel)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = _VisitorTypeModel.GetType().Name,
					StoredProcedureName = @"SP_VisitorType_CURD",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				if (_VisitorTypeModel.ActionType == "Remote")
				{
					objQueryBuilder.AddFieldValue("@Code", _VisitorTypeModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _VisitorTypeModel.CompanyId, DataTypes.Text, false);
				}
				else
				{
					objQueryBuilder.AddFieldValue("@Id", _VisitorTypeModel.Id, DataTypes.Numeric, false);
					objQueryBuilder.AddFieldValue("@Name", _VisitorTypeModel.Name, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _VisitorTypeModel.CompanyId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Code", _VisitorTypeModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Isactive", _VisitorTypeModel.IsActive, DataTypes.Boolean, false);
					objQueryBuilder.AddFieldValue("@CreatedBy", _VisitorTypeModel.CreatedBy, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@ModifiedBy", _VisitorTypeModel.ModifiedBy, DataTypes.Text, false);
				}
				objQueryBuilder.AddFieldValue("@ActionType", _VisitorTypeModel.ActionType, DataTypes.Text, false);

				return objgm.ExecuteObjectUsingSp<VisitorTypeModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public List<VisitorTypeModel> GetVisitorTypeList(string searchtxt = "", int page = 1, int pagesize = 10)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_VisitorType",
					StoredProcedureName = @"SP_GetVisitorTypeData",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@page", page, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@pagesize", pagesize, DataTypes.Numeric, false);
				return objgm.GetListUsingSp<VisitorTypeModel>(objQueryBuilder);
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
					TableName = "Tbl_Mstr_VisitorType",
					StoredProcedureName = @"SP_GetVisitorTypeData_Count",
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

		public DataTable GetVisitorTypeData_Export(string searchtxt = "")
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "VisitorType",
					StoredProcedureName = @"SP_DownloadVisitorTypeData_Export",
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